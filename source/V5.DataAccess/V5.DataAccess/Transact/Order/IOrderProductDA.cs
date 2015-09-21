// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderProductDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单商品数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单商品数据访问接口
    /// </summary>
    public interface IOrderProductDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 根据订单编码查询相应的订单商品信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Order_Product> SelectByOrderId(int orderId);

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Order_Product> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 批量添加订单商品
        /// </summary>
        /// <param name="orderProducts">
        /// 订单商品列表
        /// </param>
        /// <param name="orderId">
        /// The order Id.
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 成功添加的订单商品记录数量
        /// </returns>
		int BatchInsertOrderProduct(List<Order_Product> orderProducts, int cpsId, int orderId, SqlTransaction transaction);

		/// <summary>
		/// 根据订单编码更新订单商品
		/// </summary>
		/// <param name="orderProducts">
		/// 订单商品列表
		/// </param>
		/// <param name="orderID">
		/// 订单编码
		/// </param>
		/// <param name="transaction">
		/// 事务对象
		/// </param>
		void UpdateOrderProductByOrderID(List<Order_Product> orderProducts, int cpsId, int orderID, SqlTransaction transaction);

        /// <summary>
        /// 删除订单商品
        /// </summary>
        /// <param name="id">
        /// 订单商品编码
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 根据会员编号商品编号查询结果.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="userID">
        /// 会员编号.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SelectByProductIDAndUserID(int productID, int userID);

        #endregion
    }
}