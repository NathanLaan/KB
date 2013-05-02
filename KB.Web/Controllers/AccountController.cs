using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KB.Web.Models;
using KB.Lib.Data;
using KB.Lib.Entity;

namespace KB.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        private IDataRepository dataRepository;

        public AccountController()
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
        public ActionResult Login(AccountLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                if (this.dataRepository.ValidateAccount(model.Username, model.Password))
                {

                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Default");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid model state.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Account account = this.dataRepository.AddAccount(model.ToAccount());

                    if (account != null && account.ID >= 0)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, false /* createPersistentCookie */);
                        return RedirectToAction("Index", "Default");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to create account");
                    }
                }
                catch (Exception e)
                {
                    //
                    // TODO: TEMP!
                    //
                    ModelState.AddModelError("", e.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult PasswordReset()
        {
            return View();
        }
        public ActionResult All()
        {
            return View();
        }

    }
}
