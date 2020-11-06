using Calculator.Engine;
using Calculator.Infrastructure;
using Calculator.Infrastructure.Cache;
using Calculator.Infrastructure.Cache.CacheKeyGeneration;
using Calculator.Infrastructure.Cache.InMemory;
using Calculator.Operations;

using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.ConsoleApp
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
            //container.Register(Component.For<ICacheKeyTypeStrategy>().ImplementedBy<OperationCacheKeyGeneratorStrategy>().LifestyleTransient());
            container.Register(Component.For<ICacheRules>().ImplementedBy<CalculateCacheRules>().LifestyleTransient());
            container.Register(Component.For<IOperationParser>().ImplementedBy<OperationInputParser>().LifestyleTransient());
            container.Install(
                EngineInstaller.Create,
                OperationsInstaller.Create,
                CacheInstaller.Create<InMemoryCache>());
        }
    }
}
