using System.Web.Mvc;
using library.Web.Common.NHibernate;
using library.Web.Common.Logging;

namespace library.Web.Common.Mvc
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