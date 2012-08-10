using library.Core.Repositories;
using library.Core.Domain.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using library.Core.Common.NHibernate;
using library.Core.Common.Persistence;
using library.Core.Common.Persistence.NHibernate;
using NHibernate;

namespace library.Core.Common.Windsor
{
    public class CoreWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<SharedNHibernateUnitOfWork>());
            container.Register(Component.For<IUnitOfWorkFactory>().ImplementedBy<NHibernateUnitOfWorkFactory>());
           
            container.Register(Component.For<ISessionFactory>().Instance(NHibernateConfiguration.SessionFactory));
            container.Register(Component.For<IBookRepository>().ImplementedBy<BookRepository>());
            container.Register(Component.For<IAuthorRepository>().ImplementedBy<AuthorRepository>());
            container.Register(Component.For<ILibraryCardRepository>().ImplementedBy<LibraryCardRepository>());
        }
    }
}
