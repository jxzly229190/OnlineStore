// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Rights.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The System_Rights class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    /// The System_Rights class.
    /// </summary>
    public class System_Rights
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置角色编码．
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 获取或设置用户编码．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置权限．
        /// </summary>
        public string UserRights { get; set; }

        #endregion
    }
}