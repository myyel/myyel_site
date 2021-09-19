using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myyel.Models
{
    
    public class ForgotPasswordEmail
    {
        public int Id { get; set; }

        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string Email { get; set; }

        [System.Web.Mvc.AllowHtml]
        [DisplayName("Doğrulama Kodu")]
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Doğrulama Kodunu yanlış girdiniz")]
        public string Code { get; set; }
    }
}