using myyel.Entity;
using myyel.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace myyel
{
    public class MvcApplication : System.Web.HttpApplication
    {

        DataContext _context = new DataContext();
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DataInitializer());
            Database.SetInitializer(new IdentityInitializer());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Counter counter = new Counter();
            counter = _context.counters.Where(i => i.Id == 1).FirstOrDefault();
            counter.Count=counter.Count+1;
            _context.Entry(counter).State = EntityState.Modified;
            _context.SaveChanges();
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Counter counter = new Counter();
            counter = _context.counters.Where(i => i.Id == 1).FirstOrDefault();
            counter.Count = counter.Count - 1;
            _context.Entry(counter).State = EntityState.Modified;
            _context.SaveChanges();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
