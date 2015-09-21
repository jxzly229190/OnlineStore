// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderProductService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单商品服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单商品服务类
    /// </summary>
    public class OrderProductService
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private readonly IOrderProductDA orderProductDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProductService"/> class.
        /// </summary>
        public OrderProductService()
        {
            this.orderProductDA = new DAFactoryTransact().CreateOrderProductDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 分页查询订单信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Product> QueryByOrderId(int orderId)
        {
            return this.orderProductDA.SelectByOrderId(orderId);
        }

        /// <summary>
        /// 删除订单商品
        /// </summary>
        /// <param name="productID">
        /// 订单商品编码
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int RemoveOrderProduct(int productID)
        {
            return this.orderProductDA.Delete(productID);
        }

        /// <summary>
        /// 批量添加订单商品数据
        /// </summary>
        /// <param name="orderProducts">
        /// 订单商品列表
        /// </param>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 已成功添加的订单商品数量
        /// </returns>
		public int BatchAddOrderProduct(List<Order_Product> orderProducts, int cpsId, int orderId, SqlTransaction transaction)
		{
			return this.orderProductDA.BatchInsertOrderProduct(orderProducts, cpsId, orderId, transaction);
		}

	    /// <summary>
	    /// 根据订单编码更新订单商品
	    /// </summary>
	    /// <param name="orderProducts">
	    /// 订单商品列表
	    /// </param>
	    /// <param name="cpsId"></param>
	    /// <param name="orderID">
	    /// 订单编码
	    /// </param>
	    /// <param name="transaction">
	    /// 事务对象
	    /// </param>
	    public void ModifyOrderProductByOrderID(
			List<Order_Product> orderProducts,
			int cpsId,
			int orderID,
			SqlTransaction transaction)
		{
			this.orderProductDA.UpdateOrderProductByOrderID(orderProducts, cpsId, orderID, transaction);
		}

        /// <summary>
        /// 分页查询订单商品数据
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
        /// 查询结果
        /// </returns>
        public List<Order_Product> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.orderProductDA.Paging(paging, out pageCount, out totalCount);
        }

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
        public int QueryByProductIDAndUserID(int productID, int userID)
        {
            return this.orderProductDA.SelectByProductIDAndUserID(productID, userID);
        }

        #endregion
    }
}