// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统角色模型类
    /// </summary>
    public class RoleModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置后台角色名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入角色名称")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "角色名称长度范围为：2 ~ 32")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}