namespace Mezon_sdk.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICacheStore<TKey, TValue> where TKey : notnull
    {
        int Size { get; }
        TValue? Get(TKey key);
        Task<TValue?> GetAsync(TKey key);
        void Set(TKey key, TValue value);
        Task SetAsync(TKey key, TValue value);
        bool Delete(TKey key);
        Task<bool> DeleteAsync(TKey key);
        TValue? First();
        TKey? FirstKey();
        void Clear();
        Task ClearAsync();
        bool Contains(TKey key);
        Task<bool> ContainsAsync(TKey key);
        IEnumerable<TValue> Values();
        IEnumerable<TKey> Keys();
    }
}
