
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;

namespace Procurement.Service.Entities
{
    public class ServiceAttributeValue: ServiceProcurement<AttributeValue>, IServiceAttributeValue
    {
        public ServiceAttributeValue(IRepositoryAttributeValue repositoryAttributeValue) : base(repositoryAttributeValue)
        {

        }
 


    }
}
