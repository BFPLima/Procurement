
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;

namespace Procurement.Service.Entities
{
    public class ServiceItem: ServiceProcurement<Item>, IServiceItem
    {
        public ServiceItem(IRepositoryItem iRepositoryProcurement) : base(iRepositoryProcurement)
        {
            
        }    


    }
}
