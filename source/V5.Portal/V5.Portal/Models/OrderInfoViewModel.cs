namespace V5.Portal.Models
{
    using System.Collections.Generic;

    using V5.DataContract.Transact.ShoppingCart;

	public class OrderInfoViewModel
    {
		//收货地址信息
        public List<UserReceiveAddressModel> UserReceiveAddressList;

		//商品信息
        public List<CartProduct> Products;

		//账单详情
		public Order_Bill BillDetail;
    }
}