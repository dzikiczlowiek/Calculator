using System;

using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheManager cacheManager;
        private readonly ICacheKeyGenerator cacheKeyGenerator;

        public CacheInterceptor(ICacheManager cacheManager, ICacheKeyGenerator cacheKeyGenerator)
        {
            if (cacheManager == null)
            {
                throw new ArgumentNullException(nameof(cacheManager));
            }

            if (cacheKeyGenerator == null)
            {
                throw new ArgumentNullException(nameof(cacheKeyGenerator));
            }

            this.cacheManager = cacheManager;
            this.cacheKeyGenerator = cacheKeyGenerator;
        }

        public void Intercept(IInvocation invocation)
        {
            var cacheDetails = cacheManager.GetCacheDetails(invocation);
            if (!cacheDetails.IsCacheable)
            {
                invocation.Proceed();
                return;
            }

            var key = cacheKeyGenerator.GenerateKey(invocation);
            var cacheEntry = cacheManager.CacheProvider.Get(key);
            if(cacheEntry != null)
            {
                invocation.ReturnValue = cacheEntry.Value;
                return;
            }

            invocation.Proceed();
            cacheManager.CacheProvider.Set(key, invocation.ReturnValue, cacheDetails.ExpirationTime.Value);
        }
    }
}
