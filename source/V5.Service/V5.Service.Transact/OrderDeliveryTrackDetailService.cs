// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDeliveryTrackDetailService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单配送物流流转明细服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单配送跟踪明细服务类
    /// </summary>
    public class OrderDeliveryTrackDetailService
    {
        #region Constants and Fields

        /// <summary>
        /// 订单配送明细记录数据访问对象
        /// </summary>
        private readonly IOrderDeliveryTrackDetailDA orderDeliveryTrackDetailDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDeliveryTrackDetailService"/> class.
        /// </summary>
        public OrderDeliveryTrackDetailService()
        {
            this.orderDeliveryTrackDetailDA = new DAFactoryTransact().CreateOrderDeliveryTrackDetailDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询订单配送明细记录
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public Order_Delivery_Tracking QueryOrderDeliveryTrackDetailsByOrderID(int orderID)
        {
            return this.orderDeliveryTrackDetailDA.SelectByOrderId(orderID);
        }

        #endregion
    }
}