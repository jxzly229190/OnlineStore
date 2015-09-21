// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancelModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   取消订单Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    /// <summary>
    /// 取消订单Model
    /// </summary>
    public class OrderCancelModel
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
        /// 获取或设置订单编号．
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 获取或设置订单申请取消原因编号．
        /// </summary>
        public int OrderCancelCauseID { get; set; }

        /// <summary>
        /// 获取或设置后台员工操作编号．
        /// </summary>
        public int EmployeeID { get; set; }

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