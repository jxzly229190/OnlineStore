// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Delivery_Tracking_Details.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单配送跟踪明细类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    using System;

    /// <summary>
    /// 订单配送跟踪明细类
    /// </summary>
    public class Order_Delivery_Tracking_Details
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置订单配送跟踪编号．
        /// </summary>
        public int OrderDeilveryTrackingID { get; set; }

        /// <summary>
        /// 获取或设置操作时间．
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 获取或设置操作概要．
        /// </summary>
        public string OperateSummary { get; set; }

        /// <summary>
        /// 获取或设置操作人．
        /// </summary>
        public string Operator { get; set; }

        #endregion
    }
}