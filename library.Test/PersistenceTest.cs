using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Library.Core.Common.NHibernate;
using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Library.Test
{
    public class PersistenceTest
    {
        protected ISessionFactory SessionFactory;
        protected ISession UnitOfWorkSession;
        private readonly SingleNHibernateUnitOfWork unitOfWork;
        protected SingleNHibernateUnitOfWork UnitOfWork { get { return unitOfWork; } }

        public PersistenceTest()
        {
            NHibernate.Cfg.Configuration configuration = null;
            SessionFactory = Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.InMemory().DoNot.ShowSql
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateConfiguration>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersistenceTest>())
                .ExposeConfiguration(cfg => configuration = cfg)
                .CurrentSessionContext<ThreadStaticSessionContext>()
                .BuildSessionFactory();
            UnitOfWorkSession = SessionFactory.OpenSession();
            new SchemaExport(configuration).Execute(false, true, false, UnitOfWorkSession.Connection, null);
            unitOfWork = new SingleNHibernateUnitOfWork(UnitOfWorkSession);
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return new SingleNHibernateUnitOfWork(CreateSession());
        }

        protected ISession CreateSession()
        {
            return SessionFactory.OpenSession(UnitOfWorkSession.Connection);
        }
    }
}
