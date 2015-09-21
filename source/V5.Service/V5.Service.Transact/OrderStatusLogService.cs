// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderStatusLogService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态日志服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单状态日志服务类
    /// </summary>
    public class OrderStatusLogService
    {
        #region Constants and Fields

        /// <summary>
        /// 数据访问对象
        /// </summary>
        private IOrderStatusLogDA orderStatusLogDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStatusLogService"/> class.
        /// </summary>
        public OrderStatusLogService()
        {
            this.orderStatusLogDA = new DAFactoryTransact().CreateOrderStatusLogDA();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 新增一条订单状态日志
        /// </summary>
        /// <param name="orderStatusLog">
        /// 订单状态日志
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 新增的订单状态日子编码
        /// </returns>
        public int Insert(Order_Status_Log orderStatusLog, SqlTransaction transaction)
        {
            return this.orderStatusLogDA.Insert(orderStatusLog, transaction);
        }

        /// <summary>
        /// 根据订单编码查询对应的订单状态处理信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Status_Log> QueryOrderStatusLogsByOrderID(int orderId)
        {
            return this.orderStatusLogDA.SelectByOrderID(orderId);
        } 

        #endregion
    }
}