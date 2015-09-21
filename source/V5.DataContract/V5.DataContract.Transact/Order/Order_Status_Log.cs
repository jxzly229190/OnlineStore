// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Status_Log.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态日志类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    using System;

    /// <summary>
    /// 订单状态日志类
    /// </summary>
    public class Order_Status_Log
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}