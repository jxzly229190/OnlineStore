// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统权限模型类
    /// </summary>
    public class PermissionModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置父权限编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置后台权限名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入权限名称")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "权限名称长度范围为：2 ~ 32")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置后台权限层级．
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}