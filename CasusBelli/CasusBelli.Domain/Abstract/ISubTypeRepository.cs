using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ISubTypeRepository
    {
        IQueryable<ProductSubType> ProductSubTypes { get; }

        void AddOrUpdateSubType(ProductSubType productSubType);

        void DeleteSubType(ProductSubType productSubType);
    }
}
