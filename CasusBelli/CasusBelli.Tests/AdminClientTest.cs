using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Areas.Admin.Controllers;
using CasusBelli.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CasusBelli.Tests
{
    [TestClass]
    public class AdminClientTest
    {
        private Mock<IClientRepository> mockClient;

        [TestInitialize]
        public void SetUp()
        {
            mockClient = new Mock<IClientRepository>();
            mockClient.Setup((m => m.Clients)).Returns(new Client[]
            {
                new Client() {ClientId = 1, Name = "Vasya", City = "Kyiv"},
                new Client() {ClientId = 2, Name = "Vanya", City = "Kharkiv"}
            }.AsQueryable());
        }

        [TestMethod]
        public void CanShowAllClients()
        {
            //Arrange
            ClientsController clientC = new ClientsController(mockClient.Object);

            //Action
            List<ClientsViewModel> clients = ((IEnumerable<ClientsViewModel>)clientC.Index().ViewData.Model).ToList();

            //Assert
            Assert.AreEqual(clients.Count, 2);
            Assert.AreEqual(clients.First().ClientId, 1);
        }

        [TestMethod]
        public void CanDeleteClient()
        {
            //Arrange
            Client client = new Client() { ClientId = 3, Name = "Vasya", City = "Kyiv" };
            mockClient = new Mock<IClientRepository>();
            mockClient.Setup((m => m.Clients)).Returns(new Client[]
            {
                new Client() {ClientId = 1, Name = "Vasya", City = "Kyiv"},
                new Client() {ClientId = 2, Name = "Vanya", City = "Kharkiv"},
                client
            }.AsQueryable());
            ClientsController clientC = new ClientsController(mockClient.Object);

            //Action
            clientC.DeleteClient(client.ClientId);

            //Assert
            mockClient.Verify(m => m.DeleteClient(It.IsAny<Client>()), Times.Once);
            mockClient.Verify(m => m.DeleteClient(It.Is<Client>(c=>c.ClientId == 3)), Times.Once);
        }

        [TestMethod]
        public void CanCreateClient()
        {
            //Arrange
            Client client = new Client() { Name = "Vasya", City = "Kyiv" };
            ClientsController clientC = new ClientsController(mockClient.Object);

            //Action
            clientC.CreateClient(client.Name, client.Phone, client.Email, client.City, client.NPOffice.ToString(), "");

            //Assert
            mockClient.Verify(m => m.AddOrUpdateClient(It.IsAny<Client>()), Times.Once);
            mockClient.Verify(m => m.AddOrUpdateClient(It.Is<Client>(c=>c.Name == "Vasya")), Times.Once);
        }

        [TestMethod]
        public void CanEditClient()
        {
            //Arrange
            ClientsViewModel client = new ClientsViewModel() { Name = "Vasya", City = "Kyiv", ClientId = 1 };
            ClientsController clientC1 = new ClientsController(mockClient.Object);
            ClientsController clientC2 = new ClientsController(mockClient.Object);

            //Action
            clientC1.EditClient(client);
            clientC2.ModelState.AddModelError("error", new ValidationException());
            clientC2.EditClient(client);

            //Assert
            mockClient.Verify(c=>c.AddOrUpdateClient(It.IsAny<Client>()), Times.Once);
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a=>a.ClientId == client.ClientId)));
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a => a.NPOffice == client.NPOffice)));
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a => a.AdditionalInfo == client.AdditionalInfo)), Times.Once);
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a => a.City == client.City)), Times.Once);
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a => a.Email == client.Email)), Times.Once);
            mockClient.Verify(c => c.AddOrUpdateClient(It.Is<Client>(a => a.Name == client.Name)), Times.Once);
        }
    }
}
