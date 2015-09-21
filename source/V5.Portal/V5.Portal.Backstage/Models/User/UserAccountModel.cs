// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAccountModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the UserAccount type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;

    /// <summary>
    /// 会员账户模型类.
    /// </summary>
    public class UserAccountModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置会员编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置会员账户余额．
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// 获取或设置会员账户状态（1：正常，2：锁定）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}