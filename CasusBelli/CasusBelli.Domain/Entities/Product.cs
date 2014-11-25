using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class Product
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The typeId of product is required")]
        [HiddenInput(DisplayValue = false)]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "The subtypeId of product is required")]
        [HiddenInput(DisplayValue = false)]
        public int SubTypeId { get; set; }
        [Required(ErrorMessage = "The countryId of product is required")]
        [HiddenInput(DisplayValue = false)]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "The statusId of product is required")]
        [HiddenInput(DisplayValue = false)]
        public int StatusId { get; set; }
        public string Size { get; set; }
        public string NATOSize { get; set; }
        [DataType(DataType.MultilineText)]
        public string Condition { get; set; }
        [DataType(DataType.MultilineText)]
        public string AdditionalInfo { get; set; }
        public int TradePrice { get; set; }
    }
}
