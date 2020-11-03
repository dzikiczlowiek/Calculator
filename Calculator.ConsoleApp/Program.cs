using System;
using System.Threading;
using System.Threading.Tasks;

using Calculator.Infrastructure;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;

using Microsoft.Extensions.Configuration;
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
            });

            return hostBuilder;
        }
    }

    public class InputListener : IHostedService
    {
        private readonly ICalculate calculate;

        public InputListener(ICalculate calculate)
        {
            this.calculate = calculate;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(StartListen);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void StartListen()
        {
            while (true)
            {
                var input = Console.ReadLine();
                var result = calculate.Execute(new Operation()
                {
                    Operator = Operator.Add,
                    Parameter1 = 100m,
                    Parameter2 = 66m
                });
                Console.WriteLine(result);
                if(string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }
    }
}
