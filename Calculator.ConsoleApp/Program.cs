using System;
using System.Threading;
using System.Threading.Tasks;

using Calculator.Infrastructure;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                // configuration

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });
            hostBuilder.UseServiceProviderFactory<IWindsorContainer>(new WindsorServiceProviderFactory());
            hostBuilder.ConfigureContainer<IWindsorContainer>(builder =>
            {
                builder.Install(MainInstaller.Create);
                builder.Register(Component.For<IHostedService>().ImplementedBy<InputListener>().LifestyleSingleton());
            });

            hostBuilder.ConfigureServices(
                (hostContext, services) =>
                {
                    services.AddMemoryCache();
                });

            return hostBuilder;
        }
    }
}
