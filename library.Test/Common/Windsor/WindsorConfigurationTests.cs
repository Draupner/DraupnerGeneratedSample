using library.Web.Controllers;
using library.Core.Domain.Repositories;
using System.Web.Mvc;
using library.Core.Common.Windsor;
using library.Core.Common.NHibernate;
using library.Core.Common.Persistence;
using library.Web.Common.Windsor;
using Castle.Windsor;
using NHibernate;
using Xunit;
using Rhino.Mocks;
using System;

namespace library.Test.Common.Windsor
{
    public class WindsorConfigurationTests : IDisposable 
    {
        private readonly MockRepository mocks;
        private readonly WindsorConfiguration windsorConfiguration;
        private readonly IWindsorContainer container;

        public WindsorConfigurationTests()
        {
            mocks = new MockRepository();

            var sessionFactoryMock = mocks.DynamicMock<ISessionFactory>();
            NHibernateConfiguration.SessionFactory = sessionFactoryMock; 
            
            windsorConfiguration = new WindsorConfiguration();
            windsorConfiguration.Configure();
            container = Ioc.Container;
		}

        [Fact]
        public void ShouldSetControllerFactory()
        {
            Assert.True(ControllerBuilder.Current.GetControllerFactory() is WindsorControllerFactory);
        }

        [Fact]
        public void ShouldRegisterDependencies()
        {
            Assert.NotNull(container.Resolve<IUnitOfWork>());
            Assert.NotNull(container.Resolve<IUnitOfWorkFactory>());
            Assert.NotNull(container.Resolve<ISessionFactory>());
            Assert.NotNull(container.Resolve<IBookRepository>());
            Assert.NotNull(container.Resolve<BookController>());
            Assert.NotNull(container.Resolve<IAuthorRepository>());
            Assert.NotNull(container.Resolve<AuthorController>());
            Assert.NotNull(container.Resolve<ILibraryCardRepository>());
            Assert.NotNull(container.Resolve<LibraryCardController>());
            Assert.NotNull(container.Resolve<IAuthorRepository>());
            Assert.NotNull(container.Resolve<AuthorController>());
        }

		public void Dispose()
        {
            Ioc.Container = null;
        }
    }
}
