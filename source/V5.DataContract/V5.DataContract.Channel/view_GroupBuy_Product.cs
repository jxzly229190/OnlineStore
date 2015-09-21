// --------------------------------------------------------------------------------------------------------------------
// <copyright file="view_GroupBuy_Product.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the view_GroupBuy_Product type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Channel
{
	using System;

	/// <summary>
	/// 团购商品视
	/// </summary>
	public class View_GroupBuy_Product
	{
		#region Public Properties

		/// <summary>
		/// 团购商品ID
		/// </summary>
		public int ProductId
		{
			get;
			set;
		}

		/// <summary>
		/// 团购状态
		/// </summary>
		public int Status
		{
			get;
			set;
		}

		/// <summary>
		/// 团购名称
		/// </summary>
		public string GroupBuyName
		{
			get;
			set;
		}

		/// <summary>
		/// 条形码，商品编码
		/// </summary>
		public string Barcode
		{
			get;
			set;
		}

		/// <summary>
		/// 图片路径
		/// </summary>
		public string ImageUrl
		{
			get;
			set;
		}

		/// <summary>
		/// 团购开始时间
		/// </summary>
		public DateTime StartTime
		{
			get;
			set;
		}

		/// <summary>
		/// 团购结束时间
		/// </summary>
		public DateTime EndTime
		{
			get;
			set;
		}

		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName
		{
			get;
			set;
		}

		/// <summary>
		/// 总数
		/// </summary>
		public int TotalNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 市场价格
		/// </summary>
		public double GoujiuPrice
		{
			get;
			set;
		}

		/// <summary>
		/// 团购价格
		/// </summary>
		public double GBPrice
		{
			get;
			set;
		}

		/// <summary>
		/// 会员级别
		/// </summary>
		public string LevelName
		{
			get;
			set;
		}

		/// <summary>
		/// 显示级别
		/// </summary>
		public int ShowLevel
		{
			get;
			set;
		}

		/// <summary>
		/// 会员级别ID
		/// </summary>
		public int UserLevelID
		{
			get;
			set;
		}

		#endregion
	}
}
