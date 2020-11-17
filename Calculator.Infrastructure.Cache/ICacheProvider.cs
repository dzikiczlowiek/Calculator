using System;
using System.Collections.Generic;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        CacheEntry<T> Get<T>(string key);

        CacheEntry Get(string key);

        void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow);

        IReadOnlyCollection<CacheEntryInfo> ActiveKeys();

        void RemoveEntry(string key);

        void ClearCache();
    }

    public class CacheEntryInfo
    {
        public string Key { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRealtiveToNow { get; set; }
        public string ValueType { get; set; }
        public object Value { get; set; }
    }
}
