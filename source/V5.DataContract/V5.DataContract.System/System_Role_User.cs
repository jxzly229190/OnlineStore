// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Role.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台角色用户类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     系统角色类
    /// </summary>
    public class System_Role_User
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置角色/用户名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置父级编码.
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     获取或设置类型（角色/用户 ）
        /// </summary>
        public int Type { get; set; }

        #endregion
    }
}
