using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;

using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Proxy;

namespace Calculator.Infrastructure.Cache
{

    public sealed class CacheInterceptorSelector : IModelInterceptorsSelector
    {
        public bool HasInterceptors(ComponentModel model)
        {
            if(CacheManagerFacility.Current == null)
            {
                return false;
            }

            return CacheManagerFacility.Current.AllowCacheInterceptorOnComponent(model);
        }

        public InterceptorReference[] SelectInterceptors(ComponentModel model, InterceptorReference[] interceptors)
        {
            var cacheInterceptor = new InterceptorReference(typeof(CacheInterceptor));
            var tmp = interceptors.ToList();
            tmp.Add(cacheInterceptor);
            return tmp.ToArray();
        }
    }
}
