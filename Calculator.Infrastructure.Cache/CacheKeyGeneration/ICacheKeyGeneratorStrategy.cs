using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public interface ICacheKeyGeneratorStrategy
    {
        bool IsApplicable(IInvocation invocation);

        string GenerateKey(IInvocation invocation);
    }
}
