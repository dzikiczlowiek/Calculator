using System;

namespace Calculator.Infrastructure.Cache
{
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
