using Calculator.Infrastructure;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

namespace Calculator.Engine
{
    public class EngineInstaller : IWindsorInstaller
    {
        public static IWindsorInstaller Create => new EngineInstaller();

        private EngineInstaller()
        {
        }

        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICalculate>().ImplementedBy<Calculator>().LifestyleTransient());
            container.Register(Component.For<OperationHandlerFactorySelector>().LifestyleTransient());
            container.Register(Component.For<IOperationStrategyFactory>().AsFactory(x => x.SelectedWith<OperationHandlerFactorySelector>()).LifestyleTransient());
        }
    }
}
