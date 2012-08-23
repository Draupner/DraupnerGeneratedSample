using Library.Core.Common.Persistence.NHibernate;
using NHibernate;
using Xunit;
using Rhino.Mocks;

namespace Library.Test.Common.Persistence.NHibernate
{
    public class NHibernateUnitOfWorkFactoryTests
    {
        private readonly MockRepository mocks;
        private readonly ISessionFactory sessionFactoryMock;
        private readonly NHibernateUnitOfWorkFactory unitOfWorkFactory;

        public NHibernateUnitOfWorkFactoryTests()
        {
            mocks = new MockRepository();
            sessionFactoryMock = mocks.DynamicMock<ISessionFactory>();
            unitOfWorkFactory = new NHibernateUnitOfWorkFactory(sessionFactoryMock);
        }

        [Fact]
        public void ShouldCreateSession()
        {
            var session = mocks.DynamicMock<ISession>();
            Expect.Call(sessionFactoryMock.OpenSession()).Return(session);

            mocks.ReplayAll();
            var unitOfWork = unitOfWorkFactory.Create();
            mocks.VerifyAll();

            Assert.NotNull(unitOfWork);
        }
    }
}
