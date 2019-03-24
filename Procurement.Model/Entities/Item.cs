using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Entities
{
    public class Item : ProcurementObject
    {
        public Item() : base()
        {
            this.AttributeValues = new List<Entities.AttributeValue>();
        }

        public virtual TemplateItem Template { get; set; }

        public virtual List<AttributeValue> AttributeValues { get; set; }

        public virtual string UNSPSC { get; set; }

        public virtual string Description { get; set; }

        [NotMapped]
        public virtual ItemType ItemType { get { return this.Template.ItemType; } }

        [NotMapped]
        public virtual List<TemplateAttribute> TemplateAttributes { get { return this.Template.TemplateAttributeOrdered; } }

        [NotMapped]
        public virtual IDictionary<string, string> AttributesAndValue
        {
            get
            {
                IDictionary<string, string> dic = new Dictionary<string, string>();

                foreach (TemplateAttribute template in this.TemplateAttributes)
                {
                    AttributeValue value = this.AttributeValues.Find(o => o.TemplateAttribute.ID == template.ID);
                    dic.Add(template.Name, value != null ? value.Value : string.Empty);
                }

                return dic;
            }
        }






    }
}
