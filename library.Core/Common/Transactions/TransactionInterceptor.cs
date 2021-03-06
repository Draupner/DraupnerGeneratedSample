using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Library.Core.Common.Transactions
{
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo = invocation.MethodInvocationTarget ?? invocation.Method;

            if (HasTransactionAttribute(methodInfo))
            {
                Inside.Transaction(invocation.Proceed);
            } else
            {
                invocation.Proceed();
            }
        }

        private bool HasTransactionAttribute(MethodInfo methodInfo)
        {
            return HasMethodLevelTransactionAttribute(methodInfo) || HassClassLevelTransactionAttribute(methodInfo);
        }

        private bool HassClassLevelTransactionAttribute(MethodInfo methodInfo)
        {
            return methodInfo.ReflectedType.GetCustomAttributes(typeof (TransactionAttribute), false).Count() > 0;
        }

        private bool HasMethodLevelTransactionAttribute(MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttributes(typeof (TransactionAttribute), false).Count() > 0;
        }
    }
}