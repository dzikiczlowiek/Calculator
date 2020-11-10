using System;
using System.Linq;

using Calculator.Engine;
using Calculator.Infrastructure;
using Calculator.Infrastructure.Cache;
using Calculator.Infrastructure.Cache.InMemory;
using Calculator.InputParser;
using Calculator.Operations;

using Castle.Core;
using Castle.DynamicProxy;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.WebApi.IoC
{
    public class MainInstaller : IWindsorInstaller
    {
        public static IWindsorInstaller Create => new MainInstaller();

        private MainInstaller()
        {
        }

        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(Component.For<ICacheRules>().ImplementedBy<CalculateCacheRules>().LifestyleTransient());
            container.Register(Component.For<IOperationParser>().ImplementedBy<OperationInputParser>().LifestyleTransient());
            container.Install(
                EngineInstaller.Create,
                OperationsInstaller.Create,
                CacheInstaller.Create<InMemoryCache>());
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
}
