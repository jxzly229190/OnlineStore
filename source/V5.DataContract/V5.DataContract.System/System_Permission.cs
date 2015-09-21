// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Permission.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台权限类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     后台权限类
    /// </summary>
    public class System_Permission
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置父权限编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置后台权限名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置后台权限层级．
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}