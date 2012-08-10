using System;
using library.Core.Common.Persistence;
using library.Core.Common.Windsor;

namespace library.Core.Common.Transactions
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