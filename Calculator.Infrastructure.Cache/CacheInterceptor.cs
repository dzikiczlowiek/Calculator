using System;

using Calculator.Infrastructure.Cache.CacheKeyGeneration;

using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheManager cacheManager;
        private readonly ICacheKeyGeneratorStrategy cacheKeyGenerator;

        public CacheInterceptor(ICacheManager cacheManager, ICacheKeyGeneratorStrategy cacheKeyGenerator)
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
            if (!cacheManager.Intercept(invocation))
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
            cacheManager.CacheProvider.Set(key, invocation.ReturnValue, TimeSpan.FromMinutes(5));
        }
    }
}
