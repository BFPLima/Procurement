using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Users
{
    public interface IServiceRole : IServiceProcurement<Role>
    {
        Role ByName(string name);

        void AddUserOnRole(Role role, User user);
        void AddUserOnRole(string roleName, User user);
    }
}
