namespace V5.DataAccess.Transact
{
	using global::System;
	using global::System.Collections.Generic;
	using global::System.Data;
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact;
	using V5.Library.Storage.DB;

	public class CpsLinkRecordDA : ICpsLinkRecordDA
	{
		private SqlServer sqlServer;

		/// <summary>
		/// 构造方法
		/// </summary>
		public CpsLinkRecordDA()
		{
			this.sqlServer = new SqlServer();
		}

		/// <summary>
		/// 插入一条CPS记录
		/// </summary>
		/// <param name="linkRecord">记录对象</param>
		/// <param name="transaction"></param>
		/// <returns>新增的ID</returns>
		public int Insert(Cps_LinkRecord linkRecord, SqlTransaction transaction)
		{
			//Create Procedure sp_Cps_LinkRecord_Insert
			//    @CpsID int,
			//    @URL nvarchar(1024),
			//    @TargetURL nvarchar(4000),
			//    @CreateTime datetime,
			//    @IsDelete int=default,
			//    @ExtField nvarchar(50)=default,
			//    @ReferenceID int output
			//As

			var paras = new List<SqlParameter>
				            {
					            this.sqlServer.CreateSqlParameter(
						            "CpsID",
						            SqlDbType.Int,
						            linkRecord.CpsID,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "URL",
						            SqlDbType.NVarChar,
						            linkRecord.URL,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "TargetURL",
						            SqlDbType.NVarChar,
						            linkRecord.TargetURL,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "CreateTime",
						            SqlDbType.DateTime,
						            DateTime.Now,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "IsDelete",
						            SqlDbType.Int,
						            0,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ExtField",
						            SqlDbType.NVarChar,
						            linkRecord.ExtField,
						            ParameterDirection.Input),
					            this.sqlServer.CreateSqlParameter(
						            "ReferenceID",
						            SqlDbType.Int,
						            null,
						            ParameterDirection.Output)
				            };

			this.sqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Cps_LinkRecord_Insert", paras, transaction);

			return (int)paras.Find(p => p.ParameterName == "ReferenceID").Value;
		}
	}
}