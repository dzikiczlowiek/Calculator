using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Calculator.WebApi.SwaggerConfiguration
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, Assembly assembly)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo List", Version = "0.0.0.2" });
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
