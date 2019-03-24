using Procurement.Model;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Procurement.Repository.Interface;

namespace Procurement.Repository.EF
{
    public abstract class RepositoryProcurement<T> : Repository<T>,
                                                     IRepositoryProcurement<T>
                                                     where T : ProcurementObject
    {
        public RepositoryProcurement(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) 
            : base(context, unitOfWork)
        {

        }
         
    }
}
