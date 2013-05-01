using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KB.Web.Models;
using KB.Lib.Data;

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
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB"].ConnectionString;
            this.dataRepository = new ADODataRepository(cs);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(AccountLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                if (this.dataRepository.ValidateUser(model.Username, model.Password))
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

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
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
