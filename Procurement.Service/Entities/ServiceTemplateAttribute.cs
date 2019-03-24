
using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Procurement.Repository.Interface.Entities;

namespace Procurement.Service.Entities
{
    public class ServiceTemplateAttribute: ServiceProcurement<TemplateAttribute>, IServiceTemplateAttribute
    {
        public ServiceTemplateAttribute(IRepositoryTemplateAttribute repositoryTemplateAttribute) : base(repositoryTemplateAttribute)
        {

        }
 


    }
}
