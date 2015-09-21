// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserLevelModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员等级模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 会员等级模型类.
    /// </summary>
    public class UserLevelModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置会员等级编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置等级名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "会员等级名称不能为空")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "长度不能大于{1}")]
        [RegularExpression(@"^[0-9a-zA-Z\u4E00-\u9FA5]+$", ErrorMessage = "输入不正确")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置需要满足金额．
        /// </summary>
        public double Money { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}