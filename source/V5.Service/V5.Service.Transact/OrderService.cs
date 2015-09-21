// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Xml;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
	using V5.DataContract.Transact.Order;
    using V5.DataContract.Transact.ShoppingCart;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Library.Storage.DB;
    using V5.Service.Promote;
    using V5.Service.User;

    /// <summary>
    /// 送货公司访问类
    /// </summary>
    public class OrderService
    {
        #region Constants and Fields

        /// <summary>
        /// 数据访问对象
        /// </summary>
        private readonly IOrderDA orderDA;

        /// <summary>
        /// 用户编码
        /// </summary>
        private readonly int userID;

        private readonly bool isBackage = true;

        #endregion

	    public SqlServer SqlServer {
		    get
		    {
			    return this.orderDA.SqlServer;
		    }
	    }

	    #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="userID">
        /// 职员编码
        /// </param>
        public OrderService(int userID)
        {
            this.userID = userID;
            this.orderDA = new DAFactoryTransact().CreateOrderDA();
        }

        /// <summary>
        /// 实例化订单服务对象
        /// </summary>
        /// <param name="userID">用户编码</param>
        /// <param name="isBackage">当前环境是否为后台</param>
        public OrderService(int userID, bool isBackage)
        {
            this.isBackage = isBackage;
            this.userID = userID;
            this.orderDA = new DAFactoryTransact().CreateOrderDA();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        public OrderService()
        {
            this.orderDA = new DAFactoryTransact().CreateOrderDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 分页查询订单信息
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="rowCount">
        /// 总行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order> Query(Paging paging, out int pageCount, out int rowCount)
        {
            return this.orderDA.Paging(paging, out pageCount, out rowCount);
        }

        /// <summary>
        /// 根据订单编码查询订单
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// The <see cref="Order"/>.
        /// </returns>
        public Order QueryById(int orderId)
        {
            return this.orderDA.SelectByID(orderId);
        }

	    /// <summary>
	    /// 添加订单
	    /// </summary>
	    /// <param name="order">
	    /// 订单兑现
	    /// </param>
	    /// <param name="orderProducts">
	    /// 订单商品列表
	    /// </param>
	    /// <param name="invoice">
	    /// 发票信息
	    /// </param>
	    /// <param name="Coupons">赠送的优惠券信息</param>
	    /// <param name="promotes">订单促销信息</param>
	    /// <returns>
	    /// 已添加订单的编码
	    /// </returns>
		public int Add(Order order, List<Order_Product> orderProducts, Order_Invoice invoice, List<Gift_Coupon> Coupons = null, List<Order_Product_Promote> promotes = null)
        {
            // 1.添加订单
            // 2.添加订单商品
            // 3.添加发票（若有）
            var orderProductService = new OrderProductService();
            SqlTransaction transaction = null;
            int orderId;
            try
			{
				if (order.TotalMoney == 0) //如果0，则认为是前端的订单，需要从新计算金额，不能享受到优惠和促销
				{
					double totalMoney = 0;
					foreach (var orderProduct in orderProducts)
					{
						totalMoney += orderProduct.TransactPrice * orderProduct.Quantity;
					}

					order.TotalMoney = totalMoney;
					order.DeliveryCost = totalMoney >= 100 ? 0 : 10; // todo: 读取数据库配置，设置运费。
				}

				order.TotalIntegral = (int)order.TotalMoney / 1;
				
                orderId = this.orderDA.Insert(order, out transaction);
                order.ID = orderId;

				orderProductService.BatchAddOrderProduct(orderProducts, order.CpsID, orderId, transaction);

                if (invoice != null)
                {
                    invoice.OrderID = orderId;
                    invoice.InvoiceCost = order.TotalMoney;
                    new OrderInvoiceService().Add(invoice, transaction);
                }

				//赠送优惠券
				if (Coupons != null && Coupons.Count > 0)
				{
					foreach (var giftCoupon in Coupons)
					{
						switch (giftCoupon.CouponType)
						{
							case 0:
								//默认新方法的券状态为2，未激活
								new CouponCashBindingService().Add(orderId, giftCoupon.CouponID, this.userID, "购物赠券", 2, transaction);
								break;
							case 1:
								//默认新方法的券状态为2，未激活
								new CouponDecreaseBindingService().Add(orderId, giftCoupon.CouponID, this.userID, "购物赠券", 2, transaction);
								break;
						}
					}
				}

	            if (promotes != null && promotes.Count > 0)
	            {
		            foreach (var promote in promotes)
		            {
			            promote.OrderID = orderId;
		            }

					var rowCount = new OrderProductPromoteService().BatchAdd(promotes, transaction);
					if (rowCount < 1)
					{
						LogUtils.Log("添加订单（订单号:" + order.OrderCode + "）时添加的订单商品促销信息数为：" + rowCount, "添加订单服务层", Category.Info);
					}
	            }

                string remark;

                if (order.PaymentMethodID == 1||order.PaymentMethodID == 2)
                {
                    remark = "添加订单成功，等待确认";
                }
                else
                {
                    remark = order.PaymentStatus == 0 ? "添加订单成功，等待付款" : "添加订单成功，等待确认";
                }

                new OrderStatusTrackingService().Add(
                    new Order_Status_Tracking
                        {
                            OrderID = orderId,
                            EmployeeID = isBackage? this.userID:0,
                            Remark = remark,
                            Status = 0,
                            UserID = order.UserID
                        },
                    transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw;
            }

            // 写订单修改日志
            try
            {
                var orderStatusLogService = new OrderStatusLogService();
                orderStatusLogService.Insert(
                    new Order_Status_Log
                    {
                        EmployeeID = isBackage? this.userID:0, // 若是后台，则填写操作员工编码
                        OrderID = orderId,
                        Remark = order.Description,
                        Status = 0
                    },
                    null);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log(
                    string.Format("后台添加订单--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", order.ID, this.userID),
                    Category.Error,
                    exception);
            }

            return orderId;
        }

        /// <summary>
        /// 手动完成订单（即订单签收）
        /// </summary>
        /// <param name="orderId"></param>
        public bool CompleteOrder(int orderId, out string message)
        {
            var order = this.QueryById(orderId);
            if (order == null)
            {
                message = "订单不存在";
                return false;
            }

            switch (order.Status)
            {
                    //订单状态（100：待付款，0：待确认，1：已确认，2：已发货，3：已签收，4：ERP 作废，5：已损失，6：已取消，8：官网作废）
                case 100:
                    message = "订单是待付款状态，不允许签收。";
                    return false;
                case 0:
                    message = "订单尚未确认，不允许签收。";
                    return false;
                case 1:
                    message = "订单尚未发货，不允许签收。";
                    return false;
                case 2:
                    SqlTransaction transaction = null;
                    try
                    {
                        order.Status = 3;
                        order.PaymentStatus = 1;

                        this.orderDA.SqlServer.BeginTransaction();
                        transaction = this.orderDA.SqlServer.Transaction;

                        this.Edit(order, transaction);

                        new OrderStatusTrackingService().Add(
                            new Order_Status_Tracking
                                {
                                    OrderID = order.ID,
                                    EmployeeID = isBackage ? this.userID : 0,
                                    UserID = isBackage ? 0 : this.userID,
                                    Remark = "订单已签收",
                                    Status = 3,
                                },
                            transaction);

                        transaction.Commit();
                        message = "错误完成";
                        return true;
                    }
                    catch (Exception exception)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        LogUtils.Log(
                            string.Format(
                                "[Order]订单手动签收发生错误，订单号：{0}，操作人:{1},错误详情：{2}，堆栈信息:{3}",
                                orderId,
                                this.userID,
                                exception.Message + "/" + exception.InnerException,
                                exception.StackTrace),
                            "OrderService",
                            Category.Fatal);

                        message = "系统发生错误，详情：" + exception.Message + "/ " + exception.InnerException;
                        return false;
                    }
                    finally
                    {
                        if (transaction != null && transaction.Connection != null
                            && transaction.Connection.State != ConnectionState.Closed)
                        {
                            transaction.Connection.Close();
                        }
                    }
                case 3:
                    message = "订单是已签收。";
                    return true;
                case 4:
                case 6:
                case 8:
                    message = "订单已作废，不允许签收。";
                    return false;
                case 5:
                    message = "订单已损失，不允许签收。";
                    return false;
                default:
                    message = "订单状态未知，不允许签收。";
                    return false;
            }
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="order">
        /// 订单对象
        /// </param>
        /// <param name="orderProducts">
        /// 订单商品列表
        /// </param>
        /// <param name="orderInvoice">
        /// 订单发票
        /// </param>
        /// <param name="userRecieveAddress">
        /// 用户收货地址信息
        /// </param>
        public void ManualConfirmOrder(
            Order order, 
            List<Order_Product> orderProducts, 
            Order_Invoice orderInvoice, 
            User_RecieveAddress userRecieveAddress)
        {
			//todo: 订单数据需要从数据库取一次
            /************
			 * 一、修改订单
             * 1. 修改收货人信息。
             * 2. 修改订单商品信息
             * 3. 修改订单发票信息
             * 4. 修改订单信息 
			 * 二、确认订单
			 * 1.推送到ERP系统
			 * 2.修改订单为确认状态
             * ***********/
            SqlTransaction transaction = null;
            var orderProductService = new OrderProductService();
            Order orignalOrder = this.EditOrderInfo(order, orderProducts, orderInvoice, userRecieveAddress);
            
            switch (orignalOrder.Status)
            {
                case 100:
                    throw new Exception("此订单处于等待支付状态，不允许确认。");
                case 1:
                    throw new Exception("此订单已确认，请不要重复操作！");
                case 2:
                    throw new Exception("此订单已发货");
                case 3:
                    throw new Exception("此订单已签收");
                case 4:
                case 6:
                case 8:
                    throw new Exception("此订单已取消");
                case 5:
                    throw new Exception("此订单已损失");
                case 0:
                    //确认订单
                    try
                    {
                        Order_Payment payment = null;
                        if (order.PaymentStatus == 1)
                        {
                            payment = new OrderPaymentService().QueryByOrderID(order.ID);
                        }

                        orderProducts = orderProductService.QueryByOrderId(order.ID);
                        this.orderDA.SqlServer.BeginTransaction();
                        transaction = this.orderDA.SqlServer.Transaction;
                        this.ConfirmByUpdateOrder(orignalOrder, transaction, payment, orderProducts);

                        new OrderStatusTrackingService().Add(
                            new Order_Status_Tracking
                            {
                                OrderID = order.ID,
                                EmployeeID = isBackage ? this.userID : 0,
                                Remark = "订单已经确认，等待出库",
                                Status = 1,  // 已确认的订单状态为码 1
                                UserID = order.UserID
                            },
                            transaction);

                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        LogUtils.Log(
                            string.Format(
                                "确认订单出错，订单编码{0}，操作人ID：{1},错误信息：{2},内部错误：{3}，堆栈信息：{4}",
                                order.ID,
                                this.userID,
                                exception.Message,
                                exception.InnerException,
                                exception.StackTrace),
                            "确认订单--服务层",
                            Category.Fatal);
                        throw;
                    }

                    // 写订单修改日志
                    try
                    {
                        var orderStatusLogService = new OrderStatusLogService();
                        orderStatusLogService.Insert(
                            new Order_Status_Log
                            {
                                EmployeeID = this.userID,
                                OrderID = order.ID,
                                Remark = order.Description,
                                Status = 1
                            },
                            null);
                    }
                    catch (Exception exception)
                    {
                        TextLogger.Instance.Log(
                            string.Format("后台订单确认--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", order.ID, this.userID),
                            Category.Error,
                            exception);
                    }
                    break;
                default:
                    throw new Exception("订单状态异常。");
            }
        }

        public Order EditOrderInfo(
            Order order,
            List<Order_Product> orderProducts,
            Order_Invoice orderInvoice,
            User_RecieveAddress userRecieveAddress)
        {
            SqlTransaction transaction = null;
            var orignalOrder = this.QueryById(order.ID);

            //int rowCount, pageCount;
            if (orignalOrder == null)
            {
                throw new ArgumentException("订单获取错误");
            }

            try
            {
                orignalOrder.CpsID = order.CpsID;
                orignalOrder.DeliveryCost = order.DeliveryCost;
                orignalOrder.Description = order.Description;
                orignalOrder.IsRequireInvoice = order.IsRequireInvoice;

                new UserReceiveAddressService().Modify(userRecieveAddress, out transaction);
                new OrderProductService().ModifyOrderProductByOrderID(orderProducts, orignalOrder.CpsID, order.ID, transaction);

                if (order.IsRequireInvoice)
                {
                    orderInvoice.OrderID = order.ID;
                    if (orderInvoice.ID > 0)
                    {
                        new OrderInvoiceService().Modify(orderInvoice, transaction);
                    }
                    else
                    {
                        new OrderInvoiceService().Add(orderInvoice, transaction);
                    }
                }

                foreach (var orderProduct in orderProducts)
                {
                    order.TotalMoney += orderProduct.TransactPrice * orderProduct.Quantity;
                }

                order.TotalIntegral = (int)order.TotalMoney;
                orignalOrder.TotalIntegral = order.TotalIntegral;
                orignalOrder.TotalMoney = order.TotalMoney;
                this.Edit(orignalOrder, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                LogUtils.Log(
                    string.Format(
                        "确认订单是修改订单信息出错，订单编码{0}，操作人ID：{1},错误信息：{2}，堆栈信息：{3}",
                        order.ID,
                        this.userID,
                        ex.Message,
                        ex.StackTrace),
                    "确认订单--服务层",
                    Category.Fatal);

                throw;
            }

            return orignalOrder;
        }

        private bool PushOrderToHwErp(Order order, List<Order_Product> orderProducts, SqlTransaction transaction, out string errorMsg, Order_Payment payment = null)
	    {
		    #region 订单推送ERP

		    #region 订单基本信息

			errorMsg = string.Empty;

		    var hi = new HwRest.HwOrderInfo();

		    hi.orderNumber = order.OrderCode;
			hi.orderDate = order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
		    if (order.PaymentStatus == 1)
		    {
			    //var orderPayment = new OrderPaymentService().QueryByOrderID(order.ID);
			    //if (orderPayment != null) hi.payTime = Convert.ToDateTime(orderPayment.CreateTime).ToString();

				//todo:若订单已在线支付，则认为是当前时间支付，因为在线支付订单是有系统在支付时自动确认的。当时不够准确。
			    hi.payTime = payment == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : payment.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
		    }

		    if (string.IsNullOrWhiteSpace(hi.payTime))
		    {
			    hi.payTime = "";
		    }
			
		    var orderReceiveAddress = new UserReceiveAddressService().QueryByID(order.RecieveAddressID);

			hi.buyerNick = string.IsNullOrWhiteSpace(order.UserName) ? orderReceiveAddress.Consignee : order.UserName;
			hi.totalAmount = Math.Round(order.TotalMoney + order.DeliveryCost, 2).ToString();
			hi.payment = payment == null
				             ? (order.TotalMoney + order.DeliveryCost).ToString()
				             : Math.Round(payment.PaymentMoney, 2).ToString();
		    hi.postAmount = order.DeliveryCost.ToString();
		    hi.discount = order.Discount.ToString();
		    hi.points = "";
		    hi.pointsAmount = "";
		    hi.couponsAmount = "";
		    hi.virtualAmount = "";
		    hi.fullMinus = "";
		    var orderInvoice = new OrderInvoiceService().SelectByOrderID(order.ID);
		    hi.invoiceTitle = orderInvoice != null ? orderInvoice.InvoiceTitle : "";
		    hi.invoiceContent = orderInvoice != null ? orderInvoice.InvoiceContent : "";
		    hi.invoiceAmount = orderInvoice != null ? orderInvoice.InvoiceCost.ToString() : "";
			hi.tradeFrom = "官网订单";

		    if (order.PaymentMethodID == 0)
		    {
			    if (payment != null)
			    {
				    hi.paymentType = payment.PaymentOrgName;
			    }
			    else
			    {
				    hi.paymentType = "网上支付";
			    }
			    hi.sellerMemo = "";
		    }
		    else
		    {
			    hi.paymentType = "货到付款";
			    hi.sellerMemo = "";
		    }

		    hi.consignee = orderReceiveAddress != null ? orderReceiveAddress.Consignee : order.UserName;
		    if (orderReceiveAddress != null)
		    {
			    if (!string.IsNullOrWhiteSpace(orderReceiveAddress.CountyName(",")))
			    {
				    var strs = orderReceiveAddress.CountyName(",").Split(',');
				    if (strs.Length == 3)
				    {
					    hi.province = strs[0];
					    hi.city = strs[1];
					    hi.cityarea = strs[2];
				    }
				    else
				    {
					    hi.cityarea = orderReceiveAddress.CountyName(",");
					    hi.province = "";
					    hi.city = "";
				    }
			    }
		    }
		    else
		    {
			    hi.cityarea = "";
			    hi.province = "";
			    hi.city = "";
		    }

		    hi.address = orderReceiveAddress != null ? orderReceiveAddress.Address : "";
		    hi.mobilePhone = orderReceiveAddress != null ? orderReceiveAddress.Mobile : "";
		    hi.telephone = orderReceiveAddress != null ? orderReceiveAddress.Tel : "";
		    hi.zip = orderReceiveAddress != null ? orderReceiveAddress.ZipCode : "";
		    hi.buyerMessage ="客户备注："+ order.Remark + "；客服备注：" + order.Description;

		    #endregion

		    #region 商品列表

		    List<HwRest.HwProductList> hplist = new List<HwRest.HwProductList>();
		    if (orderProducts == null || orderProducts.Count < 1)
		    {
			    orderProducts = new OrderProductService().QueryByOrderId(order.ID);
		    }

		    foreach (var orderProduct in orderProducts)
		    {
			    var hwProduct =
				    hplist.FirstOrDefault(p => p.productNumber == orderProduct.Barcode && orderProduct.TransactPrice <= 0);

				//赠品与正常商品合并
			    if (hwProduct != null)
			    {
				    hwProduct.orderCount = (int.Parse(hwProduct.orderCount) + orderProduct.Quantity).ToString();
				    hwProduct.giftCount = (int.Parse(hwProduct.giftCount) + orderProduct.Quantity).ToString();
					//将商品价格当做优惠金额进行处理
				    hwProduct.discountFee = (int.Parse(hwProduct.discountFee) + int.Parse(hwProduct.price) * orderProduct.Quantity).ToString();
			    }
			    else
			    {
					HwRest.HwProductList hpl = new HwRest.HwProductList();
					hpl.productNumber = orderProduct.Barcode; //新系统中没有商品编号，此处设为商品条形码
					hpl.productName = orderProduct.ProductName;
					hpl.skuNumber = orderProduct.Barcode;
					hpl.skuName = "";
					hpl.price = orderProduct.TransactPrice.ToString();
					hpl.orderCount = orderProduct.Quantity.ToString();
					hpl.giftCount = (orderProduct.TransactPrice > 0 ? 0 : orderProduct.Quantity).ToString();
					hpl.amount = (orderProduct.TransactPrice * orderProduct.Quantity).ToString();
					hpl.memo = orderProduct.TransactPrice > 0 ? "" : "【赠品】";
					hpl.discountFee = "0";
					hpl.barcode = orderProduct.Barcode;
					hplist.Add(hpl);
			    }
		    }

		    #endregion

		    HwRest.OWebOrderItems OWebOrderItems = new HwRest.OWebOrderItems();
		    OWebOrderItems.OWebOrderItem = hplist.ToArray();

		    HwRest.HwOrderAdd_Info hai = new HwRest.HwOrderAdd_Info();
		    hi.OWebOrderItems = OWebOrderItems;
		    hai.OWebOrder = hi;

		    HwRest.HwClient HwClient = new HwRest.HwClient();
		    HwClient.XmlValues = hai.ToXmlParameter();
		    HwClient.OrderNumber = hi.orderNumber;
			string HwBackXml = HwClient.Execute();

		    XmlDocument doc = new XmlDocument();
		    doc.LoadXml(HwBackXml);
		    XmlNode root = doc.SelectSingleNode("//Response");

		    var log = new Order_Erp_Log
			              {
				              ERP = "HW_ERP",
				              OrderID = order.ID,
				              OperateType = 1,
				              ReqContent = HwClient.XmlValues,
				              ResContent = HwBackXml,
				              UserID = order.UserID,
				              Operator = isBackage ? this.userID : 0,
				              CreateTime = DateTime.Now
			              };

			if (payment != null)
			{
				log.ExtField = "在线支付订单，支付时间：" + payment.CreateTime;
			}

		    var result = false;

		    if (HwBackXml.Contains("推单异常") || root.FirstChild.InnerText.ToLower() != "true")
		    {
			    log.IsSuccess = false;

				//获取退单错误信息
				var errorNode = root.SelectSingleNode("//value");

				if (errorNode != null)
				{
					errorMsg = errorNode.InnerText;
				}

				result = false;

				LogUtils.Log("订单推送失败，订单编码：" + order.ID + "，错误消息：" + HwBackXml, "订单推送ERP", Category.Error);
		    }
		    else
		    {
			    log.IsSuccess = true;
			    result = true;
			    LogUtils.Log("订单推送成功，订单编码：" + order.ID, "订单推送ERP");
		    }

			try
			{
				var orderErpLogService = new OrderERPLogService();
				orderErpLogService.Add(log, null); //添加日志，无需事务，防止回滚
			}
			catch(Exception exception)
			{
				LogUtils.Log(
					string.Format(
						"[Order_ERP]订单推送成功，但是写入日志信息发生错误。订单编号:{0},错误信息:{1}/{2}",
						order.OrderCode,
						exception.Message,
						exception.InnerException),
					"[Order_ERP]订单推送",
					Category.Error);
			}
			
		    return result;

		    #endregion
	    }

	    /// <summary>
        /// 判断订单是否能够由系统审核
        /// </summary>
        /// <param name="order">
        /// 订单对象
        /// </param>
        /// <returns>
        /// 是否能够被系统审核。可以：True，否则：false
        /// </returns>
        public bool ValidateConfirmOrderBySystem(Order order)
        {
            /*
             *符合系统审核订单的条件：
             *1. 在线支付，且已付款。
             *2. 货到付款，且总金额小于1000.待添加
             */
		    return (order.PaymentMethodID == 0 && order.PaymentStatus == 1);
		    //|| (order.PaymentMethodID == 1 && order.TotalMoney < 1000);
        }

        /// <summary>
        /// 系统自动确认订单
        /// </summary>
        /// <param name="order">
        /// 订单对象
        /// </param>
        /// <returns>
        /// 系统确认是否成功
        /// </returns>
		public bool ConfirmOrderBySystem(Order order, Order_Payment payment = null)
        {
            /**
             * 系统自动审核订单步骤
             * 1. 检查是否符合自动审核条件
             * 2. 改变订单状态至已确认（1）
             * 3. 添加订单跟踪信息
             * **/

            if (!this.ValidateConfirmOrderBySystem(order))
            {
				LogUtils.Log(
							string.Format("系统自动确认订单失败，原因：不符合条件。订单编码:{0}", order.ID),
							"检查订单自动确认是否符合条件：ConfirmOrderBySystem",
							Category.Info,
							null,
							this.userID);

                TextLogger.Instance.Log("系统自动确认订单失败，原因：不符合条件", Category.Warn, null);
                return false;
            }

	        LogUtils.Log(
		        string.Format("检查订单符合系统自动确认条件。订单编码:{0}", order.ID),
		        "检查订单自动确认是否符合条件：ConfirmOrderBySystem",
		        Category.Info,
		        null,
		        this.userID);

            SqlTransaction transaction = null;

            try
            {
                this.orderDA.SqlServer.BeginTransaction();
                transaction = this.orderDA.SqlServer.Transaction;

				this.ConfirmByUpdateOrder(order, transaction, payment);

                new OrderStatusTrackingService().Add(
                    new Order_Status_Tracking
                    {
                        OrderID = order.ID,
                        EmployeeID = 0,
                        Remark = "订单已经确认，等待出库",
                        Status = 1,  // 已确认的订单状态为码 1
                        UserID = 0
                    },
                    transaction);
                transaction.Commit();

	            LogUtils.Log(
		            string.Format("系统自动确认订单成功。订单编码:{0}", order.ID),
		            "系统自动确认订单：ConfirmOrderBySystem",
		            Category.Info,
		            null,
		            this.userID);

                return true;
            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

	            LogUtils.Log(
		            string.Format(
			            "系统自动确认订单发生错误，需客服手动确认。订单编码:{0}，错误信息：{1},堆栈:{2}",
			            order.ID,
			            exception.Message,
			            exception.StackTrace),
		            "系统自动确认订单：ConfirmOrderBySystem",
		            Category.Error,
		            null,
		            this.userID);

                TextLogger.Instance.Log("系统自动确认订单失败，需客服手动确认", Category.Error, exception);

                return false;
            }
        }

	    /// <summary>
	    /// 确认订单--修改订单信息
	    /// </summary>
	    /// <param name="order">
	    /// 订单对象
	    /// </param>
	    /// <param name="transaction">
	    /// 事务对象
	    /// </param>
	    /// <param name="orderProducts"></param>
	    public void ConfirmByUpdateOrder(Order order, SqlTransaction transaction, Order_Payment orderPayment, List<Order_Product> orderProducts = null)
        {
            this.orderDA.UpdateForConfirmOrder(order, transaction);

		    if (Utils.IsPushERP) //判断是否推送ERP
			{
				string errorMsg;
				if (!this.PushOrderToHwErp(order, orderProducts, transaction, out errorMsg, orderPayment))
				{
					throw new Exception("订单推送失败，原因：" + errorMsg); //推送失败，主动抛出异常，让外边的程序进行回滚
				}
		    }
        }

        /// <summary>
        /// 修改订单客服备注信息
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <param name="description">
        /// 客服备注信息
        /// </param>
        public void ModifyOrderDescription(int orderID, string description)
        {
            this.orderDA.UpdateDescription(orderID, description);

            // 写订单修改日志
            try
            {
                var orderStatusLogService = new OrderStatusLogService();
                orderStatusLogService.Insert(
                    new Order_Status_Log
                    {
                        EmployeeID = this.userID,
                        OrderID = orderID,
                        Remark = description,
                        Status = this.orderDA.SelectByID(orderID).Status
                    },
                    null);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log(
                    string.Format("后台--修改订单备注信息--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", orderID, this.userID),
                    Category.Error,
                    exception);
            }
        }

        /// <summary>
        /// 添加用户基本信息和收件地址
        /// </summary>
        /// <param name="user">
        /// 用户对象
        /// </param>
        public void AddUserInfo(User user)
        {
            if (user != null)
            {
                var userService = new UserService();
                var userReceiceAddressService = new UserReceiveAddressService();
                SqlTransaction sqlTransact = null;
                try
                {
                    var userId = userService.AddUser(user, out sqlTransact);
                    var userReceiveAddress = new User_RecieveAddress
                    {
                        UserID = userId, 
                        CountyID = user.CountyID, 
                        Address = user.Address, 
                        Consignee = user.Name, 
                        Mobile = user.Mobile, 
                        Tel = user.Tel, 
                        IsDefault = true, 
                        CreateTime = DateTime.Now
                    };

                    userReceiceAddressService.Add(userReceiveAddress, sqlTransact);
                    sqlTransact.Commit();
                    user.ID = userId;
                }
                catch
                {
                    if (sqlTransact != null)
                    {
                        sqlTransact.Rollback();
                    }

                    throw;
                }
            }
        }

        /// <summary>
        /// 设置订单为废单
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <param name="reason">
        /// 废单原因
        /// </param>
        public void SetInvalidByGJW(int orderId, string reason)
        {
            SqlTransaction transaction = null;
            try
            {
                this.orderDA.UpdateStatus(orderId, 8, reason, out transaction);
                this.orderDA.RecoverProductsInventory(orderId, transaction);
                new OrderStatusTrackingService().Add(
                    new Order_Status_Tracking
                    {
                        OrderID = orderId,
                        EmployeeID = isBackage? this.userID:0,
                        Remark = "订单作废，原因："+reason,
                        Status = 8
                    },
                    transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw;
            }

            // 写订单修改日志
            try
            {
                var orderStatusLogService = new OrderStatusLogService();
                orderStatusLogService.Insert(
                    new Order_Status_Log
                    {
                        EmployeeID = this.userID,
                        OrderID = orderId,
                        Remark = reason,
                        Status = 8
                    },
                    null);

            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log(
                    string.Format("后台设为废单--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", orderId, this.userID),
                    Category.Error,
                    exception);
            }
        }

		/// <summary>
		/// 设置订单为废单
		/// </summary>
		/// <param name="orderId">
		/// 订单编码
		/// </param>
		/// <param name="reason">
		/// 废单原因
		/// </param>
		public void SetInvalidByERP(int orderId, string reason)
		{
			SqlTransaction transaction = null;
			try
			{
				this.orderDA.UpdateStatus(orderId, 4, reason, out transaction);
				this.orderDA.RecoverProductsInventory(orderId, transaction);
				new OrderStatusTrackingService().Add(
					new Order_Status_Tracking
					{
						OrderID = orderId,
						EmployeeID = isBackage ? this.userID : 0,
						Remark = "订单作废，原因："+reason,
						Status = 4
					},
					transaction);
				transaction.Commit();
			}
			catch (Exception)
			{
				if (transaction != null)
				{
					transaction.Rollback();
				}

				throw;
			}

			// 写订单修改日志
			try
			{
				var orderStatusLogService = new OrderStatusLogService();
				orderStatusLogService.Insert(
					new Order_Status_Log
					{
						EmployeeID = this.userID,
						OrderID = orderId,
						Remark = reason,
						Status = 4
					},
					null);

			}
			catch (Exception exception)
			{
				TextLogger.Instance.Log(
					string.Format("ERP订单作废--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", orderId, this.userID),
					Category.Error,
					exception);

				LogUtils.Log(
					string.Format("ERP订单作废--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}），错误消息："+exception.Message+";InnerMessage："+exception.InnerException, orderId, this.userID),
					"订单作废",
					Category.Error);
			}
		}

        /// <summary>
        /// 后台取消未付款未发货订单
        /// </summary>
        /// <param name="orderCancel">
        /// The order cancel.
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
        /// </returns>
        public int OrderCancelByBackstage(Order_Cancel orderCancel)
        {
            var state = new OrderCancelService().OrderCancel(orderCancel);
            if (state == 1)
            {
                // 取消成功时，写订单修改日志
                try
                {
                    var orderStatusLogService = new OrderStatusLogService();
                    orderStatusLogService.Insert(
                        new Order_Status_Log
                        {
                            EmployeeID = orderCancel.EmployeeID,
                            OrderID = orderCancel.OrderID,
                            Remark = orderCancel.Description,
                            Status = 6
                        },
                        null);
                }
                catch (Exception exception)
                {
                    TextLogger.Instance.Log(
                        string.Format("后台取消订单--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", orderCancel.OrderID, orderCancel.EmployeeID),
                        Category.Error,
                        exception);
                }
            }

            return state;
        }

        /// <summary>
        /// 前台取消未付款未发货订单
        /// </summary>
        /// <param name="orderCancel">
        /// The order cancel.
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
        /// </returns>
        public int OrderCancelByFront(Order_Cancel orderCancel)
        {
            var state = new OrderCancelService().OrderCancel(orderCancel);
            return state;
        }

        /// <summary>
        /// 后台取消已付款未发货订单
        /// </summary>
        /// <param name="orderCancelCauseID">
        /// 订单取消原因编码
        /// </param>
        /// <param name="cancelDescription">
        /// 订单取消备注
        /// </param>
        /// <param name="refund">
        /// 取消对象
        /// </param>
        /// <returns>
        /// 操作状态
        /// </returns>
        public int OrderCancelRefundByBackstage(
            int orderCancelCauseID,
            string cancelDescription,
            Aftersale_Refund refund)
        {
            var state = new OrderCancelService().OrderCancelRefundByBackstage(
                orderCancelCauseID,
                cancelDescription,
                refund);
            if (state == 1)
            {
                // 取消成功时，写订单修改日志
                try
                {
                    var orderStatusLogService = new OrderStatusLogService();
                    orderStatusLogService.Insert(
                        new Order_Status_Log
                        {
                            EmployeeID = refund.EmployeeID,
                            OrderID = refund.OrderID,
                            Remark = cancelDescription,
                            Status = 6
                        },
                        null);
                }
                catch (Exception exception)
                {
                    TextLogger.Instance.Log(
                        string.Format("后台取消订单--订单状态日志写入错误.（订单编码：{0}，操作员编码:{1}）", refund.OrderID, refund.EmployeeID),
                        Category.Error,
                        exception);
                }
            }

            return state;
        }

        /// <summary>
        /// 获取订单真实已支付金额（订单总金额-被抵扣的金额）
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 已支付金额
        /// </returns>
        public double GetOrderActualPayment(int orderId)
        {
            return this.orderDA.SelectOrderActualPayment(orderId);
        }
        #endregion

        /// <summary>
        /// 查询用户的订单列表信息
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <returns>查询结果</returns>
        public List<Order> QueryByUserID(int userId)
        {
            return this.orderDA.SelectByUserID(userId);
        }

        public Order QueryByOrderCode(string orderCode)
        {
            return this.orderDA.SelectByOrderCode(orderCode);
        }

        public void Edit(Order order,SqlTransaction transaction)
        {
            this.orderDA.Update(order, transaction);
        }

	    /// <summary>
	    /// 订单在线支付
	    /// </summary>
	    /// <param name="order">订单对象</param>
	    /// <param name="totalFee">支付金额</param>
	    /// <param name="orderOrgID">支付方式（支付宝，财富通，工行，农行等）</param>
	    /// <param name="tradeNo">支付交易号</param>
	    /// <returns>返回修改订单成功与否</returns>
	    public bool OrderOnLinePayment(Order order, double totalFee, int orderOrgID, string tradeNo)
        {
            //支付成功，改写数据库订单信息
            //添加支付记录信息
            //添加订单状态跟踪信息

		    LogUtils.Log(
			    string.Format("系统更新在线支付订单信息，订单编码:{0},支付平台编码：{1}，支付交易号：{2}", order.ID, orderOrgID, tradeNo),
			    "OrderOnLinePayment",
			    Category.Info,
			    null,
			    this.userID);

            order.Status = order.Status == 100 ? 0 : order.Status;
            this.orderDA.SqlServer.BeginTransaction();
            var transaction = this.orderDA.SqlServer.Transaction;
            try
            {
                this.Edit(order, transaction);
                var orderPaymentDa = new DAFactoryTransact().CreateOrderPaymentDA();
                var payment = new Order_Payment
                                  {
                                      OrderID = order.ID,
                                      IsDelete = 0,
                                      IsUseAccount = false,
                                      IsUseCoupon = false,
                                      IsUseIntegral = false,
                                      PaymentMoney = totalFee,
                                      TradeNo = tradeNo,
                                      PaymentOrgID = orderOrgID,
									  CreateTime = DateTime.Now
                                  };
                orderPaymentDa.Insert(payment, transaction);

                new OrderStatusTrackingService().Add(
                    new Order_Status_Tracking
                    {
                        OrderID = order.ID,
                        EmployeeID = isBackage ? this.userID : 0,
                        UserID = isBackage?0:this.userID,
                        Remark = "订单已支付",
                        Status = order.Status
                    },
                    transaction);
                transaction.Commit();

				//检查是否可以由系统自动确认订单
	            try
	            {
		            if (!this.ConfirmOrderBySystem(order,payment))
		            {
			            LogUtils.Log(
				            string.Format("在线支付成功后，订单自动确认失败，订单编码:{0},支付平台编码：{1}，支付交易号：{2}", order.ID, orderOrgID, tradeNo),
				            "在线支付订单状态更新",
				            Category.Warn,
				            null,
				            this.userID);
		            }
		            else
		            {
						LogUtils.Log(
							string.Format("在线支付成功后，订单自动确认成功，订单编码:{0},支付平台编码：{1}，支付交易号：{2}", order.ID, orderOrgID, tradeNo),
							"在线支付订单状态更新",
							Category.Info,
							null,
							this.userID);
		            }
	            }
	            catch (Exception ex)
	            {
					LogUtils.Log(
							string.Format("在线支付成功后，订单自动确认时出错，订单编码:{0},支付平台编码：{1}，支付交易号：{2}，错误信息：{3}，堆栈：{4}", order.ID, orderOrgID, tradeNo,ex.Message,ex.StackTrace),
							"在线支付订单状态更新",
							Category.Error,
							null,
							this.userID);
	            }

                return true;
            }
            catch(Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    if (transaction.Connection!=null&&transaction.Connection.State != ConnectionState.Closed)
                    {
                        transaction.Connection.Close();
                    }
                }

                TextLogger.Instance.Log("更新订单支付信息出错~~", Category.Error, exception);
                LogUtils.Log(
                    string.Format(
                        "用户，UserID={0}已支付订单{1}，处理订单状态是出错。错误信息：{2}",
                        this.userID,
                        order.OrderCode,
                        exception.Message),
                    "UpdatePayment",
                    Category.Error);
                return false;
            }
        }
	}
}