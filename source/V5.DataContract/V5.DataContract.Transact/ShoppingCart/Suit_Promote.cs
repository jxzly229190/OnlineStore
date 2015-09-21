namespace V5.DataContract.Transact.ShoppingCart
{
    using System.Collections.Generic;

    /// <summary>
    /// 购物车中的促销信息.
    /// </summary>
    public class Suit_Promote
    {
        /// <summary>
        /// 获取或设置促销编号.
        /// </summary>
        public int PromoteID { get; set; }

        /// <summary>
        /// 获取或设置促销类型（1:满额优惠,2:满件优惠）
        /// </summary>
        public int PromoteType { get; set; }

        /// <summary>
        /// 获取或设置促销名称
        /// </summary>
        public string PromoteName { get; set; }

        /// <summary>
        /// 获取或设置参与活动的商品总金额.
        /// </summary>
        public double TotalPrice { get; set; }

		/// <summary>
		/// 获取或设置活动优惠总金额.
		/// </summary>
	    public double PromoteDiscount { get; set; }

	    /// <summary>
        /// 获取或设置参与活动的商品总数量.
        /// </summary>
        public int TotalQuantity { get; set; }

        public string PromoteInfo { get; set; }

        public List<Cart_Product> Products { get; set; }

        public List<Gift_Product> GiftProducts { get; set; }

        public List<Gift_Coupon> GiftCoupons { get; set; }

        public int GiftIntegral { get; set; }

        public bool IsNoPostage { get; set; }
    }
}
