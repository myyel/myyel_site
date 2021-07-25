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

namespace myyel.Controllers
{
    public class UserController : Controller
    {
        IdentityDataContext _identity = new IdentityDataContext();
        DataContext _context = new DataContext();

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
            applicationUser.PasswordHash = edituser.PasswordHash;

            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(applicationUser);
        }
    }
}
