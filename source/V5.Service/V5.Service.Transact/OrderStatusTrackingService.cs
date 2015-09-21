// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderStatusTrackingService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态变化跟踪服务类
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
    /// 订单状态变化跟踪服务类
    /// </summary>
    public class OrderStatusTrackingService
    {
        #region Constants and Fields

        /// <summary>
        /// 订单状态变化跟踪数据访问对象
        /// </summary>
        private readonly IOrderStatusTrackingDA orderStatusTrackingDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStatusTrackingService"/> class.
        /// </summary>
        public OrderStatusTrackingService()
        {
            this.orderStatusTrackingDA = new DAFactoryTransact().CreateOrderStatusTrackingDA();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 根据订单编码查询订单状态跟踪信息
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Status_Tracking> QueryByOrderID(int orderID)
        {
            return this.orderStatusTrackingDA.SelectByOrderID(orderID);
        }

        /// <summary>
        /// 新增一条订单状态跟踪信息
        /// </summary>
        /// <param name="orderStatusTracking">
        /// 订单状态跟踪对象
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 新增的记录编码
        /// </returns>
        public int Add(Order_Status_Tracking orderStatusTracking, SqlTransaction transaction)
        {
            return this.orderStatusTrackingDA.Insert(orderStatusTracking, transaction);
        }

        #endregion
    }
}