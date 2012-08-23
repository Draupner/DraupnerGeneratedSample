using System;
using Library.Core.Common.Persistence;
using Library.Core.Common.Windsor;

namespace Library.Core.Common.Transactions
{
    public class TransactionRange : IDisposable
    {
        private bool disposed;
        private readonly TransactionRangeManager transactionRangeManager;

        public static Func<IUnitOfWork, TransactionRangeManager> CreateTransactionRangeManager;

        public TransactionRange()
            : this(Ioc.Container.Resolve<IUnitOfWork>())
        {
        }

        public TransactionRange(IUnitOfWork unitOfWork)
        {
            transactionRangeManager = CreateTransactionRangeManager(unitOfWork);
            transactionRangeManager.Start();
        }

        public void Complete()
        {
            transactionRangeManager.Complete();
        }

        public void Dispose()
        {
            if (disposed)
                return;
            disposed = true;

            transactionRangeManager.Close();
        }
    }
}