using Castle.Core;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public interface ICacheManager
    {
        bool AllowCacheInterceptorOnComponent(ComponentModel componentModel);

        bool Intercept(IInvocation invocation);

        ICacheProvider CacheProvider { get; }
    }
}
