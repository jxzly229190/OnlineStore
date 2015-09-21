// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统菜单模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统菜单模型类
    /// </summary>
    public class MenuModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置后台菜单权限编号．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择对应权限")]
        public int PermissionID { get; set; }

        /// <summary>
        ///     获取或设置父菜单编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置后台菜单层级．
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     获取或设置后台菜单名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入菜单名称")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "菜单名称长度范围为：2 ~ 32")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置后台菜单网址．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入连接地址")]
        public string URL { get; set; }

        /// <summary>
        ///     获取或设置后台菜单排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}