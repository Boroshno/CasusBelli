using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class Client
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int NPOffice { get; set; }
        [DataType(DataType.MultilineText)]
        public string AdditionalInfo { get; set; }
    }
}
