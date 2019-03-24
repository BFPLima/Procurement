using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class AttributeValue: ProcurementObject
    {
        public AttributeValue() : base() { }
        
        public virtual string Value { get; set; }

        public virtual Item Item { get; set; }

        public virtual TemplateAttribute TemplateAttribute { get; set; }

    }
}
