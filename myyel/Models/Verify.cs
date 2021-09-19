using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myyel.Models
{
    public class Verify
    {
        [System.Web.Mvc.AllowHtml]
        [DisplayName("Doğrulama Kodu")]
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Doğrulama Kodunu yanlış girdiniz")]
        public string Code { get; set; }
    }
}