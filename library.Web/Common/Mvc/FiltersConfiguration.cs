using System.Web.Mvc;
using Library.Web.Common.NHibernate;
using Library.Web.Common.Logging;

namespace Library.Web.Common.Mvc
{
    public class FiltersConfiguration
    {
        public void Configure()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);            
        }

        private void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NHibernateUnitOfWorkFilter());
            filters.Add(new ElmahHandleErrorAttribute());
        }
    }
}