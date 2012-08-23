using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View("Error");
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewBag.ErrorPath = aspxerrorpath;
            return View();
        }
    }
}