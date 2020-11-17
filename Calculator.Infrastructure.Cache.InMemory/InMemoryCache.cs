using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Calculator.Infrastructure.Cache.InMemory
{
    public sealed class InMemoryCache : ICacheProvider
    {
        private static readonly object _syncObject = new object();
        private static CancellationTokenSource _resetToken = new CancellationTokenSource();
        private readonly IMemoryCache memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void RemoveEntry(string key)
        {
            memoryCache.Remove(key);
        }

        public IReadOnlyCollection<CacheEntryInfo> ActiveKeys()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var keys = new List<CacheEntryInfo>();
            if (collection != null)
            {
                foreach(var item in collection)
                {
                    keys.Add(Build(item));
                }

                return keys.OrderBy(x => x.Key).ToList();
            }
            
            return new List<CacheEntryInfo>();

            CacheEntryInfo Build(object cacheEntryInstanceKeyValuePair)
            {
                var entryGetter = cacheEntryInstanceKeyValuePair.GetType().GetProperty("Value");
                var entry = entryGetter.GetValue(cacheEntryInstanceKeyValuePair) as ICacheEntry;
                var cacheEntryInfo = new CacheEntryInfo();
                cacheEntryInfo.Key = entry.Key.ToString();
                cacheEntryInfo.AbsoluteExpiration = entry.AbsoluteExpiration;
                cacheEntryInfo.AbsoluteExpirationRealtiveToNow = entry.AbsoluteExpirationRelativeToNow;
                cacheEntryInfo.ValueType = entry.Value.GetType().FullName;
                cacheEntryInfo.Value = entry.Value;
                return cacheEntryInfo;
            }
        }

        public CacheEntry<T> Get<T>(string key)
        {
            if(!memoryCache.TryGetValue<T>(key, out var value))
            {
                return null;
            }

            return new CacheEntry<T>(value);
        }

        public CacheEntry Get(string key)
        {
            if (!memoryCache.TryGetValue(key, out var value))
            {
                return null;
            }

            return new CacheEntry(value);
        }

        public void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var memoryOptions = new MemoryCacheEntryOptions();
            memoryOptions.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            memoryOptions.AddExpirationToken(new CancellationChangeToken(_resetToken.Token));
            memoryCache.Set(key, value, memoryOptions);
        }

        public void ClearCache()
        {
            lock (_syncObject)
            {
                if(_resetToken?.IsCancellationRequested == false && _resetToken.Token.CanBeCanceled)
                {
                    _resetToken.Cancel();
                    _resetToken.Dispose();
                }

                _resetToken = new CancellationTokenSource();
            }
        }
    }
}
