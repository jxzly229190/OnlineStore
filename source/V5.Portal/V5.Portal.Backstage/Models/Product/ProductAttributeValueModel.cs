// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeValueModel.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   The product attribute value model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The product attribute value model.
    /// </summary>
    public class ProductAttributeValueModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品属性编号．
        /// </summary>
        public int AttributeID { get; set; }

        /// <summary>
        ///     获取或设置商品属性值．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "属性值不能为空")]
        public string AttributeValue { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     获取或设置默认值
        /// </summary>
        public int IsDefault { get; set; }

        public string IsDefaultDisPlay
        {
            get
            {
                if (IsDefault == 1)
                {
                    return "默认";
                }
                return "否";
            }
        }

        #endregion
    }
}