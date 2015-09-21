// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Role.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台角色类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     系统角色类
    /// </summary>
    public class System_Role
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置后台角色名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置使用该角色的用户总数.
        /// </summary>
        public int Headcount { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}