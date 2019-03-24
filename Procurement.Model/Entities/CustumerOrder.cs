using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
   public  class CustumerOrder : ProcurementObject
    {
        public CustumerOrder() : base()
        {
            this.CustumerOrderItems = new List<CustumerOrderItem>();
        }
          
        public virtual CustumerInfo CustumerInfo { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual List<CustumerOrderItem> CustumerOrderItems { get; set; }

    }
}
