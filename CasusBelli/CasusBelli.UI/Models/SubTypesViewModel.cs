using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using Microsoft.Ajax.Utilities;

namespace CasusBelli.UI.Models
{
    public class SubTypesViewModel:ProductSubType
    {

        private ITypeRepository type;
        private ICountryRepository country;
        private IProductRepository product;

        private List<Product> ProductsOfST;
        [HiddenInput(DisplayValue = false)]
        public List<ProductType> AvailableTypes { get; set; }
        [HiddenInput(DisplayValue = false)]
        public List<Country> AvailableCountries { get; set; }
        [HiddenInput(DisplayValue = false)]
        public String TypeName { get; set; }
        public String CountryName { get; set; }

        public bool IsOutOfRange { get; set; }
        public SubTypesViewModel()
        {
            
        }
        public SubTypesViewModel(ITypeRepository typeRep, ICountryRepository countryRep, IProductRepository productRep)
        {
            type = typeRep;
            AvailableTypes = typeRep.Types.ToList();
            country = countryRep;
            AvailableCountries = country.Countries.ToList();
            product = productRep;
            ProductsOfST = product.Products.Where(p=>p.SubTypeId == SubTypeId).ToList();
        }
        public SubTypesViewModel(ProductSubType productSubTypes, List<ProductType> availableTypes, List<Country> availableCountries, List<Product> myProducts)
        {
            AdditionalInfo = productSubTypes.AdditionalInfo;
            AvailableTypes = availableTypes;
            AvailableCountries = availableCountries;
            Photo = productSubTypes.Photo;
            Price = productSubTypes.Price;
            SubTypeId = productSubTypes.SubTypeId;
            SubTypeName = productSubTypes.SubTypeName;
            SubTypeText = productSubTypes.SubTypeText;
            TypeId = productSubTypes.TypeId;
            CountryId = productSubTypes.CountryId;
            TypeName = availableTypes.First(p => p.TypeId == productSubTypes.TypeId).TypeName;
            CountryName = availableCountries.First(p => p.CountryId == productSubTypes.CountryId).CountryName;
            ImageData = productSubTypes.ImageData;
            ImageMimeData = productSubTypes.ImageMimeData;

            IsOutOfRange = !(myProducts.Count > 0);
        }
    }
}