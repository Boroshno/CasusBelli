using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class ProductListViewModel :Product
    {
        private IProductRepository prod;

        public ProductListViewModel(Product pr, IList<Product> products)
        {
            this.AdditionalInfo = pr.AdditionalInfo;
            this.Condition = pr.Condition;
            this.CountryId = pr.CountryId;
            this.NATOSize = pr.NATOSize;
            this.ProductId = pr.ProductId;
            this.Size = pr.Size;
            this.StatusId = pr.StatusId;
            this.SubTypeId = pr.SubTypeId;
            this.TradePrice = pr.TradePrice;
            this.TypeId = pr.TypeId;
        }
    }
}