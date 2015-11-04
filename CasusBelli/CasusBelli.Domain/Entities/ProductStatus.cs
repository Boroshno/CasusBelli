using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class ProductStatus
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int StatusId { get; set; }
        public string StatusText { get; set; }
    }

    public enum ProductStatusEnum
    {
        Available =1,
        Reserved = 2,
        Sold = 3
    }
}
