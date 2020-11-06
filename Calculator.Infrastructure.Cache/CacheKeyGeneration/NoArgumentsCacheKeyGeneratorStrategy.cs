using Castle.Core.Internal;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public sealed class NoArgumentsCacheKeyGeneratorStrategy : ICacheKeyGeneratorStrategy
    {
        public string GenerateKey(IInvocation invocation)
        {
            var target = invocation.TargetType.Name;
            var method = invocation.MethodInvocationTarget.Name;
            return $"{target}_{method}";
        }

        public bool IsApplicable(IInvocation invocation) => invocation.Arguments.IsNullOrEmpty();
    }
}
