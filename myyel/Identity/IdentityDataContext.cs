using Microsoft.AspNet.Identity.EntityFramework;
using myyel.Entity;
using myyel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myyel.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() : base("dataConnection")
        {

        }
        public DbSet<RegisterEntity> registerEntities { get; set; }
        public DbSet<LoginEntity> loginEntities { get; set; }
        public DbSet<ForgotPasswordEmail> forgotPasswordEmails { get; set; }
        public System.Data.Entity.DbSet<myyel.Identity.ApplicationRole> IdentityRoles { get; set; }

    }
}