// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemRightsDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限表
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using V5.DataContract.System;

    /// <summary>
    /// The SystemRightsDA interface.
    /// </summary>
    public interface ISystemRightsDA
    {
        /// <summary>
        /// The select by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="System_Rights"/>.
        /// </returns>
        string SelectByUserID(int userID);

        /// <summary>
        /// The select by role id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string SelectByRoleID(int roleID);

        /// <summary>
        /// 新增系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        int Insert(System_Rights rights);

        /// <summary>
        /// 修改系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        int Update(System_Rights rights);

        /// <summary>
        /// 判断角色或用户是否具有系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        bool Exists(System_Rights rights);
    }
}