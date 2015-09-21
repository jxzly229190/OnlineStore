// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCorporationModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货公司 MVC Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 送货公司 MODEL
    /// </summary>
    public class ConfigDeliveryCorporationModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货公司名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "公司名称不能为空！")]
        [StringLength(32, ErrorMessage = "公司名称不允许超过32个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置送货公司物流信息查询网址．
        /// </summary>
        [StringLength(64, ErrorMessage = "公司URL不允许超过64个字符")]
        public string URL { get; set; }

        /// <summary>
        /// 获取或设置送货公司描述．
        /// </summary>
        [StringLength(5000, ErrorMessage = "描述不允许超过5000个字符")]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}