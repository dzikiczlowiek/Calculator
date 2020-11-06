using System;
using System.Collections.Generic;
using System.Linq;

using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public class CacheKeyGenerator : ICacheKeyGenerator
    {
        private readonly IEnumerable<ICacheKeyGeneratorStrategy> cacheKeyGeneratorStrategies;

        public CacheKeyGenerator(IEnumerable<ICacheKeyGeneratorStrategy> cacheKeyGeneratorStrategies)
        {
            if (cacheKeyGeneratorStrategies == null)
            {
                throw new ArgumentNullException(nameof(cacheKeyGeneratorStrategies));
            }

            this.cacheKeyGeneratorStrategies = cacheKeyGeneratorStrategies;
        }

        public string GenerateKey(IInvocation invocation)
        {
            var strategy = cacheKeyGeneratorStrategies.SingleOrDefault(x => x.IsApplicable(invocation));
            if (strategy == null)
            {
                throw new InvalidOperationException("Didnt found valid cache key generation strategy for given action.");
            }

            return strategy.GenerateKey(invocation);
        }
    }
}
