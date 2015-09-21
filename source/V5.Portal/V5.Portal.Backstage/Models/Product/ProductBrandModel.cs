using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Product
{
    using global::System.ComponentModel.DataAnnotations;

    public class ProductBrandModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置品牌所属商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置父品牌编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置品牌名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "品牌名称不能为空")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "品牌名称不能大于{2} 且要小于{1}")]
        public string BrandName { get; set; }

        /// <summary>
        ///     获取或设置品牌名称拼音．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "品牌名称拼音不能为空")]
        public string BrandNameSpell { get; set; }

        /// <summary>
        ///     获取或设置品牌名称英文．
        /// </summary>
        public string BrandNameEnglish { get; set; }

        /// <summary>
        ///     获取或设置品牌 SEO 关键字．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "SEO关键字不可为空")]
        public string SEOKeywords { get; set; }

        /// <summary>
        ///     获取或设置品牌 SEO 描述．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "SEO描述不可为空")]
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "SEO标题不可为空")]
        public string SEOTitle { get; set; }


        #endregion
    }
}