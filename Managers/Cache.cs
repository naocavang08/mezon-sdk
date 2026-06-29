namespace Mezon_sdk.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CacheManager<TKey, TValue> where TKey : notnull
    {
        private readonly Func<TKey, Task<TValue>> _fetcher;
        private readonly Action<TValue>? _hydrator;
        private readonly int? _maxSize;
        public ICacheStore<TKey, TValue> Cache { get; }

        public CacheManager(Func<TKey, Task<TValue>> fetcher, int? maxSize = null, ICacheStore<TKey, TValue>? cacheStore = null, Action<TValue>? hydrator = null)
        {
            _fetcher = fetcher ?? throw new ArgumentNullException(nameof(fetcher));
            _maxSize = maxSize;
            _hydrator = hydrator;
            Cache = cacheStore ?? new MemoryCacheStore<TKey, TValue>();
        }

        public int Size => Cache.Size;

        public TValue? Get(TKey id)
        {
            var value = Cache.Get(id);
            if (value != null) _hydrator?.Invoke(value);
            return value;
        }

        public async Task<TValue?> GetAsync(TKey id)
        {
            var value = await Cache.GetAsync(id);
            if (value != null) _hydrator?.Invoke(value);
            return value;
        }

        public void Set(TKey id, TValue value)
        {
            if (_maxSize.HasValue && Cache.Size >= _maxSize.Value)
            {
                var firstKey = Cache.FirstKey();
                if (firstKey != null)
                {
                    Cache.Delete(firstKey);
                }
            }
            Cache.Set(id, value);
        }

        public async Task SetAsync(TKey id, TValue value)
        {
            if (_maxSize.HasValue && Cache.Size >= _maxSize.Value)
            {
                var firstKey = Cache.FirstKey();
                if (firstKey != null)
                {
                    await Cache.DeleteAsync(firstKey);
                }
            }
            await Cache.SetAsync(id, value);
        }

        public async Task<TValue> FetchAsync(TKey id)
        {
            var existing = await GetAsync(id);
            if (existing != null)
            {
                return existing;
            }

            var fetched = await _fetcher(id);
            await SetAsync(id, fetched);
            return fetched;
        }

        public TValue? First()
        {
            var value = Cache.First();
            if (value != null) _hydrator?.Invoke(value);
            return value;
        }

        public IEnumerable<TValue> Filter(Func<TValue, bool> fn)
        {
            var values = Cache.Values().Where(fn);
            if (_hydrator != null)
            {
                foreach (var v in values) _hydrator(v);
            }
            return values;
        }

        public IEnumerable<T> Map<T>(Func<TValue, T> fn)
        {
            var values = Cache.Values();
            if (_hydrator != null)
            {
                foreach (var v in values) _hydrator(v);
            }
            return values.Select(fn);
        }

        public IEnumerable<TValue> Values()
        {
            var values = Cache.Values();
            if (_hydrator != null)
            {
                foreach (var v in values) _hydrator(v);
            }
            return values;
        }

        public bool Delete(TKey id)
        {
            return Cache.Delete(id);
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            return await Cache.DeleteAsync(id);
        }

        public void Clear()
        {
            Cache.Clear();
        }

        public async Task ClearAsync()
        {
            await Cache.ClearAsync();
        }

        public bool Has(TKey id)
        {
            return Cache.Contains(id);
        }

        public async Task<bool> HasAsync(TKey id)
        {
            return await Cache.ContainsAsync(id);
        }
    }
}
