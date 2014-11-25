using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    class EFProductStatusRepository:IProductStatusRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<ProductStatus> ProductStatuses { get { return context.ProductStatuses; }}

        public void AddOrUpdateProductStatus(ProductStatus ps)
        {
            context.ProductStatuses.AddOrUpdate(p=>p.ProductStatusId, ps);
            context.SaveChanges();
        }

        public void DeleteProductStatus(ProductStatus ps)
        {
            context.ProductStatuses.Remove(ps);
            context.SaveChanges();
        }
    }
}
