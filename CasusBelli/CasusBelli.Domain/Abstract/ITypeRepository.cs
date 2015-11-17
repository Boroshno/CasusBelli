using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ITypeRepository
    {
        IEnumerable<ProductType> Types { get; }

        void AddOrUpdateType(ProductType productType);

        void DeleteType(ProductType productType);
    }
}
