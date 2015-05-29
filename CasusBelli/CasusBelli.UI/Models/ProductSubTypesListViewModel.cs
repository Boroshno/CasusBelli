using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasusBelli.Domain.Entities;
using CasusBelli.Domain.Abstract;

namespace CasusBelli.UI.Models
{
    public class ProductListViewModel : ProductSubType
    {
        private IProductRepository prod;

        public bool IsOutRange;

        public ProductListViewModel(ProductSubType pst, IList<Product> p)
        {
            IsOutRange = p.Where(s=>s.StatusId==1).ToList().Count > 0 ? false : true;
            this.AdditionalInfo = pst.AdditionalInfo;
            this.CountryId = pst.CountryId;
            this.ImageData = pst.ImageData;
            this.ImageMimeData = pst.ImageMimeData;
            this.Photo = pst.Photo;
            this.Price = pst.Price;
            this.SubTypeId = pst.SubTypeId;
            this.SubTypeName = pst.SubTypeName;
            this.SubTypeText = pst.SubTypeText;
            this.TypeId = pst.TypeId;
        }
    }
}