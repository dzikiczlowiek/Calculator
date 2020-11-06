using System;
using System.Threading;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Calculator.Infrastructure.Cache.InMemory
{
    public sealed class InMemoryCache : ICacheProvider
    {
        private static CancellationTokenSource _resetToken = new CancellationTokenSource();
        private readonly IMemoryCache memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
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
    }
}
