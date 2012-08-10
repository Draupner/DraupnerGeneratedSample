using System;
using library.Core.Common.NHibernate;
using library.Core.Common.Persistence;
using library.Core.Common.Persistence.NHibernate;
using NHibernate;

namespace library.Core.Common.Transactions
{
    public static class Inside
    {
        public static TResult Transaction<TResult>(Func<TResult> func)
        {
            ISession session = NHibernateConfiguration.SessionFactory.GetCurrentSession();
            return Transaction(session, func);
        }

        public static TResult Transaction<TResult>(IUnitOfWork unitOfWork, Func<TResult> func)
        {
            ISession session = ((NHibernateUnitOfWork)unitOfWork).Session;

            return Transaction(session, func);
        }

        private static TResult Transaction<TResult>(ISession session, Func<TResult> func)
        {
            if (!session.Transaction.IsActive)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    TResult result;
                    try
                    {
                        result = func.Invoke();
                    }
                    catch
                    {
                        if ((transaction != null) && (transaction.IsActive))
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                    finally
                    {
                        if ((transaction != null) && (transaction.IsActive))
                        {
                            transaction.Commit();
                        }
                    }
                    return result;
                }
            }
            return func.Invoke();
        }

        public static void Transaction(IUnitOfWork unitOfWork, Action action)
        {
            Transaction(unitOfWork, () =>
            {
                action.Invoke();
                return false;
            });
        }

        public static void Transaction(Action action)
        {
            Transaction(() =>
            {
                action.Invoke();
                return false;
            });
        }
    }
}