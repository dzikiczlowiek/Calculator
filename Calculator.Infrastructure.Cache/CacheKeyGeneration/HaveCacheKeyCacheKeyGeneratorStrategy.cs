
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public sealed class HaveCacheKeyCacheKeyGeneratorStrategy : ICacheKeyGeneratorStrategy
    {
        public string GenerateKey(IInvocation invocation)
        {
            var target = invocation.TargetType.Name;
            var method = invocation.MethodInvocationTarget.Name;
            var haveCacheKey = (IHaveCacheKey)invocation.Arguments[0];
            return $"{target}_{method}_{haveCacheKey.CacheKey}";
        }

        public bool IsApplicable(IInvocation invocation)
        {
            return invocation.Arguments.Length == 1 && invocation.Arguments[0] is IHaveCacheKey;
        }
    }
}
