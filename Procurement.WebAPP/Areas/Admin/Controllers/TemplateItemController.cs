using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Procurement.Model.Entities;
using Procurement.Repository.EF;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Procurement.Service.Interface.Entities;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{
    public class TemplateItemController : ControllerBaseAdmin
    {
        protected IServiceTemplateItem serviceTemplateItem;
        protected IServiceItemType serviceItemType;
        protected IServiceTemplateAttribute serviceTemplateAttribute;

        public TemplateItemController(IUnitOfWorkAsync unitOfWorkAsync,
                                      IServiceTemplateItem serviceTemplateItem,
                                      IServiceItemType serviceItemType,
                                      IServiceTemplateAttribute serviceTemplateAttribute) : base(unitOfWorkAsync)
        {
            this.serviceTemplateItem = serviceTemplateItem;
            this.serviceItemType = serviceItemType;
            this.serviceTemplateAttribute = serviceTemplateAttribute;
        }

        // GET: Admin/TemplateItem
        public async Task<ActionResult> Index()
        {
            var listItemTypes = await this.serviceItemType.Queryable().ToListAsync();

            SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name");

            this.ViewBag.SelectListItemTypes = selectListItemTypes;

            return View(await this.serviceTemplateItem.Queryable().ToListAsync());
        }

        // GET: Admin/TemplateItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateItem templateItem = await this.serviceTemplateItem.FindAsync(id);
            if (templateItem == null)
            {
                return HttpNotFound();
            }
            List<Procurement.Model.Entities.TemplateAttribute> attributes = templateItem.TemplateAttributeOrdered;




            this.ViewBag.Attributes = attributes;
            return View(templateItem);
        }

        // GET: Admin/TemplateItem/Create
        [HttpPost]
        public ActionResult Create(int itemTypeID, string name, string description)
        {
            try
            {
                ItemType itemType = this.serviceItemType.Find(itemTypeID);

                List<TemplateAttribute> attributes = new List<TemplateAttribute>()
                {
                    new TemplateAttribute() { Name ="UNSPSC", Order = 1 },
                    new TemplateAttribute() { Name="Description", Order = 2}
                };
                serviceTemplateAttribute.InsertRange(attributes);

                TemplateItem templateItem = new TemplateItem()
                {
                    ItemType = itemType,
                    ModelAttributes = attributes,
                    Name = name,
                    Description = description
                };

                this.serviceTemplateItem.Insert(templateItem);

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


        // GET: Admin/TemplateItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateItem templateItem = await this.serviceTemplateItem.FindAsync(id);
            if (templateItem == null)
            {
                return HttpNotFound();
            }
            return View(templateItem);
        }

        // POST: Admin/TemplateItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,CreatedDate")] TemplateItem templateItem)
        {
            if (ModelState.IsValid)
            {
                templateItem.ObjectState = ObjectState.Modified;
                this.serviceTemplateItem.Update(templateItem);
                await base.unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(templateItem);
        }

        [HttpPost]
        // GET: Admin/TemplateItem/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                TemplateItem item = this.serviceTemplateItem.Find(id);
                int count = item.ModelAttributes.Count();
                for (int i = 0; i < count; i++)
                {
                    this.serviceTemplateAttribute.Delete(item.ModelAttributes[0]);
                }                

                this.serviceTemplateItem.Delete(item);
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
