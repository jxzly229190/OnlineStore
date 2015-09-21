// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Cancel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   未出库订单取消类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    using System;

    /// <summary>
    /// 未出库订单取消类
    /// </summary>
    public class Order_Cancel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置订单编号．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置订单申请取消原因编号．
        /// </summary>
        public int OrderCancelCauseID { get; set; }

        /// <summary>
        /// 获取或设置后台员工操作编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置会员编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置订单申请取消备注．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion

    }
}