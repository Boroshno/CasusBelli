using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class WebLink
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
