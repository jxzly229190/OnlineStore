// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderInvoiceDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单发票数据访问类
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
    /// 订单发票数据访问类
    /// </summary>
    public class OrderInvoiceDA : IOrderInvoiceDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

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
            var reader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_Invoice_SelectByOrderID",
                new List<SqlParameter>
                    {
                        this.SqlServer.CreateSqlParameter(
                            "orderID",
                            SqlDbType.Int,
                            orderID,
                            ParameterDirection.Input)
                    },
                null);

            var list = reader.ToList<Order_Invoice>();
            if (list != null && list.Count > 0)
            {
                return list[0];
            }

            return null;
        }

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
        public int Insert(Order_Invoice orderInvoice, SqlTransaction transaction)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderInvoice.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceTypeID",
                                    SqlDbType.Int,
                                    orderInvoice.InvoiceTypeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceContentID",
                                    SqlDbType.Int,
                                    orderInvoice.InvoiceContentID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceTitle",
                                    SqlDbType.NVarChar,
                                    orderInvoice.InvoiceTitle,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceCost",
                                    SqlDbType.Float,
                                    orderInvoice.InvoiceCost,
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Invoice_Insert", paras, transaction);
            return (int)paras.Find(e => e.ParameterName == "ReferenceID").Value;
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
        public void Update(Order_Invoice orderInvoice, SqlTransaction transaction)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderInvoice.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceTypeID",
                                    SqlDbType.Int,
                                    orderInvoice.InvoiceTypeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceContentID",
                                    SqlDbType.Int,
                                    orderInvoice.InvoiceContentID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceTitle",
                                    SqlDbType.NVarChar,
                                    orderInvoice.InvoiceTitle,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "InvoiceCost",
                                    SqlDbType.Float,
                                    orderInvoice.InvoiceCost,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    orderInvoice.ID,
                                    ParameterDirection.Input)
                            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Invoice_Update", paras, transaction);
        }
    }
}