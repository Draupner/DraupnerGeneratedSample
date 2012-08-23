using System.Threading;
using Library.Core.Common.Persistence;

namespace Library.Core.Common.Transactions
{
    public class ThreadLocalTransactionRangeManager : TransactionRangeManager
    {
        private static readonly ThreadLocal<int> nested = new ThreadLocal<int>();
        private static readonly ThreadLocal<bool> nestedHasNotCompleted = new ThreadLocal<bool>();

        public ThreadLocalTransactionRangeManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override int Nested
        {
            get { return nested.Value; }
            set { nested.Value = value; }
        }

        protected override bool NestedHasNotCompleted
        {
            get { return nestedHasNotCompleted.Value; }
            set { nestedHasNotCompleted.Value = value; }
        }
    }
}