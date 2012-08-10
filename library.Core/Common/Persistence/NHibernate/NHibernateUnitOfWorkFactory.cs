using NHibernate;

namespace library.Core.Common.Persistence.NHibernate
{
    public class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISessionFactory sessionFactory;

        public NHibernateUnitOfWorkFactory(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public IUnitOfWork Create()
        {
            var session = sessionFactory.OpenSession();
            return new SingleNHibernateUnitOfWork(session);
        }
    }
}
