using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<ProductType> Types { get; set; }
        public DbSet<ProductSubType> SubTypes { get; set; }

        public DbSet<Country> Country { get; set; }
    }
}
