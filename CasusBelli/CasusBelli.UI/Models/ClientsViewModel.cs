using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class ClientsViewModel:Client
    {
        public ClientsViewModel()
        {
            
        }

        public ClientsViewModel(Client client)
        {
            ClientId = client.ClientId;
            Name = client.Name;
            Phone = client.Phone;
            Email = client.Email;
            City = client.City;
            NPOffice = client.NPOffice;
            AdditionalInfo = client.AdditionalInfo;
        }
    }
}