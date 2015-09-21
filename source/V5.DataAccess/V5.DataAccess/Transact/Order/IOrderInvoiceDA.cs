// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderInvoiceDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单发票数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Data.SqlClient;
    using global::System.Net.Sockets;
    using global::System.Security.Cryptography.X509Certificates;

    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单发票数据访问接口
    /// </summary>
    public interface IOrderInvoiceDA
    {
        /// <summary>
        /// 查询订单发票
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        Order_Invoice SelectByOrderID(int orderID);

        /// <summary>
        /// 新增订单发票
        /// </summary>
        /// <param name="orderInvoice">
        /// 订单发票
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 新增的订单编码
        /// </returns>
        int Insert(Order_Invoice orderInvoice, SqlTransaction transaction);

        /// <summary>
        /// 修改订单发票
        /// </summary>
        /// <param name="orderInvoice">
        /// 订单发票对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        void Update(Order_Invoice orderInvoice, SqlTransaction transaction);
    }
}