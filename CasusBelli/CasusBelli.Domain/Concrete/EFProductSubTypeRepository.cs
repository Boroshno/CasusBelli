using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public EFProductSubTypeRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFProductSubTypeRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }
        public IEnumerable<ProductSubType> ProductSubTypes
        {
            get
            {
                List<ProductSubType> subtypesData = null;// Cache.Get("subtypes") as List<ProductSubType>;
                if (subtypesData == null)
                {
                    subtypesData = context.SubTypes.SqlQuery("SELECT * FROM ProductSubTypes").ToList(); ;
                    if (subtypesData.Any())
                    {
                        Cache.Set("subtypes", subtypesData, 99999);
                    }
                }
                return subtypesData;
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

            ClearCache();
        }

        public void DeleteSubType(ProductSubType productSubType)
        {
            context.SubTypes.Remove(productSubType);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("clients");
        }
    }
}
