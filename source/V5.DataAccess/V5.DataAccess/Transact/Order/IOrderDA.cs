// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单数据访问接口
    /// </summary>
    public interface IOrderDA
    {
        /// <summary>
        /// Gets the sql server.
        /// </summary>
        SqlServer SqlServer { get; }

        #region Public Methods and Operators
        
        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总行数
        /// </param>
        /// <returns>
        /// 查询订单结果列表
        /// </returns>
        List<DataContract.Transact.Order.Order> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order">
        /// 订单兑现
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 已添加订单的编码
        /// </returns>
        int Insert(V5.DataContract.Transact.Order.Order order, out SqlTransaction transaction);

        /// <summary>
        /// 根据订单编码更新订单备注信息
        /// </summary>
        /// <param name="orderID">订单编码</param>
        /// <param name="description">备注信息</param>
        void UpdateDescription(int orderID, string description);

        /// <summary>
        /// 根据编码查询订单信息
        /// </summary>
        /// <param name="id">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 订单对象
        /// </returns>
        V5.DataContract.Transact.Order.Order SelectByID(int id);

        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="order">
        /// 订单对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        void UpdateForConfirmOrder(Order order, SqlTransaction transaction);

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <param name="status">
        /// 新状态
        /// </param>
        /// <param name="reason">
        /// 修改原因
        /// </param>
        /// <param name="transaction">
        /// 事务
        /// </param>
        void UpdateStatus(int orderId, int status, string reason, out SqlTransaction transaction);

        /// <summary>
        /// The recover products inventory.
        /// </summary>
        /// <param name="orderID">
        /// The order id.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        void RecoverProductsInventory(int orderID, SqlTransaction transaction);

        /// <summary>
        /// 获取订单真实已支付金额（订单总金额-被抵扣的金额）
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 已支付金额
        /// </returns>
        double SelectOrderActualPayment(int orderId);

        /// <summary>
        /// 查询用户的订单列表信息
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <returns>查询结果</returns>
        List<Order> SelectByUserID(int userId);

        /// <summary>
        /// 更新订单信息
        /// </summary>
        /// <param name="order"></param>
        void Update(Order order,SqlTransaction transaction);

        /// <summary>
        /// 根据订单编号查询订单信息
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        /// <returns>订单对象</returns>
        Order SelectByOrderCode(string orderCode);

        #endregion

    }
}