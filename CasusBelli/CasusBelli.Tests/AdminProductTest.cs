using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Areas.Admin.Controllers;
using CasusBelli.UI.Models;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RazorGenerator.Testing;
using ReflectionMagic;

namespace CasusBelli.Tests
{
    [TestClass]
    public class AdminProductTest
    {
        private Mock<ITypeRepository> mockType;
        private Mock<ISubTypeRepository> mockSubType;
        private Mock<ICountryRepository> mockCountry;
        private Mock<IProductRepository> mockProduct;
        private Mock<IProductStatusRepository> mockStatus;
        private Mock<IClientRepository> mockClient;
        private Mock<ITransactionRepository> mockTransaction;

        [TestInitialize]
        public void SetUp()
        {
            mockType = new Mock<ITypeRepository>();
            mockType.Setup(m => m.Types).Returns(new ProductType[]
            {
                new ProductType() { TypeName = "Boots", TypeId = 1, TypeText = "MyBoots"},
                new ProductType() { TypeName = "Cloth", TypeId = 2, TypeText = "MyCloth"}
            }.AsQueryable());

            mockSubType = new Mock<ISubTypeRepository>();
            mockSubType.Setup(m => m.ProductSubTypes).Returns(new ProductSubType[]
            {
                new ProductSubType() { SubTypeName = "Big", TypeId = 1, SubTypeText = "MyBootsBig", SubTypeId = 1, CountryId = 1},
                new ProductSubType() { SubTypeName = "Little", TypeId = 1, SubTypeText = "MyBootsBigLittle", SubTypeId = 2, CountryId = 1}
            }.AsQueryable());

            mockCountry = new Mock<ICountryRepository>();
            mockCountry.Setup(m => m.Countries).Returns(new Country[]
            {
                new Country() {CountryId = 1, CountryName = "Ukraine"},
                new Country() {CountryId = 2, CountryName = "Australia"}
            }.AsQueryable());

            mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup((m => m.Products)).Returns(new Product[]
            {
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 1, StatusId = 1, TradePrice = 100},
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 2, StatusId = 1, TradePrice = 100}
            }.AsQueryable());

            mockStatus = new Mock<IProductStatusRepository>();
            mockStatus.Setup((m => m.ProductStatuses)).Returns(new ProductStatus[]
            {
                new ProductStatus() {StatusId = 1, StatusText = "Sold"},
                new ProductStatus() {StatusId = 2, StatusText = "Blocked"} 
            }.AsQueryable());

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
        public void ShowsAllSubTypes()
        {
            //Arrange
            ProductsController prodC = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);

            //Action
            List<AdminProductsListViewModel> prodVM = ((IEnumerable<AdminProductsListViewModel>)prodC.Index().ViewData.Model).ToList();

            //Assert
            Assert.AreEqual(prodVM.Count(), 2);
            Assert.AreEqual(prodVM.First().ProductId, 1);
        }

        [TestMethod]
        public void CanCreateNewProduct()
        {
            //Arrange
            ProductsController prodC1 = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);
            ProductsController prodC2 = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);
            ProductsViewModel prodVM = new ProductsViewModel() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 22, StatusId = 1, TradePrice = 400, Count = 1};

            //Action
            prodC1.CreateProduct(prodVM);
            prodC2.ModelState.AddModelError("error", new ValidationException());
            prodC2.CreateProduct(prodVM);

            //Assert
            mockProduct.Verify(m=>m.AddOrUpdateProduct(prodVM), Times.Once);
        }

        [TestMethod]
        public void WasTransactionCreatedWithNewProduct()
        {
            //Arrange
            ProductsController prodC1 = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);
            ProductsViewModel prodVM = new ProductsViewModel() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 22, StatusId = 1, TradePrice = 400, Count = 1 };

            //Action
            prodC1.CreateProduct(prodVM);
            Transaction lasttrans = mockTransaction.Object.transactions.OrderByDescending(t => t.Date).First();
            Transaction newtran = new Transaction
            {
                BecameMoney = lasttrans.BecameMoney + (prodVM.TradePrice * prodVM.Count),
                ClientId = -1,
                Currency = prodVM.TradePrice * prodVM.Count,
                Date = DateTime.Now,
                WasMoney = lasttrans.BecameMoney,
                Text = "Закупка " + prodVM.subTypeName
            };

            //Assert
           mockTransaction.Verify(m=>m.AddTransaction(It.IsAny<Transaction>()), Times.Once);
           mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.ClientId == -1)));
           mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.Currency == newtran.Currency)));
           mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.Text.StartsWith("Закупка")))); 
        }

        [TestMethod]
        public void CanDeleteProduct()
        {
            //Arrange
            Product prod = new Product() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 22, StatusId = 1, TradePrice = 400 };
            mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup((m => m.Products)).Returns(new Product[]
            {
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 1, StatusId = 1, TradePrice = 100},
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 2, StatusId = 1, TradePrice = 100},
              prod
            }.AsQueryable());
            ProductsController prodC1 = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);
            
            //Action
            prodC1.DeleteProduct(prod.ProductId);
            
            //Assert
            mockProduct.Verify(m=>m.DeleteProduct(prod), Times.Once);
        }

        [TestMethod]
        public void CanSellOneProduct()
        {
            //Arrange
            Product prod = new Product() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 22, StatusId = 1, TradePrice = 400 };
            mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup((m => m.Products)).Returns(new Product[]
            {
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 1, StatusId = 1, TradePrice = 100},
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 2, StatusId = 1, TradePrice = 100},
              prod
            }.AsQueryable());
            ProductsController prodC = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);

            //Action
            prodC.SellProduct(prod.ProductId, 300, 1, -1);

            //Assert
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.IsAny<Product>()), Times.Once);
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.Is<Product>(p=>p.ProductId == prod.ProductId)));
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.Is<Product>(p => p.StatusId == (int)ProductStatusEnum.Sold)));
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.Is<Product>(p => p.SoldPrice == 300)));
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException), "There are is no enought products to sold them.")]
        public void CanSellALotOfProducts()
        {
            //Arrange
            Product prod1 = new Product() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 22, StatusId = 1, TradePrice = 400 };
            Product prod2 = new Product() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 23, StatusId = 1, TradePrice = 400 };
            Product prod3 = new Product() { CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 300, ProductId = 24, StatusId = 1, TradePrice = 400 };
            mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup((m => m.Products)).Returns(new Product[]
            {
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 1, StatusId = 1, TradePrice = 100},
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 2, StatusId = 1, TradePrice = 100},
              prod1,
              prod2,
              prod3
            }.AsQueryable());
            ProductsController prodC = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);
            ProductsController prodC2 = new ProductsController(mockType.Object, mockSubType.Object, mockCountry.Object, mockProduct.Object, mockStatus.Object, mockClient.Object, mockTransaction.Object);

            //Action
            prodC.SellProduct(prod1.ProductId, 300, 3, -1);
            prodC2.SellProduct(prod1.ProductId, 300, 4, -1);

            //Assert
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.IsAny<Product>()), Times.Exactly(3));
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.Is<Product>(p => p.StatusId == (int)ProductStatusEnum.Sold)));
            mockProduct.Verify(m => m.AddOrUpdateProduct(It.Is<Product>(p => p.SoldPrice == 300)));

            //Assert Transactions
            mockTransaction.Verify(m => m.AddTransaction(It.IsAny<Transaction>()), Times.Once);
            mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.ClientId == -1)));
            mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.Currency == 300)));
            mockTransaction.Verify(m => m.AddTransaction(It.Is<Transaction>(tran => tran.Text.StartsWith("Продано")))); 

        }
    }
}
