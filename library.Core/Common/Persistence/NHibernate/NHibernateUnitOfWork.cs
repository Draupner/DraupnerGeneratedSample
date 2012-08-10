using NHibernate;

namespace library.Core.Common.Persistence.NHibernate
{
    public abstract class NHibernateUnitOfWork : IUnitOfWork
    {
        public abstract ISession Session { get; }

        public abstract void SaveChanges();
        public abstract void Dispose();
    }
}
