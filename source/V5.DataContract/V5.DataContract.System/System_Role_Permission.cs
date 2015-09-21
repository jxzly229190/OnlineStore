// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Role_Permission.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台角色权限关系类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    /// <summary>
    ///     后台角色权限关系类
    /// </summary>
    public class System_Role_Permission
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置后台角色编号．
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        ///     获取或设置后台权限编号．
        /// </summary>
        public int PermissionID { get; set; }

        #endregion
    }
}