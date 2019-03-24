
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Procurement.Service.Entities
{
    public class ServiceSupplierOffer : ServiceProcurement<SupplierOffer>, IServiceSupplierOffer
    {
        public ServiceSupplierOffer(IRepositorySupplierOffer iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public List<SupplierOffer> GetByItem(Item item)
        {
            return this.Queryable().Where(o => o.Item.ID == item.ID).ToList();
        }

        public List<SupplierOffer> GetBySupplierInfo(SupplierInfo SupplierInfo)
        {
            return this.Queryable().Where(o => o.SupplierInfo.ID == SupplierInfo.ID).ToList();
        }

        public bool HasRelation(Item item)
        {
            var list = this.Queryable().Where(o => o.Item.ID == item.ID).ToList();
            return list.Count() >= 1;
        }
    }
}
