using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Areas.Admin.Controllers;
using CasusBelli.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CasusBelli.Tests
{
    [TestClass]
    public class AdminWebLinkTest
    {
        private Mock<IWebLinkRepository> mockLink;

        [TestInitialize]
        public void SetUp()
        {
            mockLink = new Mock<IWebLinkRepository>();
            mockLink.Setup((m => m.WebLink)).Returns(new WebLink[]
            {
                new WebLink() {Id = 1, Name = "Vasya"},
                new WebLink() {Id = 2, Name = "Vanya"}
            });
        }
    }
}
