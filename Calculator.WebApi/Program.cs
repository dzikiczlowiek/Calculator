using Calculator.WebApi.Configuration;

using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Calculator.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseServiceProviderFactory<IWindsorContainer>(new WindsorServiceProviderFactory())
               //.ConfigureLogging(c =>
               //{
               //    c.ClearProviders();
               //    c.AddConsole(x => x.IncludeScopes = true);
               //})
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder
                   .ConfigureAppConfiguration(AppConfiguration.Setup)
                   .UseStartup<Startup>();
               });
    }
}
