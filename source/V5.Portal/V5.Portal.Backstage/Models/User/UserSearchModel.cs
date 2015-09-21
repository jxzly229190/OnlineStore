// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserSearchModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员搜索条件模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;

    /// <summary>
    /// 会员搜索条件模型类.
    /// </summary>
    public class UserSearchModel
    {
        #region Public Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSearchModel"/> class.
        /// </summary>
        public UserSearchModel()
        {
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置会员等级编号.
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        /// 获取或设置会员名称.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置会员状态.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置会员的手机号.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置会员电子邮箱地址.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置会员注册时间范围的开始时间.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置会员注册时间范围的结束时间.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置会员是否成功交易过.
        /// </summary>
        public int IsHasOrder { get; set; }

        /// <summary>
        /// 获取或设置查询结果数（导出）.
        /// </summary>
        public int TotalCount { get; set; }

        #endregion
    }
}