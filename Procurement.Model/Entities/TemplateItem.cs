using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class TemplateItem : ProcurementObject
    {

        public TemplateItem() : base()
        {
            this.ModelAttributes = new List<TemplateAttribute>();
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        [NotMapped]
        public virtual int ItemTypeID { get; set; }
        public virtual ItemType ItemType { get; set; }

        public virtual List<TemplateAttribute> ModelAttributes { get; set; }


        [NotMapped]
        public virtual List<TemplateAttribute> TemplateAttributeOrdered
        {
            get
            {
                return this.ModelAttributes.OrderBy(o => o.Order).ToList();
            }
        }

    }
}
