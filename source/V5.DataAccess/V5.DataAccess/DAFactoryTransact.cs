// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryTransact.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   交易模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.Transact;
    using V5.DataAccess.Transact.Order;

    /// <summary>
    /// 交易模块数据访问工厂类
    /// </summary>
    public class DAFactoryTransact : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryTransact"/> class.
        /// </summary>
        public DAFactoryTransact()
        {
            this.AssemblyPath = this.AssemblyPath + ".Transact";
        }

        /// <summary>
        /// The create user da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICpsDA"/>.
        /// </returns>
        public ICpsDA CreateCpsDA()
        {
            string nameSpace = AssemblyPath + ".CpsDA";
            object cpsDA = Create(AssemblyPath, nameSpace);
            return (ICpsDA)cpsDA;
        }

		/// <summary>
		/// 创建CPS链接记录数据库操作对象
		/// </summary>
		/// <returns></returns>
		public ICpsLinkRecordDA CreateCpsLinkRecordDA()
		{
			string nameSpace = AssemblyPath + ".CpsLinkRecordDA";
			object cpsDA = Create(AssemblyPath, nameSpace);
			return (ICpsLinkRecordDA)cpsDA;
		}

        /// <summary>
        /// The create cps commission ratio da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICpsDA"/>.
        /// </returns>
        public ICpsCommissionRatioDA CreateCpsCommissionRatioDA()
        {
            string nameSpace = AssemblyPath + ".CpsCommissionRatioDA";
            object cpsCommissionRatioDA = Create(AssemblyPath, nameSpace);
            return (ICpsCommissionRatioDA)cpsCommissionRatioDA;
        }

        /// <summary>
        /// 创建商品评价数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IProductCommentDA"/>.
        /// </returns>
        public IProductCommentDA CreateProductCommentDA()
        {
            string nameSpace = AssemblyPath + ".ProductCommentDA";
            object productCommentDA = Create(AssemblyPath, nameSpace);
            return (IProductCommentDA)productCommentDA;
        }

        /// <summary>
        /// 创建商品评价回复数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IProductCommentReplyDA"/>.
        /// </returns>
        public IProductCommentReplyDA CreateProductCommentReplyDA()
        {
            string nameSpace = AssemblyPath + ".ProductCommentReplyDA";
            object da = Create(AssemblyPath, nameSpace);
            return (IProductCommentReplyDA)da;
        }

        /// <summary>
        /// 创建商品咨询数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IProductConsultDA"/>.
        /// </returns>
        public IProductConsultDA CreateProductConsultDA()
        {
            string nameSpace = AssemblyPath + ".ProductConsultDA";
            object da = Create(AssemblyPath, nameSpace);
            return (IProductConsultDA)da;
        }

        /// <summary>
        /// 创建订单数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IOrderDA"/>.
        /// </returns>
        public IOrderDA CreateOrderDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderDA";
            object orderDA = Create(AssemblyPath, nameSpace);
            return (IOrderDA)orderDA;
        }

        /// <summary>
        /// 创建订单商品数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IOrderProductDA"/>.
        /// </returns>
        public IOrderProductDA CreateOrderProductDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderProductDA";
            object orderProductDA = Create(AssemblyPath, nameSpace);
            return (IOrderProductDA)orderProductDA;
        }

        /// <summary>
        /// 创建订单发票数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IOrderInvoiceDA"/>.
        /// </returns>
        public IOrderInvoiceDA CreateOrderInvoiceDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderInvoiceDA";
            object orderInvoiceDA = Create(AssemblyPath, nameSpace);
            return (IOrderInvoiceDA)orderInvoiceDA;
        }

        /// <summary>
        /// 创建订单状态日志数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IOrderStatusLogDA"/>.
        /// </returns>
        public IOrderStatusLogDA CreateOrderStatusLogDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderStatusLogDA";
            object orderStatusLogDA = Create(AssemblyPath, nameSpace);
            return (IOrderStatusLogDA)orderStatusLogDA;
        }

        /// <summary>
        /// 创建订单配送流转明细数据访问对象
        /// </summary>
        /// <returns>
        /// The <see cref="IOrderDeliveryTrackDetailDA"/>.
        /// </returns>
        public IOrderDeliveryTrackDetailDA CreateOrderDeliveryTrackDetailDA()
        {
			string nameSpace = AssemblyPath + ".Order.OrderDeliveryTrackDetailDA";
            object orderDeliveryTrackDtrailDA = Create(AssemblyPath, nameSpace);
            return (IOrderDeliveryTrackDetailDA)orderDeliveryTrackDtrailDA;
        }

        /// <summary>
        /// 创建订单状态跟踪数据库访问对象
        /// </summary>
        /// <returns>
        /// 订单状态跟踪数据库访问对象
        /// </returns>
        public IOrderStatusTrackingDA CreateOrderStatusTrackingDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderStatusTrackingDA";
            object orderStatusTrackingDA = Create(AssemblyPath, nameSpace);
            return (IOrderStatusTrackingDA)orderStatusTrackingDA;
        }

        /// <summary>
        /// 创建订单取消原因数据库访问对象
        /// </summary>
        /// <returns>
        /// 数据库访问对象
        /// </returns>
        public IOrderCancelCauseDA CreateOrderCancelCauseDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderCancelCauseDA";
            object orderCancelCauseDA = Create(AssemblyPath, nameSpace);
            return (IOrderCancelCauseDA)orderCancelCauseDA;
        }

        /// <summary>
        /// 创建订单取消数据库访问对象
        /// </summary>
        /// <returns>
        /// 数据库访问对象
        /// </returns>
        public IOrderCancelDA CreateOrderCancelDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderCancelDA";
            object orderCancelDA = Create(AssemblyPath, nameSpace);
            return (IOrderCancelDA)orderCancelDA;
        }

        /// <summary>
        /// 创建订单支付数据库访问对象
        /// </summary>
        /// <returns>
        /// 数据库访问对象
        /// </returns>
        public IOrderPaymentDA CreateOrderPaymentDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderPaymentDA";
            object orderPaymentDA = Create(AssemblyPath, nameSpace);
            return (IOrderPaymentDA)orderPaymentDA;
        }

        /// <summary>
        /// 创建订单结算数据库访问对象
        /// </summary>
        /// <returns>
        /// 数据库访问对象
        /// </returns>
        public IOrderBillDA CreateOrderBillDA()
        {
            string nameSpace = AssemblyPath + ".Order.OrderBillDA";
            object orderBillDA = Create(AssemblyPath, nameSpace);
            return (IOrderBillDA)orderBillDA;
        }

		/// <summary>
		/// 创建订单ERP交互日志数据库访问对象
		/// </summary>
		/// <returns>
		/// 数据库访问对象
		/// </returns>
		public IOrderErpLogDA CreateOrderErpLogDA()
		{
			string nameSpace = AssemblyPath + ".Order.OrderErpLogDA";
			object orderErpLogDA = Create(AssemblyPath, nameSpace);
			return (IOrderErpLogDA)orderErpLogDA;
		}

		/// <summary>
		/// 创建订单商品促销数据库访问对象
		/// </summary>
		/// <returns>
		/// 数据库访问对象
		/// </returns>
		public IOrderProductPromoteDA CreateOrderProductPromoteDA()
		{
			string nameSpace = AssemblyPath + ".Order.OrderProductPromoteDA";
			object da = Create(AssemblyPath, nameSpace);
			return (IOrderProductPromoteDA)da;
		}
    }
}