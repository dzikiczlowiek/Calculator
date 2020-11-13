using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Calculator.WebApi.Configuration
{
    internal static class AppConfiguration
    {
        internal static Action<WebHostBuilderContext, IConfigurationBuilder> Setup =
            (wc, builder) =>
            {
                builder
                .SetBasePath($"{wc.HostingEnvironment.ContentRootPath}\\Configuration\\")
                .AddJsonFile("cacheRulesSettings.json", false);
            };
    }
}
