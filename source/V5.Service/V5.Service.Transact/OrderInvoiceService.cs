// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderInvoiceService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单发票服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单发票服务类
    /// </summary>
    public class OrderInvoiceService
    {
        /// <summary>
        /// 订单发票数据访问对象
        /// </summary>
        private readonly IOrderInvoiceDA orderInvoiceDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderInvoiceService"/> class. 
        /// 实例化订单发票服务对象
        /// </summary>
        public OrderInvoiceService()
        {
            this.orderInvoiceDA = new DAFactoryTransact().CreateOrderInvoiceDA();
        }

        /// <summary>
        /// 查询订单发票
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public Order_Invoice SelectByOrderID(int orderID)
        {
            return this.orderInvoiceDA.SelectByOrderID(orderID);
        }

        /// <summary>
        /// 修改订单发票
        /// </summary>
        /// <param name="orderInvoice">
        /// 订单发票对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        public void Modify(Order_Invoice orderInvoice, SqlTransaction transaction)
        {
            this.orderInvoiceDA.Update(orderInvoice, transaction);
        }

        /// <summary>
        /// 添加一条订单发票
        /// </summary>
        /// <param name="orderInvoice">
        /// 订单发票对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Add(Order_Invoice orderInvoice, SqlTransaction transaction)
        {
            return this.orderInvoiceDA.Insert(orderInvoice, transaction);
        }
    }
}