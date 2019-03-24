using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Users
{
    public abstract class UserInfo : ProcurementObject
    {
        public UserInfo() : base() { }

        public virtual User User { get; set; }
    }
}
