// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderStatusLogDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态日志数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单状态日志数据访问接口
    /// </summary>
    public interface IOrderStatusLogDA
    {
        /// <summary>
        /// 新增一条订单状态日志
        /// </summary>
        /// <param name="orderStatusLog">
        /// The order Status Log.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 新增的订单状态日子编码
        /// </returns>
        int Insert(Order_Status_Log orderStatusLog, SqlTransaction transaction);

        /// <summary>
        /// 根据订单编码查询对应的订单状态处理信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Order_Status_Log> SelectByOrderID(int orderId);
    }
}