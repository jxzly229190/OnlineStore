namespace V5.Service.Transact
{
	using System;
	using System.Data.SqlClient;

	using V5.DataAccess;
	using V5.DataAccess.Transact.Order;
	using V5.DataContract.Transact;
	using V5.DataContract.Transact.Order;

	public class OrderERPLogService
	{
		private IOrderErpLogDA erpLogDa;

		public OrderERPLogService()
		{
			this.erpLogDa = new DAFactoryTransact().CreateOrderErpLogDA();
		}

		public int Add(Order_Erp_Log log,SqlTransaction transaction)
		{
			if (log == null)
			{
				throw new NullReferenceException("参数Log不能为空");
			}

			return this.erpLogDa.Insert(log, transaction);
		}

		public int AddHwLog(Hw_Log log, SqlTransaction transaction)
		{
			return this.erpLogDa.InertHwUpdateLog(log, transaction);
		}
	}
}