using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class ProductsViewModel:Product
    {
        private ITypeRepository type;
        private ISubTypeRepository subtype;
        private ICountryRepository country;

        [HiddenInput(DisplayValue = false)]
        public List<ProductType> AvailableTypes { get; set; }
        [HiddenInput(DisplayValue = false)]
        public List<ProductSubType> AvailableSubTypes { get; set; }
        [HiddenInput(DisplayValue = false)]
        public List<Country> AvailableCountries { get; set; }

        public string typeName { get; set; }
        public string subTypeName { get; set; }
        public string countryName { get; set; }
        public string Status { get; set; }

        public ProductsViewModel()
        {
            
        }

        public ProductsViewModel(ITypeRepository typeRep, ISubTypeRepository subTypeRep, ICountryRepository countryRep)
        {
            type = typeRep;
            AvailableTypes = typeRep.Types.ToList();
            subtype = subTypeRep;
            AvailableSubTypes = subTypeRep.ProductSubTypes.ToList();
            country = countryRep;
            AvailableCountries = country.Countries.ToList();
        }

        public ProductsViewModel(Product prod, List<ProductType> availableTypes, List<ProductSubType> availableSubTypes, List<Country> availableCountries)
        {
            ProductId = prod.ProductId;
            AdditionalInfo = prod.AdditionalInfo;
            Condition = prod.Condition;
            Count = prod.Count;
            CountryId = prod.CountryId;
            NATOSize = prod.NATOSize;
            Price = prod.Price;
            Size = prod.Size;
            SoldCount = prod.SoldCount;
            SoldPrice = prod.SoldPrice;
            TradePrice = prod.TradePrice;
            StatusId = prod.StatusId == 0 ? 1 : prod.StatusId;
            SubTypeId = prod.SubTypeId;
            TypeId = prod.TypeId;

            typeName = availableTypes.First(p => p.TypeId == prod.TypeId).TypeName;
            subTypeName = availableSubTypes.First(p => p.SubTypeId == prod.SubTypeId).SubTypeName;
            countryName = availableCountries.First(p => p.CountryId == prod.CountryId).CountryName;
        }
    }
}