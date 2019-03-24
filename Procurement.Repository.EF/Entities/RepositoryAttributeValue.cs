using Procurement.Model.Users;
using Procurement.Repository.Interface.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Procurement.Model.Entities;
using Procurement.Repository.Interface.Entities;

namespace Procurement.Repository.EF.Entities
{
    public class RepositoryAttributeValue: RepositoryProcurement<AttributeValue>, IRepositoryAttributeValue
    {
        public RepositoryAttributeValue(IDataContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {
           
        }
    }
}
