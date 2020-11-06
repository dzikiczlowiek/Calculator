using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public sealed class TypeCacheKeyGeneratorStrategy : ICacheKeyGeneratorStrategy
    {
        private readonly IEnumerable<ICacheKeyTypeStrategy> cacheKeyTypeStrategies;
        private ICacheKeyTypeStrategy selectedCacheKeyTypeStrategy;

        public TypeCacheKeyGeneratorStrategy(IEnumerable<ICacheKeyTypeStrategy> cacheKeyTypeStrategies)
        {
            if (cacheKeyTypeStrategies == null)
            {
                throw new ArgumentNullException(nameof(cacheKeyTypeStrategies));
            }

            this.cacheKeyTypeStrategies = cacheKeyTypeStrategies;
        }

        public string GenerateKey(IInvocation invocation)
        {
            var argument = invocation.Arguments[0];
            return selectedCacheKeyTypeStrategy.GetKey(argument);
        }

        public bool IsApplicable(IInvocation invocation)
        {
            if (invocation.Arguments.Length != 1)
            {
                return false;
            }

            var argumentType = invocation.Arguments[0].GetType();
            selectedCacheKeyTypeStrategy = cacheKeyTypeStrategies.SingleOrDefault(x => x.IsApplicable(argumentType));

            return selectedCacheKeyTypeStrategy != null;
        }
    }
}
