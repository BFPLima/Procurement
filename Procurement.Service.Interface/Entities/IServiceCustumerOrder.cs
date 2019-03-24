using Procurement.Model.Entities;
using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Entities
{
   

    public interface IServiceCustumerOrder: IServiceProcurement<CustumerOrder>
    {

        List<CustumerOrder> GetByCustumerInfo(CustumerInfo custumerInfo);

   
    }
}
