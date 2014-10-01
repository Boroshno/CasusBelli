using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(logOnModel.UserName, logOnModel.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Невірне ім'я користувача або пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
