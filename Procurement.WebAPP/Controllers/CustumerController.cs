using PagedList;
using Procurement.Model.Entities;
using Procurement.Model.Users;
using Procurement.Service.Interface.Entities;
using Procurement.Service.Interface.Users;
using Procurement.WebAPP.Models.Custumer;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Procurement.WebAPP.Controllers
{
    [Authorize(Roles = "Custumer")]
    public class CustumerController : Controller
    {
        protected IUnitOfWorkAsync unitOfWorkAsync;
        protected IServiceUser serviceUser;
        protected IServiceCustumerInfo serviceCustumerInfo;
        protected IServiceCustumerOrder serviceCustumerOrder;
        protected IServiceItemType serviceItemType;
        protected IServiceTemplateItem serviceTemplateItem;
        protected IServiceItem serviceItem;
        protected IServiceCustumerOrderItem serviceCustumerOrderItem;
        protected IServiceSupplierOffer serviceSupplierOffer;

        public CustumerController(IUnitOfWorkAsync unitOfWorkAsync,
                                  IServiceUser serviceUser,
                                  IServiceCustumerInfo serviceCustumerInfo,
                                  IServiceCustumerOrder serviceCustumerOrder,
                                  IServiceItemType serviceItemType,
                                  IServiceTemplateItem serviceTemplateItem,
                                  IServiceItem serviceItem,
                                  IServiceCustumerOrderItem serviceCustumerOrderItem,
                                  IServiceSupplierOffer serviceSupplierOffer)
        {
            this.unitOfWorkAsync = unitOfWorkAsync;
            this.serviceUser = serviceUser;
            this.serviceCustumerInfo = serviceCustumerInfo;
            this.serviceCustumerOrder = serviceCustumerOrder;
            this.serviceItemType = serviceItemType;
            this.serviceTemplateItem = serviceTemplateItem;
            this.serviceItem = serviceItem;
            this.serviceCustumerOrderItem = serviceCustumerOrderItem;
            this.serviceSupplierOffer = serviceSupplierOffer;
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
            //id => CustumerOrder

            var listItemTypes = await this.serviceItemType.Queryable().ToListAsync();


            if (!pageSize.HasValue)
            {
                pageSize = 100;
            }

            //http://stackoverflow.com/questions/8107439/why-is-contains-slow-most-efficient-way-to-get-multiple-entities-by-primary-ke

            var listTotalIens = this.serviceItem.Queryable().ToList();


            this.ViewBag.SelectListPageSize = BuildSelectListPageSize(pageSize.Value);

            this.ViewBag.CustumerID = id;
            this.ViewBag.ItemTypeID = null;
            this.ViewBag.PageSize = pageSize;

            int pageNumber = (page ?? 1);

            List<TemplateItem> listTemplateItems = null;

            if (this.Request.Form["ddItemTypes"] != null || idItemType != null)
            {
                CustumerOrder custumerOrder = this.serviceCustumerOrder.Find(id);

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
                    var listOrdersToInsert = new List<CustumerOrderItem>();

                    foreach (var itemToRelate in listItemToRelate)
                    {
                        CustumerOrderItem orderItem = new CustumerOrderItem()
                        {
                            Item = itemToRelate,
                            DesiredDateDelivery = DateTime.Today,
                            Quantity = 0,
                            CustumerOrder = custumerOrder

                        };
                        listOrdersToInsert.Add(orderItem);
                    }
                    this.serviceCustumerOrderItem.InsertRange(listOrdersToInsert);
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
                listHeader.Insert(0, "Description");

                var listItems = listTotalIens.Where(o => o.Template.ItemType.ID == itemType.ID).ToList();

                custumerOrder = this.serviceCustumerOrder.Find(id);

                var listOrderItem = custumerOrder.CustumerOrderItems;
                // var listItemsByOffers = listOffers.Select(o => o.Item).ToList();
                var listItemsByOrders = listOrderItem.Where(o => o.Item.ItemType.ID == itemType.ID).Select(j => j.Item).ToList();
                var listItensFiltered = listItems.Except(listItemsByOrders);

                int i1 = listItems.Count();
                int i2 = listItensFiltered.Count();



                //List<string[]> listOfListAttributeValues = new List<string[]>();


                //foreach (var item in listItensFiltered)
                //{
                //    string[] values = new string[listHeader.Count + 1];
                //    values[0] = item.ID.ToString();

                //    for (int i = 0; i < item.AttributeValues.Count; i++)
                //    {
                //        values[1 + i] = item.AttributeValues[i].Value;
                //    }


                //    listOfListAttributeValues.Add(values);
                //}


                //IEnumerable<string[]> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, pageSize.Value);
                

                IEnumerable<Item> listPaged = listItensFiltered.ToPagedList(pageNumber, pageSize.Value);


                this.ViewBag.ListOfListAttributeValues = listPaged;

                this.ViewBag.ListHeader = listHeader;

                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name", itemTypeID);

                this.ViewBag.SelectListItemTypes = selectListItemTypes;


                return View(listPaged);



            }
            else
            {
                //this.ViewBag.ListHeader = new List<string>();
                //List<string[]> listOfListAttributeValues = new List<string[]>();
                //IEnumerable<string[]> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, 50);


                //SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name");

                //this.ViewBag.SelectListItemTypes = selectListItemTypes;

                this.ViewBag.ListHeader = new List<string>();
                List<Item> listOfListAttributeValues = new List<Item>();
                IEnumerable<Item> listPaged = listOfListAttributeValues.ToPagedList(pageNumber, 50);


                SelectList selectListItemTypes = new SelectList(listItemTypes, "ID", "Name");

                this.ViewBag.SelectListItemTypes = selectListItemTypes;


                return View(listPaged);

            }






        }


        public async Task<ActionResult> Orders(int id)
        {
            User user = this.serviceUser.Find(id);
            CustumerInfo custumerInfo = this.serviceCustumerInfo.GetByUser(user);
            var orders = this.serviceCustumerOrder.GetByCustumerInfo(custumerInfo);

            this.ViewBag.userID = user.ID;

            return View(orders);
        }


        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            //id => User

            User user = this.serviceUser.Find(id);
            CustumerInfo custumerInfo = this.serviceCustumerInfo.GetByUser(user);

            this.ViewBag.userID = user.ID;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, string Name, string Description)
        {
            //id => User

            User user = this.serviceUser.Find(id);
            CustumerInfo custumerInfo = this.serviceCustumerInfo.GetByUser(user);

            CustumerOrder order = new CustumerOrder()
            {
                Name = Name,
                Description = Description,
                CustumerInfo = custumerInfo
            };

            this.serviceCustumerOrder.Insert(order);
            this.unitOfWorkAsync.SaveChanges();

            ////a.
            //this.ViewBag.CustumerInfoID = custumerInfo.ID;

            //Redirect To AddItems
            return RedirectToAction("Orders", new { id = user.ID });
        }



        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            //id => CustumerOrder

            CustumerOrder order = this.serviceCustumerOrder.Find(id);
    

            //List<string[]> ordersItemsValues = new List<string[]>();

            //if (order.CustumerOrderItems.Count() >= 1)
            //{ 

            //    int columnsCount = 4;
                


            //    foreach (CustumerOrderItem custumerOrderItem in order.CustumerOrderItems)
            //    {
            //        string[] values = new string[columnsCount];
            //        values[0] = custumerOrderItem.ID.ToString();
            //        values[1] = custumerOrderItem.Item.AttributeValues[0].Value;
            //        values[2] = custumerOrderItem.Quantity.ToString();
            //        values[3] = custumerOrderItem.DesiredDateDelivery.ToString();

            //        ordersItemsValues.Add(values);
            //    }
            //}




            User user = this.serviceUser.Find(order.CustumerInfo.User.ID);
            CustumerInfo custumerInfo = this.serviceCustumerInfo.GetByUser(user);


            this.ViewBag.CustumerOrder = order;



            return View(order.CustumerOrderItems);
        }




        [HttpGet]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            //id => CustumerOrderItem ID

            CustumerOrderItem custumerOrderItem = this.serviceCustumerOrderItem.Find(id);


            return View(custumerOrderItem);
        }


        [HttpPost, ActionName("DeleteOrderItem")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteOrderItemConfirmed(int id)
        {
            //id => CustumerOrderItem ID
            CustumerOrderItem custumerOrderItem = this.serviceCustumerOrderItem.Find(id);
            int orderID = custumerOrderItem.CustumerOrder.ID;

            this.serviceCustumerOrderItem.Delete(custumerOrderItem);

            this.unitOfWorkAsync.SaveChanges();


            return RedirectToAction("Details", new { id = orderID });
        }

        [HttpGet]
        public async Task<ActionResult> EditOrderItem(int id)
        {
            //id => CustumerOrderItem ID

            CustumerOrderItem custumerOrderItem = this.serviceCustumerOrderItem.Find(id);


            return View(custumerOrderItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrderItem(CustumerOrderItem custumerOrderItem)
        {
            //id => CustumerOrderItem ID

            var orderItem = this.serviceCustumerOrderItem.Find(custumerOrderItem.ID);
            orderItem.Description = custumerOrderItem.Description;
            orderItem.DesiredDateDelivery = custumerOrderItem.DesiredDateDelivery;
            orderItem.Quantity = custumerOrderItem.Quantity;


            this.serviceCustumerOrderItem.Update(orderItem);
            this.unitOfWorkAsync.SaveChanges();

            return RedirectToAction("Details", new { id = orderItem.CustumerOrder.ID });
        }


        [HttpGet]
        public async Task<ActionResult> OrderItemListing(int id)
        {
            //id => CustumerOrderItem ID

            CustumerOrderItem custumerOrderItem = this.serviceCustumerOrderItem.Find(id);
            var listing = this.serviceSupplierOffer.GetByItem(custumerOrderItem.Item);

            //this.ViewBag.CustumerOrderItem = custumerOrderItem;
            //this.ViewBag.CustumerOrder = custumerOrderItem.CustumerOrder;

            ViewModelCustumerOrderItemListing vmCustumerOrderItemListing = new ViewModelCustumerOrderItemListing(custumerOrderItem, listing);

            this.ViewBag.ViewModelCustumerOrderItemListing = vmCustumerOrderItemListing;

            return View(vmCustumerOrderItemListing.ViewModelCustumerOrderItemListingItems);
        }


        // GET: Supplier
        public ActionResult Dashboard(int id)
        {

            return View();
        }

    }

}