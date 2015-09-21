// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMessageEmailModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   邮件信息模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 邮件信息模型类.
    /// </summary>
    public class UserMessageEmailModel
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
        ///     获取或设置邮件名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置邮件主题．
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     获取或设置邮件内容．
        /// </summary>
        [DataType(DataType.MultilineText)]  
        public string Content { get; set; }

        /// <summary>
        ///     获取或设置邮件模版的状态．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置邮件模版的状态.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置邮件模版的创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}