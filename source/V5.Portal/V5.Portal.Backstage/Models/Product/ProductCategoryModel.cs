// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoryModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类别模型.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 商品类别模型.
    /// </summary>
    public class ProductCategoryModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置父类别编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置类别名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "类别名称不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "类别名称长度必须在 2~10 之间")]
        public string CategoryName { get; set; }

        /// <summary>
        ///     获取或设置类别名称拼音．
        /// </summary>
        public string CategoryNameSpell { get; set; }

        /// <summary>
        ///     获取或设置类别名称英文．
        /// </summary>
        public string CategoryNameEnglish { get; set; }

        /// <summary>
        ///     获取或设置类别 SEO 关键字．
        /// </summary>
        public string SEOKeywords { get; set; }

        /// <summary>
        ///     获取或设置类别 SEO 描述．
        /// </summary>
        public string SEODescription { get; set; }

        /// <summary>
        ///     获取或设置类别是否属于官网．
        /// </summary>
        public bool IsGjw { get; set; }

        /// <summary>
        ///     获取或设置类别是否显示．
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        ///     获取或设置类别所在层级．
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
        ///     设置SEO标题
        /// </summary>
        public string SEOTitle { get; set; }

        #endregion
    }
}