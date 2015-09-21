// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDeliveryTrackDetailModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单物流信息跟踪详情
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    /// <summary>
    /// 订单物流信息跟踪详情
    /// </summary>
    public class OrderDeliveryTrackDetailModel
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