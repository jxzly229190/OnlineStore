// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DepartmentModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统部门模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统部门模型类
    /// </summary>
    public class DepartmentModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置部门名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "部门名称不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "部门名称长度必须在 2~10 之间")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置部门总人数．
        /// </summary>
        public int Headcount { get; set; }

        /// <summary>
        ///     获取或设置部门负责人．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "负责人姓名不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "负责人姓名长度必须在 2~10 之间")]
        public string Principal { get; set; }

        /// <summary>
        ///     获取或设置部门负责人手机号码．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码不能为空")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号码长度必须为 11 位")]
        [RegularExpression(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", ErrorMessage = "手机号码格式错误")]
        public string PrincipalMobile { get; set; }

        /// <summary>
        ///     获取或设置部门描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}