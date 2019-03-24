using Procurement.Model.Entities;
using Procurement.Model.Users;
using Procurement.Service.Interface.Entities;
using Procurement.Service.Interface.Users;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using PagedList;
using System.Net;
using Repository.Pattern.Infrastructure;

namespace Procurement.WebAPP.Controllers
{
    [Authorize(Roles = "Supplier")]
    public class SupplierController : Controller
    {

        protected IUnitOfWorkAsync unitOfWorkAsync;
        protected IServiceUser serviceUser;
        protected IServiceSupplierInfo serviceSupplierInfo;
        protected IServiceSupplierOffer serviceSupplierOffer;
        protected IServiceItemType serviceItemType;
        protected IServiceTemplateItem serviceTemplateItem;
        protected IServiceItem serviceItem;

        public SupplierController(IUnitOfWorkAsync unitOfWorkAsync,
                                  IServiceUser serviceUser,
                                  IServiceSupplierInfo serviceSupplierInfo,
                                  IServiceSupplierOffer serviceSupplierOffer,
                                  IServiceItemType serviceItemType,
                                  IServiceTemplateItem serviceTemplateItem,
                                  IServiceItem serviceItem)
        {
            this.unitOfWorkAsync = unitOfWorkAsync;
            this.serviceUser = serviceUser;
            this.serviceSupplierInfo = serviceSupplierInfo;
            this.serviceSupplierOffer = serviceSupplierOffer;
            this.serviceItemType = serviceItemType;
            this.serviceTemplateItem = serviceTemplateItem;
            this.serviceItem = serviceItem;
        }


