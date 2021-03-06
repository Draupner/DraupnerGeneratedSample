using Library.Core.Common.Persistence.NHibernate;
using NHibernate;
using Xunit;
using Rhino.Mocks;

namespace Library.Test.Common.Persistence.NHibernate
{
    public class SingleNHibernateUnitOfWorkTests
    {
        private readonly MockRepository mocks;

        public SingleNHibernateUnitOfWorkTests()
        {
            mocks = new MockRepository();
        }

        [Fact]
        public void ShouldGetCurrentSession()
        {
            var singleSession = mocks.DynamicMock<ISession>();

            mocks.ReplayAll();
            var sharedUnitOfWork = new SingleNHibernateUnitOfWork(singleSession);
            var session = sharedUnitOfWork.Session;
            mocks.VerifyAll();

            Assert.Equal(singleSession, session);
        }

        [Fact]
        public void ShouldSaveChanges()
        {
            var singleSession = mocks.DynamicMock<ISession>();
            Expect.Call(singleSession.Flush);

            mocks.ReplayAll();
            var sharedUnitOfWork = new SingleNHibernateUnitOfWork(singleSession);
            sharedUnitOfWork.SaveChanges();
            mocks.VerifyAll();
        }

        [Fact]
        public void ShouldDispose()
        {
            var singleSession = mocks.DynamicMock<ISession>();
            Expect.Call(singleSession.Dispose); 
            
            mocks.ReplayAll();
            var sharedUnitOfWork = new SingleNHibernateUnitOfWork(singleSession);
            sharedUnitOfWork.Dispose();
            mocks.VerifyAll();
        }

    }
}
