using Procurement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Users
{
    public class SupplierInfo: UserInfo
    {

        public SupplierInfo() : base() { }


        public List<SupplierOffer> SupplierOffers { get; set; }
    }
}
