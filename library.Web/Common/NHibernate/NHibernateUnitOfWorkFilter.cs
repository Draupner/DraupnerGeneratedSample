using System.Web.Mvc;
using library.Core.Common.NHibernate;
using NHibernate;
using NHibernate.Context;

namespace library.Web.Common.NHibernate
{
    public class NHibernateUnitOfWorkFilter : ActionFilterAttribute
    {
        protected ISessionFactory SessionFactory
        {
            get
            {
                return NHibernateConfiguration.SessionFactory;
            }
        }
         
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            session.Flush();
            session.Close();
            session.Dispose();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ISession session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);           
        }
    }
}