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
    public class AdminTransactionTest
    {
        private Mock<IClientRepository> mockClient;
        private Mock<ITransactionRepository> mockTransaction;

        [TestInitialize]
        public void SetUp()
        {
            mockClient = new Mock<IClientRepository>();
            mockClient.Setup((m => m.Clients)).Returns(new Client[]
            {
                new Client() {ClientId = 1, Name = "Vasya", City = "Kyiv"},
                new Client() {ClientId = 2, Name = "Vanya", City = "Kharkiv"}
            }.AsQueryable());

            mockTransaction = new Mock<ITransactionRepository>();
            mockTransaction.Setup((m => m.transactions)).Returns(new Transaction[]
            {
                new Transaction() {ClientId = 1, TransactionId = 1, Currency = 20, Text = "Sold"},
                new Transaction() {ClientId = 1, TransactionId = 2, Currency = 20, Text = "Sold"},
            }.AsQueryable());
        }

        [TestMethod]
        public void ShowsAllTransactions()
        {
            //Arrange
            TransactionsController transC = new TransactionsController(mockTransaction.Object, mockClient.Object);

            //Action
            List<TransactionsViewModel> transVM =
                ((IEnumerable<TransactionsViewModel>) transC.Index().ViewData.Model).ToList();

            //Assert
            Assert.AreEqual(transVM.Count(), 2);
            Assert.AreEqual(transVM.First().TransactionId, 1);
            Assert.AreEqual(transVM.Last().TransactionId, 2);
        }

        [TestMethod]
        public void CanCreateTransaction()
        {
            //Arrange
            TransactionsController transC1 = new TransactionsController(mockTransaction.Object, mockClient.Object);

            //Action
            transC1.CreateTransaction("Sold", 1, -1);

            //Assert
            mockTransaction.Verify(m => m.AddTransaction(It.IsAny<Transaction>()), Times.Once);
        }

        [TestMethod]
        public void CanDeleteTransaction()
        {
            //Arrange
            TransactionsController transC1 = new TransactionsController(mockTransaction.Object, mockClient.Object);
            
            //Action
            transC1.DeleteTransaction(2);

            //Assert
            mockTransaction.Verify(m => m.DeleteTransaction(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
