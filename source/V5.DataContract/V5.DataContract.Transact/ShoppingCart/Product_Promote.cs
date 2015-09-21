// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Promote.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   非促销及单品促销.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.ShoppingCart
{
    /// <summary>
    /// 单品促销.
    /// </summary>
    public class Product_Promote
    {
        /// <summary>
        ///     获取或设置商品参加的促销活动编号．
        /// </summary>
        public int PromoteID { get; set; }

        /// <summary>
        ///     获取或设置商品参加的促销活动类型（1：限时打折，2：团购，3：等级价格）．
        /// </summary>
        public int PromoteType { get; set; }

        /// <summary>
        ///     获取或设置商品参加的促销活动名称．
        /// </summary>
        public string PromoteName { get; set; }

        /// <summary>
        ///     获取或设置商品优惠金额．
        /// </summary>
        public double DiscountPrice { get; set; }
    }
}
