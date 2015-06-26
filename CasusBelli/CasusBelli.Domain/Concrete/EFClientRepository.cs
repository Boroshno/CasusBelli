using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFClientRepository:IClientRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Client> Clients { get { return context.Clients; } }

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
        }

        public void DeleteClient(Client client)
        {
            context.Clients.Remove(client);
            context.SaveChanges();
        }
    }
}
