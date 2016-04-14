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
    public class EFClientRepository:IClientRepository,ICacheableRepository
    {
        public EFClientRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFClientRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }

        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<Client> Clients
        {
            get
            {
                List<Client> clientsData = Cache.Get("clients") as List<Client>;
                if (clientsData == null)
                {
                    clientsData = context.Clients.SqlQuery("SELECT * FROM Clients").ToList();
                    if (clientsData.Any())
                    {
                        Cache.Set("clients", clientsData, 99999);
                    }
                }
                return clientsData;
            }
        }

        public void AddOrUpdateClient(Client client)
        {
            Client newclient = new Client();
            newclient.ClientId = client.ClientId;
            newclient.AdditionalInfo = client.AdditionalInfo;
            newclient.City = client.City;
            newclient.Email = client.Email;
            newclient.NPOffice = client.NPOffice;
            newclient.Name = client.Name;
            newclient.Phone = client.Phone;
            context.Clients.AddOrUpdate(p => p.ClientId, newclient);
            context.SaveChanges();

            ClearCache();
        }

        public void DeleteClient(Client client)
        {
            context.Clients.Remove(context.Clients.First(c => c.ClientId == client.ClientId));
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("clients");
        }
    }
}
