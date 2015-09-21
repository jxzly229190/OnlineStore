namespace V5.DataContract.Transact.Order
{
	using System;

	public class Order_Erp_Log
	{
		public int ID { get; set; }
		public string ERP { get; set; }
		public int OrderID { get; set; }
		public int OperateType { get; set; }
		public int UserID { get; set; }
		public string ReqContent { get; set; }
		public string ResContent { get; set; }
		public bool IsSuccess { get; set; }
		public int Operator { get; set; }
		public string ExtField { get; set; }
		public DateTime CreateTime { get; set; }
	}
}