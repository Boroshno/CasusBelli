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
            context.Products.AddOrUpdate(p=>p.ProductId, prod);
            context.SaveChanges();
        }

        public void DeleteProduct(Product prod)
        {
            context.Products.Remove(prod);
            context.SaveChanges();
        }
    }
}
