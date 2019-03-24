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
using Procurement.Service.Interface.Users;
using Repository.Pattern.Infrastructure;
using Procurement.Service.Interface.Entities;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{
    public class ItemTypeController : ControllerBaseAdmin
    {      
        protected IServiceItemType serviceItemType;

        public ItemTypeController(IUnitOfWorkAsync unitOfWorkAsync,
                                  IServiceItemType serviceItemType): base(unitOfWorkAsync)
        {
            this.unitOfWorkAsync = unitOfWorkAsync;
            this.serviceItemType = serviceItemType;
        }

        // GET: Admin/ItemType
        public async Task<ActionResult> Index()
        {
            return View(await this.serviceItemType.Queryable().ToListAsync());
        }

        // GET: Admin/ItemType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = await this.serviceItemType.FindAsync(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        //// GET: Admin/ItemType/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}




        // POST: Admin/TemplateAttribute/Delete/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public JsonResult Delete(int itemTypeID)
        {
            try
            {
                this.serviceItemType.Delete(itemTypeID);
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

        // POST: Admin/TemplateAttribute/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(string name, string description)
        {
            try
            {
                ItemType itemType = new ItemType()
                {
                    Name = name,
                    Description = description
                };

                this.serviceItemType.Insert(itemType);

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


        // GET: Admin/ItemType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = await this.serviceItemType.FindAsync(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // POST: Admin/ItemType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,CreatedDate")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                itemType.ObjectState = ObjectState.Modified;
                this.serviceItemType.Update(itemType);
                await this.unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(itemType);
        }

        // GET: Admin/ItemType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = await this.serviceItemType.FindAsync(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
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
