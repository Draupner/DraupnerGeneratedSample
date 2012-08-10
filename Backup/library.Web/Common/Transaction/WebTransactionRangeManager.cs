using System.Web;
using library.Core.Common.Persistence;
using library.Core.Common.Transactions;

namespace library.Web.Common.Transaction
{
    public class WebTransactionRangeManager : TransactionRangeManager
    {
        private const string NestedItemName = "TransactionRange.Nested";
        private const string NestedHasNotCompletedItemName = "TransactionRange.NestedHasNotCompleted";

        public WebTransactionRangeManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override int Nested
        {
            get { return (int) (HttpContext.Current.Items[NestedItemName] ?? 0); }
            set { HttpContext.Current.Items[NestedItemName] = value; }
        }

        protected override bool NestedHasNotCompleted
        {
            get { return (bool) (HttpContext.Current.Items[NestedHasNotCompletedItemName] ?? false); }
            set { HttpContext.Current.Items[NestedHasNotCompletedItemName] = value; }
        }
    }
}