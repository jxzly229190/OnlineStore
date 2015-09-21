// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderProductDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单商品数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单商品数据访问类
    /// </summary>
    public class OrderProductDA : IOrderProductDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Constructors and Destructors

        #endregion

        #region Public Methods and Operators 

        /// <summary>
        /// 根据订单编码查询对应商品信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Order_Product> SelectByOrderId(int orderId)
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_Product_SelectByOrderId",
                new List<SqlParameter>
                    {
                        this.SqlServer.CreateSqlParameter(
                            "OrderId",
                            SqlDbType.Int,
                            orderId,
                            ParameterDirection.Input)
                    },
                null);

            var list = dataReader.ToList<Order_Product>();

            return list;
        }

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
        public List<Order_Product> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            paging.TableName = "view_Order_Products";
            return this.SqlServer.Paging<Order_Product>(paging, out pageCount, out totalCount, null);
        }

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
		public int BatchInsertOrderProduct(List<Order_Product> orderProducts, int cpsId, int orderId, SqlTransaction transaction)
		{
			if (orderProducts != null && orderProducts.Count > 0)
			{
				var dt = this.BuildOrderProductDataTable(orderProducts, orderId);

				var paramsList = new List<SqlParameter>
		                             {
			                             this.SqlServer.CreateSqlParameter(
				                             "CpsId",
				                             SqlDbType.Int,
				                             cpsId,
				                             ParameterDirection.Input),
			                             new SqlParameter("OP", SqlDbType.Structured)
				                             {
					                             TypeName =
						                             "[dbo].OrderProductTable",
					                             Value = dt,
					                             Direction =
						                             ParameterDirection
						                             .Input
				                             },
			                             this.SqlServer.CreateSqlParameter(
				                             "RowCount",
				                             SqlDbType.Int,
				                             null,
				                             ParameterDirection.Output)
		                             };

				this.SqlServer.ExecuteNonQuery(
					CommandType.StoredProcedure,
					"sp_Order_Product_BatchInsert",
					paramsList,
					transaction);

				return (int)paramsList.Find(parameter => parameter.ParameterName == "RowCount").Value;
			}

			return 0;
		}

        /// <summary>
        /// 根据订单编码更新订单商品
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
		public void UpdateOrderProductByOrderID(List<Order_Product> orderProducts, int cpsId, int orderId, SqlTransaction transaction)
		{
			// OrderID,ProductID,Quantity,TransactPrice,Createtime
			if (orderProducts == null || orderProducts.Count <= 0)
			{
				return;
			}

			var dt = this.BuildOrderProductDataTable(orderProducts, orderId);

			var paramsList = new List<SqlParameter>
                                 {
									 this.SqlServer.CreateSqlParameter(
				                             "CpsId",
				                             SqlDbType.Int,
				                             cpsId,
				                             ParameterDirection.Input),
                                     new SqlParameter("OPT", SqlDbType.Structured)
                                         {
                                             TypeName =
                                                 "[dbo].OrderProductTable",
                                             Value = dt,
                                             Direction =
                                                 ParameterDirection
                                                 .Input
                                         },
                                     this.SqlServer.CreateSqlParameter(
                                         "RowCount",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output),
                                     this.SqlServer.CreateSqlParameter(
                                         "OrderID",
                                         SqlDbType.Int,
                                         orderId,
                                         ParameterDirection.Input)
                                 };

			this.SqlServer.ExecuteNonQuery(
				CommandType.StoredProcedure,
				"sp_Order_Product_BatchUpdateByOrderID",
				paramsList,
				transaction);
		}

        /// <summary>
        /// 删除订单商品
        /// </summary>
        /// <param name="id">
        /// 订单商品编码
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete(int id)
        {
            this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Order_Product_DeleteRow",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "ID",
                                SqlDbType.Int,
                                id,
                                ParameterDirection.Input)
                        },
                    null);
            return id;
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
        public int SelectByProductIDAndUserID(int productID, int userID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         userID,
                                         ParameterDirection.Input)
                                 };
            var count = this.SqlServer.ExecuteScalar(
                CommandType.StoredProcedure,
                "sp_Order_Product_SelectCount",
                parameters,
                null);
            return (int)count;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建订单商品表结构
        /// </summary>
        /// <returns>
        /// 表结构
        /// </returns>
        private DataTable GetOrderProductTableSchema()
        {
			var dt = new DataTable();
	        dt.Columns.AddRange(
		        new[]
			        {
				        new DataColumn("OrderID", typeof(int)), new DataColumn("ProductID", typeof(int)),
				        new DataColumn("Quantity", typeof(int)), new DataColumn("TransactPrice", typeof(double)),
				        new DataColumn("PromotionID", typeof(int)), new DataColumn("PromotionType", typeof(int)),
				        new DataColumn("PromotionResult", typeof(int)), new DataColumn("MarketPrice", typeof(float)),
				        new DataColumn("GoujiuPrice", typeof(double)), new DataColumn("ProductName", typeof(string)),
				        new DataColumn("Integral", typeof(int)), new DataColumn("RebateRate", typeof(double)),
				        new DataColumn("Commission", typeof(double)), new DataColumn("Remark", typeof(string)),
				        new DataColumn("ExtField", typeof(string)), new DataColumn("CreateTime", typeof(DateTime)),
			        });

            return dt;
        }

        /// <summary>
        /// The build order product data table.
        /// </summary>
        /// <param name="orderProducts">
        /// The order products.
        /// </param>
        /// <param name="orderId">
        /// The order Id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private DataTable BuildOrderProductDataTable(IEnumerable<Order_Product> orderProducts, int orderId)
        {
            DataTable dt = this.GetOrderProductTableSchema();
            foreach (var orderProduct in orderProducts)
            {
                DataRow r = dt.NewRow();
                r[0] = orderId;
                r[1] = orderProduct.ProductID;
                r[2] = orderProduct.Quantity;
                r[3] = orderProduct.TransactPrice;
				r[4] = orderProduct.PromotionID;
				r[5] = orderProduct.PromotionType;
				r[6] = orderProduct.PromotionResult;
				r[7] = orderProduct.MarketPrice;
				r[8] = orderProduct.GoujiuPrice;
				r[9] = orderProduct.ProductName;
				r[10] = orderProduct.Integral;
				r[11] = orderProduct.RebateRate;
				r[12] = orderProduct.Commission;
				r[13] = orderProduct.Remark;
				r[14] = orderProduct.ExtField;
                r[15] = DateTime.Now;
                dt.Rows.Add(r);
            }

            return dt;
        }

        #endregion
    }
}