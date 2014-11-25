using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFProductSubTypeRepository:ISubTypeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<ProductSubType> ProductSubTypes
        {
            get
            {
                return context.SubTypes;
            }
        }

        public void AddOrUpdateSubType(ProductSubType productSubType)
        {
            ProductSubType subtype = new ProductSubType
            {
                AdditionalInfo = productSubType.AdditionalInfo,
                Photo = productSubType.Photo,
                Price = productSubType.Price,
                SubTypeId = productSubType.SubTypeId,
                SubTypeName = productSubType.SubTypeName,
                SubTypeText = productSubType.SubTypeText,
                TypeId = productSubType.TypeId,
                CountryId = productSubType.CountryId,
                ImageData = productSubType.ImageData,
                ImageMimeData = productSubType.ImageMimeData
            };
            context.SubTypes.AddOrUpdate(p=>p.SubTypeId, subtype);
            context.SaveChanges();
        }

        public void DeleteSubType(ProductSubType productSubType)
        {
            context.SubTypes.Remove(productSubType);
            context.SaveChanges();
        }
    }
}
