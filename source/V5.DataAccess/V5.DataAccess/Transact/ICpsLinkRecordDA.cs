namespace V5.DataAccess.Transact
{
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact;

	public interface ICpsLinkRecordDA
	{
		/// <summary>
		/// 插入一条CPS记录
		/// </summary>
		/// <param name="linkRecord">记录对象</param>
		/// <param name="transaction"></param>
		/// <returns>新增的ID</returns>
		int Insert(Cps_LinkRecord linkRecord, SqlTransaction transaction);
	}
}
