using Calculator.Engine;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.Operations
{
    public class OperationsInstaller : IWindsorInstaller
    {
        public static IWindsorInstaller Create => new OperationsInstaller();
        private OperationsInstaller()
        {
        }

        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<AddOperationStrategy>().BasedOn<IOperationStrategy>().WithService.Self());
            container.Register(Classes.FromAssemblyContaining<AddOperationStrategy>().BasedOn<IOperationStrategySpecification>().WithServiceAllInterfaces());
        }
    }
}
