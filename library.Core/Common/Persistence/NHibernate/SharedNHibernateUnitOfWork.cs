using NHibernate;

namespace library.Core.Common.Persistence.NHibernate
{
    public class SharedNHibernateUnitOfWork : NHibernateUnitOfWork
    {
        private readonly ISessionFactory sessionFactory;

        public SharedNHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public override ISession Session
        {
            get { return sessionFactory.GetCurrentSession(); }
        }

        public override void SaveChanges()
        {
            sessionFactory.GetCurrentSession().Flush();
        }

        public override void Dispose()
        {
            // The session is closed and disposed else where
        }
    }
}
