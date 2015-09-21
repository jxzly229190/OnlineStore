namespace V5.Portal.Models
{
	using System;
	using System.Collections.Generic;

	public class SpecialPromote
	{
		/// <summary>
		/// 活动编码
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 活动名称
		/// </summary>
		public string PromoteName { get; set; }
		
		/// <summary>
		/// 赠品商品编码
		/// </summary>
		public int GiftProductID { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		public bool IsValid { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// 参与会员列表
		/// </summary>
		public List<UserPromote> UserPromotes { get; set; }
	}
}