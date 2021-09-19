using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myyel.Models
{
    public class PasswordChangeWithVerify
    {
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Şifre")]
        [RegularExpression(@"^(?=.*[A-Za-zıİçÇşŞğĞÜüÖö])(?=.*\d)[A-Za-zıİçÇşŞğĞÜüÖö\d]{8,}$", ErrorMessage = "Şifreniz en az 8 karakterden oluşmalıdır ve içinde en az 1 harf, 1 sayı olmalıdır. ")]
        public string Password { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifre uyuşmuyor")]
        public string RePassword { get; set; }
    }
}