using System;
using System.Web;
using System.Web.Mvc;

namespace KB.Web.Controllers
{
    public class EntryController : Controller
    {
        //
        // GET: /Entry/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Entry/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Entry/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Entry/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Entry/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Entry/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Entry/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Entry/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
