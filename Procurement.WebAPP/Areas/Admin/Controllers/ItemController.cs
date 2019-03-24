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
using Procurement.Service.Interface.Entities;
using Repository.Pattern.UnitOfWork;

using PagedList;
using Procurement.Service.Entities;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{
    public class ItemController : ControllerBaseAdmin
    {
        protected IServiceItemType serviceItemType;
        protected IServiceItem serviceItem;
        protected IServiceTemplateItem serviceTemplateItem;
        protected IServiceTemplateAttribute serviceTemplateAttribute;
        protected IServiceAttributeValue serviceAttributeValue;


        public ItemController(IUnitOfWorkAsync unitOfWorkAsync,
                              IServiceItemType serviceItemType,
                              IServiceItem serviceItem,
                              IServiceTemplateItem serviceTemplateItem,
                              IServiceTemplateAttribute serviceTemplateAttribute,
                              IServiceAttributeValue serviceAttributeValue) : base(unitOfWorkAsync)
        {
            this.serviceItemType = serviceItemType;
            this.serviceItem = serviceItem;
            this.serviceTemplateItem = serviceTemplateItem;
            this.serviceTemplateAttribute = serviceTemplateAttribute;
            this.serviceAttributeValue = serviceAttributeValue;
        }


        private SelectList BuildSelectListPageSize(int pageSize)
        {


            SelectList selectionList = new SelectList(new List<SelectListItem>()
                       {
                             new SelectListItem() { Value = "50" , Text ="50" },
                            new SelectListItem() { Value = "100" , Text ="100" },
                            new SelectListItem() { Value = "200" , Text ="200" },
                       },
                       "Value",
                       "Text",
                       pageSize.ToString());

            return selectionList;

        }
        // GET: Admin/Item
        public async Task<ActionResult> Index(int? page, int? idItemType, int? pageSize)
        {
            var listItemTypes = await this.serviceItemType.Queryable().ToListAsync();


            if (!pageSize.HasValue)
            {
                pageSize = 100;
            }


            this.ViewBag.SelectListPageSize = BuildSelectListPageSize(pageSize.Value);

            int pageNumber = (page ?? 1);

            List<TemplateItem> listTemplateItems = null;

            if (this.Request.Form["ddItemTypes"] != null || idItemType != null)
            {
                int itemTypeID = 0;
                if (this.Request.Form["ddItemTypes"] != null)
                {
                    itemTypeID = Convert.ToInt32(this.Request.Form["ddItemTypes"]);
                }
                else
                {
                    itemTypeID = idItemType.Value;
                }


                this.ViewBag.IDItemType = itemTypeID;
                ItemType itemType = this.serviceItemType.Find(itemTypeID);

                listTemplateItems = this.serviceTemplateItem.Queryable().Where(o => o.ItemType.ID == itemType.ID).ToList();


                var listHeader = listTemplateItems[0].TemplateAttributeOrdered.Select(o => o.Name).ToList();
                listHeader.Insert(0, "UNSPSC");
                listHeader.Insert(1, "Description");

                var listItems = this.serviceItem.Queryable().Where(o => o.Template.ItemType.ID == itemType.ID).ToList();



                //List<List<AttributeValue>> listOfListAttributeValues = new List<List<AttributeValue>>();

                //foreach (var item in listItems)
                //{
                //    listOfListAttributeValues.Add(item.AttributeValues);
                //}

                //IEnumerable<List<AttributeValue>> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, pageSize.Value);

                //this.ViewBag.ListOfListAttributeValues = listPaged;

                //this.ViewBag.ListHeader = listHeader;

                //SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name", itemTypeID);

                //this.ViewBag.SelectListItemTypes = selectListItemTypes;


 

                IEnumerable<Item> listPaged = listItems.ToPagedList(pageNumber, pageSize.Value);


                this.ViewBag.ListHeader = listHeader;

                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name", itemTypeID);

                this.ViewBag.SelectListItemTypes = selectListItemTypes;


                return View(listPaged);
            }
            else
            {
                this.ViewBag.ListHeader = new List<string>();
                List<Item> listOfListAttributeValues = new List<Item>();
                IEnumerable<Item> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, 50);


                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name");

                this.ViewBag.SelectListItemTypes = selectListItemTypes;
                return View(listPaged);

            }






        }

        // GET: Admin/Item/Details/5
        public async Task<ActionResult> Details(int? id, int? page, int? idItemType, int? pageSize)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await this.serviceItem.FindAsync(id.Value);

            if (item == null)
            {
                return HttpNotFound();
            }

            this.ViewBag.page = page;
            this.ViewBag.idItemType = idItemType;
            this.ViewBag.pageSize = pageSize;

            this.ViewBag.FieldAndValues = item.AttributesAndValue;
            return View(item);

        }

        // GET: Admin/Item/Create
        public ActionResult Create()
        {
            var listItemTypes = this.serviceItemType.Queryable().ToList();
            this.ViewBag.SelectListItemTypes = new SelectList(listItemTypes, "ID", "Name");


            return View(new Item());
        }

        //POST: Admin/Item/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection)
        {
            ItemType itemType = this.serviceItemType.Find(Convert.ToInt32(formCollection["ddItemTypes"]));

            TemplateItem templateItem = this.serviceTemplateItem.Find(Convert.ToInt32(formCollection["ddTemplateItems"]));

            Item item = new Item() { Template = templateItem };
            this.serviceItem.Insert(item);
            base.unitOfWorkAsync.SaveChanges();

            List<AttributeValue> values = new List<AttributeValue>();

            foreach (TemplateAttribute templateAttribute in templateItem.TemplateAttributeOrdered)
            {

                string value = string.Empty;

                if (formCollection[templateAttribute.Name] != null)
                {
                    value = formCollection[templateAttribute.Name];
                }

                values.Add(new AttributeValue() { Item = item, TemplateAttribute = templateAttribute, Value = value });
            }
            this.serviceAttributeValue.InsertRange(values);

            base.unitOfWorkAsync.SaveChanges();

            return RedirectToAction("Index", "Item", new { Area = "Admin", idItemType = itemType.ID });

            // return View();
        }


        [HttpPost]
        public JsonResult GetTemplateItems(int itemTypeID)
        {
            var list = this.serviceTemplateItem.Queryable().Where(o => o.ItemType.ID == itemTypeID).ToList();

            List<SelectListItem> templates = new List<SelectListItem>();
            foreach (var item in list)
            {
                templates.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }

            return Json(new SelectList(templates, "Value", "Text"));
        }

        [HttpPost]
        public ActionResult GetHtmlTemplateAttributes(int templateItemID)
        {
            string result = string.Empty;
            TemplateItem templateItem = this.serviceTemplateItem.Find(templateItemID);

            foreach (var item in templateItem.TemplateAttributeOrdered)
            {

                string str = "<div class=\"form-group\">" +
                                "    <label class=\"control-label col-md-2\">" + item.Name + ":</label>" +
                                "   <div class=\"col-md-10\">" +
                                "        <input type = \"text\" id=\"" + item.Name + "\" name=\"" + item.Name + "\"  class=\"form-control dynamic-field\" />" +
                                "   </div>" +
                                "</div>";

                result += str + Environment.NewLine;
            }



            return Content(result);

        }


        //GET: Admin/Item/Edit/5
        public async Task<ActionResult> Edit(int? id, int? page, int? idItemType, int? pageSize)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await this.serviceItem.FindAsync(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            this.ViewBag.page = page;
            this.ViewBag.idItemType = idItemType;
            this.ViewBag.pageSize = pageSize;

            this.ViewBag.FieldAndValues = item.AttributesAndValue;

            return View(item);

        }

        //POST: Admin/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormCollection formCollection, int? page, int? idItemType, int? pageSize)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(item).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //return View(item);


            //////

            Item item = this.serviceItem.Find(Convert.ToInt32(formCollection["ID"].ToString()));

            ItemType itemType = item.ItemType;

            TemplateItem templateItem = item.Template;


            List<AttributeValue> valuesNew = new List<AttributeValue>();

            foreach (TemplateAttribute templateAttribute in templateItem.TemplateAttributeOrdered)
            {
                string value = string.Empty;

                if (formCollection[templateAttribute.Name] != null)
                {
                    value = formCollection[templateAttribute.Name];
                }

                AttributeValue attributeValue = item.AttributeValues.Find(o => o.TemplateAttribute.ID == templateAttribute.ID);

                if (attributeValue != null)
                {
                    attributeValue.Value = value;
                    this.serviceAttributeValue.Update(attributeValue);
                }
                else
                {
                    valuesNew.Add(new AttributeValue() { Item = item, TemplateAttribute = templateAttribute, Value = value });
                }

            }

            if (valuesNew.Count() >= 1)
            {
                this.serviceAttributeValue.InsertRange(valuesNew);
            }

            base.unitOfWorkAsync.SaveChanges();


            //return RedirectToAction("Index", "Item", new { Area = "Admin", idItemType = itemType.ID });

            this.ViewBag.FieldAndValues = item.AttributesAndValue;

            this.ViewBag.page = page;
            this.ViewBag.idItemType = idItemType;
            this.ViewBag.pageSize = pageSize;

            return View(item);
        }

        // GET: Admin/Item/Delete/5
        public async Task<ActionResult> Delete(int? id, int? page, int? idItemType, int? pageSize)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await this.serviceItem.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }


            IServiceCustumerOrderItem serviceCustumerOrderItem = DependencyResolver.Current.GetService<ServiceCustumerOrderItem>();
            IServiceSupplierOffer serviceSupplierOffer = DependencyResolver.Current.GetService<ServiceSupplierOffer>();


            bool hasRelationOffers = serviceSupplierOffer.HasRelation(item); ;
            bool hasRelationOrders = serviceCustumerOrderItem.HasRelation(item); 

            this.ViewBag.hasRelationOffers = hasRelationOffers;
            this.ViewBag.hasRelationOrders = hasRelationOrders;


            this.ViewBag.page = page;
            this.ViewBag.idItemType = idItemType;
            this.ViewBag.pageSize = pageSize;

            return View(item);
        }

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int? page, int? idItemType, int? pageSize)
        {
            Item item = await this.serviceItem.FindAsync(id);


            IServiceCustumerOrderItem serviceCustumerOrderItem = DependencyResolver.Current.GetService<ServiceCustumerOrderItem>();
            IServiceSupplierOffer serviceSupplierOffer = DependencyResolver.Current.GetService<ServiceSupplierOffer>();


            if (!serviceSupplierOffer.HasRelation(item) && !serviceCustumerOrderItem.HasRelation(item))
            {
                var listValuesToDelete = item.AttributeValues;

                int count = item.AttributeValues.Count();
                for (int i = 0; i < count; i++)
                {
                    this.serviceAttributeValue.Delete(item.AttributeValues[0]);
                }

                this.unitOfWorkAsync.SaveChanges();



                this.serviceItem.Delete(item);
                await this.unitOfWorkAsync.SaveChangesAsync();
            }
 
        
            

            return RedirectToAction("Index", new { page = this.ViewBag.page, idItemType = this.ViewBag.idItemType, pageSiz = this.ViewBag.pageSize });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
