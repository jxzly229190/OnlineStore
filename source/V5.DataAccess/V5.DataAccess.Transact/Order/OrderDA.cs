// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;
    using global::System.Linq;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单数据访问类
    /// </summary>
    public class OrderDA : IOrderDA
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

        #region  Constructors and Destructors

        #endregion

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
        /// 查询结果
        /// </returns>
        public List<Order> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                paging.TableName = "[view_Orders]";
                return this.SqlServer.Paging<Order>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order">
        /// 订单兑现
        /// </param>
        /// <param name="transaction">
        /// 数据库事务
        /// </param>
        /// <returns>
        /// 已添加订单的编码
        /// </returns>
        public int Insert(Order order, out SqlTransaction transaction)
        {
            this.SqlServer.BeginTransaction();
            transaction = this.SqlServer.Transaction;
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "UserID",
                                    SqlDbType.Int,
                                    order.UserID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "RecieveAddressID",
                                    SqlDbType.Int,
                                    order.RecieveAddressID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CpsID",
                                    SqlDbType.Int,
                                    order.CpsID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentMethodID",
                                    SqlDbType.Int,
                                    order.PaymentMethodID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderCode",
                                    SqlDbType.VarChar,
                                    order.OrderCode,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderNumber",
                                    SqlDbType.VarChar,
                                    order.OrderNumber,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalMoney",
                                    SqlDbType.Float,
                                    order.TotalMoney,
                                    ParameterDirection.Input),
								this.SqlServer.CreateSqlParameter(
                                    "Discount",
                                    SqlDbType.Float,
                                    order.Discount,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalIntegral",
                                    SqlDbType.Int,
                                    order.TotalIntegral,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentStatus",
                                    SqlDbType.Int,
                                    order.PaymentStatus,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Status",
                                    SqlDbType.Int,
                                    order.Status,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    order.Description,
                                    ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                    "Remark",
                                    SqlDbType.NVarChar,
                                    order.Remark,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CreateTime",
                                    SqlDbType.DateTime,
                                    order.CreateTime,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsRequireInvoice",
                                    SqlDbType.Bit,
                                    order.IsRequireInvoice,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "DeliveryCost",
                                    SqlDbType.Float,
                                    order.DeliveryCost,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "ReferenceID",
                                    SqlDbType.Int,
                                    order.ID,
                                    ParameterDirection.Output)
                            };
            this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Order_Insert",
                    paras,
                    transaction);
            return (int)paras.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 根据订单编码更新订单备注信息
        /// </summary>
        /// <param name="orderID">
        /// 订单编码
        /// </param>
        /// <param name="description">
        /// 备注信息
        /// </param>
        public void UpdateDescription(int orderID, string description)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    orderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    description,
                                    ParameterDirection.Input)
                            };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_UpdateDescription", paras, null);
        }

        /// <summary>
        /// 根据编码查询订单信息
        /// </summary>
        /// <param name="id">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 订单对象
        /// </returns>
        public Order SelectByID(int id)
        {
            var reader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_SelectById",
                new List<SqlParameter>
                    {
                        this.SqlServer.CreateSqlParameter(
                            "ID",
                            SqlDbType.Int,
                            id,
                            ParameterDirection.Input)
                    },
                null);
            var list = reader.ToList<Order>();
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        /// <summary>
        /// 确认订单信息
        /// </summary>
        /// <param name="order">
        /// 订单对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        public void UpdateForConfirmOrder(Order order, SqlTransaction transaction)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    order.ID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CpsID",
                                    SqlDbType.Int,
                                    order.CpsID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "DeliveryCost",
                                    SqlDbType.Float,
                                    order.DeliveryCost,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalMoney",
                                    SqlDbType.Float,
                                    order.TotalMoney,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalIntegral",
                                    SqlDbType.Float,
                                    order.TotalIntegral,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    order.Description,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsRequireInvoice",
                                    SqlDbType.Bit,
                                    order.IsRequireInvoice,
                                    ParameterDirection.Input)
                            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_UpdateForConfirmOrder", paras, transaction);
        }

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
        public void UpdateStatus(int orderId, int status, string reason, out SqlTransaction transaction)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    orderId,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Status",
                                    SqlDbType.Int,
                                    status,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    reason,
                                    ParameterDirection.Input)
                            };
            this.SqlServer.BeginTransaction();
            transaction = this.SqlServer.Transaction;
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_UpdateStatus", paras, null);
        }

        public void Update(Order order, SqlTransaction transaction)
        {
            /**
             Create Procedure sp_Order_Update
	            @ID int,
	            @UserID int,
	            @RecieveAddressID int,
	            @CpsID int,
	            @PaymentMethodID int,
	            @OrderCode varchar(16),
	            @OrderNumber varchar(16),
	            @DeliveryCost float,
	            @TotalMoney float,
	            @TotalIntegral int,
	            @PaymentStatus int,
	            @IsRequireInvoice bit,
	            @Status int,    
	            @Description nvarchar(512),
	            @Remark nvarchar(512)
             **/

            this.SqlServer.BeginTransaction();
            if (transaction == null)
            {
                transaction = this.SqlServer.Transaction;
            }

            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    order.ID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "UserID",
                                    SqlDbType.Int,
                                    order.UserID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "RecieveAddressID",
                                    SqlDbType.Int,
                                    order.RecieveAddressID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CpsID",
                                    SqlDbType.Int,
                                    order.CpsID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentMethodID",
                                    SqlDbType.Int,
                                    order.PaymentMethodID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderCode",
                                    SqlDbType.VarChar,
                                    order.OrderCode,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalMoney",
                                    SqlDbType.Float,
                                    order.TotalMoney,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TotalIntegral",
                                    SqlDbType.Int,
                                    order.TotalIntegral,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentStatus",
                                    SqlDbType.Int,
                                    order.PaymentStatus,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Status",
                                    SqlDbType.Int,
                                    order.Status,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    order.Description,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Remark",
                                    SqlDbType.NVarChar,
                                    order.Remark,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderNumber",
                                    SqlDbType.VarChar,
                                    order.OrderNumber,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsRequireInvoice",
                                    SqlDbType.Bit,
                                    order.IsRequireInvoice,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "DeliveryCost",
                                    SqlDbType.Float,
                                    order.DeliveryCost,
                                    ParameterDirection.Input),
									this.SqlServer.CreateSqlParameter(
                                    "DeliveryCorporationID",
                                    SqlDbType.Float,
                                    order.DeliveryCorporationID,
                                    ParameterDirection.Input)
                            };

            this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Order_Update",
                    paras,
                    transaction);
        }

        /// <summary>
        /// 根据订单编号查询订单信息
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        /// <returns>订单对象</returns>
        public Order SelectByOrderCode(string orderCode)
        {
            /*
             CREATE PROCEDURE [dbo].[sp_Order_SelectByOrderCode]
	            @OrderCode int
            As
             */
            var reader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_SelectByOrderCode",
                new List<SqlParameter>()
                    {
                        this.SqlServer.CreateSqlParameter(
                            "OrderCode",
                            SqlDbType.VarChar,
                            orderCode,
                            ParameterDirection.Input)
                    },
                null);

            if (reader != null)
            {
                return reader.ToList<Order>().FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// The recover products inventory.
        /// </summary>
        /// <param name="orderID">
        /// The order id.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void RecoverProductsInventory(int orderID, SqlTransaction transaction)
        {
            /*
             Create Procedure sp_Product_RecoverOrderInventory
	                @OrderID int
                As
             */

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Product_RecoverOrderInventory",
                new List<SqlParameter>
                    {
                        this.SqlServer.CreateSqlParameter(
                            "OrderID",
                            SqlDbType.Int,
                            orderID,
                            ParameterDirection.Input)
                    },
                transaction);
        }

        /// <summary>
        /// 获取订单真实已支付金额（订单总金额-被抵扣的金额）
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 已支付金额
        /// </returns>
        public double SelectOrderActualPayment(int orderId)
        {
            /*
             Create Procedure sp_Get_Order_ActualPayment
                @OrderId int,
                @ActualPaymentMoney float output
             As
             */
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderId",
                                    SqlDbType.Int,
                                    orderId,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "ActualPaymentMoney",
                                    SqlDbType.Float,
                                    null,
                                    ParameterDirection.Output)
                            };
            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Get_Order_ActualPayment",
                paras,
                null);
            return (double)paras.Find(p => p.ParameterName == "ActualPaymentMoney").Value;
        }

        /// <summary>
        /// 查询用户的订单列表信息
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <returns>查询结果</returns>
        public List<Order> SelectByUserID(int userId)
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_SelectByUserID",
                new List<SqlParameter>
                    {
                        this.SqlServer.CreateSqlParameter(
                            "UserID",
                            SqlDbType.Int,
                            userId,
                            ParameterDirection.Input)
                    },
                null);
            if (dataReader != null)
            {
                return dataReader.ToList<Order>();
            }
            return new List<Order>();
        }

        #endregion
    }
}