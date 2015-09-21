// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gift_Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   礼品.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.ShoppingCart
{
    /// <summary>
    /// 礼品.
    /// </summary>
    public class Gift_Product
    {
        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置商品数量．
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 商品主图地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置促销活动编号*．
        /// </summary>
        public int PromotID { get; set; }

        /// <summary>
        ///     获取或设置促销活动类型（1:MeetMoney,2:MeetAmount）*．
        /// </summary>
        public int PromotType { get; set; }
    }
}
