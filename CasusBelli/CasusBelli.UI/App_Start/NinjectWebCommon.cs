using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.UI.Areas.Admin.Infrastructure;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CasusBelli.UI.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CasusBelli.UI.NinjectWebCommon), "Stop")]

namespace CasusBelli.UI
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                //kernel.Bind<EFDbContext>().ToSelf().InRequestScope();

                kernel.Bind<ITypeRepository>().To<EFProductTypeRepository>();
                kernel.Bind<ISubTypeRepository>().To<EFProductSubTypeRepository>();
                kernel.Bind<ICountryRepository>().To<EFCountryRepository>();
                kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
                kernel.Bind<IProductRepository>().To<EFProductRepository>();
                kernel.Bind<IProductStatusRepository>().To<EFProductStatusRepository>();
                kernel.Bind<IClientRepository>().To<EFClientRepository>();
                kernel.Bind<ITransactionRepository>().To<EFTransactionRepository>();
                kernel.Bind<IWebLinkRepository>().To<EFWebLinkRepository>();
                //kernel.Bind<ITaskDateTypeRepository>().To<EFTaskDateTypeRepository>();
                //kernel.Bind<ITaskStatusRepository>().To<EFTaskStatusRepository>();
                //kernel.Bind<ITaskRepository>().To<EFTaskRepository>();
                EMailSettings eMailSettings = new EMailSettings();
                kernel.Bind<IOrderProcessor>()
                    .To<EmailOrderProcessor>()
                    .WithConstructorArgument("mailSettingsParam", eMailSettings);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            
        }        
    }
}
