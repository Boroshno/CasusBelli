using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CasusBelli.Domain.Entities
{
    public class Transaction
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TransactionId { get; set; }
        public string Text { get; set; }
        [DisplayFormat(DataFormatString = "{0:$ #,#.00}", ApplyFormatInEditMode = true)]
        public decimal WasMoney { get; set; }
        [DisplayFormat(DataFormatString = "{0:$ #,#.00}", ApplyFormatInEditMode = true)]
        public decimal Currency { get; set; }
        [DisplayFormat(DataFormatString = "{0:$ #,#.00}", ApplyFormatInEditMode = true)]
        public decimal BecameMoney { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? ClientId { get; set; }
    }
}
