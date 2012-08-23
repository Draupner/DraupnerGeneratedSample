using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Library.Core.Common.Transactions;

namespace Library.Core.Common.Windsor
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