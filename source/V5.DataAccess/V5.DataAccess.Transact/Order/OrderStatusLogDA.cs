// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderStatusLogDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态日志数据访问类
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
    /// 订单状态日志数据访问类
    /// </summary>
    public class OrderStatusLogDA : IOrderStatusLogDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问服务对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 数据库访问服务对象
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
        /// 新增一条订单状态日志
        /// </summary>
        /// <param name="orderStatusLog">
        /// 订单状态日志
        /// </param>
        /// <param name="transaction">
        /// 数据库事务对象
        /// </param>
        /// <returns>
        /// 新增的订单状态日子编码
        /// </returns>
        public int Insert(Order_Status_Log orderStatusLog, SqlTransaction transaction)
        {
            /*
             Create Procedure sp_Order_Status_Log_Insert
	            @OrderID int,
	            @EmployeeID int,
	            @Status int,
	            @Remark nvarchar(512),
	            @CreateTime datetime,
	            @ReferenceID int output
            As
             */
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderStatusLog.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    orderStatusLog.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Status",
                                    SqlDbType.Int,
                                    orderStatusLog.Status,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Remark",
                                    SqlDbType.NVarChar,
                                    orderStatusLog.Remark,
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Status_Log_Insert", paras, transaction);
            return (int)paras.Find(e => e.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 根据订单编码查询对应的订单状态处理信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Status_Log> SelectByOrderID(int orderId)
        {
            return
                this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Order_Status_Log_SelectByOrderID",
                    new List<SqlParameter>()
                        {
                            this.SqlServer.CreateSqlParameter(
                                "OrderID",
                                SqlDbType.Int,
                                orderId,
                                ParameterDirection.Input)
                        },
                    null).ToList<Order_Status_Log>();
        }

        #endregion
    }
}