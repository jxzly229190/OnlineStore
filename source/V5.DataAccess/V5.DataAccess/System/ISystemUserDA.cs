// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemUserDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统用户数据库访问接口
    /// </summary>
    public interface ISystemUserDA
    {
        #region Public Methods and Operators

        int Insert(System_User user);

        void DeleteByID(int userID);

        void Update(System_User user);

        System_User SelectByLoginName(string loginName);

        List<System_User> Paging(Paging paging, out int pageCount, out int totalCount);

        int IsLoginNameExists(string loginName);

        int UpdatePassWord(int userId, string loginpassword);

        #endregion
    }
}