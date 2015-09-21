namespace V5.Portal.Models
{
    using System.Collections.Generic;

    public class UserCartModel
    {
        public int UserId { get; set; }

        public string VisitorKey { get; set; }

        public List<CartProduct> ProductItems { get; set; }

	    public List<CartProduct> BuyList { get; set; }
    }
}