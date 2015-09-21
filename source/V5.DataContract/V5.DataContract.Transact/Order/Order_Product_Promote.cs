namespace V5.DataContract.Transact.Order
{
	using System;

	public class Order_Product_Promote
	{
		public int ID { get; set; }
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public int OrderProductID { get; set; }
		public double PromoteDiscount { get; set; }
		public int PromoteType { get; set; }
		public int PromoteID { get; set; }
		public string Remark { get; set; }
		public string ExtField { get; set; }
		public DateTime CreateTime { get; set; }
	}
}