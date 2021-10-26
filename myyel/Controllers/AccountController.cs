using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using myyel.Entity;
using myyel.Identity;
using myyel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace myyel.Controllers
{
    public class AccountController : Controller
    {
        DataContext _context = new DataContext();
        IdentityDataContext _identity = new IdentityDataContext();

        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        public int sayi;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        private bool SendEmail(string email, int dog, string name)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("myyeldesign@gmail.com");
            mail.Subject = "Şifre Yenileme İşlemi Doğrulama Kodu ";
            mail.Body = "<div style='width:70%; margin-left:2vw;'>" +
        "<p> Merhaba " + name + ",</p><hr/>" +
          "<div style = 'display:flex; margin-top:4vw;'>" +
               "<span style = 'margin-top:2vw;' > Doğrulama Kodunuz:            </span>" +
                    "<div style = 'background-color: #b6976c; color:#000; text-align:center; font-size:3vw; width:30%; height:10%; margin:0vw 2vw;padding:1vw;' >" + dog + "</div>" +
                     "</div>" +
                     "<h3 style = 'margin - top: 8vw; ' > Önemli Hatırlatma </h3>" +
                          "<p>" +
                              "Eğer şifre yenileme talebinin size ait olmadığını düşünüyorsanız lütfen bu e-postayı dikkate almayın. Mevcut şifreniz ile giriş yapmaya devam edebilirsiniz." +
                          "</p>" +
                          "<p style = 'margin-top:6vw;' > Teşekkür ederiz </p>" +
                           "</div>";
            mail.IsBodyHtml = true;
            mail.SubjectEncoding = Encoding.Default;
            mail.BodyEncoding = Encoding.Default;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("myyeldesign@gmail.com", "xefyzc11");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.hata = ex;
                return false;
            }

        }

        public ActionResult Register()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterEntity model)
        {
            if (ModelState.IsValid)
            {
                if (model.Confirm == false)
                {
                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    ViewData["check"] = "Kullanım Szöleşmesini okuyup kabul ediniz";
                    return View(model);
                }
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.LockoutEnabled = model.Confirm;

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
                    ViewData["check"] = "Kayıt Oluşturulamadı...";
                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    return View(model);
                }
            }
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(model);
        }

        public ActionResult Login()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
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
                    var authProperties = new AuthenticationProperties() { IsPersistent = model.RememberMe };
                    authmanager.SignOut();
                    authmanager.SignIn(authProperties, identityclaims);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı bulunamadı...");
                }
            }

            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(model);
        }

        [Authorize(Roles = "user")]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PrivacyPolicy()
        {
            return View();

        }

        [HttpGet]
        public ActionResult PasswordOperations()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordOperations([Bind(Include = "Id,Email,Code")] ForgotPasswordEmail _forgotPasswordEmail)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = _identity.Users.Where(i => i.Email == _forgotPasswordEmail.Email).FirstOrDefault();

            if (applicationUser != null)
            {
                Random rnd = new Random();
                int dogKod = rnd.Next(100000, 1000000);
                _forgotPasswordEmail.Code = dogKod.ToString();
                bool result = SendEmail(_forgotPasswordEmail.Email, dogKod, applicationUser.Name);

                if (result == false)
                {
                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    return View();
                }

                TempData["code"] = dogKod;
                TempData["mail"] = _forgotPasswordEmail.Email;
                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return RedirectToAction("VerifyCode");
            }

            ViewBag.hata = "Kullanıcı Bulunamadı...";
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }
        [HttpGet]
        public ActionResult VerifyCode()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(Verify _verify)
        {
            string tempDataCode = TempData["code"].ToString();

            if (tempDataCode == _verify.Code)
            {
                return RedirectToAction("PasswordChangeWithVerify");
            }
            return View();
        }
        [HttpGet]
        public ActionResult PasswordChangeWithVerify()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordChangeWithVerify(PasswordChangeWithVerify _passwordChangeWithVerify)
        {
            string tempDataMail = TempData["mail"].ToString();
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = _identity.Users.Where(i => i.Email == tempDataMail).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _identity.Users.Remove(applicationUser);
                _identity.SaveChanges();

                ApplicationUser register = new ApplicationUser();
                register.Email = tempDataMail;
                register.Name = applicationUser.Name;
                register.Surname = applicationUser.Surname;
                register.UserName = applicationUser.UserName;
                register.LockoutEnabled = true;

                IdentityResult resultCreate = userManager.Create(register, _passwordChangeWithVerify.Password);
                _identity.SaveChanges();

                if (resultCreate.Succeeded)
                {
                    if (roleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(register.Id, "user");
                    }

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewData["hata"] = "Şifre Değiştirilemedi";
                    ViewBag.homeEntity = _context.HomeEntities.Find(1);
                    return View(_passwordChangeWithVerify);
                }

            }

            return View();
        }
    }
}