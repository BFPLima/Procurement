using Procurement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Procurement.WebAPP.Models.Custumer
{
    public class ViewModelCustumerOrderItemListing
    {

        public ViewModelCustumerOrderItemListing(CustumerOrderItem custumerOrderItem,
                                                 List<SupplierOffer> SupplierOffers)
        {            
            this.CustumerOrderItem = custumerOrderItem;
            this.SupplierOffers = SupplierOffers;
            Compute();
        }

        protected void Compute()
        {
            this.ViewModelCustumerOrderItemListingItems = new List<ViewModelCustumerOrderItemListingItem>();
            foreach (SupplierOffer SupplierOffer in this.SupplierOffers)
            {
                this.ViewModelCustumerOrderItemListingItems.Add(new ViewModelCustumerOrderItemListingItem(this.CustumerOrderItem, SupplierOffer));
            }
        }


        public CustumerOrderItem CustumerOrderItem { get; protected set; }

        public CustumerOrder CustumerOrder { get { return this.CustumerOrderItem.CustumerOrder; } }

        public List<SupplierOffer> SupplierOffers { get; protected set; }

        public List<ViewModelCustumerOrderItemListingItem> ViewModelCustumerOrderItemListingItems { get; protected set; }

    }
}