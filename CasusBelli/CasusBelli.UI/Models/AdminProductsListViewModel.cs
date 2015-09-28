using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class AdminProductsListViewModel:Product
    {
        public string TypeName { get; set; }
        public string SubTypeName { get; set; }
        public string CountryName { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
        public int ClientId { get; set; }

        IList<ProductType> availableTypes { get; set; }
        IList<Country> availableCountries { get; set; }
        IList<ProductSubType> availablesubtypes { get; set; }
        IList<ProductStatus> availablestatuses { get; set; }

        public IList<Client> availableclients { get; set; } 

        public AdminProductsListViewModel(Product product, IList<ProductType> availableTypes,
            IList<Country> availableCountries, IList<ProductSubType> availablesubtypes,
            IList<ProductStatus> availablestatuses, IList<Client> availableclients)
        {
            this.AdditionalInfo = product.AdditionalInfo;
            this.Condition = product.Condition;
            this.CountryId = product.CountryId;
            this.CountryName =
                availableCountries.First(c => c.CountryId == product.CountryId).CountryName;
            this.NATOSize = product.NATOSize;
            this.ProductId = product.ProductId;
            this.Size = product.Size;
            this.StatusId = product.StatusId;
            this.StatusName =
                availablestatuses.First(s => s.StatusId == product.StatusId).StatusText;
            this.SubTypeId = product.SubTypeId;
            this.SubTypeName =
                availablesubtypes.First(s => s.SubTypeId == product.SubTypeId).SubTypeName;
            this.TradePrice = product.TradePrice;
            this.TypeId = product.TypeId;
            this.TypeName = availableTypes.First(t => t.TypeId == product.TypeId).TypeName;
            this.availableCountries = availableCountries;
            this.availableTypes = availableTypes;
            this.availablestatuses = availablestatuses;
            this.availablesubtypes = availablesubtypes;
            this.availableclients = availableclients;
            this.Price = product.Price;
            this.SoldPrice = product.SoldPrice;
        }
    }
}