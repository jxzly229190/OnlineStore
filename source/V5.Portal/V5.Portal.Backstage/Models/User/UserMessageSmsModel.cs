// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMessageSmsModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   短信模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;

    /// <summary>
    /// 短信模型类.
    /// </summary>
    public class UserMessageSmsModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工编号.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置短信名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置短信内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     获取或设置启用状态.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置启用状态.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置创建时间.
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}