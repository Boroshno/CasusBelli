using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFCountryRepository:ICountryRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Country> Countries { get { return context.Country; } }

        public void AddOrUpdateCountry(Country country)
        {
            context.Country.AddOrUpdate(p=>p.CountryId, country);
            context.SaveChanges();
        }

        public void DeleteCountry(Country country)
        {
            context.Country.Remove(country);
            context.SaveChanges();
        }
    }
}
