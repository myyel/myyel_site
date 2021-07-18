using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using myyel.Entity;
using myyel.Identity;
using myyel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Controllers
{
    public class AccountController : Controller
    {
        DataContext _context = new DataContext();
        //todo: Title Ekle
        //todo: Validate operations 
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Register()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }

        //todo: user control while registering
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterEntity model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;

                IdentityResult result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if (roleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(user.Id, "user");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegisterError","Kayıt Oluşturulamadı..." );
                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    return View(model);
                }
            }
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginEntity model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.UserName, model.Password);

                if (user != null)
                {
                    var authmanager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authmanager.SignIn(authProperties, identityclaims);

                    return RedirectToAction("Blog", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı bulunamadı...");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}