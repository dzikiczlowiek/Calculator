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
                container.Register(Component.For<ICacheManager>().ImplementedBy<CacheManager>().LifestyleTransient());
                container.Register(Component.For<CacheInterceptor>().LifestyleTransient());
                container.AddFacility<StartableFacility>()
                    .Register(Component.For<CacheManagerFacility>()
                    .LifestyleTransient()
                    .StartUsingMethod(nameof(CacheManagerFacility.Init))
                    .StopUsingMethod(nameof(CacheManagerFacility.Stop)));
                container.Kernel.ProxyFactory.AddInterceptorSelector(new CacheInterceptorSelector());

            }
        }
    }
}
