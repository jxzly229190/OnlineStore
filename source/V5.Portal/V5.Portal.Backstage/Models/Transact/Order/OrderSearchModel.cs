// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderSearchModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单搜索Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    /// <summary>
    /// 订单搜索Model类
    /// </summary>
    public class OrderSearchModel
    {
        #region Public Methods and Operators

        /// <summary>
        /// 获取或设置订单编号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 获取或设置订单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置起始时间
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 获取或设置结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 获取或设置最小订单金额
        /// </summary>
        public double MinTotalMoney { get; set; }

        /// <summary>
        /// 获取或设置最大订单金额
        /// </summary>
        public double MaxTotalMoney { get; set; }

        /// <summary>
        /// 获取或设置支付方式编码．
        /// </summary>
        public int PaymentMethodID { get; set; }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置收货人
        /// </summary>
        public string Consignee { get; set; }

		/// <summary>
		/// 获取或设置收货人电话
		/// </summary>
		public string ReceiveMoblie { get; set; }

        /// <summary>
        /// 获取或设置订单来源编号
        /// </summary>
        public int CpsID { get; set; }

        #endregion
    }
}