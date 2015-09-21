// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserMessageEmailDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   邮件模版数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 邮件数据访问接口.
    /// </summary>
    public interface IUserMessageEmailDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加邮件数据.
        /// </summary>
        /// <param name="userMessageEmail">
        /// User_Message_Email 的对象实例.
        /// </param>
        /// <returns>
        /// 邮件编号.
        /// </returns>
        int Insert(User_Message_Email userMessageEmail);

        /// <summary>
        /// 查询邮件模版列表
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
        /// 邮件模版列表
        /// </returns>
        List<User_Message_Email> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 根据指定编号查询.
        /// </summary>
        /// <param name="id">
        /// 邮件模版编号.
        /// </param>
        /// <returns>
        /// User_Message_Email的对象实例.
        /// </returns>
        User_Message_Email SelectByID(int id);

        /// <summary>
        /// 更新邮件模版.
        /// </summary>
        /// <param name="userMessageEmail">
        /// User_Message_Email对象实例.
        /// </param>
        void Update(User_Message_Email userMessageEmail);

        /// <summary>
        /// 查询所有邮件信息.
        /// </summary>
        /// <returns>
        /// 邮件模版列表.
        /// </returns>
        List<User_Message_Email> SelectAll();

        /// <summary>
        /// 删除指定的邮件信息.
        /// </summary>
        /// <param name="id">
        /// 邮件信息编号.
        /// </param>
        void DeleteByID(int id);

        #endregion
    }
}
