// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Category.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类别类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品类别类
    /// </summary>
    public class Product_Category
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
        /// SEO标题
        /// </summary>
        public string SEOTitle { get; set; }
        
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
        public int? Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}