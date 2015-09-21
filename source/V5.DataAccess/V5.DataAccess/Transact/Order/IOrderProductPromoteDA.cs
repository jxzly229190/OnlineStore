namespace V5.DataAccess.Transact.Order
{
	using global::System.Collections.Generic;
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact.Order;

	public interface IOrderProductPromoteDA
	{
		/// <summary>
		/// ���붩����Ʒ������Ϣ
		/// </summary>
		/// <param name="orderProductPromote">����Ķ���</param>
		/// <param name="transaction">���ݿ�����</param>
		/// <returns>�������������ݱ���</returns>
		int Insert(Order_Product_Promote orderProductPromote, SqlTransaction transaction);

		/// <summary>
		/// �������붩����Ʒ������Ϣ
		/// </summary>
		/// <param name="productPromotes">������Ϣ�б�</param>
		/// <param name="transaction">���ݿ�����</param>
		/// <returns>����ļ�¼��</returns>
		int BatchInsert(List<Order_Product_Promote> productPromotes, SqlTransaction transaction);
	}
}