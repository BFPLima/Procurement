
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Procurement.Service.Entities
{
    public class ServiceCustumerOrderItem : ServiceProcurement<CustumerOrderItem>, IServiceCustumerOrderItem
    {
        public ServiceCustumerOrderItem(IRepositoryCustumerOrderItem iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public List<CustumerOrderItem> GetByCustumerInfo(CustumerInfo custumerInfo)
        {

            return this.Queryable().Where(o => o.CustumerOrder.CustumerInfo.ID == custumerInfo.ID).ToList();
        }

        public bool HasRelation(Item item)
        {
            var list = this.Queryable().Where(o => o.Item.ID == item.ID).ToList();
            return list.Count() >= 1;
        }

    }
}
