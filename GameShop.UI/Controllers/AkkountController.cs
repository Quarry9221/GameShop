using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameShop.UI.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace GameShop.UI.Controllers
{
    public class AkkountController : Controller
    {
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
            }
            return View(model);
        }












            public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == model.Username);
            }
            if( user == null)
            {
                using (UserContext db = new UserContext ())
                {
                    db.Users.Add(new User {Username = model.Username, Password = model.Password });
                    db.SaveChanges();
                    user = db.Users.Where(u => u.Username == model.Username && u.Password == u.Password).FirstOrDefault();
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("List", "Game");
                }
            }
            else
            {
                ModelState.AddModelError("", "User існує");
            }
            return View(model);

        }
    }
}