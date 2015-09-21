namespace V5.Portal.Models
{
	using System;

	/// <summary>
	/// 会员促销活动
	/// </summary>
	public class UserPromote
	{
		/// <summary>
		/// 参与活动会员编码
		/// </summary>
		public int UserID { get; set; }

		/// <summary>
		/// 参与活动时间
		/// </summary>
		public DateTime ParticipateTime { get; set; }

		/// <summary>
		/// 是否领取赠品
		/// </summary>
		public bool HasGetGift { get; set; }

		/// <summary>
		/// 获取赠品时间
		/// </summary>
		public DateTime GetGiftTime { get; set; }

		/// <summary>
		/// 赠品编码
		/// </summary>
		public int GiftID { get; set; }

		/// <summary>
		/// 领取赠品的订单编码
		/// </summary>
		public int OrderID { get; set; }
	}
}