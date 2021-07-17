using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myyel.Entity
{
    public class HomeFormEntites
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public string FormSurname { get; set; }
        public string FormTelefon { get; set; }
        public string FormMail { get; set; }
        public string FormMesaj { get; set; }
    }
}