namespace V5.Portal.Models
{
    using System;
    using System.Collections.Generic;

	public class UserSession
    {
        /// <summary>
        /// 获取或设置Session编号.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 访问用户唯一标识（即 SessionId）
        /// </summary>
        public string VisitorKey { get; set; }

        /// <summary>
        /// 获取或设置会员编号.
        /// </summary>
        public int UserID { get; set; }

        ///// <summary>
        ///// 获取或设置登录成功后显示的名称（昵称-登录名-邮箱-手机）
        ///// </summary>
        //public string ShowName { get; set; }

        ///// <summary>
        ///// 获取或设置登录名称
        ///// </summary>
        //public string LoginName { get; set; }

        ///// <summary>
        ///// 获取或设置用户姓名
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// 获取或设置最后访问时间.
        ///// </summary>
        //public DateTime LastVisitTime { get; set; }

        ///// <summary>
        ///// 获取或设置会员登录失败次数.
        ///// </summary>
        //public int LoginErrCount { get; set; }

        ///// <summary>
        ///// 获取或设置会员登录时间.
        ///// </summary>
        //public DateTime LoginTime { get; set; }
    }
}