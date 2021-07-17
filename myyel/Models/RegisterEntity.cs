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
        [Required]
        [DisplayName("Ad:")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyad:")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Email:")]
        [EmailAddress(ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Şifre Tekrar:")]
        [Compare("Password", ErrorMessage = "Şifre uyuşmuyor")]
        public string RePassword { get; set; }
    }
}