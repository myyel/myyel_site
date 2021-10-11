using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(myyel.App_Start.Startup1))]

namespace myyel.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions() { 
                AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath= new PathString("/Home/Index")
            });
        }
    }

}
