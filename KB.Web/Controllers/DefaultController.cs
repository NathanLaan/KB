using System;
using System.Web;
using System.Web.Mvc;

namespace KB.Web.Controllers
{
    public class DefaultController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}
