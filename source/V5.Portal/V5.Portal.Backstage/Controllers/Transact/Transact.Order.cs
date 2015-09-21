// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transact.Order.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   交易管理局部视图
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using NPOI.SS.Formula.Functions;

    using V5.DataContract.Product;
    using V5.DataContract.System;
    using V5.DataContract.Transact.Order;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Transact;
    using V5.Portal.Backstage.Models.Transact.Order;
    using V5.Portal.Backstage.Models.User;
    using V5.Service.Product;
    using V5.Service.System;
    using V5.Service.Transact;
    using V5.Service.User;

    /// <summary>
    /// The transact controller.
    /// </summary>
    public partial class TransactController
    {
        /// <summary>
        /// The order.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult Order()
        {
            var list = new ProductService().Query(ProductType.HotSale, 10, null);
            return this.PartialView("Order");
        }

        public PartialViewResult All()
        {
            return this.PartialView("Order/All");
        }

        public PartialViewResult Unconfirmed()
        {
            return this.PartialView("Order/Unconfirmed");
        }

        public PartialViewResult Confirmed()
        {
            return this.PartialView("Order/Confirmed");
        }

		/// <summary>
		/// 未支付订单（等待支付）
		/// </summary>
		/// <returns></returns>
		public PartialViewResult Unpaid()
		{
			return this.PartialView("Order/Unpaid");
		}

        public PartialViewResult Posted()
        {
            return this.PartialView("Order/Posted");
        }

        public PartialViewResult Received()
        {
            return this.PartialView("Order/Received");
        }

        public PartialViewResult Invalid()
        {
            return this.PartialView("Order/Invalid");
        }

        public PartialViewResult Cancelled()
        {
            return this.PartialView("Order/Cancelled");
        }

        public PartialViewResult OrderEdit_AddProducts()
        {
            return this.PartialView("Order/OrderEdit_AddProducts", new OrderProductSearchModel());
        }

        #region Public Methods and Operators

        #region 查询订单相关方法
		/// <summary>
		/// 分页查询等待支付订单数据
		/// </summary>
		/// <param name="request">请求对象</param>
		/// <param name="OrderCode">订单编号</param>
		/// <param name="PaymentMethodID">支付方式</param>
		/// <param name="UserName">会员名称</param>
		/// <param name="Consignee">收货人</param>
		/// <param name="CpsID">订单来源</param>
		/// <param name="StartDateTime">起始时间</param>
		/// <param name="EndDateTime">结束时间</param>
		/// <param name="MinTotalMoney">最小金额</param>
		/// <param name="MaxTotalMoney">最大金额</param>
		/// <returns></returns>
		public ActionResult QueryUnpaidOrders(
			[DataSourceRequest] DataSourceRequest request,
			string OrderCode,
			string PaymentMethodID,
			string UserName,
			string Consignee,
			string CpsID,
			string StartDateTime,
			string EndDateTime,
			string MinTotalMoney,
			string MaxTotalMoney,
			string ReceiveMoblie)
		{
			return this.QueryOrders(
				request,
				OrderCode,
				"100",
				PaymentMethodID,
				UserName,
				Consignee,
				CpsID,
				StartDateTime,
				EndDateTime,
				MinTotalMoney,
				MaxTotalMoney,
				ReceiveMoblie);
		}

        /// <summary>
        /// 分页查询未确认订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryUnconfirmedOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie)
        {
	        return this.QueryOrders(
		        request,
		        OrderCode,
		        "0",
		        PaymentMethodID,
		        UserName,
		        Consignee,
		        CpsID,
		        StartDateTime,
		        EndDateTime,
		        MinTotalMoney,
		        MaxTotalMoney,
		        ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询已确认订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryConfirmedOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
            string MaxTotalMoney,string ReceiveMoblie)
        {
            return this.QueryOrders(
                request,
                OrderCode,
                "1",
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询取消的订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryCancelledOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie)
        {
            return this.QueryOrders(
                request,
                OrderCode,
                "6",
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询取消的订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryPostedOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie)
        {
            return this.QueryOrders(
                request,
                OrderCode,
                "2",
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询签收的订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryReceivedOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie)
        {
            return this.QueryOrders(
                request,
                OrderCode,
                "3",
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询废弃的订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryInvalidOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
            string MaxTotalMoney,
			string ReceiveMoblie)
        {
            return this.QueryOrders(
                request,
                OrderCode,
                "4,8",
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);
        }

        /// <summary>
        /// 分页查询所有订单数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="Status">订单状态</param>
        /// <param name="PaymentMethodID">支付方式</param>
        /// <param name="UserName">会员名称</param>
        /// <param name="Consignee">收货人</param>
        /// <param name="CpsID">订单来源</param>
        /// <param name="StartDateTime">起始时间</param>
        /// <param name="EndDateTime">结束时间</param>
        /// <param name="MinTotalMoney">最小金额</param>
        /// <param name="MaxTotalMoney">最大金额</param>
        /// <returns></returns>
        public ActionResult QueryOrders(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string Status,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie)
        {
            int pageCount, rowCount;

            string condition = this.BuildOrderSearchCondition(
                OrderCode,
                Status,
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney,ReceiveMoblie);

            var paging = new Paging(null, null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);
            var service = new OrderService(this.SystemUserSession.EmployeeID);

            var list = service.Query(paging, out pageCount, out rowCount);
            if (list == null || list.Count <= 0)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var modelList = new List<OrderModel>();
            foreach (var item in list)
            {
                var order = DataTransfer.Transfer<OrderModel>(item, typeof(Order));
                modelList.Add(order);
            }

            var dataSource = new DataSource { Data = modelList, Total = rowCount, Page = pageCount };

            return Json(dataSource, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QueryOrderProducts(
            [DataSourceRequest] DataSourceRequest request,
            string OrderCode,
            string Status,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
            string MaxTotalMoney)
        {
            int pageCount, rowCount;
            string condition = this.BuildOrderSearchCondition(
                OrderCode,
                Status,
                PaymentMethodID,
                UserName,
                Consignee,
                CpsID,
                StartDateTime,
                EndDateTime,
                MinTotalMoney,
                MaxTotalMoney);
            var paging = new Paging(null, null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);
            var list = new OrderProductService().Paging(paging, out pageCount, out rowCount);
            var modelList = new List<OrderProductModel>();
            foreach (var orderProduct in list)
            {
                modelList.Add(DataTransfer.Transfer<OrderProductModel>(orderProduct, typeof(Order_Product)));
            }

            return this.Json(modelList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// The modify order.
        /// </summary>
        /// <param name="id">
        /// The order ID.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ModifyOrderDescription(int id, string description)
        {
            if (id > 0)
            {
                var service = new OrderService(this.SystemUserSession.EmployeeID);
                service.ModifyOrderDescription(id, description);
            }

            return null;
        }

        /// <summary>
        /// 移除订单商品
        /// </summary>
        /// <param name="id">
        /// 订单商品编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveOrderProduct(int id)
        {
            var orderProductService = new OrderProductService();
            orderProductService.RemoveOrderProduct(id);
            return null;
        }

        /// <summary>
        /// 获取订单状态列表
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetOrderStatusList()
        {
            var list = new List<SelectListItem>
                           {
                               new SelectListItem { Text = "待付款", Value = "100" },
                               new SelectListItem { Text = "待确认", Value = "0" },
                               new SelectListItem { Text = "已确认", Value = "1" },
                               new SelectListItem { Text = "已发货", Value = "2" },
                               new SelectListItem { Text = "已签收", Value = "3" },
                               new SelectListItem { Text = "官网作废", Value = "8" },
                               new SelectListItem { Text = "ERP作废", Value = "4" },
                               new SelectListItem { Text = "已取消", Value = "6" }
                           };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据订单编码查询成交产品信息
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryOrderProductByOrderId([DataSourceRequest] DataSourceRequest request, int orderId)
        {
            var orderProductService = new OrderProductService();
            var list = orderProductService.QueryByOrderId(orderId);

            if (list == null || list.Count < 1)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var modelList = new List<OrderProductModel>();
            foreach (var item in list)
            {
                var order = DataTransfer.Transfer<OrderProductModel>(item, typeof(Order_Product));
                modelList.Add(order);
            }

            var dataSource = new DataSource { Data = modelList };

            return Json(dataSource, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryOrderDetails(int orderID)
        {
            var orderDetailModel = new OrderDetailModel();

            if (orderID > 0)
            {
                var order = new OrderService().QueryById(orderID);
                orderDetailModel.OrderInfo = DataTransfer.Transfer<OrderModel>(order, typeof(Order));
                var orderStatusTrackings = new OrderStatusTrackingService().QueryByOrderID(orderID);

                #region 订单状态信息

                var orderState = new OrderState { PaymentMethod = order.PaymentMethodID };
                switch (order.Status)
                {
                    case 0:
                    case 1:
                        orderState.Status = order.PaymentMethodID == 0 ? 1 : 0;
                        break;
                    case 2:
                        orderState.Status = 2;
                        break;
                    case 3:
                        orderState.Status = 4;
                        break;
                    case 100:
                        if (order.PaymentMethodID == 0)
                        {
                            orderState.Status = 0;
                        }

                        break;
                }

                if (orderStatusTrackings != null)
                {
                    orderState.ProcessDatetimes = new List<string>();
                    foreach (var orderStatusTracking in orderStatusTrackings)
                    {
                        if (orderState.Status == 0 && orderStatusTracking.Status == 0)
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (order.PaymentMethodID == 0 && orderState.Status == 1
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (orderState.Status == 2
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1
                                || orderStatusTracking.Status == 2))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (orderState.Status == 4
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1
                                || orderStatusTracking.Status == 2 || orderStatusTracking.Status == 3))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }
                    }
                }

                orderDetailModel.CurrentOrderState = orderState;

                #endregion

                #region 订单发票信息

                if (order.IsRequireInvoice)
                {
                    orderDetailModel.InvoiceInfo =
                        DataTransfer.Transfer<OrderInvoiceModel>(
                            new OrderInvoiceService().SelectByOrderID(orderID),
                            typeof(Order_Invoice));
                }

                #endregion

                #region 订单变化追踪记录

				orderDetailModel.OrderTrackDetails = new List<OrderTrackDetailModel>();

				if (orderStatusTrackings != null)
				{
					foreach (var orderStatusTracking in orderStatusTrackings)
					{
						System_Employee systemUser = null;

						if (orderStatusTracking.EmployeeID > 0) systemUser = new SystemEmployeeService().QueryByID(orderStatusTracking.EmployeeID);

						orderDetailModel.OrderTrackDetails.Add(
							new OrderTrackDetailModel()
							{
								Operator =
									orderStatusTracking.EmployeeID > 0
										? systemUser==null?"客服":"客服:"+systemUser.Name
										: (orderStatusTracking.UserID > 0) ? "客户" : "系统",
								OperateSummary = orderStatusTracking.Remark,
								OperateTime = orderStatusTracking.CreateTime
							});

						if (orderStatusTracking.Status == 2)
						{
							// 若产品已发货，则获取快递跟踪信息
							var orderDeliverTrackDetail =
								new OrderDeliveryTrackDetailService().QueryOrderDeliveryTrackDetailsByOrderID(orderID);
							if (orderDeliverTrackDetail != null && !string.IsNullOrWhiteSpace(orderDeliverTrackDetail.Steps))
							{
								var details = orderDeliverTrackDetail.Steps.Split(';');

								foreach (var orderDeliveryTrackingDetail in details)
								{
									if (!string.IsNullOrWhiteSpace(orderDeliveryTrackingDetail))
									{
										var di = orderDeliveryTrackingDetail.Split('|');
										if (di.Length >= 2)
										{
											var tracking = new OrderTrackDetailModel { OperateSummary = di[1], Operator = "快递公司" };

											DateTime dt;
											if (DateTime.TryParse(di[0], out dt))
											{
												tracking.OperateTime = dt;
											}
											orderDetailModel.OrderTrackDetails.Add(tracking);
										}
									}
								}
							}
						}
					}
				}

                #region 老的方法

                /*
                string summary = string.Empty;
                if (order.PaymentMethodID == 0)
                {
                    summary = order.PaymentStatus == 0 ? "您提交了订单，等待付款。" : "您提交了订单，商品准备出库。";
                }
                else if (order.PaymentMethodID == 1)
                {
                    summary = "您提交了订单，请等待确认。";
                }

                orderDetailModel.OrderDeliveryTrackDetails.Add(
                       new OrderDeliveryTrackDetailModel
                       {
                           OperateSummary = summary,
                           Operator = "客户",
                           OperateTime = order.CreateTime
                       });

                if (order.Status == 6)
                {
                    orderDetailModel.OrderDeliveryTrackDetails.Add(
                       new OrderDeliveryTrackDetailModel
                       {
                           OperateSummary = "订单已取消",
                           Operator = "客户",
                           OperateTime = order.CreateTime
                       });
                }
                else if (order.Status == 4 || order.Status == 8)
                {
                    orderDetailModel.OrderDeliveryTrackDetails.Add(
                        new OrderDeliveryTrackDetailModel
                            {
                                OperateSummary = "订单已作废",
                                Operator = "系统",
                                OperateTime = order.CreateTime
                            });
                }
                else
                {
                    if (orderStatusLogs != null)
                    {
                        foreach (var orderStatusLog in orderStatusLogs)
                        {
                            if (orderStatusLog.Status == 1)
                            {
                                summary = "您的订单已确认，准备出库";
                            }

                            if (orderStatusLog.Status == 2)
                            {
                                summary = "您的订单已出库"; // todo: 添加快递单号,快递公司名称
                            }

                            orderDetailModel.OrderDeliveryTrackDetails.Add(
                            new OrderDeliveryTrackDetailModel
                            {
                                OperateSummary = summary,
                                Operator = new SystemEmployeeService().QueryByID(orderStatusLog.EmployeeID).Name,  // todo: 添加操作员工名称
                                OperateTime = order.CreateTime
                            });
                        }
                    }
                }

                // 若订单已发货或已签收，则有物流流转信息
                if (order.Status == 3 || order.Status == 2)
                {
                    var orderDeliverTrackDetails =
                        new OrderDeliveryTrackDetailService().QueryOrderDeliveryTrackDetailsByOrderID(orderID);
                    foreach (var orderDeliveryTrackingDetail in orderDeliverTrackDetails)
                    {
                        orderDetailModel.OrderDeliveryTrackDetails.Add(
                            DataTransfer.Transfer<OrderDeliveryTrackDetailModel>(
                                orderDeliveryTrackingDetail,
                                typeof(Order_Delivery_Tracking_Details)));
                    }
                }
                */

                #endregion

                #endregion

                #region 订单收货信息

                orderDetailModel.ReceiverInfo =
                    DataTransfer.Transfer<UserReceiveAddressModel>(
                        new UserReceiveAddressService().QueryByID(order.RecieveAddressID),
                        typeof(User_RecieveAddress));

                #endregion

                #region 订单商品信息

                orderDetailModel.OrderProducts = new List<OrderProductModel>();

                var orderProducts = new OrderProductService().QueryByOrderId(orderID);
                if (orderProducts != null)
                {
                    foreach (var orderProduct in orderProducts)
                    {
                        orderDetailModel.OrderProducts.Add(
                            DataTransfer.Transfer<OrderProductModel>(orderProduct, typeof(Order_Product)));
                    }
                }

                #endregion

                #region 订单支付信息

                orderDetailModel.PaymentInfo = new PaymentInfoModel
                                                   {
                                                       OrderID = orderID,
                                                       PaymentMethodID = order.PaymentMethodID,
                                                       TotalMoney = order.TotalMoney,
                                                       DeliveryCost = order.DeliveryCost,
                                                       ActualPaid =
                                                           order.PaymentStatus == 1
                                                               ? order.TotalMoney + order.DeliveryCost
                                                               : 0,
                                                       ActualMoney =
                                                           order.TotalMoney + order.DeliveryCost
                                                   };

                #endregion
            }

            return this.View("Order/OrderDetail", orderDetailModel);
        }

        /// <summary>
        /// 获取订单编辑部分视图
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult OrderEdit(int orderID)
        {
            return this.PartialView("Order/Order.Edit", null);
        }
        
        /// <summary>
        /// 订单后台-添加订单-查询商品
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <param name="searchModel">
        /// 查询对象
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public ActionResult OrderEditQueryOrderProduct(
            [DataSourceRequest] DataSourceRequest request,
            OrderProductSearchModel searchModel)
        {
            int rowCount = 0;
            int pageCount;
            var productService = new ProductService();
            var condition = productService.BuildProductQueryCondition(
                searchModel.ProductCategoryID < 1 ? string.Empty : searchModel.ProductCategoryID.ToString(),
                searchModel.SubProductCategoryID < 1 ? string.Empty : searchModel.SubProductCategoryID.ToString(),
                searchModel.ProductBrandID < 1 ? string.Empty : searchModel.ProductBrandID.ToString(),
                searchModel.SubProductBrandID < 1 ? string.Empty : searchModel.SubProductBrandID.ToString(),
                searchModel.ProductName,
                searchModel.Barcode,
                string.Empty,
                string.Empty);
            var paging = new Paging("view_Product_Paging", null, "ID", condition, request.Page, request.PageSize);
            var list = productService.Query(paging, out pageCount, out rowCount);
            if (list == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var modelList = new List<OrderProductModel>();

            foreach (var item in list)
            {
                var model = DataTransfer.Transfer<OrderProductModel>(item, typeof(ProductSearchResult));
                model.ProductID = item.ID;
                model.ProductName = item.Name;
                model.Path = item.ThumbnailPath;
                model.TransactPrice = item.GoujiuPrice;
                model.ID = 0;
                modelList.Add(model);
            }

            var result = new DataSourceResult { Data = modelList, Total = rowCount };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编码查询订单信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 返回Json结果
        /// </returns>
        public ActionResult GetOrderInfoByID(int orderId)
        {
            var order = new OrderService(this.SystemUserSession.EmployeeID).QueryById(orderId);
            return Json(
                DataTransfer.Transfer<OrderModel>(order, typeof(Order)),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编码查询用户信息
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <returns>返回Json结果</returns>
        public ActionResult GetUserInfoByID(int userId)
        {
            var user = new UserService().QueryUserByID(userId);
            return Json(
                DataTransfer.Transfer<UserModel>(user, typeof(User)),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据地址编码查询收货地址信息
        /// </summary>
        /// <param name="addressId">地址编码</param>
        /// <returns>返回地址信息Json</returns>
        public ActionResult GetReceiveAddressByID(int addressId)
        {
            var address = new UserReceiveAddressService().QueryByID(addressId);
            return Json(
                DataTransfer.Transfer<UserReceiveAddressModel>(address, typeof(User_RecieveAddress)),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询所有CPS数据
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCpsList()
        {
            var response = new AjaxResponse();
            try
            {
                var cpsList = new CpsService().QueryAll();

                if (cpsList == null)
                {
                    response.State = 2;
                    response.Message = "没有获取到CPS数据";
                    return this.Json(response, JsonRequestBehavior.AllowGet);
                }

                var items = new List<SelectListItem>();
                foreach (var cps in cpsList)
                {
                    items.Add(new SelectListItem { Text = cps.Name, Value = cps.ID.ToString() });
                }

                response.State = 1;
                response.Data = items;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                response.State = -1;
                response.Message = exception.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        /// <summary>
        /// 获取邮编
        /// </summary>
        /// <param name="countyId">
        /// 地区编码
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryPostCodeByCountyID(int countyId)
        {
            var postCode = new UserReceiveAddressService().QueryPostCodeByID(countyId);
            return this.Json(postCode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditOrderInfo(OrderDetailModel orderDetail, List<OrderProductModel> products)
        {
            try
            {
                if (orderDetail.OrderInfo.IsRequireInvoice
                    && (orderDetail.InvoiceInfo == null
                        || string.IsNullOrWhiteSpace(orderDetail.InvoiceInfo.InvoiceTitle)
                        || orderDetail.InvoiceInfo.InvoiceCost <= 0
                        || orderDetail.InvoiceInfo.InvoiceTypeID < 1))
                {
                    return this.Json(new AjaxResponse() { State = -1, Message = "要求开发票，但发票信息不全!!" });
                }

                if (products == null || products.Count < 0)
                {
                    return this.Json(new AjaxResponse { State = -1, Message = "订单商品不能为空!!" });
                }

                var orderService = new OrderService(this.SystemUserSession.EmployeeID);
                List<Order_Product> productList;
                Order order = ResetOrderInfo(orderDetail, products, out productList);

                orderService.EditOrderInfo(
                    order,
                    productList,
                    DataTransfer.Transfer<Order_Invoice>(orderDetail.InvoiceInfo, typeof(OrderInvoiceModel)),
                    DataTransfer.Transfer<User_RecieveAddress>(orderDetail.ReceiverInfo, typeof(UserReceiveAddressModel)));

                return this.Json(new AjaxResponse { State = 1, Message = "操作成功" });
            }
            catch (Exception ex)
            {
                return this.Json(new AjaxResponse { State = -1, Message = ex.Message });
            }
        }

        /// <summary>
        /// 确认并修改订单
        /// </summary>
        /// <param name="orderDetail">
        /// 订单详情对象
        /// </param>
        /// <param name="products">
        /// 订单商品列表
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ConfirmAndEditOrderDetail(OrderDetailModel orderDetail, List<OrderProductModel> products)
        {
            try
            {
                if (orderDetail.OrderInfo.IsRequireInvoice
                    && (orderDetail.InvoiceInfo == null
                        || string.IsNullOrWhiteSpace(orderDetail.InvoiceInfo.InvoiceTitle)
                        || orderDetail.InvoiceInfo.InvoiceCost <= 0
                        || orderDetail.InvoiceInfo.InvoiceTypeID < 1))
                {
                    return this.Json(new AjaxResponse() { State = -1, Message = "要求开发票，但发票信息不全!!" });
                }

                if (products == null || products.Count < 0)
                {
                    return this.Json(new AjaxResponse { State = -1, Message = "订单商品不能为空!!" });
                }

                var orderService = new OrderService(this.SystemUserSession.EmployeeID);
                List<Order_Product> productList;
                Order order = ResetOrderInfo(orderDetail, products,out productList);

                orderService.ManualConfirmOrder(
		            order,
		            productList,
		            DataTransfer.Transfer<Order_Invoice>(orderDetail.InvoiceInfo, typeof(OrderInvoiceModel)),
		            DataTransfer.Transfer<User_RecieveAddress>(orderDetail.ReceiverInfo, typeof(UserReceiveAddressModel)));

                return this.Json(new AjaxResponse { State = 1, Message = "操作成功" });
            }
            catch (Exception ex)
            {
                return this.Json(new AjaxResponse { State = -1, Message = ex.Message });
            }
        }

        /// <summary>
        /// 后台手动完成订单
        /// </summary>
        /// <param name="id">订单编码</param>
        /// <returns>返回结果</returns>
        public ActionResult CompleteOrder(int id)
        {
            string message;
            var result = new OrderService(this.SystemUserSession.SystemUserID, true).CompleteOrder(id, out message);

            if (result)
            {
                return this.Json(new AjaxResponse(1, "签收完成"));
            }
            return this.Json(new AjaxResponse(0, message));
        }

        /// <summary>
        /// 重置订单信息
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="products"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        private Order ResetOrderInfo(OrderDetailModel orderDetail, List<OrderProductModel> products, out List<Order_Product> productList)
        {
            productList = new List<Order_Product>();
            var orginalOrderProducts = new OrderProductService().QueryByOrderId(orderDetail.OrderInfo.ID);

            foreach (var product in products)
            {
                var originalOrderProduct = orginalOrderProducts.FirstOrDefault(p => p.ID == product.ID);
                Order_Product orderProduct = null;
                if (originalOrderProduct != null)
                {
                    originalOrderProduct.TransactPrice = product.TransactPrice;
                    originalOrderProduct.Quantity = product.Quantity;
                    orderProduct = originalOrderProduct;
                }
                else
                {
                    //新增的商品
                    orderProduct = DataTransfer.Transfer<Order_Product>(product, typeof(OrderProductModel));
                }

                productList.Add(orderProduct);
            }

            var order = new Order
                        {
                            CpsID = orderDetail.OrderInfo.CpsID,
                            DeliveryCost = orderDetail.OrderInfo.DeliveryCost,
                            Description = orderDetail.OrderInfo.Description,
                            ID = orderDetail.OrderInfo.ID,
                            IsRequireInvoice = orderDetail.OrderInfo.IsRequireInvoice
                        };
            return order;
        }

        /// <summary>
        /// 设置订单为废单
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SetOrderToInvalid(int orderId, string reason)
        {
            var result = new AjaxResponse();
            try
            {
                var orderService = new OrderService(this.SystemUserSession.EmployeeID);
                orderService.SetInvalidByGJW(orderId, reason);
                result.State = 1;
                result.Message = "修改成功！";
                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result.State = -1;
                result.Message = ex.Message;
				LogUtils.Log("将订单作废操作失败(订单编码：" + orderId + ")，错误消息："+ex.Message+";"+ex.InnerException+";"+ex.StackTrace, "后台作废订单", Category.Error, this.SystemUserSession.SessionID, 0, "Order/SetOrderToInvalid");
                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The get order invoic info.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetOrderInvoicInfo(int orderId)
        {
            var orderInvoiceService = new OrderInvoiceService();
            var result = new AjaxResponse();
            try
            {
                var orderInvoice = orderInvoiceService.SelectByOrderID(orderId);
                if (orderInvoice == null)
                {
                    result.State = 2;
                }
                else
                {
                    result.State = 1;
	                if (string.IsNullOrWhiteSpace(orderInvoice.InvoiceTitle))
	                {
		                orderInvoice.InvoiceTitle = "个人";
	                }

                    result.Data = DataTransfer.Transfer<OrderInvoiceModel>(orderInvoice, typeof(Order_Invoice));
                }
            }
            catch (Exception exception)
            {
                result.State = -1;
                result.Message = exception.Message;
            }

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Methods

        private string BuildOrderSearchCondition(
            string OrderCode,
            string Status,
            string PaymentMethodID,
            string UserName,
            string Consignee,
            string CpsID,
            string StartDateTime,
            string EndDateTime,
            string MinTotalMoney,
			string MaxTotalMoney, string ReceiveMoblie="")
        {
            var searchStringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And UserName like '%" + UserName + "%'"
                        : " UserName like '%" + UserName + "%'");
            }

            if (!string.IsNullOrWhiteSpace(OrderCode))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And OrderCode like '%" + OrderCode + "%'"
                        : " OrderCode like '%" + OrderCode + "%'");
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And Status in (" + Status + ")"
                        : " Status in (" + Status + ")");
            }

            if (!string.IsNullOrWhiteSpace(Consignee))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And Consignee like '%" + Consignee + "%'"
                        : " Consignee like '%" + Consignee + "%'");
            }

            if (!string.IsNullOrWhiteSpace(PaymentMethodID))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0 ? " And PaymentMethodID=" + PaymentMethodID : " PaymentMethodID=" + PaymentMethodID); // 默认设置为未审核状态
            }

            if (!string.IsNullOrWhiteSpace(CpsID))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0 ? " And CpsID=" + CpsID : " CpsID=" + CpsID); // 默认设置为未审核状态
            }

            if (!string.IsNullOrWhiteSpace(StartDateTime))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And CreateTime >= '" + StartDateTime + "'"
                        : " CreateTime >= '" + StartDateTime + "'");
            }

            if (!string.IsNullOrWhiteSpace(EndDateTime))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And CreateTime <= '" + EndDateTime + "'"
                        : " CreateTime <= '" + EndDateTime + "'");
            }

            if (!string.IsNullOrWhiteSpace(MinTotalMoney))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And TotalMoney >= '" + MinTotalMoney + "'"
                        : " TotalMoney >= '" + MinTotalMoney + "'");
            }

            if (!string.IsNullOrWhiteSpace(MaxTotalMoney))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And TotalMoney <= '" + MaxTotalMoney + "'"
                        : " TotalMoney <= '" + MaxTotalMoney + "'");
            }

			if (!string.IsNullOrWhiteSpace(ReceiveMoblie))
			{
				searchStringBuilder.Append(
					searchStringBuilder.Length > 0
						? " And ReceiverMoblie like '%" + ReceiveMoblie + "%'"
						: " ReceiverMoblie like '%" + ReceiveMoblie + "%'");
			}

            return searchStringBuilder.ToString().Length > 0 ? searchStringBuilder.ToString() : null;
        }

        #endregion
    }
}