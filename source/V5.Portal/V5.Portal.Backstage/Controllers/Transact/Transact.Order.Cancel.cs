// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transact.Order.Cancel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   交易控制器部分类 -- 取消订单
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using V5.DataContract.Transact.Order;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Portal.Backstage.Models.Transact.Order;
    using V5.Service.Transact;

    /// <summary>
    /// 交易控制器部分类 -- 取消订单
    /// </summary>
    public partial class TransactController
    {
        #region 取消订单相关方法

        /// <summary>
        /// The order cancel view.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="orderCode">
        /// The order code.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult OrderCancelView(int orderId, string orderCode)
        {
            return this.PartialView(
                "Order/OrderCancelTemplate",
                new OrderCancelModel { OrderID = orderId, OrderCode = orderCode });
        }

        /// <summary>
        /// The order cancel with refund view.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="orderCode">
        /// The order code.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult OrderCancelWithRefundView(int orderId, string orderCode)
        {
            var order = new OrderService().QueryById(orderId);
            return this.PartialView(
                "Order/OrderCancelWithRefundTemplate",
                new AftersaleRefundModel
                    {
                        OrderID = orderId,
                        TotalMoney = order.TotalMoney,
                        PaymentMoney = new OrderService().GetOrderActualPayment(orderId),
                        OrderCode = orderCode
                    });
        }

        /// <summary>
        /// 检查订单状态
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CheckOrderStatus(int orderId)
        {
            // 检查订单是否已经发货
            // 未付款未发货：1，已付款未发货：2，已发货:-1，已取消或是废单:-2，订单不存在:-3, 服务器出错:-4
            try
            {
                var order = new OrderService(this.SystemUserSession.EmployeeID).QueryById(orderId);
                if (order == null)
                {
                    return this.Json(new AjaxResponse(3, "订单不存在"), JsonRequestBehavior.AllowGet);
                }

                // 已发货
                if (order.Status == 2 || order.Status == 3)
                {
                    return this.Json(new AjaxResponse(-1, "订单已发货或者签收"), JsonRequestBehavior.AllowGet);
                }

                // 未付款未发货
                if (order.PaymentStatus == 0 && (order.Status == 0 || order.Status == 1 || order.Status == 100))
                {
                    return this.Json(new AjaxResponse(1, "未付款未发货"), JsonRequestBehavior.AllowGet);
                }

                // 已付款未发货
                if (order.PaymentStatus == 1 && (order.Status == 0 || order.Status == 1 || order.Status == 100))
                {
                    return this.Json(new AjaxResponse(2, "已付款未发货"), JsonRequestBehavior.AllowGet);
                }

                // 已取消或是废单
                if (order.Status == 4 || order.Status == 5 || order.Status == 6 || order.Status == 8)
                {
                    return this.Json(new AjaxResponse(-2, "已取消或是废单"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("取消订单--查询订单状态出错", Category.Error, exception);
                return this.Json(new AjaxResponse(-4, "服务器出错"), JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query cancel causes.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCancelCauses()
        {
            var orderCancelCauses = new OrderCancelCauseService().QueryAll();
            var modelList = new List<OrderCancalCauseModel>();
            foreach (var orderCancelCause in orderCancelCauses)
            {
                modelList.Add(
                    DataTransfer.Transfer<OrderCancalCauseModel>(orderCancelCause, typeof(Order_Cancel_Cause)));
            }

            return this.Json(modelList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取退款方式列表
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetRefundMethods()
        {
            return this.Json(null);
        }

        /// <summary>
        /// 取消未发货未付款订单
        /// </summary>
        /// <param name="orderCancelModel">
        /// The order cancel model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CancelOrder(OrderCancelModel orderCancelModel)
        {
            var response = new AjaxResponse();
            if (orderCancelModel == null || orderCancelModel.OrderID < 1 || orderCancelModel.OrderCancelCauseID < 1)
            {
                response.State = -1;
                response.Message = "参数错误";
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var orderCancel = DataTransfer.Transfer<Order_Cancel>(orderCancelModel, typeof(OrderCancelModel));
                orderCancel.EmployeeID = this.SystemUserSession.EmployeeID;
                int state = new OrderService(this.SystemUserSession.EmployeeID).OrderCancelByBackstage(orderCancel);

                // 0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
                if (state == 0)
                {
                    response.State = 0;
                    response.Message = "订单状态异常";
                }
                else if (state == 1)
                {
                    response.State = 1;
                    response.Message = "订单取消成功";
                }
                else if (state == 2)
                {
                    response.State = 2;
                    response.Message = "已发货，取消订单操作取消";
                }
                else if (state == 3)
                {
                    response.State = 3;
                    response.Message = "订单已取消、已损失或者已作废";
                }
                else if (state == 4)
                {
                    response.State = 4;
                    response.Message = "订单已付款";
                }

                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("订单取消操作发生错误", Category.Error, exception);

                response.State = -2;
                response.Message = exception.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The cancel order with refund.
        /// </summary>
        /// <param name="orderCancelCauseID">
        /// The order Cancel Cause ID.
        /// </param>
        /// <param name="orderCancelDescription">
        /// The order Cancel Description.
        /// </param>
        /// <param name="refundModel">
        /// The refund model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CancelOrderRefund(int orderCancelCauseID, string Description, AftersaleRefundModel refundModel, int type)
        {
            if (type == 0)
            {
                return
                    this.CancelOrder(
                        new OrderCancelModel()
                            {
                                OrderID = refundModel.OrderID,
                                OrderCancelCauseID = orderCancelCauseID,
                                Description = Description
                            });
            }

            var response = new AjaxResponse();
            if (orderCancelCauseID < 1 || refundModel == null || refundModel.OrderID < 1
                || refundModel.ActualRefundMoney < 0 || refundModel.RefundMethodID < 1)
            {
                response.State = -1;
                response.Message = "参数错误";
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }

            var orderService = new OrderService(this.SystemUserSession.EmployeeID);

            var actualPayment = orderService.GetOrderActualPayment(refundModel.OrderID);
            if (refundModel.ActualRefundMoney > actualPayment)
            {
                response.State = -2;
                response.Message = "退款金额不能大于已支付金额";
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }

            try
            {
                refundModel.EmployeeID = this.SystemUserSession.EmployeeID;
                int state = orderService.OrderCancelRefundByBackstage(
                    orderCancelCauseID,
                    Description,
                    DataTransfer.Transfer<Aftersale_Refund>(refundModel, typeof(AftersaleRefundModel)));

                // 0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废，4-订单未付款
                if (state == 0)
                {
                    response.State = 0;
                    response.Message = "订单状态异常";
                }
                else if (state == 1)
                {
                    response.State = 1;
                    response.Message = "订单取消成功";
                }
                else if (state == 2)
                {
                    response.State = 2;
                    response.Message = "已发货，操作取消";
                }
                else if (state == 3)
                {
                    response.State = 3;
                    response.Message = "订单已取消、已损失或者已作废";
                }
                else if (state == 4)
                {
                    response.State = 4;
                    response.Message = "订单未付款";
                }

                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("订单取消操作发生错误", Category.Error, exception);

                response.State = -2;
                response.Message = exception.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取退款方式
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetRefundMethod()
        {
            // 1：退至虚拟账户，2：人工退款至指定帐号
            var selectListItems = new List<SelectListItem>
                                                       {
                                                           new SelectListItem
                                                               {
                                                                   Text = "退至虚拟账户",
                                                                   Value = "1"
                                                               },
                                                           new SelectListItem
                                                               {
                                                                   Text = "人工退款",
                                                                   Value = "2"
                                                               }
                                                       };
            return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}