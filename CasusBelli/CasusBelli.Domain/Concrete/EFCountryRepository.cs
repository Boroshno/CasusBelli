using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFCountryRepository:ICountryRepository, ICacheableRepository
    {
        public EFCountryRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFCountryRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<Country> Countries
        {
            get
            {

                context.Configuration.ProxyCreationEnabled = false;
                List<Country> countriesData = null;// Cache.Get("countries") as List<Country>;
                if (countriesData == null)
                {
                    countriesData = context.Country.SqlQuery("SELECT * FROM Countries").ToList();
                    if (countriesData.Any())
                    {
                        Cache.Set("countries", countriesData, 9999);
                    }
                }
                return countriesData;
            }
        }

        public void AddOrUpdateCountry(Country country)
        {
            context.Country.AddOrUpdate(p=>p.CountryId, country);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteCountry(Country country)
        {
            context.Country.Remove(country);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("countries");
        }
    }
}
