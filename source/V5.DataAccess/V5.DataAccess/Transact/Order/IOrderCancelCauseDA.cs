// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderCancelCauseDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消原因数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单取消原因数据访问接口
    /// </summary>
    public interface IOrderCancelCauseDA
    {
        /// <summary>
        /// 查询所有订单取消原因
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Order_Cancel_Cause> SelectAll();
    }
}