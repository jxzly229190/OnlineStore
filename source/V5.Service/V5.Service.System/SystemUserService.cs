// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemUserService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System;
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统用户服务类
    /// </summary>
    public class SystemUserService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户数据访问对象
        /// </summary>
        private readonly ISystemUserDA systemUserDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUserService"/> class.
        /// </summary>
        public SystemUserService()
        {
            this.systemUserDA = new DAFactorySystem().CreateSystemUserDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="user">
        /// 系统用户对象
        /// </param>
        /// <returns>
        /// 系统用户编号
        /// </returns>
        public int AddUser(System_User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return this.systemUserDA.Insert(user);
        }

        /// <summary>
        /// 删除指定编号的系统用户
        /// </summary>
        /// <param name="userID">
        /// 用户编号
        /// </param>
        public void RemoveUserByID(int userID)
        {
            if (userID <= 0)
            {
                return;
            }

            try
            {
                this.systemUserDA.DeleteByID(userID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="user">
        /// 系统用户对象
        /// </param>
        public void ModifyUser(System_User user)
        {
            this.systemUserDA.Update(user);
        }

        /// <summary>
        /// 查询指定编号的系统用户
        /// </summary>
        /// <param name="loginName">
        /// 登录名
        /// </param>
        /// <returns>
        /// 系统用户对象
        /// </returns>
        public System_User QueryByLoginName(string loginName)
        {
            return this.systemUserDA.SelectByLoginName(loginName);
        }

        /// <summary>
        /// 查询系统用户列表
        /// </summary>
        /// <param name="paging">
        /// 分页信息对象
        /// </param>
        /// <param name="pageCount">
        /// The page Count.
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 系统用户列表
        /// </returns>
        public List<System_User> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemUserDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The is user name exists.
        /// </summary>
        /// <param name="loginName">
        /// The login Name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int IsLoginNameExists(string loginName)
        {
            return this.systemUserDA.IsLoginNameExists(loginName);
        }

        public int UpdatePassWord(int userId, string loginpassword)
        {
            return this.systemUserDA.UpdatePassWord(userId, loginpassword);
        }

        #endregion
    }
}