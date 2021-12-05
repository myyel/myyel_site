using Microsoft.AspNetCore.Mvc.Formatters;
using myyel.Entity;
using myyel.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace myyel.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IdentityDataContext _identity = new IdentityDataContext();
        DataContext _context = new DataContext();

        /*todo: İmg src yolu servera göre değişecek*/
        private bool SendEmailAll(List<ApplicationUser> applicationUsers, SendMail sendMail, List<SendMailPhoto> sendMailPhotos)
        {
            try
            {
                foreach (var item in applicationUsers)
                {
                    string imgHtml = "";
                    foreach (var i in sendMailPhotos) 
                    {
                        imgHtml += "<img src='https://www.myyeldesign.com/Admin/SendMailPhotoDetails/" + i.Id + " .jpg' class='img-fluid'/>";
                    };

                    string htmlBody = "<div style = 'width:70%; margin-left:2vw;' >" +

          "<h2>" + sendMail.Title + " </h2><hr />" +
          imgHtml +
                        "<p>" + sendMail.Content + "</p>" +

                    "<h3 style = 'margin-top: 8vw; ' > Yaratıcı Tasarımlar </h3>" +
                        "<h4 style = 'margin-top:6vw;' > İrtibat </h4>" +
                        "<p> Tel: 0 544 295 19 87 </p>" +
                        "<p> Mail: myyeldesign@gmail.com </p>" +

                        "</div>";

                    /*AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody,null,MediaTypeNames.Text.Html);

                    MailMessage mail = new MailMessage();
                    mail.AlternateViews.Add(avHtml);

                    foreach (var i in sendMailPhotos)
                    {
                        LinkedResource inline = new LinkedResource("D:/masaustu/myyel_site/myyel/Content/mail_images/" + i.Id + " .jpg", MediaTypeNames.Image.Jpeg);
                        inline.ContentId = Guid.NewGuid().ToString();
                        avHtml.LinkedResources.Add(inline);
                        Attachment att = new Attachment("D://masaustu//myyel_site//myyel//Content//mail_images//"+i.Id+" .jpg");
                        att.ContentDisposition.Inline = true;
                        mail.Attachments.Add(att);
                    };*/


                    MailMessage mail = new MailMessage();
                    mail.To.Add(item.Email);
                    mail.From = new MailAddress("myyeldesign@gmail.com");
                    mail.Subject = sendMail.Title;
                    mail.Body = htmlBody;
                    mail.IsBodyHtml = true;
                    mail.SubjectEncoding = Encoding.Default;
                    /*for (int i = 1; i-1 < sendMailPhotos.Count(); i++)
                    {
                        mail.Attachments.Add(new Attachment("D://masaustu//myyel_site//myyel//Content//mail_images//"+i+" .jpg", MediaTypeNames.Application.Octet));
                    }*/
                    mail.BodyEncoding = Encoding.Default;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Credentials = new NetworkCredential("myyeldesign@gmail.com", "xefyzc11");
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.hata = ex;
                return false;
            }

        }

        // GET: Admin
        public ActionResult Index()
        {
            Counter counter = new Counter();
            counter = _context.counters.FirstOrDefault();
            ViewBag.visitor = counter.Count.ToString();
            ViewBag.userCount = _identity.Users.Count();
            ViewBag.messageCount = _context.HomeFormEntites.Count();
            return View(_context.HomeEntities.Where(i => i.Id == 1).FirstOrDefault());
        }

        public ActionResult HomeEdit()
        {
            return View(_context.HomeEntities.Where(i => i.Id == 1).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HomeEdit([Bind(Include = "Id,SliderItemA1,SliderItemA2,SliderItemB1,SliderItemB2,"+
            "SliderItemC1,SliderItemC2,About1,About2,ProjectImage1,ProjectImage2,ProjectImage3,AdTitle1,AdTitle2,"+
            "AdItemImageA,AdItemImageB,AdItemImageC,AdItemImageAText1,AdItemImageAText2,AdItemImageBText1,"+
            "AdItemImageBText2,AdItemImageCText1,AdItemImageCText2,LessonTitle,LessonItem1,LessonItem2,LessonItem3,"+
            "LessonItem4,LessonItem5,LessonItem6,ContactTitleA,ContactTitleB,ContactPhone,ContactFacebook,ContactMail,"+
            "ContactInsta,FooterImage,FooterText")] HomeEntity homeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(homeEntity).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(homeEntity);
        }

        [HttpGet]
        public ActionResult ImageEdit(int? id)
        {
            id = 1;
            HomeEntity homeEntity = _context.HomeEntities.Find(id);
            if (homeEntity == null)
            {
                return HttpNotFound();
            }
            return View(homeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImageEdit(string id, HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage == null)
            {
                return RedirectToAction("HomeEdit");

            }
            else
            {
                string ImageFileName = id + " .jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/Content/img"), ImageFileName);
                if (System.IO.File.Exists(FolderPath))
                {
                    System.IO.File.Delete(FolderPath);
                }

                uploadedImage.SaveAs(FolderPath);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ProjectImage()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View(_context.ProjectImageEntities.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectImage([Bind(Include = "Id")] ProjectImageEntity projectImageEntity,
            HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage == null)
            {
                ViewData["hata"] = "Yükleme İşlemi başarısız";
                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                return View(_context.ProjectImageEntities.ToList());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.ProjectImageEntities.Add(projectImageEntity);
                    _context.SaveChanges();
                }

                string ImageFileName = projectImageEntity.Id + " .jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/Content/portfoy"), ImageFileName);
                if (System.IO.File.Exists(FolderPath))
                {
                    System.IO.File.Delete(FolderPath);
                }

                uploadedImage.SaveAs(FolderPath);
                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["sonuc"] = "Yükleme işlemi başarılı";
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                return View(_context.ProjectImageEntities.ToList());
            }
        }

        [HttpGet]
        public ActionResult ProjectDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ProjectImageEntity projectImageEntity = _context.ProjectImageEntities.Find(id);
            if (projectImageEntity == null)
            {
                return HttpNotFound();
            }
            _context.ProjectImageEntities.Remove(projectImageEntity);
            _context.SaveChanges();
            string ImageFileName = id.ToString() + " .jpg";
            string FolderPath = Path.Combine(Server.MapPath("~/Content/portfoy"), ImageFileName);
            if (System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            return RedirectToAction("ProjectImage");
        }

        [HttpGet]
        public ActionResult Blog()
        {
            return View(_context.HomeEntities.Find(1));
        }

        [HttpGet]
        public ActionResult BlogCreate()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogCreate([Bind(Include = "Id, BlogTitle, BlogItem")] BlogEntity blogEntity,
            HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage == null)
            {
                ViewData["hata"] = "Yükleme İşlemi başarısız";
                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.BlogEntities.Add(blogEntity);
                    _context.SaveChanges();
                }

                string ImageFileName = blogEntity.Id + " .jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/Content/blog-img"), ImageFileName);
                if (System.IO.File.Exists(FolderPath))
                {
                    System.IO.File.Delete(FolderPath);
                }

                uploadedImage.SaveAs(FolderPath);

                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["mesaj"] = "Yükleme İşlemi gerçekleşti.";
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                return RedirectToAction("Blog");
            }
        }

        [HttpGet]
        public ActionResult BlogList()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View(_context.BlogEntities.ToList());
        }

        [HttpGet]
        public ActionResult BlogEdit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            BlogEntity blogEntity = _context.BlogEntities.Find(id);
            if (blogEntity == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;

            return View(blogEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogEdit([Bind(Include = "Id, BlogTitle, BlogItem")] BlogEntity blogEntity,
            HttpPostedFileBase uploadedImage)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(blogEntity).State = EntityState.Modified;
                _context.SaveChanges();

                if (uploadedImage != null)
                {
                    string ImageFileName = blogEntity.Id + " .jpg";
                    string FolderPath = Path.Combine(Server.MapPath("~/Content/blog-img"), ImageFileName);
                    if (System.IO.File.Exists(FolderPath))
                    {
                        System.IO.File.Delete(FolderPath);
                    }

                    uploadedImage.SaveAs(FolderPath);
                }

                return RedirectToAction("BlogList");
            }

            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View();
        }

        [HttpGet]
        public ActionResult BlogDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            BlogEntity blogEntity = _context.BlogEntities.Find(id);
            if (blogEntity == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View(blogEntity);
        }

        [HttpGet]
        public ActionResult BlogDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            BlogEntity blogEntity = _context.BlogEntities.Find(id);
            if (blogEntity == null)
            {
                return HttpNotFound();
            }
            _context.BlogEntities.Remove(blogEntity);
            _context.SaveChanges();
            string ImageFileName = id.ToString() + " .jpg";
            string FolderPath = Path.Combine(Server.MapPath("~/Content/blog-img"), ImageFileName);
            if (System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            return RedirectToAction("BlogList");
        }

        [HttpGet]
        public ActionResult UserPage()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View(_identity.Users);
        }

        [HttpGet]
        public ActionResult UserDelete(string id)
        {
            if (id == "")
            {
                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return RedirectToAction("Error", "Home");
            }
            ApplicationUser applicationUser = _identity.Users.Find(id);
            if (applicationUser == null)
            {
                ViewBag.homeEntity = _context.HomeEntities.Find(1);
                return HttpNotFound();
            }
            _identity.Users.Remove(applicationUser);
            _identity.SaveChanges();
            return RedirectToAction("UserPage");
        }
        [HttpGet]
        public ActionResult SendMail()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail([Bind(Include = "Id, Title, Content")] SendMail sendMail)
        {
            if (ModelState.IsValid)
            {
                _context.SendMails.Add(sendMail);
                _context.SaveChanges();
                return RedirectToAction("SendMailPhoto");
            }
            return View(sendMail);
        }
        [HttpGet]
        public ActionResult SendMailPhoto()
        {
            SendMail sendMail = new SendMail();
            sendMail = _context.SendMails.ToList().LastOrDefault();


            string title = sendMail.Title;
            string content = sendMail.Content;

            ViewData["title"] = title;
            ViewData["content"] = content;
            TempData["title"] = sendMail.Title;
            TempData["content"] = sendMail.Content;
            TempData["id"] = sendMail.Id;
            return View(_context.SendMailPhotos.ToList());
        }

        [HttpPost]
        public ActionResult SendMailPhoto([Bind(Include = "Id")] SendMailPhoto sendMailPhoto,
            HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage == null)
            {
                ViewData["hata"] = "Yükleme İşlemi başarısız";
                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                return View(_context.SendMailPhotos.ToList());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.SendMailPhotos.Add(sendMailPhoto);
                    _context.SaveChanges();
                }

                string ImageFileName = sendMailPhoto.Id + " .jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/Content/mail_images"), ImageFileName);
                if (System.IO.File.Exists(FolderPath))
                {
                    System.IO.File.Delete(FolderPath);
                }


                uploadedImage.SaveAs(FolderPath);
                var homeEntity = _context.HomeEntities.Find(1);
                ViewData["sonuc"] = "Yükleme işlemi başarılı";
                ViewData["logo"] = homeEntity.FooterImage;
                ViewData["text"] = homeEntity.FooterText;
                SendMail sendMail = new SendMail();
                sendMail.Content = TempData["content"].ToString();
                sendMail.Title = TempData["title"].ToString();
                sendMail.Id = Convert.ToInt32(TempData["id"]);
                return RedirectToAction("SendMailPhoto", sendMail);
            }
        }

        /* Mail Body içerinde image kullanabilmek için oluşturuldu*/
        [HttpGet]
        public ActionResult SendMailPhotoDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            SendMailPhoto sendMailPhoto = _context.SendMailPhotos.Find(id);
            if (sendMailPhoto == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;

            return View(sendMailPhoto);
        }

        [HttpGet]
        public ActionResult SendMailPhotoDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            SendMailPhoto sendMailPhoto = _context.SendMailPhotos.Find(id);
            if (sendMailPhoto == null)
            {
                return HttpNotFound();
            }
            _context.SendMailPhotos.Remove(sendMailPhoto);
            _context.SaveChanges();
            string ImageFileName = id.ToString() + " .jpg";
            string FolderPath = Path.Combine(Server.MapPath("~/Content/mail_images"), ImageFileName);
            if (System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            SendMail sendMail = new SendMail();
            sendMail.Content = TempData["content"].ToString();
            sendMail.Title = TempData["title"].ToString();
            sendMail.Id = Convert.ToInt32(TempData["id"]);
            return RedirectToAction("SendMailPhoto", sendMail);
        }

        [HttpGet]
        public ActionResult MailSended()
        {
            SendMail sendMail = new SendMail();
            sendMail = _context.SendMails.ToList().LastOrDefault();
            List<ApplicationUser> applicationUser = _identity.Users.ToList();
            List<SendMailPhoto> sendMailPhotos = _context.SendMailPhotos.ToList();
            bool result = SendEmailAll(applicationUser, sendMail, sendMailPhotos);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult MailView()
        {
            return View();
        }

        public ActionResult Message()
        {
            var homeEntity = _context.HomeEntities.Find(1);
            ViewData["logo"] = homeEntity.FooterImage;
            ViewData["text"] = homeEntity.FooterText;
            return View(_context.HomeFormEntites.ToList());
        }

        public ActionResult MessageDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            HomeFormEntites blogEntity = _context.HomeFormEntites.Find(id);
            if (blogEntity == null)
            {
                return HttpNotFound();
            }
            _context.HomeFormEntites.Remove(blogEntity);
            _context.SaveChanges();
            
            return RedirectToAction("Message");
        }
    }
}