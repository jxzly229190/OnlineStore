// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderCancelDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单取消数据访问接口
    /// </summary>
    public interface IOrderCancelDA
    {
        /// <summary>
        /// 后台取消未付款未发货订单
        /// </summary>
        /// <param name="orderCancel">
        /// 订单取消对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废，4-订单已付款
        /// </returns>
        int OrderCancel(Order_Cancel orderCancel);

        /// <summary>
        /// 后台取消已付款未发货订单
        /// </summary>
        /// <param name="orderCancel">
        /// 订单取消对象
        /// </param>
        /// <param name="refund">
        /// 售后退款对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废，4-订单未付款
        /// </returns>
        int OrderCancelWithRefundByBackstage(Order_Cancel orderCancel, Aftersale_Refund refund);
    }
}