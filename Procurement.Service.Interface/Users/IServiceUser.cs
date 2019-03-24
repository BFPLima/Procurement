using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface.Users
{
    public interface IServiceUser : IServiceProcurement<User>
    {
        bool Validade(string name, string password);

        User GetByEmail(string email);

        bool AlreadyRegistredEmail(string email);
    }
}
