using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Repository.Interface.Users
{
    public interface IRepositoryUserInfo<T> : IRepositoryProcurement<T> where T : UserInfo
    {

    }

    public interface IRepositorySupplierInfo : IRepositoryUserInfo<SupplierInfo>
    {

    }

    public interface IRepositoryCustumerInfo : IRepositoryUserInfo<CustumerInfo>
    {

    }

    public interface IRepositoryAdminInfo : IRepositoryUserInfo<AdminInfo>
    {

    }
}
