namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;

    public interface IOrderPaymentDA
    {
        int Insert(Order_Payment orderPayment,SqlTransaction transaction);

        List<Order_Payment> SelectByTradeNo(string tradeNo);

	    /// <summary>
	    /// 查询订单支付信息
	    /// </summary>
	    /// <param name="orderID">订单编码</param>
	    /// <returns>订单支付信息</returns>
	    Order_Payment SelectByOrderID(int orderID);
    }
}