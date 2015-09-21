// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancalCauseModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   取消订单原因
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    /// <summary>
    /// 取消订单原因
    /// </summary>
    public class OrderCancalCauseModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置订单取消原因．
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}