        // GET: Supplier
        public ActionResult Dashboard(int id)
        {

            return View();
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


        public async Task<ActionResult> Relates(int id, int? page, int? idItemType, int? pageSize, FormCollection collection)
        {
            var listItemTypes = await this.serviceItemType.Queryable().ToListAsync();


            if (!pageSize.HasValue)
            {
                pageSize = 100;
            }

            //http://stackoverflow.com/questions/8107439/why-is-contains-slow-most-efficient-way-to-get-multiple-entities-by-primary-ke

            var listTotalIens = this.serviceItem.Queryable().ToList();


            this.ViewBag.SelectListPageSize = BuildSelectListPageSize(pageSize.Value);

            this.ViewBag.SupplierID = id;
            this.ViewBag.ItemTypeID = null;
            this.ViewBag.PageSize = pageSize;

            int pageNumber = (page ?? 1);

            List<TemplateItem> listTemplateItems = null;

            if (this.Request.Form["ddItemTypes"] != null || idItemType != null)
            {
                User user = this.serviceUser.Find(id);
                SupplierInfo SupplierInfo = this.serviceSupplierInfo.GetByUser(user);


                #region relate

                var listItemToRelate = new List<Item>();

                foreach (var item in collection)
                {
                    if (item.ToString().Contains("item_"))
                    {
                        int itemID = Convert.ToInt32(item.ToString().Replace("item_", ""));
                        listItemToRelate.Add(listTotalIens.Where(i => i.ID == itemID).FirstOrDefault());
                    }
                }

                if (listItemToRelate.Count() >= 1)
                {
                    var listOffersToInsert = new List<SupplierOffer>();

                    foreach (var itemToRelate in listItemToRelate)
                    {
                        SupplierOffer offer = new SupplierOffer()
                        {
                            Item = itemToRelate,
                            Price = 0,
                            ProvidedDateDelivery = new DateTime(2017, 07, 26),
                            SupplierInfo = SupplierInfo
                        };
                        listOffersToInsert.Add(offer);
                    }
                    this.serviceSupplierOffer.InsertRange(listOffersToInsert);
                    this.unitOfWorkAsync.SaveChanges();


                }
                #endregion



                int itemTypeID = 0;
                if (this.Request.Form["ddItemTypes"] != null)
                {
                    itemTypeID = Convert.ToInt32(this.Request.Form["ddItemTypes"]);
                }
                else
                {
                    itemTypeID = idItemType.Value;
                }

                this.ViewBag.ItemTypeID = itemTypeID;

                this.ViewBag.IDItemType = itemTypeID;
                ItemType itemType = this.serviceItemType.Find(itemTypeID);

                listTemplateItems = this.serviceTemplateItem.Queryable().Where(o => o.ItemType.ID == itemType.ID).ToList();


                var listHeader = listTemplateItems[0].TemplateAttributeOrdered.Select(o => o.Name).ToList();
                listHeader.Insert(0, "UNSPSC");
                listHeader.Insert(1, "Description");

                var listItems = listTotalIens.Where(o => o.Template.ItemType.ID == itemType.ID).ToList();



                var listOffers = this.serviceSupplierOffer.GetBySupplierInfo(SupplierInfo);
                // var listItemsByOffers = listOffers.Select(o => o.Item).ToList();
                var listItemsByOffers = listOffers.Where(o => o.Item.ItemType.ID == itemType.ID).Select(j => j.Item).ToList();
                var listItensFiltered = listItems.Except(listItemsByOffers);

                int i1 = listItems.Count();
                int i2 = listItensFiltered.Count();

                IEnumerable<Item> listPaged = listItensFiltered.ToPagedList(pageNumber, pageSize.Value);


                this.ViewBag.ListOfListAttributeValues = listPaged;

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



        public async Task<ActionResult> Offers(int id, int? page, int? idItemType, int? pageSize, FormCollection collection)
        {
            var listItemTypes = await this.serviceItemType.Queryable().ToListAsync();

            User user = this.serviceUser.Find(id);
            SupplierInfo SupplierInfo = this.serviceSupplierInfo.GetByUser(user);

            var listOffers = this.serviceSupplierOffer.GetBySupplierInfo(SupplierInfo);

            if (!pageSize.HasValue)
            {
                pageSize = 100;
            }


            var listTotalIens = listOffers.Select(o => o.Item).ToList();


            this.ViewBag.SelectListPageSize = BuildSelectListPageSize(pageSize.Value);

            this.ViewBag.SupplierID = id;
            this.ViewBag.ItemTypeID = null;
            this.ViewBag.PageSize = pageSize;

            int pageNumber = (page ?? 1);

            List<TemplateItem> listTemplateItems = null;

            if (this.Request.Form["ddItemTypes"] != null || idItemType != null)
            {

                #region to remove relate

                var listItemIDToRemoveRelate = new List<int>();
                var listItemSIDsPage = new List<int>();
                var listItemSIDs = listTotalIens.Select(o => o.ID).ToList();
                foreach (var item in collection)
                {
                    if (item.ToString().Contains("item_"))
                    {
                        int itemID = Convert.ToInt32(item.ToString().Replace("item_", ""));
                        listItemSIDsPage.Add(itemID);
                    }
                }


                foreach (var idOffer in listItemSIDs)
                {
                    bool found = false;


                    foreach (var idPage in listItemSIDsPage)
                    {
                        if (idPage == idOffer)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == false && listItemSIDsPage.Count() >= 1)
                    {
                        listItemIDToRemoveRelate.Add(idOffer);
                    }

                }

                if (listItemIDToRemoveRelate.Count() >= 1)
                {
                    foreach (var idItem in listItemIDToRemoveRelate)
                    {
                        var toRemove = listOffers.Where(o => o.Item.ID == idItem).FirstOrDefault();
                        if (toRemove != null)
                        {
                            listOffers.Remove(toRemove);
                            this.serviceSupplierOffer.Delete(toRemove);
                        }

                    }

                    this.unitOfWorkAsync.SaveChanges();
                    listOffers = this.serviceSupplierOffer.GetBySupplierInfo(SupplierInfo);
                    //listTotalIens = listOffers.Select(o => o.Item).ToList();
                }

                #endregion



                int itemTypeID = 0;
                if (this.Request.Form["ddItemTypes"] != null)
                {
                    itemTypeID = Convert.ToInt32(this.Request.Form["ddItemTypes"]);
                }
                else
                {
                    itemTypeID = idItemType.Value;
                }

                this.ViewBag.ItemTypeID = itemTypeID;

                this.ViewBag.IDItemType = itemTypeID;
                ItemType itemType = this.serviceItemType.Find(itemTypeID);

                listTemplateItems = this.serviceTemplateItem.Queryable().Where(o => o.ItemType.ID == itemType.ID).ToList();


                var listHeader = new List<string>();
                listHeader.Add("UNSPSC");
                listHeader.Add("Price");
                listHeader.Add("Provided Date Delivery");
                listHeader.Add("Description");

                // var listItems = listTotalIens.Where(o => o.Template.ItemType.ID == itemType.ID).ToList();

                IEnumerable<SupplierOffer> listPaged = listOffers.ToPagedList(pageNumber, pageSize.Value);

                this.ViewBag.ListOfListAttributeValues = listPaged;

                this.ViewBag.ListHeader = listHeader;

                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name", itemTypeID);

                this.ViewBag.SelectListItemTypes = selectListItemTypes;


                return View(listPaged);



            }
            else
            {
               
                this.ViewBag.ListHeader = new List<string>();
                List<SupplierOffer> listOfListAttributeValues = new List<SupplierOffer>();
                IEnumerable<SupplierOffer> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, 50);


                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name");

                this.ViewBag.SelectListItemTypes = selectListItemTypes;
                return View(listPaged);

            }






        }




        // GET: Supplier
        public ActionResult Index(int id)
        {
            User user = this.serviceUser.Find(id);
            SupplierInfo SupplierInfo = this.serviceSupplierInfo.GetByUser(user);

            List<SupplierOffer> listOffers = this.serviceSupplierOffer.GetBySupplierInfo(SupplierInfo);

            this.ViewBag.ListOffers = listOffers;

            return View();
        }

        // GET: Admin/ItemType/Edit/5
        public async Task<ActionResult> OfferEdit(int? id, int userID, int page, int pageSize, int idItemType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierOffer SupplierOffer = await this.serviceSupplierOffer.FindAsync(id);
            if (SupplierOffer == null)
            {
                return HttpNotFound();
            }

            this.ViewBag.userID = userID;
            this.ViewBag.page = page;
            this.ViewBag.pageSize = pageSize;
            this.ViewBag.idItemType = idItemType;

            return View(SupplierOffer);

        }

        // POST: Admin/ItemType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<ActionResult> OfferEdit([Bind(Include = "ID,Price,ProvidedDateDelivery,Description")] SupplierOffer SupplierOffer)
        public async Task<ActionResult> OfferEdit(int? id, double Price, DateTime ProvidedDateDelivery, string Description,
                                                  int page, int userID, int pageSize, int idItemType)


        {
            SupplierOffer SupplierOfferOrinal = null;
            if (ModelState.IsValid)
            {
                //SupplierOfferOrinal = this.serviceSupplierOffer.Find(SupplierOffer.ID);
                //SupplierOfferOrinal.Price = SupplierOffer.Price;
                //SupplierOfferOrinal.ProvidedDateDelivery = SupplierOffer.ProvidedDateDelivery;
                //SupplierOfferOrinal.Description = SupplierOffer.Description;
                int ID = Convert.ToInt32(Request.Form["ID"].ToString());
                SupplierOfferOrinal = this.serviceSupplierOffer.Find(ID);
                SupplierOfferOrinal.Price = Price;
                SupplierOfferOrinal.ProvidedDateDelivery = ProvidedDateDelivery;
                SupplierOfferOrinal.Description = Description;
                SupplierOfferOrinal.ObjectState = ObjectState.Modified;
                this.serviceSupplierOffer.Update(SupplierOfferOrinal);
                await this.unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Offers", "Supplier", new { id = userID, @page = page, @pageSize = pageSize, @idItemType = idItemType });
            }
            return View(SupplierOfferOrinal);
        }



    }
}