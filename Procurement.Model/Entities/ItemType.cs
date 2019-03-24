using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class ItemType: ProcurementObject
    {
        public ItemType() : base() { }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
