using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mezon_sdk;
using Mezon_sdk.Models;
using Mezon_sdk.Constants;
using DotNetEnv;

namespace Mezon_sdk.Test
{
    public class ReplyTest
    {
        static ReplyTest()
        {
            Env.Load();
        }

        public static async Task RunAsync(string[]? args = null)
        {
            var botId = Environment.GetEnvironmentVariable("MEZON_BOT_ID");
            var token = Environment.GetEnvironmentVariable("MEZON_BOT_TOKEN");
            var clanIdStr = Environment.GetEnvironmentVariable("MEZON_TEST_CLAN");
            var channelIdStr = Environment.GetEnvironmentVariable("MEZON_TEST_CHANNEL");

            if (string.IsNullOrEmpty(botId) || string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Error: Set MEZON_BOT_ID and MEZON_BOT_TOKEN environment variables in .env");
                return;
            }

            long.TryParse(botId, out var botIdLong);

            // 1. Authenticate
            var loginApi = new MezonApi(botIdLong, token, "https://gw.mezon.ai:443", 7000);
            var sessApi = await loginApi.AuthenticateAsync(botId, token, new ApiAuthenticateRequest
            {
                Account = new ApiAccountApp
                {
                    Appid = botId,
                    Token = token
                }
            });
            Console.WriteLine($"Authenticated: user={sessApi.UserId} api_url={sessApi.ApiUrl} ws_url={sessApi.WsUrl}");

            var sess = new Session(sessApi);

            var apiUrl = Mezon_sdk.Api.Utils.ParseUrlComponents(sessApi.ApiUrl ?? "", true);
            var restApi = new MezonApi(botIdLong, token, Mezon_sdk.Api.Utils.BuildUrl(apiUrl.Scheme, apiUrl.Hostname, apiUrl.Port), 7000);

            // Determine clan
            long clanId = 0;
            if (!string.IsNullOrEmpty(clanIdStr)) long.TryParse(clanIdStr, out clanId);

            if (clanId == 0)
            {
                try
                {
                    var clanList = await restApi.ListClansAsync(sess.Token);
                    var firstClan = clanList?.Clandesc?.FirstOrDefault();
                    if (firstClan != null && firstClan.ClanId.HasValue)
                    {
                        clanId = firstClan.ClanId.Value;
                        Console.WriteLine($"Selected clan: {clanId} '{firstClan.ClanName}'");
                    }
                }
                catch { }
            }

            var wsUrl = Mezon_sdk.Api.Utils.ParseUrlComponents(sessApi.WsUrl ?? "", true);
            var socket = new Socket.DefaultSocket(wsUrl.Hostname, wsUrl.Port, wsUrl.UseSsl);
            await socket.ConnectAsync(sess);
            restApi.AttachSocket(socket);

            if (clanId != 0)
            {
                try
                {
                    await socket.JoinClanChatAsync(clanId);
                    Console.WriteLine($"JoinClanChatAsync({clanId}) OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"JoinClanChatAsync({clanId}): {ex.Message} (continuing)");
                }
            }

            // 2. Control test: Find a PUBLIC channel in clan and send/reply
            try
            {
                var descs = await restApi.ListChannelsAsync(sess.Token, clanId, (int)ChannelType.ChannelTypeChannel);
                if (descs?.Channeldesc != null)
                {
                    foreach (var d in descs.Channeldesc)
                    {
                        if (!d.ChannelId.HasValue || d.ChannelId.Value == 0 || (d.ChannelPrivate ?? 0) != 0)
                        {
                            continue;
                        }

                        var pubId = d.ChannelId.Value;
                        Console.WriteLine($"Control: public channel id={pubId} label='{d.ChannelLabel}'");

                        var contentText = new ChannelMessageContent { Text = "replytest: control send to public channel" };
                        var ack = await socket.WriteChatMessageAsync(
                            clanId: clanId,
                            channelId: pubId,
                            mode: (int)ChannelStreamMode.StreamModeChannel,
                            isPublic: true,
                            content: contentText);

                        Console.WriteLine($"Control SENT ok: message_id={ack.MessageId}");

                        var replyContent = new ChannelMessageContent { Text = "replytest: control reply" };
                        var rack = await socket.WriteChatMessageAsync(
                            clanId: clanId,
                            channelId: pubId,
                            mode: (int)ChannelStreamMode.StreamModeChannel,
                            isPublic: true,
                            content: replyContent,
                            references: new List<ApiMessageRef>
                            {
                                new ApiMessageRef
                                {
                                    MessageRefId = ack.MessageId ?? 0,
                                    MessageSenderId = botIdLong,
                                    Content = "replytest: control send to public channel"
                                }
                            });

                        Console.WriteLine($"Control REPLIED ok: message_id={rack.MessageId}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Control public channel test error: {ex.Message}");
            }

            // 3. Send message into private channel
            long channelId = 0;
            if (!string.IsNullOrEmpty(channelIdStr)) long.TryParse(channelIdStr, out channelId);

            if (channelId != 0)
            {
                var privateContent = new ChannelMessageContent { Text = "replytest: send over socket into private channel" };
                var sendAck = await socket.WriteChatMessageAsync(
                    clanId: clanId,
                    channelId: channelId,
                    mode: (int)ChannelStreamMode.StreamModeChannel,
                    isPublic: false,
                    content: privateContent);

                if (sendAck != null && sendAck.MessageId != 0)
                {
                    Console.WriteLine($"SENT ok: message_id={sendAck.MessageId} channel={sendAck.ChannelId}");

                    var privateReplyContent = new ChannelMessageContent { Text = "replytest: reply over socket into private channel" };
                    var replyAck = await socket.WriteChatMessageAsync(
                        clanId: clanId,
                        channelId: channelId,
                        mode: (int)ChannelStreamMode.StreamModeChannel,
                        isPublic: false,
                        content: privateReplyContent,
                        references: new List<ApiMessageRef>
                        {
                            new ApiMessageRef
                            {
                                MessageRefId = sendAck.MessageId ?? 0,
                                MessageSenderId = sessApi.UserId ?? 0,
                                Content = "replytest: send over socket into private channel"
                            }
                        });

                    if (replyAck != null && replyAck.MessageId != 0)
                    {
                        Console.WriteLine($"REPLIED ok: message_id={replyAck.MessageId}");
                    }
                }
            }

            Console.WriteLine("replyTest OK — send+reply finished");
        }
    }
}
