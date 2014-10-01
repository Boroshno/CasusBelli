using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class SubTypesViewModel:ProductSubType
    {

        private ITypeRepository type;
        private ICountryRepository country;

        [HiddenInput(DisplayValue = false)]
        public List<ProductType> AvailableTypes { get; set; }
        [HiddenInput(DisplayValue = false)]
        public List<Country> AvailableCountries { get; set; }
        [HiddenInput(DisplayValue = false)]
        public String TypeName { get; set; }
        public String CountryName { get; set; }

        public SubTypesViewModel()
        {
            
        }
        public SubTypesViewModel(ITypeRepository typeRep, ICountryRepository countryRep)
        {
            type = typeRep;
            AvailableTypes = typeRep.Types.ToList();
            country = countryRep;
            AvailableCountries = country.Countries.ToList();
        }
        public SubTypesViewModel(ProductSubType productSubTypes, List<ProductType> availableTypes, List<Country> availableCountries)
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
        }

        //public ProductType GetProductType()
        //{
        //    return new ProductType
        //    {
        //        AdditionalInfo = A
        //    }
        //}
    }
}