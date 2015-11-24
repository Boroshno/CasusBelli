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
    public class EFProductRepository:IProductRepository, ICacheableRepository
    {
        public EFProductRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFProductRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<Product> Products
        {
            get
            {
                List<Product> productsData = Cache.Get("products") as List<Product>;
                if (productsData == null)
                {
                    productsData = context.Products.SqlQuery("SELECT * FROM Products").ToList(); ;
                    if (productsData.Any())
                    {
                        Cache.Set("products", productsData, 99999);
                    }
                }
                return productsData;
            }
        }

        public void AddOrUpdateProduct(Product prod)
        {
            Product efprod = new Product
            {
                ProductId = prod.ProductId,
                AdditionalInfo = prod.AdditionalInfo,
                Condition = prod.Condition,
                CountryId = prod.CountryId,
                NATOSize = prod.NATOSize,
                Price = prod.Price,
                Size = prod.Size,
                SoldPrice = prod.SoldPrice,
                TradePrice = prod.TradePrice,
                StatusId = prod.StatusId == 0 ? 1 : prod.StatusId,
                SubTypeId = prod.SubTypeId,
                TypeId = prod.TypeId
            };
            context.Products.AddOrUpdate(p => p.ProductId, efprod);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteProduct(Product prod)
        {
            Product deltedProduct = context.Products.First(p => p.ProductId == prod.ProductId);
            context.Products.Remove(deltedProduct);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("products");
        }
    }
}
