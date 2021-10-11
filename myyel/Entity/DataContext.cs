using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myyel.Entity
{
    public class DataContext:DbContext
    {
        public DataContext():base("dataConnection")
        {
            
        }

        public DbSet<HomeEntity> HomeEntities { get; set; }
        public DbSet<BlogEntity> BlogEntities { get; set; }
        public System.Data.Entity.DbSet<myyel.Entity.ProjectImageEntity> ProjectImageEntities { get; set; }
        public DbSet<HomeFormEntites> HomeFormEntites { get; set; }
        public DbSet<Counter> counters { get; set; }
        public DbSet<SendMail> SendMails { get; set; }
        public DbSet<SendMailPhoto> SendMailPhotos { get; set; }

    }
}