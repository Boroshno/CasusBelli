using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        void AddOrUpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
