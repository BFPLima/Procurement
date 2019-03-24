using Procurement.Repository.Interface;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;

namespace Procurement.Repository.EF
{
    public class UnitOfWorkProcurement : UnitOfWork, IUnitOfWorkProcurement
    {
        public UnitOfWorkProcurement(IDataContextAsync dataContext) : base(dataContext)
        {
         
        }
    }
}
