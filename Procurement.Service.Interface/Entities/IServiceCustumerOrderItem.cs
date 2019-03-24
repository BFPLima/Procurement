using Procurement.Model.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Entities
{
   

    public interface IServiceCustumerOrderItem: IServiceProcurement<CustumerOrderItem>
    {

        List<CustumerOrderItem> GetByCustumerInfo(CustumerInfo custumerInfo);

        bool HasRelation(Item item);
    }
}
