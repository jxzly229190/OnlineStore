// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserMessageSendRecordDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发送信息记录数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using V5.DataContract.User;

    /// <summary>
    ///     发送信息记录数据访问接口.
    /// </summary>
    public interface IUserMessageSendRecordDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加发送信息记录.
        /// </summary>
        /// <param name="userMessageSendRecord">
        /// User_Message_SendRecord的对象实例.
        /// </param>
        /// <returns>
        /// 发送信息记录的编号.
        /// </returns>
        int Insert(User_Message_SendRecord userMessageSendRecord);

        #endregion
    }
}
