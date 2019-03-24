using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procurement.Repository.Interface;
using Procurement.Repository.Interface.Users;
using Procurement.Service.Interface.Users;

namespace Procurement.Service.Users
{
    public abstract class ServiceUserInfo<T> : ServiceProcurement<T>, IServiceUserInfo<T> where T : UserInfo
    {
        public ServiceUserInfo(IRepositoryUserInfo<T> iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public T GetByUser(User user)
        {
            return this.Queryable().Where(o => o.User.ID == user.ID).FirstOrDefault();
        }
    }


    public  class ServiceSupplierInfo : ServiceUserInfo<SupplierInfo>, IServiceSupplierInfo 
    {
        public ServiceSupplierInfo(IRepositorySupplierInfo iRepositoryProcurement) : base(iRepositoryProcurement)
        {
           
        }

        
    }

    public  class ServiceCustumerInfo: ServiceUserInfo<CustumerInfo>, IServiceCustumerInfo
    {
        public ServiceCustumerInfo(IRepositoryCustumerInfo iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        
    }

    public  class ServiceAdminInfo : ServiceUserInfo<AdminInfo>, IServiceAdminInfo
    {
        public ServiceAdminInfo(IRepositoryAdminInfo iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

       
    }

}
