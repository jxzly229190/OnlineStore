// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员数据库访问借口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员数据库访问接口.
    /// </summary>
    public interface IUserDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 查询互联登陆信息.
        /// </summary>
        /// <param name="openID">
        /// 互联登陆会员标识.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User SelectByOpenID(string openID);

        /// <summary>
        /// 根据用户名、手机、邮箱查询会员信息.
        /// </summary>
        /// <param name="login">
        /// 用户名或手机或邮箱.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User SelectRow(string login);

        /// <summary>
        /// 会员注册/后台添加会员.
        /// </summary>
        /// <param name="user">
        /// 会员对象.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(User user);

        /// <summary>
        /// 会员注册/后台添加会员.
        /// </summary>
        /// <param name="user">
        /// 会员对象.
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(User user, out SqlTransaction transaction);

        /// <summary>
        /// 根据指定ID编号查询用户详细信息.
        /// </summary>
        /// <param name="userID">
        /// 会员ID编号
        /// </param>
        /// <returns>
        /// User的对象实例
        /// </returns>
        User SelectUserByID(int userID);

        /// <summary>
        /// 查询指定会员等级.
        /// </summary>
        /// <param name="userLevelID">
        /// 会员等级编号.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        List<User> SelectUserByLevelID(int userLevelID);

        /// <summary>
        /// 根据电话或者邮箱查询会员信息
        /// </summary>
        /// <param name="searchStr">
        /// 查询字符串
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        User SelectUserByMobileOrEmail(string searchStr);

        /// <summary>
        /// 更新会员信息.
        /// </summary>
        /// <param name="user">
        /// User的对象实例
        /// </param>
        void UpdateUserInfo(User user);

        /// <summary>
        /// 锁定、解锁会员帐号.
        /// </summary>
        /// <param name="userID">
        /// 会员ID编号.
        /// </param>
        /// <param name="status">
        /// 会员的状态0正常，1锁定.
        /// </param>
        void UpdateUserStatus(int userID, int status);

        /// <summary>
        /// 重置指定会员的密码
        /// </summary>
        /// <param name="userID">
        /// 会员ID
        /// </param>
        void ResetPassword(int userID);

        /// <summary>
        /// 修改密码.
        /// </summary>
        /// <param name="userID">
        /// 用户编号.
        /// </param>
        /// <param name="password">
        /// 修改后的密码
        /// </param>
        void UpdatePassword(int userID, string password);

        /// <summary>
        /// 查询所有的会员列表
        /// </summary>
        /// <returns>
        /// User的对象实例.
        /// </returns>
        List<User> SelectAll();

        /// <summary>
        /// 查询会员列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 会员列表
        /// </returns>
        List<User> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 验证邮箱是否重复.
        /// </summary>
        /// <param name="email">
        /// 电子邮箱.
        /// </param>
        /// <returns>
        /// 0：没有重复，1：有重复.
        /// </returns>
        int IsEmailExists(string email);

        /// <summary>
        /// 验证手机是否重复.
        /// </summary>
        /// <param name="mobile">
        /// 手机号码.
        /// </param>
        /// <returns>
        /// 0：没有重复，1：有重复.
        /// </returns>
        int IsMobileExists(string mobile);

        /// <summary>
        /// 验证登录名是否重复.
        /// </summary>
        /// <param name="loginName">
        /// 登录名.
        /// </param>
        /// <returns>
        /// 0：没有重复，1：有重复.
        /// </returns>
        int IsLoginNameExists(string loginName);

        /// <summary>
        /// 查询会员统计结果.
        /// </summary>
        /// <returns>
        /// User_Statistics对象的实例.
        /// </returns>
        User_Statistics SelectUser_StatisticsAll();

        /// <summary>
        /// 查询会员的邮箱地址（发送邮件）.
        /// </summary>
        /// <param name="paging">
        /// 搜索数据对象.
        /// </param>
        /// <returns>
        /// 会员邮箱地址列表.
        /// </returns>
        List<string> SelectEmail(Paging paging);

        /// <summary>
        /// 查询会员的手机号码（发送短信）.
        /// </summary>
        /// <param name="paging">
        /// 搜索数据对象.
        /// </param>
        /// <returns>
        /// 手机号码列表.
        /// </returns>
        List<string> SelectMobile(Paging paging);

        /// <summary>
        /// 成功登录后更新会员最后的登录时间.
        /// </summary>
        /// <param name="id">
        /// 会员编号.
        /// </param>
        void UpdateLastLoginTime(int id);

        /// <summary>
        /// 用户修改个人中心信息
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int UpdateUserMessage(User user);
        
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="pwd">
        /// the pwd
        /// </param>
        /// <param name="Id">
        /// the id
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int UpdateUserPassword(string pwd, int Id);

        /// <summary>
        /// 验证手机保存手机号.
        /// </summary>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        void UpdateMobile(string mobile, int id);

        /// <summary>
        /// 验证该手机号是否验证过.
        /// </summary>
        /// <param name="mobile">
        /// 手机号.
        /// </param>
        /// <param name="userID">
        /// 会员编号.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SelectByMobileVerify(string mobile,int userID);

        #endregion
    }
}