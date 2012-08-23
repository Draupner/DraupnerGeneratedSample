using Library.Core.Common.Transactions;

namespace Library.Web.Common.Transaction
{
    public class TransactionConfiguration
    {
        public void Configure()
        {
            TransactionRange.CreateTransactionRangeManager = (unitOfWork => new WebTransactionRangeManager(unitOfWork));
        }
    }
}