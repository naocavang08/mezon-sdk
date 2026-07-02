using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mezon_sdk.Constants;

namespace Mezon_sdk.Managers
{
    public class SSEConfig
    {
        public string Url { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool AutoReconnect { get; set; } = true;
        public int ReconnectDelay { get; set; } = 3000;
        public int MaxReconnectAttempts { get; set; } = 5;
        public Dictionary<string, string>? Headers { get; set; }
    }

    public class SSEMessage
    {
        public string? Id { get; set; }
        public string? Event { get; set; }
        public string Data { get; set; } = string.Empty;
        public long Timestamp { get; set; }
    }

    public class ReconnectingEventArgs
    {
        public int Attempt { get; set; }
        public int MaxAttempts { get; set; }
        public int Delay { get; set; }
    }

    public class SSEErrorEventArgs
    {
        public string Message { get; set; } = string.Empty;
        public Exception? Exception { get; set; }
        public long Timestamp { get; set; }
    }

    public class EventSourceManager
    {
        private readonly SSEConfig _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger? _logger;
        private CancellationTokenSource? _cts;
        private Task? _listenTask;
        private int _reconnectAttempts;
        private bool _isManualClose;
        private string? _currentPath;
        private Dictionary<string, string>? _currentQueryParams;
        private SSEConnectionState _state = SSEConnectionState.Closed;

        public event Action? OnOpen;
        public event Action<SSEMessage>? OnMessage;
        public event Action<SSEErrorEventArgs>? OnError;
        public event Action? OnClose;
        public event Action<ReconnectingEventArgs>? OnReconnecting;
        public event Action? OnReconnected;

        public EventSourceManager(SSEConfig config, HttpClient? httpClient = null, ILogger? logger = null)
        {
            _config = config;
            _httpClient = httpClient ?? new HttpClient();
            _logger = logger;

            if (string.IsNullOrEmpty(_config.AppId))
                throw new ArgumentException("AppId is required", nameof(config));
            if (string.IsNullOrEmpty(_config.Token))
                throw new ArgumentException("Token is required", nameof(config));
        }

        public SSEConnectionState State => _state;
        public bool IsConnected => _state == SSEConnectionState.Open;

        public void Connect(string? path = null, Dictionary<string, string>? queryParams = null)
        {
            Close();

            _isManualClose = false;
            _reconnectAttempts = 0;
            _currentPath = path;
            _currentQueryParams = queryParams;

            _cts = new CancellationTokenSource();
            _state = SSEConnectionState.Connecting;

            var url = BuildUrl(path, queryParams);
            _listenTask = Task.Run(() => ListenLoopAsync(url, _cts.Token));
        }

        public void Close()
        {
            _isManualClose = true;
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            _state = SSEConnectionState.Closed;
            OnClose?.Invoke();
        }

        private string BuildUrl(string? path, Dictionary<string, string>? queryParams)
        {
            var url = _config.Url;
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            if (!string.IsNullOrEmpty(path))
            {
                var cleanPath = path.StartsWith("/") ? path.Substring(1) : path;
                url = $"{url}/{cleanPath}";
            }

            var builder = new UriBuilder(url);
            var query = new List<string>();

            if (!string.IsNullOrEmpty(builder.Query))
            {
                query.Add(builder.Query.TrimStart('?'));
            }

            query.Add($"appid={Uri.EscapeDataString(_config.AppId)}");
            query.Add($"token={Uri.EscapeDataString(_config.Token)}");

            if (queryParams != null)
            {
                foreach (var kvp in queryParams)
                {
                    query.Add($"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}");
                }
            }

            builder.Query = string.Join("&", query);
            return builder.ToString();
        }

        private async Task ListenLoopAsync(string url, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

                    if (_config.Headers != null)
                    {
                        foreach (var header in _config.Headers)
                        {
                            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }

                    using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                    response.EnsureSuccessStatusCode();

                    using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                    using var reader = new StreamReader(stream, Encoding.UTF8);

                    var wasReconnecting = _reconnectAttempts > 0;
                    _reconnectAttempts = 0;
                    _state = SSEConnectionState.Open;

                    if (wasReconnecting)
                    {
                        OnReconnected?.Invoke();
                    }
                    OnOpen?.Invoke();

                    string? currentId = null;
                    string? currentEvent = null;
                    var currentData = new List<string>();

                    while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
                    {
                        var line = await reader.ReadLineAsync(cancellationToken);
                        if (line == null) break;

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            if (currentData.Count > 0)
                            {
                                var dataString = string.Join("\n", currentData);
                                var message = new SSEMessage
                                {
                                    Id = currentId,
                                    Event = currentEvent,
                                    Data = dataString,
                                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                                };
                                OnMessage?.Invoke(message);
                            }
                            currentEvent = null;
                            currentData.Clear();
                        }
                        else if (line.StartsWith("id:"))
                        {
                            currentId = line.Substring(3).Trim();
                        }
                        else if (line.StartsWith("event:"))
                        {
                            currentEvent = line.Substring(6).Trim();
                        }
                        else if (line.StartsWith("data:"))
                        {
                            currentData.Add(line.Substring(5).TrimStart(' '));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (cancellationToken.IsCancellationRequested || _isManualClose)
                    {
                        break;
                    }

                    OnError?.Invoke(new SSEErrorEventArgs
                    {
                        Message = ex.Message,
                        Exception = ex,
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    });

                    if (!ShouldReconnect())
                    {
                        Close();
                        break;
                    }

                    await ScheduleReconnectAsync(cancellationToken);
                }
            }
        }

        private bool ShouldReconnect()
        {
            if (_isManualClose) return false;
            if (!_config.AutoReconnect) return false;
            if (_config.MaxReconnectAttempts > 0 && _reconnectAttempts >= _config.MaxReconnectAttempts) return false;
            return true;
        }

        private async Task ScheduleReconnectAsync(CancellationToken cancellationToken)
        {
            _state = SSEConnectionState.Connecting;
            var delay = CalculateReconnectDelay();
            _reconnectAttempts++;

            OnReconnecting?.Invoke(new ReconnectingEventArgs
            {
                Attempt = _reconnectAttempts,
                MaxAttempts = _config.MaxReconnectAttempts,
                Delay = delay
            });

            await Task.Delay(delay, cancellationToken);
        }

        private int CalculateReconnectDelay()
        {
            var baseDelay = _config.ReconnectDelay;
            var exponentialDelay = (int)Math.Min(baseDelay * Math.Pow(2, _reconnectAttempts), 30000);
            var jitter = new Random().Next(0, 1000);
            return exponentialDelay + jitter;
        }
    }
}
