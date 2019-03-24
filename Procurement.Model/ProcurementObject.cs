using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model
{
    public abstract class ProcurementObject : Entity
    {
        public ProcurementObject()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual int ID { get; set; }

        public virtual DateTime CreatedDate { get; set; }

    }
}
