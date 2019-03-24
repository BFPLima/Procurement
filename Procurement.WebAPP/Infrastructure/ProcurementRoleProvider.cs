using Procurement.Model.Users;
using Procurement.Service.Interface.Users;
using Procurement.Service.Users;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Procurement.WebAPP.Infrastructure
{
    public class ProcurementRoleProvider : RoleProvider
    {
        protected IServiceUser serviceUser;
        protected IUnitOfWorkAsync unitOfWorkAsync;
        protected IServiceRole serviceRole;

        public ProcurementRoleProvider() { }


        public ProcurementRoleProvider(IUnitOfWorkAsync unitOfWorkAsync,
                                       IServiceRole serviceRole,
                                       IServiceUser serviceUser)
        {
            this.unitOfWorkAsync = unitOfWorkAsync;
            this.serviceRole = serviceRole;
            this.serviceUser = serviceUser;
        }


        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string email)
        {
           
            IServiceRole serviceRole = DependencyResolver.Current.GetService<ServiceRole>();
            IServiceUser serviceUser = DependencyResolver.Current.GetService<ServiceUser>();

            User user = serviceUser.GetByEmail(email);

            string[] roles = user.Roles.Select(r=> r.Name).ToArray();

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}