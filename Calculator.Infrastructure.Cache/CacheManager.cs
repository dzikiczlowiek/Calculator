﻿using System.Collections.Generic;
using System.Linq;

using Castle.Core;
using Castle.DynamicProxy;

namespace Calculator.Infrastructure.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly ICacheProvider cacheProvider;
        private readonly IEnumerable<ICacheRules> cacheRules;

        public CacheManager(ICacheProvider cacheProvider, IEnumerable<ICacheRules> cacheRules)
        {
            if (cacheProvider == null)
            {
                throw new System.ArgumentNullException(nameof(cacheProvider));
            }

            this.cacheProvider = cacheProvider;
            this.cacheRules = cacheRules;
        }

        public ICacheProvider CacheProvider => cacheProvider;

        public bool AllowCacheInterceptorOnComponent(ComponentModel componentModel)
        {
            if (cacheRules?.Any() != true)
            {
                return false;
            }

            return cacheRules.Any(x => x.Allow(componentModel));
        }

        public bool Intercept(IInvocation invocation)
        {
            if (cacheRules?.Any() != true)
            {
                return false;
            }

            return cacheRules.Any(x => x.CacheInvocation(invocation));
        }
    }
}
