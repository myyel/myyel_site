using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Entity
{
    public class SendMail
    {
        public int Id { get; set; }
        [AllowHtml]
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}