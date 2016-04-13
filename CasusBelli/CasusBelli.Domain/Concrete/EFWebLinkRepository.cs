using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFWebLinkRepository : IWebLinkRepository, ICacheableRepository
    {
        public EFWebLinkRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFWebLinkRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<WebLink> WebLink
        {
            get
            {
                List<WebLink> linksData = Cache.Get("weblinks") as List<WebLink>;
                if (linksData == null)
                {
                    linksData = context.WebLink.SqlQuery("SELECT * FROM WebLink").ToList();
                    if (linksData.Any())
                    {
                        Cache.Set("weblinks", linksData, 99999);
                    }
                }
                return linksData;
            }
        }

        public void AddOrUpdateWebLink(WebLink link)
        {
            WebLink newlink = new WebLink();
            newlink.Id = link.Id;
            newlink.AdditionalInfo = link.AdditionalInfo;
            newlink.Name = link.Name;
            newlink.URL = link.URL;
            newlink.Login = link.Login;
            newlink.Password = link.Password;
            context.WebLink.AddOrUpdate(p => p.Id, newlink);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteWebLink(WebLink link)
        {
            context.WebLink.Remove(link);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("weblinks");
        }
    }
}
