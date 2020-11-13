using Castle.Core;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheManager
    {
        bool AllowCacheInterceptorOnComponent(ComponentModel componentModel);

        CacheInvocationDetails GetCacheDetails(IInvocation invocation);

        ICacheProvider CacheProvider { get; }
    }
}
