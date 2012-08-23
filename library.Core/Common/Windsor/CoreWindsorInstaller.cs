using Library.Core.Repositories;
using Library.Core.Domain.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Library.Core.Common.NHibernate;
using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;
using NHibernate;

namespace Library.Core.Common.Windsor
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
