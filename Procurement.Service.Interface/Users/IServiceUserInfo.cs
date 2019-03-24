using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Users
{
    public interface IServiceUserInfo<T> : IServiceProcurement<T> where T : UserInfo
    {
        T GetByUser(User user);
    }

    public interface IServiceSupplierInfo : IServiceUserInfo<SupplierInfo>
    {
        
    }

    public interface IServiceCustumerInfo : IServiceUserInfo<CustumerInfo>
    {

    }

    public interface IServiceAdminInfo : IServiceUserInfo<AdminInfo>
    {

    }
}
