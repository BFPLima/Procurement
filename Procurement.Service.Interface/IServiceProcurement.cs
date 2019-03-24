using Procurement.Model;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Service.Interface
{
    public interface IServiceProcurement<T> : IService<T> where T: ProcurementObject
    {


    }
}
