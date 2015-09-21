// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Bill.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   购物车信息.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.ShoppingCart
{
    using System.Collections.Generic;

    /// <summary>
    /// 购物车信息.
    /// </summary>
    public class Order_Bill
    {
        /// <summary>
        /// 获取或设置购物车商品.
        /// </summary>
        public List<Cart_Product> Products { get; set; }

        /// <summary>
        /// 获取或设置购物车商品.
        /// </summary>
        public List<Suit_Promote> SuitPromoteInfos { get; set; }

        /// <summary>
        /// 获取或设置运费.
        /// </summary>
        public double Freight {
	        get
	        {
		        return TotalPrice - TotalDiscount >= 100 ? 0 : 10;
	        }
        }

        /// <summary>
        /// 获取或设置总价格（不含运费）.
        /// </summary>
        public double TotalPrice { get; set; }

		/// <summary>
		/// 获取或设置总优惠金额
		/// </summary>
	    public double TotalDiscount { get; set; }

	    /// <summary>
		/// 获取或设置商品数量
		/// </summary>
	    public int ProductCount { get; set; }

	    /// <summary>
        /// 获取或设置积分抵扣.
        /// </summary>
        public double IntegralDeduction { get; set; }

        /// <summary>
        /// 获取或设置优惠券抵扣.
        /// </summary>
        public double CouponDeduction { get; set; }
    }
}