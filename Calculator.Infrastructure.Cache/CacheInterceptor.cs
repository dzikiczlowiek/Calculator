using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheManager cacheManager;
        private readonly ICacheKeyGenerator cacheKeyGenerator;

        public CacheInterceptor(ICacheManager cacheManager, ICacheKeyGenerator cacheKeyGenerator)
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
            var cacheDetails = cacheManager.GetCacheDetails(invocation);
            if (!cacheDetails.IsCacheable)
            {
                invocation.Proceed();
                return;
            }
            
            var key = cacheKeyGenerator.GenerateKey(invocation);
            var cacheEntry = cacheManager.CacheProvider.Get(key);
            if(cacheEntry != null)
            {
                if (typeof(Task).IsAssignableFrom(invocation.Method.ReturnType))
                {
                    var genericTaskArguments = invocation.Method.ReturnType.GetGenericArguments();
                    if (!genericTaskArguments.Any())
                    {
                        return;
                    }
                    var type = genericTaskArguments[0];
                    var method = typeof(Task).GetMethod("FromResult").MakeGenericMethod(new[] { type }).Invoke(null, new object[] { cacheEntry.Value });
                    invocation.ReturnValue = method;
                    return;
                }

                invocation.ReturnValue = cacheEntry.Value;
                return;
            }

            invocation.Proceed();

            var returnedValueTask = invocation.ReturnValue as System.Threading.Tasks.Task;
            if (returnedValueTask == null)
            {
                cacheManager.CacheProvider.Set(key, invocation.ReturnValue, cacheDetails.ExpirationTime.Value);
                return;
            }

            returnedValueTask.ContinueWith(t =>
            {
                var genericTaskArguments = invocation.Method.ReturnType.GetGenericArguments();
                if (!genericTaskArguments.Any())
                {
                    return;
                }

                var type = genericTaskArguments[0];
                var taskType = typeof(Task<>).MakeGenericType(type);
                var returnedValue = taskType.GetProperty(nameof(Task<object>.Result)).GetValue(t);
                cacheManager.CacheProvider.Set(key, returnedValue, cacheDetails.ExpirationTime.Value);
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }
    }
}
