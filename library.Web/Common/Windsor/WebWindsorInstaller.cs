using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Library.Web.Controllers;

namespace Library.Web.Common.Windsor
{
    public class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().Configure(ConfigureControllers()));
        }

        private static Action<ComponentRegistration> ConfigureControllers()
        {
            return c => c.LifestyleTransient();
        }

        private static BasedOnDescriptor FindControllers()
        {
            return AllTypes.FromThisAssembly()
                .BasedOn<IController>()
                .If(Component.IsInSameNamespaceAs<HomeController>())
                .If(t => t.Name.EndsWith("Controller"));
        }
    }
}