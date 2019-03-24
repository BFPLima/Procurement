using Procurement.Model;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using Procurement.Repository.Interface;

namespace Procurement.Service
{
    public abstract class ServiceProcurement<T> : Service<T> where T : ProcurementObject
    {

        protected IRepositoryProcurement<T> iRepositoryProcurement;

        public ServiceProcurement(IRepositoryProcurement<T> iRepositoryProcurement) : base(iRepositoryProcurement)
        {
            this.iRepositoryProcurement = iRepositoryProcurement;
        }
    }


}
