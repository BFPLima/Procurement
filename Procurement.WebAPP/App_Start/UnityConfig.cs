using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.DataContext;
using Procurement.Repository.Interface.Users;
using Procurement.Service.Interface.Users;
using Procurement.Service.Users;
using Procurement.Service.Entities;
using Procurement.Repository.Interface.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.EF.Entities;
using Procurement.Repository.EF.Users;

namespace Procurement.WebAPP.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
           

            container.RegisterType<IUnitOfWorkAsync, Repository.EF.UnitOfWorkProcurement>(new PerRequestLifetimeManager());
            container.RegisterType<IDataContextAsync, Repository.EF.ProcurementContext>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceUser, ServiceUser>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryUser, Repository.EF.Users.RepositoryUser>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceRole, ServiceRole>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryRole, Repository.EF.Users.RepositoryRole>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceItemType, ServiceItemType>(new PerRequestLifetimeManager());
         
            container.RegisterType<IRepositoryItemType, RepositoryItemType>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceTemplateItem, ServiceTemplateItem>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryTemplateItem, RepositoryTemplateItem>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceTemplateAttribute, ServiceTemplateAttribute>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryTemplateAttribute, RepositoryTemplateAttribute>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceItem, ServiceItem>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryItem, RepositoryItem>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceAttributeValue, ServiceAttributeValue>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryAttributeValue, RepositoryAttributeValue>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceSupplierInfo, ServiceSupplierInfo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositorySupplierInfo, RepositorySupplierInfo>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceCustumerInfo, ServiceCustumerInfo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryCustumerInfo, RepositoryCustumerInfo>(new PerRequestLifetimeManager());


            container.RegisterType<IServiceAdminInfo, ServiceAdminInfo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryAdminInfo, RepositoryAdminInfo>(new PerRequestLifetimeManager());

            container.RegisterType<IServiceSupplierOffer, ServiceSupplierOffer>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositorySupplierOffer, RepositorySupplierOffer>(new PerRequestLifetimeManager());


            container.RegisterType<IServiceCustumerOrder, ServiceCustumerOrder>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryCustumerOrder, RepositoryCustumerOrder>(new PerRequestLifetimeManager());


            container.RegisterType<IServiceCustumerOrderItem, ServiceCustumerOrderItem>(new PerRequestLifetimeManager());
            container.RegisterType<IRepositoryCustumerOrderItem, RepositoryCustumerOrderItem>(new PerRequestLifetimeManager());

        }
    }
}
