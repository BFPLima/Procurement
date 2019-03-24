
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Procurement.Service.Entities
{
    public class ServiceCustumerOrder : ServiceProcurement<CustumerOrder>, IServiceCustumerOrder
    {
        public ServiceCustumerOrder(IRepositoryCustumerOrder iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public List<CustumerOrder> GetByCustumerInfo(CustumerInfo custumerInfo)
        {
            return this.Queryable().Where(o => o.CustumerInfo.ID == custumerInfo.ID).ToList();
        }

       
    }
}
