// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_User.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台用户类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     后台用户类
    /// </summary>
    public class System_User
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置角色编号．
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        ///     获取或设置后台用户名．
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        ///     获取或设置后台用户登录密码．
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        ///     获取或设置后台用户姓名．
        /// </summary>
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