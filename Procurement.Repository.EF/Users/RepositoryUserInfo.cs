using Procurement.Model.Users;
using Procurement.Repository.Interface.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;

namespace Procurement.Repository.EF.Users
{
    public abstract class RepositoryUserInfo<T> : RepositoryProcurement<T>, IRepositoryUserInfo<T> where T : UserInfo
    {
        public RepositoryUserInfo(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {
           
        }
    }


    public  class RepositorySupplierInfo  : RepositoryUserInfo<SupplierInfo>, IRepositorySupplierInfo  
    {
        public RepositorySupplierInfo(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {

        }
    }


    public class RepositoryCustumerInfo : RepositoryUserInfo<CustumerInfo>, IRepositoryCustumerInfo
    {
        public RepositoryCustumerInfo(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {

        }
    }

    public class RepositoryAdminInfo : RepositoryUserInfo<AdminInfo>, IRepositoryAdminInfo
    {
        public RepositoryAdminInfo(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {

        }
    }

}
