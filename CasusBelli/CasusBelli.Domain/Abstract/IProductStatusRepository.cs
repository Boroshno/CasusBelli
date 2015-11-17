using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface IProductStatusRepository
    {
        IEnumerable<ProductStatus> ProductStatuses { get; }
        void AddOrUpdateProductStatus(ProductStatus ps);

        void DeleteProductStatus(ProductStatus ps);
    }
}
