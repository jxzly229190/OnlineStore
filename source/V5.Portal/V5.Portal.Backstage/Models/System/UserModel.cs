// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统用户模型类
    /// </summary>
    public class UserModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工编号．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择指定员工")]
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置角色编号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择指定角色")]
        public int RoleID { get; set; }

        /// <summary>
        ///     获取或设置后台用户名．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入您的登录名")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "登录名长度范围为：5 ~ 16")]
        public string LoginName { get; set; }

        /// <summary>
        ///     获取或设置后台用户登录密码．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入您的密码")]
        [StringLength(128, MinimumLength = 6, ErrorMessage = "密码长度范围为：6 ~ 16")]
        public string LoginPassword { get; set; }

        /// <summary>
        ///     获取或设置后台用户姓名．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入您的姓名")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "姓名长度范围为：2 ~ 16")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置后台用户状态．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}