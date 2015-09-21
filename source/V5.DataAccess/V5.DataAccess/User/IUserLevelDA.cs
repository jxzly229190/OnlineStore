// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserLevelDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员等级数据库访问接口类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;

    /// <summary>
    /// 会员等级数据库访问接口类.
    /// </summary>
    public interface IUserLevelDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 新增会员等级
        /// </summary>
        /// <param name="userLevel">
        /// User_Leve对象.
        /// </param>
        /// <returns>
        /// 会员等级主键编号.
        /// </returns>
        int Insert(User_Level userLevel);

        /// <summary>
        /// 删除指定的会员等级.
        /// </summary>
        /// <param name="userLevelID">
        /// 等级编号.
        /// </param>
        void DeleteByID(int userLevelID);

        /// <summary>
        /// 修改更新会员等级信息.
        /// </summary>
        /// <param name="userLevel">
        /// User_Level的对象实例.
        /// </param>
        void Update(User_Level userLevel);

        /// <summary>
        /// 查询所有的会员等级.
        /// </summary>
        /// <returns>
        /// 会员的对象列表.
        /// </returns>
        List<User_Level> SelectAll();

        /// <summary>
        /// 根据指定ID查询会员等级信息.
        /// </summary>
        /// <param name="userLevelID">
        /// 会员等级编号.
        /// </param>
        /// <returns>
        /// 会员等级的对象实例.
        /// </returns>
        User_Level SeLevelByID(int userLevelID);

        #endregion
    }
}