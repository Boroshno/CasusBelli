using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CasusBelli.Domain.Abstract;

namespace CasusBelli.Tests
{
    /// <summary>
    /// Summary description for AdminSubTypeTest
    /// </summary>
    [TestClass]
    public class AdminSubTypeTest
    {
        private Mock<ITypeRepository> mockType;
        private Mock<ISubTypeRepository> mockSubType;
        private Mock<ICountryRepository> mockCountry;
        private Mock<IProductRepository> mockProduct;

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
            mockProduct.Setup((m=>m.Products)).Returns(new Product[]
            {
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 1, StatusId = 1, TradePrice = 100},
              new Product() {CountryId = 1, SubTypeId = 1, TypeId = 1, Price = 200, ProductId = 2, StatusId = 1, TradePrice = 100}
            }.AsQueryable());
        }

        [TestMethod]
        public void ShowsAllSubTypes()
        {
            //Arrange
            SubTypesController target = new SubTypesController(mockSubType.Object, mockType.Object, mockCountry.Object, mockProduct.Object );

            // Action
            ProductSubType[] result = ((IEnumerable<ProductSubType>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(1, result[0].SubTypeId);
            Assert.AreEqual("Little", result[1].SubTypeName);
            Assert.AreEqual(1, result[1].TypeId);
        }

        [TestMethod]
        public void CanGetSubTypeById()
        {
            //Arrange
            SubTypesController target = new SubTypesController(mockSubType.Object, mockType.Object, mockCountry.Object, mockProduct.Object);
            SubTypesController target2 = new SubTypesController(mockSubType.Object, mockType.Object, mockCountry.Object, mockProduct.Object);

            //Action
            ProductSubType st1 = target.EditSubType(1).ViewData.Model as ProductSubType;
            ProductSubType st2 = target.EditSubType(2).ViewData.Model as ProductSubType;

            try
            {
                ProductSubType st3 = target2.EditSubType(88).ViewData.Model as ProductSubType;
            }
            catch (HttpException ex)
            {
                Assert.AreEqual(ex.GetHttpCode(), 404);
            }

            //Assert
            Assert.AreEqual(st1.SubTypeName, "Big");
            Assert.AreEqual(st1.SubTypeId, 1);
            Assert.AreEqual(st2.SubTypeId, 2);
        }
    }
}
