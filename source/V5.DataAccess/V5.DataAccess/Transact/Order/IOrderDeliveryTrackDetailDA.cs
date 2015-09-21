// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderDeliveryTrackDetailDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单配送物流流转数据接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单配送物流流转数据接口
    /// </summary>
    public interface IOrderDeliveryTrackDetailDA
    {
        /// <summary>
        /// 根据订单编码查询订单流程信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        Order_Delivery_Tracking SelectByOrderId(int orderId);
    }
}