using System;
using System.Web;
using System.Web.Mvc;

namespace KB.Web.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Results(string queryString)
        {
            return View();
        }

    }
}
