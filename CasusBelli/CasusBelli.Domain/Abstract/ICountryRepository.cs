using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ICountryRepository
    {
            IQueryable<Country> Countries { get; }

            void AddOrUpdateCountry(Country country);

            void DeleteCountry(Country country);
        
    }
}
