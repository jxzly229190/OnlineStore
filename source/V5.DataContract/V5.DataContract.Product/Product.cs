// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品类
    /// </summary>
    public class Product
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
        /// Gets or sets the parent category name.
        /// </summary>
        public string ParentCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the product category name.
        /// </summary>
        public string ProductCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the parent brand name.
        /// </summary>
        public string ParentBrandName { get; set; }

        /// <summary>
        /// Gets or sets the product brand name.
        /// </summary>
        public string ProductBrandName { get; set; }
        
        /// <summary>
        ///     获取或设置商品条形码．
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置商品 SEO Title．
        /// </summary>
        public string SEOTitle { get; set; }

        /// <summary>
        ///     获取或设置商品 SEO 关键字．
        /// </summary>
        public string SEOKeywords { get; set; }

        /// <summary>
        ///     获取或设置商品 SEO 描述．
        /// </summary>
        public string SEODescription { get; set; }

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
        /// 真实销售数量
        /// </summary>
        public int SoldOfReality { get; set; }

        /// <summary>
        /// 虚拟销售数量
        /// </summary>
        public int SoldOfVirtual { get; set; }

        /// <summary>
        ///     获取或设置库存数量．
        /// </summary>
        public int InventoryNumber { get; set; }

        /// <summary>
        ///     获取或设置评论总数．
        /// </summary>
        public int CommentNumber { get; set; }

        /// <summary>
        ///     获取或设置页面浏览量．
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置商品状态（0：未上架，1：已上架，2：已下架，3：已删除）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        ///     获取或设置扩展属性
        /// </summary>
        public string Attributes { get; set; }
        
        #endregion
    }
}