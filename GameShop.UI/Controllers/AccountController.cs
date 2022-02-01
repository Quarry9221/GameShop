using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.UI.Models;
using GameShop.UI.Infrastructure.Repository;
namespace GameShop.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthentication authProvider;

        public AccountController(IAuthentication auth)
        {
            authProvider = auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.Username, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
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