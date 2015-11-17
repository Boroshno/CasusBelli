using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFProductTypeRepository:ITypeRepository
    {
        public EFProductTypeRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFProductTypeRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<ProductType> Types
        {
            get
            {
                List<ProductType> typesData = Cache.Get("types") as List<ProductType>;
                if (typesData == null)
                {
                    typesData = context.Types.ToList();
                    if (typesData.Any())
                    {
                        Cache.Set("types", typesData, 99999);
                    }
                }
                return typesData;
            }
        }

        public void AddOrUpdateType(ProductType productType)
        {
            context.Types.AddOrUpdate(p => p.TypeId, productType);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteType(ProductType productType)
        {
            context.Types.Remove(productType);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("clients");
        }
    }
}
