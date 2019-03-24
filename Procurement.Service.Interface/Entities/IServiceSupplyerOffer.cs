using Procurement.Model.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Entities
{
   

    public interface IServiceSupplierOffer: IServiceProcurement<SupplierOffer>
    {

        List<SupplierOffer> GetBySupplierInfo(SupplierInfo SupplierInfo);

        List<SupplierOffer> GetByItem(Item item);

        bool HasRelation(Item item);
    }

}

    
