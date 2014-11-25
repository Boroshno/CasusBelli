using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class ProductSubType
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SubTypeId { get; set; }
        [Required(ErrorMessage = "The typeId of subtype is required")]
        [HiddenInput(DisplayValue = false)]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "The name of subtype is required")]
        public string SubTypeName { get; set; }
        [DataType(DataType.MultilineText)]
        public string SubTypeText { get; set; }
        public string Photo { get; set; }
        [DataType(DataType.MultilineText)]
        public string AdditionalInfo { get; set; }
        public int Price { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CountryId { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue=false)]
        public string ImageMimeData { get; set; }
    }
}
