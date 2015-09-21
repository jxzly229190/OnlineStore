namespace V5.DataAccess.Transact.Order
{
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact;
	using V5.DataContract.Transact.Order;

	public interface IOrderErpLogDA
	{
		/// <summary>
		/// 向数据库写入一条ERP交互日志信息
		/// </summary>
		/// <param name="log">日志</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>写入日志编码</returns>
		int Insert(Order_Erp_Log log,SqlTransaction transaction);

		/// <summary>
		/// 写入HWERP 回写日志信息
		/// </summary>
		/// <param name="log"></param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		int InertHwUpdateLog(Hw_Log log, SqlTransaction transaction);
	}
}