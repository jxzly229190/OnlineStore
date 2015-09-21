// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order_Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单商品类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.Order
{
    using System;

    /// <summary>
    ///     订单商品类
    /// </summary>
    public class Order_Product
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置商品条形码．
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置商品库存．
        /// </summary>
        public int InventoryNumber { get; set; }

        /// <summary>
        ///     获取或设置商品主图路径．
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置数量．
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     获取或设置市场价
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        ///     获取或设置购酒价
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        ///     获取或设置成交金额小计.
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        ///     获取或设置成交价．
        /// </summary>
        public double TransactPrice { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime TransactTime { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime CreateTime { get; set; }

		/// <summary>
		/// 促销编码
		/// </summary>
		public int PromotionID { get; set; }

		/// <summary>
		/// 促销类型
		/// </summary>
		public int PromotionType { get; set; }

		/// <summary>
		/// 促销结果：0失败，1成功
		/// </summary>
	    public int PromotionResult { get; set; }

		/// <summary>
		/// 赠送积分
		/// </summary>
		public int Integral { get; set; }

		/// <summary>
		/// 返利比例
		/// </summary>
		public double RebateRate { get; set; }

		/// <summary>
		/// 所属系列佣金比例 
		/// </summary>
		public double Commission { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
	    public string Remark { get; set; }

		/// <summary>
		/// 扩展字段
		/// </summary>
	    public string ExtField { get; set; }

        #endregion

    }
}