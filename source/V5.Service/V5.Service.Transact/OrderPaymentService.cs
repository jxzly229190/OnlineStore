namespace V5.Service.Transact
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    public class OrderPaymentService
    {
        private readonly IOrderPaymentDA orderPaymentDA;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public OrderPaymentService()
        {
            orderPaymentDA = new DAFactoryTransact().CreateOrderPaymentDA();
        }

        /// <summary>
        /// 根据第三方交易号查询订单支付信息
        /// </summary>
        /// <param name="tradeNo">交易号</param>
        /// <returns>订单支付信息</returns>
        public List<Order_Payment> QueryByTradeNo(string tradeNo)
        {
            if (tradeNo == null)
            {
                return new List<Order_Payment>();
            }

            return this.orderPaymentDA.SelectByTradeNo(tradeNo);
        }

	    public Order_Payment QueryByOrderID(int orderID)
	    {
		    if (orderID > 0)
		    {
			    return this.orderPaymentDA.SelectByOrderID(orderID);
		    }

		    return null;
	    }
    }
}