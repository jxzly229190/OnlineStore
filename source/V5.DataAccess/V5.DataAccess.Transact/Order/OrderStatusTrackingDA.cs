// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderStatusTrackingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态变化跟踪信息数据库访问类
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
    /// 订单状态变化跟踪信息数据库访问类
    /// </summary>
    public class OrderStatusTrackingDA : IOrderStatusTrackingDA
    {
        #region Constants and Fields

        /// <summary>
        /// The sql server.
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the sql server.
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion


        #region Public Methods and Operators

        /// <summary>
        /// 根据订单编码查询订单状态跟踪信息
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Status_Tracking> SelectByOrderID(int orderID)
        {
            return
                this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Order_Status_Tracking_SelectByOrderID",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "OrderID",
                                SqlDbType.Int,
                                orderID,
                                ParameterDirection.Input)
                        },
                    null).ToList<Order_Status_Tracking>();
        }

        /// <summary>
        /// 插入一条订单状态跟踪信息
        /// </summary>
        /// <param name="orderStatusTracking">
        /// 订单状态跟踪对象
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 新增的记录编码
        /// </returns>
        public int Insert(Order_Status_Tracking orderStatusTracking, SqlTransaction transaction)
        {
	        var paras = new List<SqlParameter>
		                    {
			                    this.SqlServer.CreateSqlParameter(
				                    "EmployeeID",
				                    SqlDbType.Int,
				                    orderStatusTracking.EmployeeID,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "UserID",
				                    SqlDbType.Int,
				                    orderStatusTracking.UserID,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "OrderID",
				                    SqlDbType.Int,
				                    orderStatusTracking.OrderID,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "Remark",
				                    SqlDbType.NVarChar,
				                    orderStatusTracking.Remark,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "ExpressNumber",
				                    SqlDbType.NVarChar,
				                    orderStatusTracking.ExpressNumber,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "MailNo",
				                    SqlDbType.NVarChar,
				                    orderStatusTracking.MailNo,
				                    ParameterDirection.Input),
			                    this.SqlServer.CreateSqlParameter(
				                    "Status",
				                    SqlDbType.Int,
				                    orderStatusTracking.Status,
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

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Order_Status_Tracking_Insert",
                paras,
                transaction);
            return (int)paras.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        #endregion
    }
}