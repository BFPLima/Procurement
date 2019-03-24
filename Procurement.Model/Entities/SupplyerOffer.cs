using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class SupplierOffer : ProcurementObject
    {
        public SupplierOffer() : base() { }


        public virtual Item Item { get; set; }
        public virtual SupplierInfo SupplierInfo { get; set; }
        public virtual double Price { get; set; }
        public virtual DateTime ProvidedDateDelivery { get; set; }
        public virtual string Description { get; set; }

    }
}
