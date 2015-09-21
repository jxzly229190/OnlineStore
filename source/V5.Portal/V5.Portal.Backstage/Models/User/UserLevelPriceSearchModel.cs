// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserLevelPriceSearchModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员等级价格搜索条件模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;

    /// <summary>
    /// 会员等级价格搜索条件模型类.
    /// </summary>
    public class UserLevelPriceSearchModel
    {
        #region Public Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLevelPriceSearchModel"/> class.
        /// </summary>
        public UserLevelPriceSearchModel()
        {
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置会员等级编号.
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        ///     获取或设置商品名称.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置员工姓名.
        /// </summary>
        public string EmployeeName { get; set; }

       /// <summary>
        /// 获取或设置会员注册时间范围的开始时间.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置会员注册时间范围的结束时间.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     获取或设置状态（0：正常，1：已停止）．
        /// </summary>
        public int Status { get; set; }

        #endregion
    }
}