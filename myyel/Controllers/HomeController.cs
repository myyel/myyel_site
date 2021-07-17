using myyel.Entity;
using myyel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Controllers
{
    //todo: formValidates
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();

        public ActionResult Index()
        {
            ViewBag.blog = _context.BlogEntities.ToList();
            return View(_context.HomeEntities.Where(i => i.Id == 1).FirstOrDefault());
        }
        public ActionResult Project()
        {
            ViewBag.ProjectImage = _context.ProjectImageEntities;
            return View(_context.HomeEntities.Where(i => i.Id == 1).FirstOrDefault());
        }

        public ActionResult Blog()
        {
            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(_context.BlogEntities.ToList());
        }

        [HttpGet]
        public ActionResult BlogDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error");
            }
            BlogEntity blogEntity = _context.BlogEntities.Find(id);
            if (blogEntity == null)
            {
                return RedirectToAction("Error");
            }

            ViewBag.homeEntity = _context.HomeEntities.Find(1);
            return View(blogEntity);
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View(_context.HomeEntities.Find(1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim([Bind(Include ="Id, FormName, FormSurname, FormTelefon, FormMail, FormMesaj")] 
        HomeFormEntites homeFormEntites)
        {
            if (ModelState.IsValid)
            {
                    _context.HomeFormEntites.Add(homeFormEntites);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}