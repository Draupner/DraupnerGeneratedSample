using System.Web;
using System.Web.Mvc;
using Elmah;
using library.Core.Common.Logging;
using library.Core.Common.NHibernate;
using library.Web.Common.AutoMapper;
using library.Web.Common.Mvc;
using library.Web.Common.Transaction;
using library.Web.Common.Windsor;

namespace library.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private AutoMapperConfiguration autoMapperConfiguration;
        private FiltersConfiguration filtersConfiguration;
        private Log4NetConfiguration log4NetConfiguration;
        private RoutesConfiguration routesConfiguration;
        private NHibernateConfiguration nHibernateConfiguration;
        private WindsorConfiguration windsorConfiguration;
        private TransactionConfiguration transactionConfiguration;

        protected void Application_Start()
        {
            log4NetConfiguration = new Log4NetConfiguration();
            filtersConfiguration = new FiltersConfiguration();
            routesConfiguration = new RoutesConfiguration();
            autoMapperConfiguration = new AutoMapperConfiguration();
            nHibernateConfiguration = new NHibernateConfiguration();
            windsorConfiguration = new WindsorConfiguration();
            transactionConfiguration = new TransactionConfiguration();
            
            AreaRegistration.RegisterAllAreas();

            log4NetConfiguration.Configure();
            filtersConfiguration.Configure();
            routesConfiguration.Configure();
            autoMapperConfiguration.Configure();
            nHibernateConfiguration.Configure();
            windsorConfiguration.Configure();
            transactionConfiguration.Configure();
        }

        public void Application_End()
        {
            windsorConfiguration.Shutdown();
        }
		
        void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException && ((HttpException)e.Exception.GetBaseException()).GetHttpCode() == 404)
            {
                e.Dismiss();
            }
        }		
    }
}