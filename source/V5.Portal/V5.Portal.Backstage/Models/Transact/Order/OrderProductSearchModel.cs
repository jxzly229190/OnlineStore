using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Transact.Order
{
    public class OrderProductSearchModel
    {
        public int ProductCategoryID { get; set; }

        public int SubProductCategoryID { get; set; }

        public int ProductBrandID { get; set; }

        public int SubProductBrandID { get; set; }

        public string ProductName { get; set; }

        public string Barcode { get; set; }
    }
}