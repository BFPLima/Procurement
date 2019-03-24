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
    public class RepositoryRole : RepositoryProcurement<Role>, IRepositoryRole
    {
        public RepositoryRole(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {
           
        }
    }
}
