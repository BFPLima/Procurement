using Procurement.Model;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Repository.Interface
{


    public interface IRepositoryProcurement<T> : IRepositoryAsync<T>
        where T : ProcurementObject
    {


    }
}
