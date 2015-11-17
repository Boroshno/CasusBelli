using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;
using Newtonsoft.Json;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class ClientsController : Controller
    {
        //
        // GET: /Admin/Clients/

        private IClientRepository clientRepository;

        public ClientsController(IClientRepository clientRep)
        {
            clientRepository = clientRep;
        }

        public ViewResult Index()
        {
            List<ClientsViewModel> clientsVM = new List<ClientsViewModel>();
            var clients = clientRepository.Clients.ToList();
            foreach (Client client in clients)
            {
                clientsVM.Add(new ClientsViewModel(client));
            }

            return View(clientsVM);
        }


        public ActionResult CreateClient(string name, string phone, string email, string city, string NPOffice, string additionalInfo)
        {
            if (ModelState.IsValid)
            {
                ClientsViewModel clientVM = new ClientsViewModel();
                clientVM.ClientId = (clientRepository.Clients.Max(x => (int?)x.ClientId) ?? 0) + 1;
                clientVM.Name = name;
                clientVM.Phone = phone;
                clientVM.Email = email;
                clientVM.City = city;
                int np = String.IsNullOrEmpty(NPOffice) ? -1 : int.Parse(NPOffice);
                clientVM.NPOffice = np;
                clientVM.AdditionalInfo = additionalInfo;
                clientRepository.AddOrUpdateClient(clientVM);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ClientEditPartial()
        {
            return View(new ClientsViewModel());
        }

        public ActionResult DeleteClient(int id)
        {
            Client client = clientRepository.Clients.First(c => c.ClientId == id);
            clientRepository.DeleteClient(client);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditClient(ClientsViewModel ClientsVM)
        {
            if (ModelState.IsValid)
            {
                Client client = clientRepository.Clients.First(c => c.ClientId == ClientsVM.ClientId);
                client.AdditionalInfo = ClientsVM.AdditionalInfo;
                client.City = ClientsVM.City;
                client.Email = ClientsVM.Email;
                client.NPOffice = ClientsVM.NPOffice;
                client.Name = ClientsVM.Name;
                client.Phone = ClientsVM.Phone;
                clientRepository.AddOrUpdateClient(client);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public string GetClientById(int id)
        {
            Client client = clientRepository.Clients.First(c => c.ClientId == id);
            string jsonClient = JsonConvert.SerializeObject(client);
            return jsonClient;
        }
    }
}
