using System;
using System.Collections.Generic;
using System.Linq;

using Castle.Core;
using Castle.Core.Internal;
using Castle.DynamicProxy;

using Microsoft.Extensions.Options;

namespace Calculator.Infrastructure.Cache.Extensions
{
    public class ConfigureableCacheRules : ICacheRules
    {
        private readonly IOptions<CacheRulesSettings> options;

        public ConfigureableCacheRules(IOptions<CacheRulesSettings> options)
        {
            this.options = options;
        }

        public bool Allow(ComponentModel componentModel)
        {
            var services = options.Value.Rules.Select(x => x.Type);
            var result = services.Intersect(componentModel.Services.Select(x => x.FullName)).Any();
            return result;
        }

        public CacheInvocationDetails CacheInvocation(IInvocation invocation)
        {
            var rule = options.Value.Rules.SingleOrDefault(x => x.Type == invocation.TargetType.FullName
            || invocation.TargetType.GetInterfaces().Any(i => i.FullName == x.Type));
            if (rule == null)
            {
                return CacheInvocationDetails.NotCacheable;
            }

            var defaultExpirationTime = TimeSpan.FromSeconds(options.Value.DefaultCacheExpirationTimeInSeconds ?? 300);

            if (rule.Methods.IsNullOrEmpty())
            {
                return CacheInvocationDetails.CacheFor(defaultExpirationTime);
            }

            var methodEntry = rule.Methods.SingleOrDefault(x => x.Name.Trim() == invocation.Method.Name);
            if (methodEntry != null)
            {
                if (methodEntry.ExpirationTimeInSeconds.HasValue)
                {
                    return CacheInvocationDetails.CacheFor(TimeSpan.FromSeconds(methodEntry.ExpirationTimeInSeconds.Value));
                }

                return CacheInvocationDetails.CacheFor(defaultExpirationTime);
            }

            var globalMethodEntry = rule.Methods.SingleOrDefault(x => x.Name.Trim() == "*");
            if (globalMethodEntry != null)
            {
                if (globalMethodEntry.ExpirationTimeInSeconds.HasValue)
                {
                    return CacheInvocationDetails.CacheFor(TimeSpan.FromSeconds(methodEntry.ExpirationTimeInSeconds.Value));
                }

                return CacheInvocationDetails.CacheFor(defaultExpirationTime);
            }

            return CacheInvocationDetails.NotCacheable;
        }
    }
}
