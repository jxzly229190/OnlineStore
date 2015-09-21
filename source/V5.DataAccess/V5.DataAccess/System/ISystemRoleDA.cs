// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemRoleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;
    using global::System.Data;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统角色数据库访问接口
    /// </summary>
    public interface ISystemRoleDA
    {
        #region Public Methods and Operators

        int Insert(System_Role role, out SqlTransaction transaction);

        void DeleteByID(int roleID, SqlTransaction transaction);

        List<System_Role> Paging(Paging paging, out int pageCount, out int totalCount);

        List<System_Role> SelectAll();

        List<System_Role_User> SelectAllWithUser();

        #endregion
    }
}