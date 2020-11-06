using Calculator.Infrastructure.Cache.CacheKeyGeneration;

using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.Infrastructure.Cache
{
    public static class CacheInstaller
    {
        public static IWindsorInstaller Create<TCacheProvider>()
            where TCacheProvider : class, ICacheProvider
            => new InnerInstaller<TCacheProvider>();

        private class InnerInstaller<TCacheProvider> : IWindsorInstaller
            where TCacheProvider : class, ICacheProvider
        {
            void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
            {
                container.Register(Component.For<ICacheProvider>().ImplementedBy<TCacheProvider>().LifestyleSingleton());
                container.Register(Component.For<ICacheManager>().ImplementedBy<CacheManager>().LifestyleSingleton());
                container.Register(Component.For<CacheInterceptor>().LifestyleTransient());
                container.Register(Classes.FromAssemblyContaining<ICacheKeyGeneratorStrategy>()
                    .BasedOn<ICacheKeyGeneratorStrategy>()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient());
                container.Register(Component.For<ICacheKeyGenerator>().ImplementedBy<CacheKeyGenerator>().LifestyleTransient());
                container.AddFacility<StartableFacility>(f=>f.DeferredStart())
                    .Register(Component
                    .For<CacheManagerFacility>()
                   // .DependsOn(Dependency.OnComponent<CacheManager,ICacheManager>())
                    .LifestyleSingleton()
                    .StartUsingMethod(f => f.Start)
                    .StopUsingMethod(f=>f.Stop)
                    .Start());
                container.Kernel.ProxyFactory.AddInterceptorSelector(new CacheInterceptorSelector());

            }
        }
    }
}
