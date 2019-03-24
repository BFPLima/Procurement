
using Procurement.Service.Interface.Users;
using Procurement.Model.Entities;
using Procurement.Repository.Interface.Entities;
using Procurement.Service.Interface.Entities;

namespace Procurement.Service.Entities
{
    public class ServiceItemType : ServiceProcurement<ItemType>, IServiceItemType
    {
        public ServiceItemType(IRepositoryItemType iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }
 


    }
}
