namespace V5.DataAccess.Transact.Order
{
	using global::System;
	using global::System.Collections.Generic;
	using global::System.Data;
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact;
	using V5.Library.Storage.DB;

	public class OrderErpLogDA : IOrderErpLogDA
	{
		private SqlServer sqlServer;

		public OrderErpLogDA()
		{
			sqlServer=new SqlServer();
		}

		/// <summary>
		/// 向数据库写入一条ERP交互日志信息
		/// </summary>
		/// <param name="log">日志</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>写入日志编码</returns>
		public int Insert(DataContract.Transact.Order.Order_Erp_Log log, SqlTransaction transaction)
		{
			/*
			 @ERP nvarchar(64),
			@OrderID int,
			@OperateType int,
			@UserID int,
			@ReqContent nvarchar(MAX),
			@ResContent nvarchar(MAX),
			@IsSuccess bit,
			@Operator int,
			@ExtField nvarchar(MAX),
			@CreateTime datetime,
			@ReferenceID int output
			 */

			var paras = new List<SqlParameter>
				            {
					            this.sqlServer.CreateSqlParameter(
						            "ERP",
						            SqlDbType.NVarChar,
						            log.ERP,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "OrderID",
						            SqlDbType.Int,
						            log.OrderID,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "OperateType",
						            SqlDbType.Int,
						            log.OperateType,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "UserID",
						            SqlDbType.Int,
						            log.UserID,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ReqContent",
						            SqlDbType.NVarChar,
						            log.ReqContent,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ResContent",
						            SqlDbType.NVarChar,
						            log.ResContent,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "IsSuccess",
						            SqlDbType.Bit,
						            log.IsSuccess,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "Operator",
						            SqlDbType.Int,
						            log.Operator,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ExtField",
						            SqlDbType.NVarChar,
						            log.ExtField,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "CreateTime",
						            SqlDbType.DateTime,
						            DateTime.Now.ToLocalTime(),
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ReferenceID",
						            SqlDbType.Int,
						            null,
						            ParameterDirection.Output)
				            };

			this.sqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Order_Erp_Log_Insert", paras, transaction);

			return (int)paras.Find(p => p.ParameterName == "ReferenceID").Value;
		}

		/// <summary>
		/// 写入HWERP 回写日志信息
		/// </summary>
		/// <param name="log"></param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public int InertHwUpdateLog(Hw_Log log, SqlTransaction transaction)
		{
			/*@Number nvarchar(50),
				@Content ntext,
				@State tinyint,
				@CreateTime datetime,
				@ExtField nvarchar(50),
			 */

			var paras = new List<SqlParameter>
				            {
					            this.sqlServer.CreateSqlParameter(
						            "Number",
						            SqlDbType.VarChar,
						            log.Number,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "Content",
						            SqlDbType.NVarChar,
						            log.Content,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "State",
						            SqlDbType.Int,
						            log.State,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ExtField",
						            SqlDbType.NVarChar,
						            log.ExtField,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "CreateTime",
						            SqlDbType.DateTime,
						            DateTime.Now.ToLocalTime(),
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ReferenceID",
						            SqlDbType.Int,
						            null,
						            ParameterDirection.Output)
				            };

			this.sqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_hw_Log_Insert", paras, transaction);

			return (int)paras.Find(p => p.ParameterName == "ReferenceID").Value;
		}
	}
}