
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;

namespace Procurement.Service.Entities
{
    public class ServiceTemplateItem : ServiceProcurement<TemplateItem>, IServiceTemplateItem
    {
        public ServiceTemplateItem(IRepositoryTemplateItem iRepositoryProcurement) : base(iRepositoryProcurement)
        {

        }
 


    }
}
