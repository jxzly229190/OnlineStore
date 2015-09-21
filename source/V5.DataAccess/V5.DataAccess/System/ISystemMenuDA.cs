// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemMenuDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统菜单数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统菜单数据库访问接口
    /// </summary>
    public interface ISystemMenuDA
    {
        #region Public Methods and Operators

        int Insert(System_Menu menu);

        void DeleteByID(int menuID);

        void Update(System_Menu menu);

        List<System_Menu> Paging(Paging paging, out int pageCount, out int totalCount);

        List<System_Menu> SelectAll();

        List<System_Menu> SelectByRoleID(int roleID);

        #endregion
    }
}