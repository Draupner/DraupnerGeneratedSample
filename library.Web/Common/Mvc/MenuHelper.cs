using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;

namespace Library.Web.Common.Mvc
{
    public static class MenuHelper
    {
        public static HtmlString Menu(this HtmlHelper helper)
        {
            var sb = new StringBuilder();

            var controllerNames = ControllerNames();
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            sb.Append("<ul>");
            foreach (string controllerName in controllerNames)
            {
                if(controllerName == "ErrorController")
                {
                    continue;
                }
                string name = controllerName.Replace("Controller", "");
                string url = urlHelper.Action("Index", name, null);
                sb.AppendLine("<li>");
                sb.AppendFormat(
                    @"<a title='{0}' href='{1}' style='padding-left: 4px;'>{0}</a>",
                    name, url);
                sb.AppendLine("</li>");
            }
            sb.Append("</ul>");

            return new HtmlString(sb.ToString());
        }

        public static IList<string> ControllerNames()
        {
            var controllerNames = new List<string>();
            SubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name));
            return controllerNames;
        }

        private static IList<Type> SubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(T))).ToList();
        }
    }
}