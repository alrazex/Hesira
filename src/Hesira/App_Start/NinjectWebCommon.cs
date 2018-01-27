using Hesira.Repositories.Nucleus.Implementations;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Implementations;
using Hesira.Repositories.Repositories.Interfaces;
using Hesira.Services.Services.Implementations;
using Hesira.Services.Services.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Hesira.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Hesira.App_Start.NinjectWebCommon), "Stop")]

namespace Hesira.App_Start
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

            #region Nucleus

            kernel.Bind<IDBFactory>().To<DBFactory>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            #endregion

            #region Services

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IAddressService>().To<AddressService>().InRequestScope();
            kernel.Bind<IUserProfileService>().To<UserProfileService>().InRequestScope();

            #endregion

            #region Repositories

            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>().InRequestScope();
            kernel.Bind<IAddressRepository>().To<AddressRepository>().InRequestScope();

            #endregion

        }        
    }
}
