using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class TemplateAttribute: ProcurementObject
    {
        public TemplateAttribute() : base() { }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Order { get; set; }
    }
}
