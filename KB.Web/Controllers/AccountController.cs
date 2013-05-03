using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KB.Web.Models;
using KB.Lib.Data;
using KB.Lib.Entity;
using KB.Lib.Utility;
//AccountList:
using System.Web.Script.Serialization;

namespace KB.Web.Controllers
{

    //
    // http://www.asp.net/mvc/tutorials/older-versions/security/authenticating-users-with-forms-authentication-cs
    //
    //
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

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }

        public ActionResult AccoutList(string sidx, string sord, int page, int rows)
        {
            List<Account> accountList = this.dataRepository.GetAccountList();

            var jsonData = new
            {
                total = 1,
                page = page,
                records = 3,
                rows = accountList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Login(AccountLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = this.dataRepository.GetAccount(model.Username);
                if (account != null)
                {

                    //
                    // TODO: check password
                    //
                    SecurityUtil.EncryptedPassword encryptedPassword = SecurityUtil.GenerateEncryptedPassword(model.Password, account.PasswordSalt);

                    if (encryptedPassword.Password == account.Password)
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
                        ModelState.AddModelError("", "The username or password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect.");
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

                    Account account = model.ToAccount();
                    SecurityUtil.EncryptedPassword encryptedPassword = SecurityUtil.GenerateEncryptedPassword(account.Password);
                    account.Password = encryptedPassword.Password;
                    account.PasswordSalt = encryptedPassword.PasswordSalt;
                    account = this.dataRepository.AddAccount(account);

                    if (account != null && account.ID >= 0)
                    {
                        FormsAuthentication.SetAuthCookie(account.Name, true);
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
