using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V5.DataContract.Product;
using V5.DataContract.Transact.ShoppingCart;

namespace V5.Portal.Models
{
    public class ProductInfo
    {
        public int ProductID
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }

        public Cart_Product CartProduct
        {
            get;
            set;
        }
    }
}