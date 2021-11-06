using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myyel.Entity
{
    public class HomeFormEntites
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required]
        [DisplayName("Ad")]
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen ismini giriniz")]
        public string FormName { get; set; }
        [AllowHtml]
        [Required]
        [DisplayName("Soyad")]
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen soyismini giriniz")]
        public string FormSurname { get; set; }
        [AllowHtml]
        [Required]
        [Phone]
        [DisplayName("Telefon")]
        [RegularExpression(@"^([(])([0-9]{3})\s?([)])([ ])([0-9]{3})\s?([-])([0-9]{4})\s?$", ErrorMessage = "Başında 0 olmadan telefon numaranızı giriniz")]
        public string FormTelefon { get; set; }
        [EmailAddress]
        [DisplayName("Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string FormMail { get; set; }
        [AllowHtml]
        [Required]
        [DisplayName("Mesaj")]
        public string FormMesaj { get; set; }
    }
}