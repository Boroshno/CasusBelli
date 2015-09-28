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
    [TestClass]
    public class AdminTypesTest
    {
        public FileStream _stream;
        public Mock<ITypeRepository> mock;

        [TestInitialize]
        public void SetUp()
        {
            _stream = new FileStream(Path.GetFullPath(@"testfiles\Lenin.jpg"),
                         FileMode.Open);

            mock = new Mock<ITypeRepository>();
            mock.Setup(m => m.Types).Returns(new ProductType[]
            {
                new ProductType() { TypeName = "Boots", TypeId = 1, TypeText = "MyBoots"},
                new ProductType() { TypeName = "Cloth", TypeId = 2, TypeText = "MyCloth"}
            }.AsQueryable());
        }

        [TestMethod]
        public void ShowsAllTypes()
        {
            // Arrange
            TypesController target = new TypesController(mock.Object);

            // Action
            ProductType[] result = ((IEnumerable<ProductType>) target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(1, result[0].TypeId);
            Assert.AreEqual("Cloth", result[1].TypeName);
        }

        [TestMethod]
        public void CanGetTypeById()
        {
            // Arrange
            TypesController target = new TypesController(mock.Object);
            TypesController target2 = new TypesController(mock.Object);


            // Action
            ProductType t1 = target.EditType(1).ViewData.Model as ProductType;
            ProductType t2 = target.EditType(2).ViewData.Model as ProductType;

            try
            {
                ProductType t3 = target2.EditType(88).ViewData.Model as ProductType;
            }
            catch (HttpException ex)
            {
                Assert.AreEqual(ex.GetHttpCode(),404);
            }

            //Assert
            Assert.AreEqual(t1.TypeId, 1);
            Assert.AreEqual(t2.TypeId,2);
        }

        [TestMethod]
        public void CanCreateNewType()
        {
            //Arrange
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns(_stream.Name);
            TypesController target = new TypesController(mock.Object);
            TypesController target2 = new TypesController(mock.Object);
            ProductType type = new ProductType() {TypeName = "Boots", TypeText = "MyBoots"};

            //Action
            target.CreateType(type, file.Object);
            target2.ModelState.AddModelError("error", new ValidationException());
            target2.CreateType(type, file.Object);

            //Assert
            mock.Verify(o=>o.AddOrUpdateType(type), Times.Exactly(1));
        }

        [TestMethod]
        public void CanEditType()
        {
            //Arrange
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns(_stream.Name);
            TypesController target = new TypesController(mock.Object);
            TypesController target2 = new TypesController(mock.Object);
            ProductType type = new ProductType() { TypeName = "Boots", TypeText = "MyBoots" };
            ProductType type2 = new ProductType() { TypeName = "Boots", TypeId = 2, TypeText = "MyBoots" };

            //Action
            target.EditType(type, file.Object);
            target.EditType(type2, file.Object);
            target2.ModelState.AddModelError("error", new ValidationException());
            target2.EditType(type2, file.Object);

            //Assert
            mock.Verify(o => o.AddOrUpdateType(type), Times.Never);
            mock.Verify(o => o.AddOrUpdateType(type2), Times.Once);
        }

        [TestMethod]
        public void CanDeleteType()
        {
            //Arrange
            ProductType type = new ProductType() { TypeName = "Boots", TypeId = 3, TypeText = "MyBoots" };
            Mock<ITypeRepository> mock = new Mock<ITypeRepository>();
            mock.Setup(m => m.Types).Returns(new ProductType[]
            {
                new ProductType() { TypeName = "Boots", TypeId = 1, TypeText = "MyBoots"},
                new ProductType() { TypeName = "Cloth", TypeId = 2, TypeText = "MyCloth"},
                type
            }.AsQueryable());
            TypesController target = new TypesController(mock.Object);

            //Action
            target.DeleteType(type.TypeId);

            //Assert
            mock.Verify(o=>o.DeleteType(type), Times.Once);
        }

        [TestCleanup]
        public void OnCleanup()
        {
            _stream.Close();
        }
    }
}
