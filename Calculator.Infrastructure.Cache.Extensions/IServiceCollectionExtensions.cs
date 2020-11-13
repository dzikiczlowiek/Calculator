using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.Infrastructure.Cache.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseFileConfigureableCacheRulesSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheRulesSettings>(options => configuration.GetSection("CacheRules").Bind(options));
            services.AddSingleton<ICacheRules, ConfigureableCacheRules>();
            return services;
        }
    }
}
