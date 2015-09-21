namespace V5.DataAccess.Transact.Order
{
	using global::System.Collections.Generic;
	using global::System.Data.SqlClient;

	using V5.DataContract.Transact.Order;

	public interface IOrderProductPromoteDA
	{
		/// <summary>
		/// 插入订单商品促销信息
		/// </summary>
		/// <param name="orderProductPromote">插入的对象</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>返回新增的数据编码</returns>
		int Insert(Order_Product_Promote orderProductPromote, SqlTransaction transaction);

		/// <summary>
		/// 批量插入订单商品促销信息
		/// </summary>
		/// <param name="productPromotes">促销信息列表</param>
		/// <param name="transaction">数据库事务</param>
		/// <returns>插入的记录数</returns>
		int BatchInsert(List<Order_Product_Promote> productPromotes, SqlTransaction transaction);
	}
}