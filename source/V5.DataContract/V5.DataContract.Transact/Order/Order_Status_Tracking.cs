// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Status_Tracking.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态跟踪信息类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    using System;

    /// <summary>
    /// 订单状态跟踪信息类
    /// </summary>
    public class Order_Status_Tracking
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置订单编码．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置操作人编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置订单变更具体信息．
        /// </summary>
        public string Remark { get; set; }

		/// <summary>
		/// 获取或设置订单配送公司代号．
		/// </summary>
		public string ExpressNumber { get; set; }

		/// <summary>
		/// 获取或设置订单快递单号．
		/// </summary>
		public string MailNo { get; set; }

        /// <summary>
        /// 获取或设置订单变更具体状态编号．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}