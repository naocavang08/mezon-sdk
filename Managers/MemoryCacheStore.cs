namespace Mezon_sdk.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MemoryCacheStore<TKey, TValue> : ICacheStore<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _data = new Dictionary<TKey, TValue>();
        // To maintain insertion order, we keep a separate list of keys
        private readonly List<TKey> _orderedKeys = new List<TKey>();

        public int Size => _data.Count;

        public TValue? Get(TKey key)
        {
            if (_data.TryGetValue(key, out var value))
            {
                return value;
            }
            return default;
        }

        public Task<TValue?> GetAsync(TKey key)
        {
            return Task.FromResult(Get(key));
        }

        public void Set(TKey key, TValue value)
        {
            if (!_data.ContainsKey(key))
            {
                _orderedKeys.Add(key);
            }
            _data[key] = value;
        }

        public Task SetAsync(TKey key, TValue value)
        {
            Set(key, value);
            return Task.CompletedTask;
        }

        public bool Delete(TKey key)
        {
            if (_data.Remove(key))
            {
                _orderedKeys.Remove(key);
                return true;
            }
            return false;
        }

        public Task<bool> DeleteAsync(TKey key)
        {
            return Task.FromResult(Delete(key));
        }

        public TValue? First()
        {
            if (_orderedKeys.Count == 0)
            {
                return default;
            }
            return _data[_orderedKeys[0]];
        }

        public TKey? FirstKey()
        {
            if (_orderedKeys.Count == 0)
            {
                return default;
            }
            return _orderedKeys[0];
        }

        public void Clear()
        {
            _data.Clear();
            _orderedKeys.Clear();
        }

        public Task ClearAsync()
        {
            Clear();
            return Task.CompletedTask;
        }

        public bool Contains(TKey key)
        {
            return _data.ContainsKey(key);
        }

        public Task<bool> ContainsAsync(TKey key)
        {
            return Task.FromResult(Contains(key));
        }

        public IEnumerable<TValue> Values()
        {
            foreach (var key in _orderedKeys)
            {
                yield return _data[key];
            }
        }

        public IEnumerable<TKey> Keys()
        {
            return _orderedKeys;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var key in _orderedKeys)
            {
                yield return new KeyValuePair<TKey, TValue>(key, _data[key]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
