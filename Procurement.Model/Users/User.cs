using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Users
{
    public class User : ProcurementObject
    {

        public User() : base()
        {
            this.Roles = new List<Users.Role>();
        }


 

        public virtual string Name { get; set; }

        public virtual string LastName { get; set; }

        // public virtual string FullName { get; set; }

        //[Required(ErrorMessage = "Login is required!", AllowEmptyStrings = false)]
        public virtual string Login { get; set; }

        //[Required(ErrorMessage = "Password is required!", AllowEmptyStrings = false)]
        //[DataType(DataType.Password)]
        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual Enums.UserType UserType { get; set; }

        public virtual Enums.UserLegalPersonality LegalPersonality { get; set; }


        //public virtual ICollection<RoleUser> Roles { get;  set; }
        public virtual List<Role> Roles { get; set; }

        public bool IsInRole(string roleName)
        {
            bool b = false;

            Role role = this.Roles.Where(r => r.Name.Equals(roleName)).FirstOrDefault();

            if (role != null)
            {
                b = true;
            }

            return b;
        }
    }
}
