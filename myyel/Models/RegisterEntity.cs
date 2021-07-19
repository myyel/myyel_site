using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myyel.Models
{
    public class RegisterEntity
    {
        public int Id { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen ismini giriniz")]
        [DisplayName("Ad")]
        public string Name { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen soyismini giriniz")]
        [DisplayName("Soyad")]
        public string Surname { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",ErrorMessage ="Lütfen sadece harf ve sayı kullanınız. Türkçe Karakter kullanmayınız")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string Email { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Şifre")]
        [RegularExpression(@"^(?=.*[A-Za-zıİçÇşŞğĞÜüÖö])(?=.*\d)[A-Za-zıİçÇşŞğĞÜüÖö\d]{8,}$", ErrorMessage ="Şifreniz en az 8 karakterden oluşmalıdır ve içinde en az 1 harf, 1 sayı olmalıdır. ")]
        public string Password { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifre uyuşmuyor")]
        public string RePassword { get; set; }
        [System.Web.Mvc.AllowHtml]
        [Required]
        [DisplayName("Confirm")]
        public bool Confirm { get; set; }
    }
}