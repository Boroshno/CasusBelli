using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;

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

    public static class TransactionFunctions
    {
        public static void WhenAddingNewProduct(ITransactionRepository transactionRepository, Product newproduct, int count)
        {
            Transaction lasttrans = transactionRepository.transactions.OrderByDescending(t => t.Date).First();
            Transaction newtran = new Transaction
            {
                BecameMoney = lasttrans.BecameMoney + (newproduct.TradePrice * count),                        
                ClientId = -1,
                Currency = newproduct.TradePrice * count,
                Date = DateTime.Now,
                WasMoney = lasttrans.BecameMoney,
                Text = "Закупка " + count
            };
            transactionRepository.AddTransaction(newtran);
        }

        public static void WhenProductWasSold(ITransactionRepository transactionRepository, string subTypeName,
            int count, int price)
        {
            Transaction lasttrans = transactionRepository.transactions.OrderByDescending(t => t.Date).First();
            Transaction newtran = new Transaction
            {
                BecameMoney = lasttrans.BecameMoney + (price * count),
                ClientId = -1,
                Currency = price * count,
                Date = DateTime.Now,
                WasMoney = lasttrans.BecameMoney,
                Text = "Продано " + count + " " + subTypeName
            };
            transactionRepository.AddTransaction(newtran);
        }
    }
}
