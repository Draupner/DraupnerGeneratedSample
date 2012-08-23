using System;

namespace Library.Core.Common.Transactions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionAttribute : Attribute
    {
    }
}