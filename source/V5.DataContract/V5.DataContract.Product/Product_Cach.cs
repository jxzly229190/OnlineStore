// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Cache.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   缓存商品实体类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    /// 缓存商品实体类.
    /// </summary>
    [Serializable]
    public class Product_Cache
    {
        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置商品父级类别编号.
        /// </summary>
        public int ParentCategoryID { get; set; }

        /// <summary>
        ///     获取或设置商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置商品父级品牌.
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
        ///     获取或设置商品广告词．
        /// </summary>
        public string Advertisement { get; set; }

        /// <summary>
        ///     市场价格
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
        ///     商品主图地址
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
        ///     商品状态.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     上架时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     条形码
        /// </summary>
        public string Barcode { get; set; }
    }
}