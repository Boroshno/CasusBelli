using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Entities
{
    public class OrderDetails
    {
        public ProductSubType ProductSubType { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public int NovaPoshta { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}",
        ErrorMessage = "Please enter correct email address")]
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Message { get; set; }
    }
}
