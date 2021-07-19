using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Models
{
    public class LoginEntity
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [AllowHtml]
        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [AllowHtml]
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}