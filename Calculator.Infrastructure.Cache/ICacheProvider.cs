using System;
using System.Collections.Generic;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        CacheEntry<T> Get<T>(string key);

        CacheEntry Get(string key);

        void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow);

        IReadOnlyCollection<string> ActiveKeys();
    }
}
