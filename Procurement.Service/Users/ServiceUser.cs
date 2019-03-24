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
    public class ServiceUser : ServiceProcurement<User>, IServiceUser
    {
        public ServiceUser(IRepositoryUser iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }

        public bool AlreadyRegistredEmail(string email)
        {
            User user = GetByEmail(email);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public User GetByEmail(string email)
        {
            User user = this.Queryable().Where(u => u.Email == email).FirstOrDefault();
            return user;
        }

        public bool Validade(string email, string password)
        {
            bool b = base.iRepositoryProcurement.Queryable().Where(u => u.Email == email && u.Password == password).Count() >= 1;



            return b;
        }
    }
}
