using Procurement.Model.Entities;
using Procurement.Service.Interface.Entities;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{
    public class TemplateAttributeController : ControllerBaseAdmin
    {

        protected IServiceTemplateItem serviceTemplateItem;
        protected IServiceTemplateAttribute serviceTemplateAttribute;

        public TemplateAttributeController(IUnitOfWorkAsync unitOfWorkAsync,
                                            IServiceTemplateItem serviceTemplateItem,
                                            IServiceTemplateAttribute serviceTemplateAttribute) : base(unitOfWorkAsync)
        {
            this.serviceTemplateItem = serviceTemplateItem;
            this.serviceTemplateAttribute = serviceTemplateAttribute;
        }




        // POST: Admin/TemplateAttribute/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(int templateItemID, string name, string description)
        {


            try
            {
                TemplateItem templateItem = this.serviceTemplateItem.Find(templateItemID);
                int order = templateItem.ModelAttributes.Count() + 1;

                TemplateAttribute templateAttribute = new TemplateAttribute()
                {
                    Name = name,
                    Description = description,
                    Order = order
                };

                this.serviceTemplateAttribute.Insert(templateAttribute);

                templateItem.ModelAttributes.Add(templateAttribute);

                this.serviceTemplateItem.Update(templateItem);


                base.unitOfWorkAsync.SaveChanges();

                var result = new { Success = "True" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var result = new { Success = "False", Message = e.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: Admin/TemplateAttribute/Delete/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public JsonResult Delete(int templateItemID, int templateAttributeID)
        {
            try
            {
                this.serviceTemplateAttribute.Delete(templateAttributeID);
                base.unitOfWorkAsync.SaveChanges();
                var result = new { Success = "True" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var result = new { Success = "True", Message = e.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
