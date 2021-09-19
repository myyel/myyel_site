using myyel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myyel.Entity;
using System.Data.Entity;
using System.IO;
using System.Web.Routing;
using System.Data.Entity.Validation;
using myyel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace myyel.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        IdentityDataContext _identity = new IdentityDataContext();
        DataContext _context = new DataContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public UserController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        [HttpGet]
        public ActionResult UserOperations(string ad)
        {
            ad = User.Identity.Name;
            if (ad == "")
            {
                return RedirectToAction("Login", "Account");
            }

            ApplicationUser applicationUser = new ApplicationUser();
            var edituser = _identity.Users.Where(i => i.UserName == ad).FirstOrDefault();

            if (edituser == null)
            {
                return RedirectToAction("Error", "Home");
            }
            applicationUser.Id = edituser.Id;
            applicationUser.Name = edituser.Name;
            applicationUser.Surname = edituser.Surname;
            applicationUser.UserName = edituser.UserName;
            applicationUser.Email = edituser.Email;

            ViewBag.applicationuser = applicationUser;
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserOperations([Bind(Include = "Email")] EmailChange emailChange)
        {
            string name = User.Identity.Name;
            string email = emailChange.Email;
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = _identity.Users.Where(i => i.UserName == name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                applicationUser.Email = email;
                _identity.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.applicationuser = applicationUser;
            ViewBag.hata = "Geçerli bir email adresi giriniz";
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(emailChange);
        }

        [HttpGet]
        public ActionResult UserPasswordChange()
        {
            string name = User.Identity.Name;
            if (name == "")
            {
                return RedirectToAction("Login", "Account");
            }

            var edituser = _identity.Users.Where(i => i.UserName == name).FirstOrDefault();

            if (edituser == null)
            {
                return RedirectToAction("Error", "Home");
            }

            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }

        [HttpPost]
        public ActionResult UserPasswordChange(PasswordChange passwordChange)
        {
            string name = User.Identity.Name;
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = _identity.Users.Where(i => i.UserName == name).FirstOrDefault();
            IdentityResult result = userManager.ChangePassword(applicationUser.Id, passwordChange.ExPassword, passwordChange.Password);

            if (!result.Succeeded)
            {
                ViewBag.applicationuser = applicationUser;
                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return View(passwordChange);
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserDelete(string userName)
        {
            userName = User.Identity.Name;
            if (userName == null)
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignOut();

                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return RedirectToAction("Error", "Home");
            }
            else
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignOut();

                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser = _identity.Users.Where(i => i.UserName == userName).FirstOrDefault();


                if (ModelState.IsValid)
                {
                    _identity.Users.Remove(applicationUser);
                    _identity.SaveChanges();

                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

