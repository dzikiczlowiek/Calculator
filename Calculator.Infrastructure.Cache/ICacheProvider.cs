using System;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        CacheEntry<T> Get<T>(string key);

        CacheEntry Get(string key);

        void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow);
    }


    public class CacheEntry<T>
    {
        public CacheEntry(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public class CacheEntry : CacheEntry<object>
    {
        public CacheEntry(object value) : base(value)
        {
        }
    }
}
