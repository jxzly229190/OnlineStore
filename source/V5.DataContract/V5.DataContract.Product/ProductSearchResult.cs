// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductSearchResult.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product search result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    /// The product search result.
    /// </summary>
    public class ProductSearchResult
    {
        #region Public Properties
        
        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

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
        ///     获取或设置商品条形码．
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置商品广告词．
        /// </summary>
        public string Advertisement { get; set; }

        /// <summary>
        ///     获取或设置市场价．
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        ///     获取或设置购酒价．
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        ///     获取或设置商品详细介绍．
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        ///     获取或设置是否开发票．
        /// </summary>
        public bool IsInvoice { get; set; }

        /// <summary>
        ///     获取或设置购买商品所赠积分．
        /// </summary>
        public int Integral { get; set; }

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
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置商品状态（1：未上架，2：已上架，3：已下架，4：已回收）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 商品品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商品父级品牌名称
        /// </summary>
        public string ParentBrandName { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 商品主图地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置图片地址
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// 商品主图名字
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 商品属性
        /// </summary>
        public string Attributes { get; set; }

        #endregion 
    }
}