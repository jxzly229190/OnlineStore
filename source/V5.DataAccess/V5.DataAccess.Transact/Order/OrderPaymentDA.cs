namespace V5.DataAccess.Transact.Order
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;
    using global::System.Linq;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    public class OrderPaymentDA:IOrderPaymentDA
    {
        private SqlServer _sqlServer;

        public SqlServer SqlServer
        {
            get
            {
                return this._sqlServer ?? (this._sqlServer = new SqlServer());
            }
        }

        public int Insert(Order_Payment orderPayment, SqlTransaction transaction)
        {
            /*
             Create Procedure [dbo].[sp_Order_Payment_Insert]
	             @OrderID Int
	            ,@PaymentOrgID Int
	            ,@PaymentMoney Float
	            ,@IsUseCoupon bit
	            ,@IsUseIntegral bit
	            ,@IsUseAccount bit
	            ,@CreateTime datetime
	            ,@ReferenceID int output
            As            
             */
            var paras = new List<SqlParameter>()
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "OrderID",
                                    SqlDbType.Int,
                                    orderPayment.OrderID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentOrgID",
                                    SqlDbType.Int,
                                    orderPayment.PaymentOrgID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "PaymentMoney",
                                    SqlDbType.Float,
                                    orderPayment.PaymentMoney,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "TradeNo",
                                    SqlDbType.VarChar,
                                    orderPayment.TradeNo,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsUseCoupon",
                                    SqlDbType.Bit,
                                    orderPayment.IsUseCoupon,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsUseIntegral",
                                    SqlDbType.Bit,
                                    orderPayment.IsUseIntegral,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "IsUseAccount",
                                    SqlDbType.Bit,
                                    orderPayment.IsUseAccount,
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
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Payment_Insert", paras, transaction);

            return (int)paras.Find(p => p.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 根据第三方交易号查询支付信息
        /// </summary>
        /// <param name="tradeNo">第三方交易号</param>
        /// <returns>交易信息</returns>
        public List<Order_Payment> SelectByTradeNo(string tradeNo)
        {
            /**
             CREATE PROCEDURE [dbo].[sp_Order_Payment_SelectByTradeNo]
	                @TradeNo int
                As
             */
            var reader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Order_Payment_SelectByTradeNo",
                new List<SqlParameter>()
                    {
                        this.SqlServer.CreateSqlParameter(
                            "TradeNo",
                            SqlDbType.VarChar,
                            tradeNo,
                            ParameterDirection.Input)
                    },
                null);

            if (reader != null)
            {
                return reader.ToList<Order_Payment>();
            }

            return new List<Order_Payment>();
        }

		/// <summary>
		/// 查询订单支付信息
		/// </summary>
		/// <param name="orderID">订单编码</param>
		/// <returns>订单支付信息</returns>
	    public Order_Payment SelectByOrderID(int orderID)
	    {
			var reader = this.SqlServer.ExecuteDataReader(
				CommandType.StoredProcedure,
				"[sp_Order_PaymentInfo_SelectByOrderID]",
				new List<SqlParameter>()
                    {
                        this.SqlServer.CreateSqlParameter(
                            "OrderID",
                            SqlDbType.Int,
                            orderID,
                            ParameterDirection.Input)
                    },
				null);

			if (reader != null)
			{
				return reader.ToList<Order_Payment>().FirstOrDefault();
			}

			return null;
	    }
    }
}