using System;
using System.Collections.Generic;
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
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen ismini giriniz")]
        public string FormName { get; set; }
        [AllowHtml]
        [Required]
        [RegularExpression(@"^[a-zA-ZıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen soyismini giriniz")]
        public string FormSurname { get; set; }
        [AllowHtml]
        [Required]
        [Phone]
        [RegularExpression(@"^([(])([0-9]{3})\s?([)])([ ])([0-9]{3})\s?([-])([0-9]{4})\s?$", ErrorMessage = "Başında 0 olmadan telefon numaranızı giriniz")]
        public string FormTelefon { get; set; }
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string FormMail { get; set; }
        [AllowHtml]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9ıİçÇşŞğĞÜüÖö]*$", ErrorMessage = "Lütfen ismini giriniz")]
        public string FormMesaj { get; set; }
    }
}