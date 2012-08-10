using library.Core.Common.Transactions;

namespace library.Web.Common.Transaction
{
    public class TransactionConfiguration
    {
        public void Configure()
        {
            TransactionRange.CreateTransactionRangeManager = (unitOfWork => new WebTransactionRangeManager(unitOfWork));
        }
    }
}