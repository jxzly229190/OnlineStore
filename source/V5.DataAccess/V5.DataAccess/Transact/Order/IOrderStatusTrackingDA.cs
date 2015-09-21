// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderStatusTrackingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态变化跟踪信息数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单状态变化跟踪信息数据库访问接口
    /// </summary>
    public interface IOrderStatusTrackingDA
    {
        /// <summary>
        /// 根据订单编码查询订单状态跟踪信息
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Order_Status_Tracking> SelectByOrderID(int orderID);

        /// <summary>
        /// 插入一条订单状态跟踪信息
        /// </summary>
        /// <param name="orderStatusTracking">
        /// 订单状态跟踪对象
        /// </param>
        /// <returns>
        /// 新增的记录编码
        /// </returns>
        int Insert(Order_Status_Tracking orderStatusTracking, SqlTransaction transaction);
    }
}