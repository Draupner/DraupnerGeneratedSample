using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using library.Core.Common.Transactions;

namespace library.Core.Common.Windsor
{
    public class TransactionWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<TransactionInterceptor>().Named(typeof(TransactionInterceptor).Name));
            container.Kernel.ComponentModelBuilder.AddContributor(new TransactionInterceptorContributer());
        }
    }
}