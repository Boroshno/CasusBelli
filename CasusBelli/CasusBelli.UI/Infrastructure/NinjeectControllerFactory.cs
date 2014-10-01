using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Infrastructure.Concrete;
using Moq;
using Ninject;

namespace CasusBelli.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernal;

        public NinjectControllerFactory()
        {
            ninjectKernal = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernal.Get(controllerType);
        }

        private void AddBindings()
        {
            Mock<ITypeRepository> mock = new Mock<ITypeRepository>();
            mock.Setup(m => m.Types).Returns((new List<ProductType>
            {
                new ProductType {TypeName = "Sleeping Bag", TypeText = "Bag to sleep"},
                new ProductType {TypeName = "Sleeping Bag", TypeText = "Bag to sleep"}
            }).AsQueryable());


            ninjectKernal.Bind<ITypeRepository>().To<EFProductTypeRepository>();
            ninjectKernal.Bind<ISubTypeRepository>().To<EFProductSubTypeRepository>();
            ninjectKernal.Bind<ICountryRepository>().To<EFCountryRepository>();
            ninjectKernal.Bind<IAuthProvider>().To<FormsAuthProvider>();
            EMailSettings eMailSettings = new EMailSettings();
            ninjectKernal.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("mailSettingsParam", eMailSettings);
        }
    }
}