using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
   public  class CustumerOrderItem : ProcurementObject
    {
        public CustumerOrderItem() : base() { }

        public virtual Item Item { get; set; }
        public virtual CustumerOrder CustumerOrder { get; set; }
        public virtual DateTime DesiredDateDelivery { get; set; }
        public virtual double Quantity { get; set; }
        public virtual string Description { get; set; }

    }
}
