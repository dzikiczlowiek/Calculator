using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Calculator.Infrastructure;
using Calculator.Infrastructure.Cache;
using Calculator.Infrastructure.Cache.CacheKeyGeneration;

using Castle.Core;
using Castle.DynamicProxy;
using Microsoft.Extensions.Hosting;

namespace Calculator.ConsoleApp
{
    public class InputListener : IHostedService
    {
        private readonly ICalculate calculate;
        private readonly ICacheProvider cacheProvider;
        private readonly IOperationParser operationParser;

        public InputListener(ICalculate calculate,IOperationParser operationParser, ICacheProvider cacheProvider)
        {
            this.calculate = calculate;
            this.cacheProvider = cacheProvider;
            this.operationParser = operationParser;
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
                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                var operation = operationParser.Parse(input);
                var result = calculate.Execute(operation);
                Console.WriteLine( $" = {result}");

                foreach(var entry in cacheProvider.ActiveKeys())
                {
                    Debug.WriteLine(entry);
                }
            }
        }
    }

    public sealed class OperationCacheKeyGeneratorStrategy : CacheTypeStrategy<Operation>
    {
        public override string GetKey(Operation instance)
        {
            return $"{instance.Parameter1} {instance.Operator} {instance.Parameter2}";
        }
    }

    public sealed class CalculateCacheRules : ICacheRules
    {
        private readonly Type[] services = new[] { typeof(ICalculate) };
        public bool Allow(ComponentModel componentModel)
        {
            var result = services.Intersect(componentModel.Services).Any();
            return result;
        }

        public bool CacheInvocation(IInvocation invocation)
        {
           return true;
        }
    }
}
