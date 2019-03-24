using Procurement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Procurement.WebAPP.Models.Custumer
{
    public class ViewModelCustumerOrderItemListingItem
    {
        public ViewModelCustumerOrderItemListingItem(CustumerOrderItem custumerOrderItem ,
                                                     SupplierOffer SupplierOffer)
        {
            this.CustumerOrderItem = custumerOrderItem;
            this.SupplierOffer = SupplierOffer;
        
                }

       
        public CustumerOrderItem CustumerOrderItem { get; protected set; }
        public SupplierOffer SupplierOffer { get; protected set; }



        public DateTime ProvidedDateDelivery
        {
            get
            {
                return this.SupplierOffer.ProvidedDateDelivery;
            }
        }

        public string Description
        {
            get
            {
                return this.SupplierOffer.Description;
            }
        }

        public double Price
        {
            get
            {
                return this.SupplierOffer.Price;
            }
        }

        public double PriceByQuantity
        {
            get
            {
                return this.CustumerOrderItem.Quantity * this.SupplierOffer.Price;
            }
        }



    }
}