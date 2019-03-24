using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Users
{
    public class Role : ProcurementObject
    {

        public Role()
        {
            this.Users = new List<User>();          
        }


    
        public virtual string Name { get; set; }


        //  public virtual ICollection<RoleUser> Users { get;   set; }
        public virtual List<User> Users { get; set; }

    }
}
