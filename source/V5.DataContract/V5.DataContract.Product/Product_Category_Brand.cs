using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V5.DataContract.Product
{
    /// <summary>
    /// 商品品牌
    /// </summary>
    [Serializable]
    public class Product_Category_Brand
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public int ProductCategory_ID { get; set; }

        /// <summary>
        /// 类别父ID
        /// </summary>
        public int ProductCategory_ParentID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ProductCategory_Name { get; set; }
        /// <summary>
        /// 类别拼音
        /// </summary>
        public string ProductCategory_NameSpell { get; set; }
        /// <summary>
        /// 类别英文名称
        /// </summary>
        public string ProductCategory_NameEnglish { get; set; }
        /// <summary>
        /// 类别SEO标题
        /// </summary>
        public string ProductCategory_SEOTitle { get; set; }
        /// <summary>
        /// 类别SEO关键词
        /// </summary>
        public string ProductCategory_SEOKeyWords { get; set; }
        /// <summary>
        /// 类别SEO关键词描述
        /// </summary>
        public string ProductCategory_SEODescription { get; set; }
        /// <summary>
        /// 类别是否为购酒网
        /// </summary>
        public bool ProductCategory_IsGJW { get; set; }
        /// <summary>
        /// 类别是否显示
        /// </summary>
        public bool ProductCategory_IsDisplay { get; set; }
        /// <summary>
        /// 类别级别
        /// </summary>
        public int ProductCategory_Layer { get; set; }
        /// <summary>
        /// 类别排序字段
        /// </summary>
        public int ProductCategory_Sorting { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int ProductBrand_ID { get; set; }
        /// <summary>
        /// 品牌关联类别ID
        /// </summary>
        public int ProductBrand_CategoryID { get; set; }
        /// <summary>
        /// 品牌父ID
        /// </summary>
        public int ProductBrand_ParentID { get; set; }
        /// <summary>
        /// 品牌父名称
        /// </summary>
        public string ProductBrand_ParentName { get; set; }
        /// <summary>
        /// 品牌父拼音
        /// </summary>
        public string ProductBrand_ParentNameSpell { get; set; }
        /// <summary>
        /// 品牌父英文
        /// </summary>
        public string ProductBrand_ParentNameEnglish { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string ProductBrand_Name { get; set; }
        /// <summary>
        /// 品牌拼音
        /// </summary>
        public string ProductBrand_NameSpell { get; set; }
        /// <summary>
        /// 品牌英文名称
        /// </summary>
        public string ProductBrand_NameEnglish { get; set; }        
        /// <summary>
        /// 品牌SEO标题
        /// </summary>
        public string ProductBrand_SEOTitle { get; set; }
        /// <summary>
        /// 品牌SEO关键词
        /// </summary>
        public string ProductBrand_SEOKeyWords { get; set; }
        /// <summary>
        /// 品牌SEO描述
        /// </summary>
        public string ProductBrand_SEODescription { get; set; }
        /// <summary>
        /// 品牌是否显示
        /// </summary>
        public bool ProductBrand_IsDisplay { get; set; }
        /// <summary>
        /// 品牌级别
        /// </summary>
        public int ProductBrand_Layer { get; set; }
        /// <summary>
        /// 品牌排序字段
        /// </summary>
        public int ProductBrand_Sorting { get; set; }
        /// <summary>
        /// 品牌URL
        /// </summary>
        public string ProductBrand_URL { get; set; }
    }
}
