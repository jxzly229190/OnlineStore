// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Payment.cs" company="www.gjw.com">
//   (C) 2014 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单支付类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    /// <summary>
    /// 订单支付类
    /// </summary>
    public class Order_Payment
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 获取或设置订单编号．
        /// </summary>
        public System.Int32 OrderID { get; set; }

        /// <summary>
        /// 获取或设置支付类型编号．
        /// </summary>
        public System.Int32 PaymentOrgID { get; set; }

	    public string PaymentOrgName {
		    get
		    {
			    switch (this.PaymentOrgID)
			    {
					case 4:
					    return "支付宝";
					default:
					    return "网上支付";
			    }
		    }
	    }

	    /// <summary>
        /// 获取或设置支付金额．
        /// </summary>
        public System.Double PaymentMoney { get; set; }

        /// <summary>
        /// 第三方交易编号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 获取或设置是否使用优惠券支付．
        /// </summary>
        public System.Boolean IsUseCoupon { get; set; }

        /// <summary>
        /// 获取或设置是否使用积分支付．
        /// </summary>
        public System.Boolean IsUseIntegral { get; set; }

        /// <summary>
        /// 获取或设置是否使用账户余额支付．
        /// </summary>
        public System.Boolean IsUseAccount { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public System.Int32 IsDelete { get; set; }

        #endregion
    }
}