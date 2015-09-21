// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemPermissionDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统权限数据库访问接口
    /// </summary>
    public interface ISystemPermissionDA
    {
        #region Public Methods and Operators

        int Insert(System_Permission permission);

        void DeleteByID(int permissionID);

        void Update(System_Permission permission);

        List<System_Permission> Paging(Paging paging, out int pageCount, out int totalCount);

        List<System_Permission> SelectAll();

        bool HasChildren(int permissionID);

        #endregion
    }
}