using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procurement.Repository.Interface;
using Procurement.Repository.Interface.Users;
using Procurement.Service.Interface.Users;

namespace Procurement.Service.Users
{
    public class ServiceRole : ServiceProcurement<Role>, IServiceRole
    {
        public ServiceRole(IRepositoryRole iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public void AddUserOnRole(Role role, User user)
        {
            role.Users.Add(user);
            this.Update(role);
        }

        public void AddUserOnRole(string roleName, User user)
        {
            Role role = ByName(roleName);
            if (role == null)
                return;

            role.Users.Add(user);
            this.Update(role);
        }

        public Role ByName(string name)
        {
            Role role = this.Queryable().Where(r => r.Name.Equals(name)).FirstOrDefault();
            return role;
        }


    }
}
