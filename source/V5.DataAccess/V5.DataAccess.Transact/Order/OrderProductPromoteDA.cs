namespace V5.DataAccess.Transact.Order
{
	using global::System;
	using global::System.Collections.Generic;
	using global::System.Data;
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact.Order;
	using V5.Library.Storage.DB;

	public class OrderProductPromoteDA : IOrderProductPromoteDA
	{
		public SqlServer SqlServer;

		public OrderProductPromoteDA()
		{
			this.SqlServer = new SqlServer();
		}

		/// <summary>
		/// 插入订单商品促销信息
		/// </summary>
		/// <param name="orderProductPromote">插入的对象</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>返回新增的数据编码</returns>
		public int Insert(Order_Product_Promote orderProductPromote, SqlTransaction transaction)
		{
			/*
			 [OrderID]
			  ,[OrderProductID]
			  ,[PromoteType]
			  ,[PromoteID]
			  ,[Remark]
			  ,[ExtField]
			  ,[CreateTime]
			 */
			var paras = new List<SqlParameter>
				            {
					            this.SqlServer.CreateSqlParameter(
						            "OrderID",
						            SqlDbType.Int,
						            orderProductPromote.OrderID,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "OrderProductID",
						            SqlDbType.Int,
						            orderProductPromote.OrderProductID,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "PromoteType",
						            SqlDbType.Int,
						            orderProductPromote.PromoteType,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "PromoteID",
						            SqlDbType.Int,
						            orderProductPromote.PromoteID,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "Remark",
						            SqlDbType.NVarChar,
						            orderProductPromote.Remark,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "ExtField",
						            SqlDbType.NVarChar,
						            orderProductPromote.ExtField,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "CreateTime",
						            SqlDbType.DateTime,
						            DateTime.Now,
						            ParameterDirection.Input),
					            this.SqlServer.CreateSqlParameter(
						            "ReferenceID",
						            SqlDbType.Int,
						            null,
						            ParameterDirection.Output)
				            };

			this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Product_Promote_Insert", paras, transaction);

			return (int)paras.Find(p => p.ParameterName == "ReferenceID").Value;
		}

		/// <summary>
		/// 批量插入订单商品促销信息
		/// </summary>
		/// <param name="productPromotes">促销信息列表</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>插入的记录数</returns>
		public int BatchInsert(List<Order_Product_Promote> productPromotes, SqlTransaction transaction)
		{
			if (productPromotes != null && productPromotes.Count > 0)
			{
				var dt = this.BuildDataTable(productPromotes);

				var paramsList = new List<SqlParameter>
                                     {
                                         new SqlParameter("OPP", SqlDbType.Structured)
                                             {
                                                 TypeName =
                                                     "[dbo].OrderProductPromoteTable",
                                                 Value = dt,
                                                 Direction =
                                                     ParameterDirection
                                                     .Input
                                             },
                                         this.SqlServer.CreateSqlParameter(
                                             "Count",
                                             SqlDbType.Int,
                                             null,
                                             ParameterDirection.Output)
                                     };

				this.SqlServer.ExecuteNonQuery(
					CommandType.StoredProcedure,
					"sp_Order_Product_Promote_BatchInsert",
					paramsList,
					transaction);

				return (int)paramsList.Find(parameter => parameter.ParameterName == "Count").Value;
			}
			throw new NotImplementedException();
		}

		/// <summary>
		/// 创建订单商品表结构
		/// </summary>
		/// <returns>
		/// 表结构
		/// </returns>
		private DataTable GetOrderProductTableSchema()
		{
			/*
			 [OrderID]
			  ,[OrderProductID]
			  ,[PromoteType]
			  ,[PromoteID]
			  ,[Remark]
			  ,[ExtField]
			  ,[CreateTime]
			 */

			var dt = new DataTable();
			dt.Columns.AddRange(
					new[]
                        {
                            new DataColumn("OrderID", typeof(int)), 
							new DataColumn("ProductID",typeof(int)),
                            new DataColumn("OrderProductID", typeof(int)),
							new DataColumn("PromoteDiscount",typeof(double)),
                            new DataColumn("PromoteType", typeof(int)), 
                            new DataColumn("PromoteID", typeof(double)),
							new DataColumn("Remark",typeof(string)), 
							new DataColumn("ExtField",typeof(string)), 
							new DataColumn("CreateTime",typeof(DateTime))
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
		private DataTable BuildDataTable(IEnumerable<Order_Product_Promote> orderProductPromotes)
		{
			DataTable dt = this.GetOrderProductTableSchema();
			foreach (var orderProductPromote in orderProductPromotes)
			{
				DataRow r = dt.NewRow();
				r[0] = orderProductPromote.OrderID;
				r[1] = orderProductPromote.ProductID;
				r[2] = orderProductPromote.OrderProductID;
				r[3] = orderProductPromote.PromoteDiscount;
				r[4] = orderProductPromote.PromoteType;
				r[5] = orderProductPromote.PromoteID;
				r[6] = orderProductPromote.Remark;
				r[7] = orderProductPromote.ExtField;
				r[8] = DateTime.Now;
				dt.Rows.Add(r);
			}

			return dt;
		}
	}
}