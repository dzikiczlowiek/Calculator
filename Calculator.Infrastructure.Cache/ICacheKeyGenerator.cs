using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheKeyGenerator
    {
        string GenerateKey(IInvocation invocation);
    }
}
