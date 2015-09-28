using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Models
{
    public class TransactionsViewModel:Transaction
    {
        private IClientRepository client;
        [HiddenInput(DisplayValue = false)]
        public List<Client> availableClients { get; set; }
        public string clientName { get; set; }
        public TransactionsViewModel()
        {
            
        }

        public TransactionsViewModel(IClientRepository clientRep)
        {
            client = clientRep;
            availableClients = clientRep.Clients.ToList();
        }

        public TransactionsViewModel(Transaction tran, List<Client> AvailClients )
        {
            availableClients = AvailClients;
            TransactionId = tran.TransactionId;
            Text = tran.Text;
            WasMoney = tran.WasMoney;
            Currency = tran.Currency;
            BecameMoney = tran.BecameMoney;
            Date = tran.Date;
            ClientId = tran.ClientId;

            if (ClientId != null)
            {
                Client myclient = availableClients.First(x => x.ClientId == ClientId);
                clientName = myclient.Name + ", " + myclient.City;
            }
        }
    }
}