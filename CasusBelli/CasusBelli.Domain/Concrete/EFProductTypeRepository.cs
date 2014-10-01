using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFProductTypeRepository:ITypeRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<ProductType> Types { get { return context.Types; } }

        public void AddOrUpdateType(ProductType productType)
        {
            context.Types.AddOrUpdate(p => p.TypeId, productType);
            context.SaveChanges();
        }

        public void DeleteType(ProductType productType)
        {
            context.Types.Remove(productType);
            context.SaveChanges();
        }
    }
}
