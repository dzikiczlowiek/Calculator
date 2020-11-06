using Castle.Core;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheRules
    {
        bool Allow(ComponentModel componentModel);

        bool CacheInvocation(IInvocation invocation);
    }
}
