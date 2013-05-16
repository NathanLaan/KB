using System;
using System.Web;
using System.Web.Mvc;
using KB.Web.Models;
using KB.Lib.Data;
using KB.Lib.Entity;

namespace KB.Web.Controllers
{
    public class SearchController : Controller
    {

        private IDataRepository dataRepository;
        public SearchController()
        {
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB-SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Results(string searchString, int page)
        {
            SearchModel model = new SearchModel();
            model.SearchString = searchString;
            model.Page = page;
            model.PageSize = 20;
            this.dataRepository.Search(searchString, model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Page(int id = 1)
        {
            EntryIndexModel model = new EntryIndexModel();
            model.Page = id;
            model.PageSize = 10;
            model.List = this.dataRepository.GetTopLevelEntryList(model.Page, model.PageSize);
            return View(model);
        }

    }
}
