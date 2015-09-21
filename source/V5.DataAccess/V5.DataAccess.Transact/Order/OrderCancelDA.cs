// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancelDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消数据访问类
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
    /// 订单取消数据访问类
    /// </summary>
    public class OrderCancelDA : IOrderCancelDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 数据库操作对象
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
        /// 后台取消订单
        /// </summary>
        /// <param name="orderCancel">
        /// 订单取消对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
        /// </returns>
        public int OrderCancel(Order_Cancel orderCancel)
        {
            /*
             Create Procedure sp_Order_Cancel
	            @OrderID int,	
                @OrderCancelCauseID int
               ,@EmployeeID int = default
               ,@UserID int =default
               ,@Description nvarchar
               ,@CreateTime datetime
	            ,@Result int output	
            As
             */

            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderCancel.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderCancelCauseID",
                                    SqlDbType.Int,
                                    orderCancel.OrderCancelCauseID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "UserID",
                                    SqlDbType.Int,
                                    orderCancel.UserID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    orderCancel.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Description",
                                    SqlDbType.NVarChar,
                                    orderCancel.Description,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CreateTime",
                                    SqlDbType.DateTime,
                                    DateTime.Now,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Result",
                                    SqlDbType.Int,
                                    null,
                                    ParameterDirection.Output)
                            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Cancel", paras, null);
            return (int)paras.Find(p => p.ParameterName == "Result").Value;
        }

        /// <summary>
        /// 后台取消已付款未发货订单
        /// </summary>
        /// <param name="orderCancel">
        /// 订单取消对象
        /// </param>
        /// <param name="refund">
        /// 售后退款对象
        /// </param>
        /// <returns>
        /// 操作结果：0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废，4-订单未付款
        /// </returns>
        public int OrderCancelWithRefundByBackstage(Order_Cancel orderCancel, Aftersale_Refund refund)
        {
            /**
            Create Procedure sp_Order_Cancel_Refund
	            @OrderID int,	
                @OrderCancelCauseID int,
                @EmployeeID int = default,
                @UserID int =default, --后台操作，则为空
                @CancelDescription ntext=default,
	            @RefundMethodID int, --退款方式编号（1：退至虚拟账户，2：人工退款至指定帐号）
	            @ActualRefundMoney float, --实际退款金额
	            @RefundDescription nvarchar(512)=default,
	            @CreateTime datetime,
	            @Result int output	
            As
             * **/
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderCancel.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "OrderCancelCauseID",
                                    SqlDbType.Int,
                                    orderCancel.OrderCancelCauseID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    orderCancel.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CancelDescription",
                                    SqlDbType.NVarChar,
                                    orderCancel.Description,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "RefundMethodID",
                                    SqlDbType.Int,
                                    refund.RefundMethodID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "ActualRefundMoney",
                                    SqlDbType.Float,
                                    refund.ActualRefundMoney,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "RefundDescription",
                                    SqlDbType.NVarChar,
                                    refund.RefundDescription,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CreateTime",
                                    SqlDbType.DateTime,
                                    DateTime.Now,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Result",
                                    SqlDbType.Int,
                                    null,
                                    ParameterDirection.Output)
                            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Cancel_Refund", paras, null);
            return (int)paras.Find(p => p.ParameterName == "Result").Value;
        }

        #endregion
    }
}