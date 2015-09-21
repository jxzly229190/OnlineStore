// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancelService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单取消服务类
    /// </summary>
    public class OrderCancelService
    {
        #region Constants and Fields

        /// <summary>
        /// The order cancel da.
        /// </summary>
        private readonly IOrderCancelDA orderCancelDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCancelService"/> class.
        /// </summary>
        public OrderCancelService()
        {
            this.orderCancelDA = new DAFactoryTransact().CreateOrderCancelDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 后台取消未发货为付款订单
        /// </summary>
        /// <param name="orderCancel">
        /// 订单取消对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
        /// </returns>
        public int OrderCancel(Order_Cancel orderCancel)
        {
            return this.orderCancelDA.OrderCancel(orderCancel);
        }

        /// <summary>
        /// 后台取消已付款为发货订单
        /// </summary>
        /// <param name="orderCancelCauseId">
        /// 订单编码
        /// </param>
        /// <param name="cancelDescription">
        /// 取消订单备注
        /// </param>
        /// <param name="refund">
        /// 退款对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废，4-订单未付款
        /// </returns>
        public int OrderCancelRefundByBackstage(int orderCancelCauseId, string cancelDescription, Aftersale_Refund refund)
        {
            var orderCancel = new Order_Cancel
                                  {
                                      OrderID = refund.OrderID,
                                      OrderCancelCauseID = orderCancelCauseId,
                                      Description = cancelDescription,
                                      EmployeeID = refund.EmployeeID
                                  };
            return this.orderCancelDA.OrderCancelWithRefundByBackstage(orderCancel, refund);
        }

        #endregion
    }
}