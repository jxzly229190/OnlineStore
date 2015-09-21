// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMessageSendRecordModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   信息发送记录模型.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;

    /// <summary>
    /// 信息发送记录模型.
    /// </summary>
    public class UserMessageSendRecordModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工编号.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置信息编号.
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        ///     获取或设置信息类型（1：邮件、2：短信）.
        /// </summary>
        public int MessageTypeID { get; set; }

        /// <summary>
        ///     获取或设置发送数量
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        ///     获取或设置发送时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}