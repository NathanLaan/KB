using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KB.Web.Models;
using KB.Lib.Data;
using KB.Lib.Entity;

namespace KB.Web.Controllers
{
    public class EntryController : Controller
    {

        private IDataRepository dataRepository;
        public EntryController()
        {
            //
            // TODO: Factory
            //
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB-SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }


        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Entry/Details/5

        public ActionResult Details(int id)
        {
            Entry entry = this.dataRepository.GetEntry(id);

            return View(entry);
        }

        private int GetFormsAuthenticationID()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            return int.Parse(ticket.UserData);
        }

        //
        // GET: /Entry/Add
        [Authorize]
        public ActionResult Add(EntryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    Entry entry = new Entry();
                    entry.ParentID = null;
                    entry.AccountID = GetFormsAuthenticationID();
                    entry.Title = model.Title;
                    entry.Contents = model.Contents;
                    entry.Timestamp = DateTime.Now;
                    entry = this.dataRepository.AddEntry(entry);

                    if (entry != null && entry.ID >= 0)
                    {
                        return RedirectToAction("Details", "Entry", new { id = entry.ID });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to create entry");
                    }

                    return RedirectToAction("Add", "Entry");
                }
                catch
                {
                    return View();
                }
            }
            return View();
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
