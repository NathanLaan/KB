﻿using System;
using System.Web;
using System.Web.Mvc;

namespace KB.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
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
