using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void AddOrUpdateProduct(Product prod);
        void DeleteProduct(Product prod);
    }
}
