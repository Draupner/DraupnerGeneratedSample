using NHibernate;

namespace library.Core.Common.Persistence.NHibernate
{
    public class SingleNHibernateUnitOfWork : NHibernateUnitOfWork
    {
        private readonly ISession session;
        private bool isDisposed;

        public SingleNHibernateUnitOfWork(ISession session)
        {
            this.session = session;
        }

        public override ISession Session
        {
            get { return session; }
        }

        public override void SaveChanges()
        {
            Session.Flush();
        }

        public override void Dispose()
        {
            if(isDisposed)
            {
                return;
            }
            isDisposed = true;

            Session.Flush();
            Session.Close();
            Session.Dispose();
        }
    }
}
