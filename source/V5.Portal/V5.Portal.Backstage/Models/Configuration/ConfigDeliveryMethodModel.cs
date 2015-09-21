// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryMethodModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货方式 Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// 送货方式 Model
    /// </summary>
    public class ConfigDeliveryMethodModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货方式名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不能为空")]
        [StringLength(16, ErrorMessage = "字符长度不允许超过16")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置送货方式描述．
        /// </summary>
        [StringLength(5000, ErrorMessage = "字符长度不允许超过5000")]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}