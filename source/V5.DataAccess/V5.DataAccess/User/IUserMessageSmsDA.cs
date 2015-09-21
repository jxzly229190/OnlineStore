// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserMessageSmsDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   短信信息数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 短信信息数据访问接口.
    /// </summary>
    public interface IUserMessageSmsDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加短信信息.
        /// </summary>
        /// <param name="userMessageSms">
        /// User_Message_Sms的对象的实例.
        /// </param>
        /// <returns>
        /// 短信信息的编号.
        /// </returns>
        int Insert(User_Message_Sms userMessageSms);

        /// <summary>
        /// 查询短信信息列表
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
        /// 短信信息列表
        /// </returns>
        List<User_Message_Sms> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 更新短信信息.
        /// </summary>
        /// <param name="userMessageSms">
        /// userMessageSms对象实例.
        /// </param>
        void Update(User_Message_Sms userMessageSms);

        /// <summary>
        /// 查询所有短信信息.
        /// </summary>
        /// <returns>
        /// 手机短信列表.
        /// </returns>
        List<User_Message_Sms> SelectAll();

        /// <summary>
        /// 查询指定的短信信息.
        /// </summary>
        /// <param name="id">
        /// 短信信息编号.
        /// </param>
        /// <returns>
        /// User_Message_Sms的对象实例.
        /// </returns>
        User_Message_Sms SelectByID(int id);

        /// <summary>
        /// 删除指定的短信信息.
        /// </summary>
        /// <param name="id">
        /// 短信信息编号.
        /// </param>
        void DeleteByID(int id);

        #endregion
    }
}
