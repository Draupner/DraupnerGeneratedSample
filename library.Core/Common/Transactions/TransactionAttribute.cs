using System;

namespace library.Core.Common.Transactions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionAttribute : Attribute
    {
    }
}