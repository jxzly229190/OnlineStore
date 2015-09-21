// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gift_Coupon.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   礼券信息.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.ShoppingCart
{
    /// <summary>
    /// 礼券信息.
    /// </summary>
    public class Gift_Coupon
    {
        /// <summary>
        /// 获取或设置赠券编号
        /// </summary>
        public int CouponID { get; set; }

        /// <summary>
        /// 获取或设置赠券类型(0:现金券，1：满减券)
        /// </summary>
        public int CouponType { get; set; }

        /// <summary>
        /// 获取或设置赠券名称
        /// </summary>
        public string CouponName { get; set; }

		/// <summary>
		///     获取或设置券的面值．
		/// </summary>
	    public double CouponFaceValue { get; set; }

	    /// <summary>
		///     获取或设置券数量．
		/// </summary>
		public int CouponCount { get; set; }
        
        /// <summary>
        ///     获取或设置促销活动编号．
        /// </summary>
        public int PromotID { get; set; }

        /// <summary>
        ///     获取或设置促销活动类型（1:MeetMoney,2:MeetAmount）．
        /// </summary>
        public int PromotType { get; set; }
    }
}
