using System.Web.Mvc;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Library.Core.Common.Windsor;

namespace Library.Web.Common.Windsor
{
    public class WindsorConfiguration
    {
        public void Configure()
        {
            Ioc.Container = new WindsorContainer()
                .Install(new IWindsorInstaller[] { new TransactionWindsorInstaller() })
                .Install(new IWindsorInstaller[]{new CoreWindsorInstaller(), new WebWindsorInstaller()});
            var controllerFactory = new WindsorControllerFactory(Ioc.Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public void Shutdown()
        {
            Ioc.Container.Dispose();
        }
    }
}