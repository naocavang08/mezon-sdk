namespace Mezon_sdk.RedisStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using StackExchange.Redis;
    using Mezon_sdk.Managers;

    public class RedisCacheStore<TKey, TValue> : ICacheStore<TKey, TValue> where TKey : notnull
    {
        private readonly IDatabase _db;
        private readonly IServer _server;
        private readonly string _prefix;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheStore(IConnectionMultiplexer redis, string prefix)
        {
            _db = redis.GetDatabase();
            _server = redis.GetServer(redis.GetEndPoints().First());
            _prefix = prefix + ":";
            _jsonOptions = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true
            };
        }

        private string GetRedisKey(TKey key)
        {
            return $"{_prefix}{key}";
        }

        // We use an approximation or scanning for Size, First, Values in Redis.
        // In a production environment, this might be slow, so we could optimize it later.

        public int Size => _server.Keys(pattern: _prefix + "*").Count();

        public TValue? Get(TKey key)
        {
            var value = _db.StringGet(GetRedisKey(key));
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<TValue>(value.ToString(), _jsonOptions);
            }
            return default;
        }

        public async Task<TValue?> GetAsync(TKey key)
        {
            var value = await _db.StringGetAsync(GetRedisKey(key));
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<TValue>(value.ToString(), _jsonOptions);
            }
            return default;
        }

        public void Set(TKey key, TValue value)
        {
            var json = JsonSerializer.Serialize(value, _jsonOptions);
            _db.StringSet(GetRedisKey(key), json);
        }

        public async Task SetAsync(TKey key, TValue value)
        {
            var json = JsonSerializer.Serialize(value, _jsonOptions);
            await _db.StringSetAsync(GetRedisKey(key), json);
        }

        public bool Delete(TKey key)
        {
            return _db.KeyDelete(GetRedisKey(key));
        }

        public async Task<bool> DeleteAsync(TKey key)
        {
            return await _db.KeyDeleteAsync(GetRedisKey(key));
        }

        public TValue? First()
        {
            var key = FirstKey();
            if (key != null)
            {
                return Get(key);
            }
            return default;
        }

        public TKey? FirstKey()
        {
            var keyStr = _server.Keys(pattern: _prefix + "*", pageSize: 1).FirstOrDefault().ToString();
            if (keyStr != null)
            {
                // Extract original key from prefix
                var origKeyStr = keyStr.Substring(_prefix.Length);
                return (TKey)Convert.ChangeType(origKeyStr, typeof(TKey));
            }
            return default;
        }

        public void Clear()
        {
            var keys = _server.Keys(pattern: _prefix + "*").ToArray();
            _db.KeyDelete(keys);
        }

        public async Task ClearAsync()
        {
            // Simple approach for now
            Clear(); 
            await Task.CompletedTask;
        }

        public bool Contains(TKey key)
        {
            return _db.KeyExists(GetRedisKey(key));
        }

        public async Task<bool> ContainsAsync(TKey key)
        {
            return await _db.KeyExistsAsync(GetRedisKey(key));
        }

        public IEnumerable<TValue> Values()
        {
            var keys = _server.Keys(pattern: _prefix + "*");
            foreach (var key in keys)
            {
                var value = _db.StringGet(key);
                if (value.HasValue)
                {
                    var obj = JsonSerializer.Deserialize<TValue>(value.ToString(), _jsonOptions);
                    if (obj != null)
                        yield return obj;
                }
            }
        }

        public IEnumerable<TKey> Keys()
        {
            var keys = _server.Keys(pattern: _prefix + "*");
            foreach (var key in keys)
            {
                var keyStr = key.ToString();
                var origKeyStr = keyStr.Substring(_prefix.Length);
                yield return (TKey)Convert.ChangeType(origKeyStr, typeof(TKey));
            }
        }
    }
}
