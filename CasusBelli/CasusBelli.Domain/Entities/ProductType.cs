using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Entities
{
    public class ProductType
    {
        [Key]
        [HiddenInput(DisplayValue=false)]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "The name of type is required")]
        public string TypeName { get; set; }
        public string TypeText { get; set; }
        [Required(ErrorMessage = "The photo address is required")]
        public string Photo { get; set; }
        [DataType(DataType.MultilineText)]
        public string AdditionalInfo { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeData { get; set; }
    }
}
