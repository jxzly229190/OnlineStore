// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductSearch.cs" company="www.gjw.com">
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
    /// 产品搜索
    /// </summary>
    [Serializable]
    public class ProductSearch
    {
        /// <summary>
        /// 系统主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///  产品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品名称拼音
        /// </summary>
        public string ProductNamePinYin { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductBarcode { get; set; }

        /// <summary>
        /// 产品目录
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// 产品目录拼音
        /// </summary>
        public string ProductCategoryPinYin { get; set; }

        /// <summary>
        /// 产品品牌
        /// </summary>
        public string ProductBrand { get; set; }

        /// <summary>
        /// 产品品牌拼音
        /// </summary>
        public string ProductBrandPinYin { get; set; }

        /// <summary>
        /// 父级目录
        /// </summary>
        public string ParentCategory { get; set; }

        /// <summary>
        /// 父级目录拼音
        /// </summary>
        public string ParentCategoryPinYin { get; set; }

        /// <summary>
        /// 父级产品
        /// </summary>
        public string ParentBrand { get; set; }

        /// <summary>
        /// 父级产品拼音
        /// </summary>
        public string ParentBrandPinYin { get; set; }

        /// <summary>
        /// 市场价格
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        /// 购酒网价格
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        /// 产品搜索条件
        /// </summary>
        public string ProductSearchText { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
