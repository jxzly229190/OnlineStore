// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Delivery_Tracking.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单配送跟踪类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
	using System;

	/// <summary>
    /// 订单配送跟踪类
    /// </summary>
    public class Order_Delivery_Tracking
    {
        #region Public Properties

		/// <summary>
		/// 获取或设置主键编号．
		/// </summary>
		public int ID { get; set; }

		public int OrderID { get; set; }
		public string Express { get; set; }
		public string MailNo { get; set; }
		public string Status { get; set; }
		public DateTime StatusTime { get; set; }
		public string Remark { get; set; }
		public string Steps { get; set; }
		public int GJWStatus { get; set; }
		public string ExtField { get; set; }
		public int IsDelete { get; set; }
		public DateTime CreateTime { get; set; }

        #endregion
    }
}