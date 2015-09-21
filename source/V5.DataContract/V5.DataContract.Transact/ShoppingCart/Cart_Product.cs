// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cart_Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   购物车商品.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact.ShoppingCart
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 购物车商品非组合促销商品.
    /// </summary>
    [Serializable]
    public class Cart_Product
    {
        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the parent category id.
        /// </summary>
        public int ParentCategoryID { get; set; }

        /// <summary>
        ///     获取或设置商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the parent brand id.
        /// </summary>
        public int ParentBrandID { get; set; }

        /// <summary>
        ///     获取或设置商品品牌编号．
        /// </summary>
        public int ProductBrandID { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置商品购买数量．
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     获取或设置商品广告词．
        /// </summary>
        public string Advertisement { get; set; }

        /// <summary>
        /// 市场价格
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        ///     获取或设置库存数量．
        /// </summary>
        public int InventoryNumber { get; set; }

        /// <summary>
        ///     获取或设置评论分数．
        /// </summary>
        public double CommentScore { get; set; }

        /// <summary>
        ///     获取或设置评论总数．
        /// </summary>
        public int CommentNumber { get; set; }

        /// <summary>
        ///     获取或设置真实销量．
        /// </summary>
        public int SoldOfReality { get; set; }

        /// <summary>
        ///     获取或设置虚拟销量．
        /// </summary>
        public int SoldOfVirtual { get; set; }

        /// <summary>
        ///     获取或设置页面浏览量．
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 商品主图地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置当前价格．
        /// </summary>
        public double PromotePrice { get; set; }

        /// <summary>
        ///     获取或设置购酒网原价．
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        ///     获取或设置商品价格小计．
        /// </summary>
        public double Subtotal
        {
            get
            {
                return this.PromotePrice * this.Quantity;
            }
        }

        /// <summary>
        ///     获取或设置商品的促销活动列表.
        /// </summary>
        public List<Product_Promote> Promotes { get; set; } 
        
        /// <summary>
        ///     获取或设置商品参加的促销活动编号．
        /// </summary>
        public string PromoteIDs { get; set; }

        /// <summary>
        ///     获取或设置商品参加的促销活动类型．
        /// </summary>
        public string PromoteTypes { get; set; }

        /// <summary>
        ///     获取或设置商品参加的促销活动名称．
        /// </summary>
        public string PromoteNames { get; set; }

	    /// <summary>
        ///     获取或设置商品优惠活动信息．
        /// </summary>
        public string Favorable { get; set; }

        /// <summary>
        ///     获取或设置商品优惠活动价格（满额、满件不在此列）．
        /// </summary>
        public double FavorablePrice
        {
            get
            {
	            return (this.GoujiuPrice - this.PromotePrice) * this.Quantity;
            }
        }

        /// <summary>
        /// 获取或设置是否仅支持在线支付．
        /// </summary>
        public bool IsOnlinePayment { get; set; }

        /// <summary>
        /// 获取或设置是否需要手机验证（true:手机验证后才可参与）．
        /// </summary>
        public bool IsMobileValidate { get; set; }

        /// <summary>
        /// 获取或设置是否可使用优惠券（true:可以使用）．
        /// </summary>
        public bool IsUseCoupon { get; set; }

        /// <summary>
        /// 获取或设置是否是新会员活动（true:只能新会员参与）．
        /// </summary>
        public bool IsNewUser { get; set; }

        /// <summary>
        /// 获取或设置限购数量．
        /// </summary>
        public int LimitedBuyQuantity { get; set; }

        /// <summary>
        /// 获取或设置活动商品剩余数量．
        /// </summary>
        public int PromoteResidueQuantity { get; set; }

        /// <summary>
        /// 获取或设置允许会员购买的最大数量．
        /// </summary>
        public int MaxBuyQuantity { get; set; }

        /// <summary>
        /// 商品状态.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 排斥商品（买了此商品不能购买的商品）.
        /// </summary>
        public string Exclude { get; set; }

        /// <summary>
        ///  不允许购买标识（0：允许购买，1：只允许新会员购买，2：只允许老会员购买，3：需要手机验证会员购买，4：需要邮箱验证会员购买，5：未登录用户不允许购买）
        /// </summary>
        public int DenyFlag { get; set; }
    }
}
