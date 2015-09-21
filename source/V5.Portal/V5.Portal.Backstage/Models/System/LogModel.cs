// --------------------------------------------------------------------------------------------------------------------
// <copyright file="logModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统日志模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 系统用户模型类
    /// </summary>
    public class LogModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置会话编号．
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        ///     获取或设置用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     获取或设置日志消息．
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     获取或设置动作．
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///     获取或设置日志等级．
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     获取或设置日志位置．
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///    获取或设置用户名称．
        /// </summary>
        public string UserName { get; set; }

        #endregion
    }
}