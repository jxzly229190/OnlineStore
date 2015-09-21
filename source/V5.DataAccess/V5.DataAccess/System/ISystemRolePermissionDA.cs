// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemRolePermissionDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色权限关系数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.System;

    /// <summary>
    /// 系统角色权限关系数据库访问接口
    /// </summary>
    public interface ISystemRolePermissionDA
    {
        #region Public Methods and Operators

        void Insert(System_Role_Permission rolePermission, SqlTransaction transaction);

        void DeleteByID(int roleID, out SqlTransaction transaction);

        List<System_Role_Permission> SelectByRoleID(int roleID);

        #endregion
    }
}