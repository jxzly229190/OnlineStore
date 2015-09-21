// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Brand.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品品牌类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品品牌类
    /// </summary>
    public class Product_Brand
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置图片品牌编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置父品牌编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置品牌名称．
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        ///     获取或设置品牌名称拼音．
        /// </summary>
        public string BrandNameSpell { get; set; }

        /// <summary>
        ///     获取或设置品牌名称英文．
        /// </summary>
        public string BrandNameEnglish { get; set; }

        /// <summary>
        ///     获取或设置品牌 SEO 关键字．
        /// </summary>
        public string SEOKeywords { get; set; }

        /// <summary>
        ///     获取或设置品牌 SEO 描述．
        /// </summary>
        public string SEODescription { get; set; }

        /// <summary>
        ///     获取或设置品牌是否显示．
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        ///     获取或设置品牌所在层级．
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string SEOTitle { get; set; }

        #endregion
    }
}