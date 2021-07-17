using myyel.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        DataContext _context = new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
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
    }
}