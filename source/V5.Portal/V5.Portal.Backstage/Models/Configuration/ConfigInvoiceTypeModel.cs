// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigInvoiceTypeModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票类型 Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 发票类型 Model
    /// </summary>
    public class ConfigInvoiceTypeModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置发票类型名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称不能为空！")]
        [StringLength(16, ErrorMessage = "长度不允许超过16个字符！！")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置发票类型描述．
        /// </summary>
        [StringLength(5000, ErrorMessage = "长度不允许超过5000个字符！！")]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}