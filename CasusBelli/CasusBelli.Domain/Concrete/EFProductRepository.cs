using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products { get { return context.Products; } }

        public void AddOrUpdateProduct(Product prod)
        {
            Product efprod = new Product
            {
                ProductId = prod.ProductId,
                AdditionalInfo = prod.AdditionalInfo,
                Condition = prod.Condition,
                Count = prod.Count,
                CountryId = prod.CountryId,
                NATOSize = prod.NATOSize,
                Price = prod.Price,
                Size = prod.Size,
                SoldCount = prod.SoldCount,
                SoldPrice = prod.SoldPrice,
                TradePrice = prod.TradePrice,
                StatusId = prod.StatusId == 0 ? 1 : prod.StatusId,
                SubTypeId = prod.SubTypeId,
                TypeId = prod.TypeId
            };
            context.Products.AddOrUpdate(p => p.ProductId, efprod);
            context.SaveChanges();
        }

        public void DeleteProduct(Product prod)
        {
            context.Products.Remove(prod);
            context.SaveChanges();
        }
    }
}
