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
    public class EFProductStatusRepository:IProductStatusRepository, ICacheableRepository
    {
        public EFProductStatusRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFProductStatusRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }
        public ICacheProvider Cache { get; set; }
        private EFDbContext context = new EFDbContext();

        public IEnumerable<ProductStatus> ProductStatuses
        {
            get
            {
                List<ProductStatus> productstatusesData = Cache.Get("productsstatuses") as List<ProductStatus>;
                if (productstatusesData == null)
                {
                    productstatusesData = context.ProductStatuses.ToList(); ;
                    if (productstatusesData.Any())
                    {
                        Cache.Set("productstatuses", productstatusesData, 99999);
                    }
                }
                return productstatusesData;
            }
        }

        public void AddOrUpdateProductStatus(ProductStatus ps)
        {
            context.ProductStatuses.AddOrUpdate(p=>p.StatusId, ps);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteProductStatus(ProductStatus ps)
        {
            context.ProductStatuses.Remove(ps);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("productstatuses");
        }
    }
}
