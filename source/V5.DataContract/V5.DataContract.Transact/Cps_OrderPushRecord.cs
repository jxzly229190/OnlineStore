// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cps_OrderPushRecord.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps 订单推送记录类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace V5.DataContract.Transact
{
    /// <summary>
    ///     Cps 订单推送记录类
    /// </summary>
    public class Cps_OrderPushRecord
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public Int32 OrderID { get; set; }

        /// <summary>
        ///     获取或设置被推送的 Cps 平台编号．
        /// </summary>
        public Int32 CpsID { get; set; }

        /// <summary>
        ///     获取或设置推送地址．
        /// </summary>
        public String PushURL { get; set; }

        /// <summary>
        ///     获取或设置接收参数．
        /// </summary>
        public String AcceptParam { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}