namespace V5.Service.Transact
{
	using System.Collections.Generic;
	using System.Data.SqlClient;

	using V5.DataAccess;
	using V5.DataAccess.Transact.Order;
	using V5.DataContract.Transact.Order;
	using V5.Library.Logger;

	public class OrderProductPromoteService
	{
		private readonly IOrderProductPromoteDA productPromoteDa;

		public OrderProductPromoteService()
		{
			productPromoteDa = new DAFactoryTransact().CreateOrderProductPromoteDA();
		}

		public int Add(Order_Product_Promote model, SqlTransaction transaction=null)
		{
			if (model == null)
			{
				LogUtils.Log("新增订单商品促销实体时实体参数为空", "服务层", Category.Warn);
				return 0;
			}

			return this.productPromoteDa.Insert(model, transaction);
		}

		public int BatchAdd(List<Order_Product_Promote> list, SqlTransaction transaction = null)
		{
			if (list == null || list.Count < 1)
			{
				LogUtils.Log("批量新增订单商品促销实体时实体参数为空", "服务层", Category.Warn);
				return 0;
			}

			return this.productPromoteDa.BatchInsert(list, transaction);
		}
	}
}