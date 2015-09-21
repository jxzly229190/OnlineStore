// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemUserSession.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户会话类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using V5.Portal.Backstage.Models.System;

    /// <summary>
    /// 系统用户会话类
    /// </summary>
    public class SystemUserSession
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUserSession"/> class.
        /// </summary>
        public SystemUserSession()
        {
            this.SessionID = HttpContext.Current.Session.SessionID;

            this.SystemUserID = 0;
            this.LoginName = string.Empty;
            this.Name = string.Empty;
            this.RoleID = 0;
            this.TopMenus = new List<MenuModel>();
            this.LeftMenus = new List<MenuModel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置会话编号
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// 获取或设置系统用户编号
        /// </summary>
        public int SystemUserID { get; set; }

        /// <summary>
        /// 获取操作员工编号（注意：此员工编号与系统用户编码一致）
        /// </summary>
        public int EmployeeID {
            get
            {
                return this.SystemUserID; //暂时将EmployeeID设置为SystemUserId，未来将修改数据表，将EmployeeId改为SystemUserID
            }
        }

        /// <summary>
        /// 获取或设置登录名称
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置系统用户的角色编号列表
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 获取或设置顶部菜单栏
        /// </summary>
        public List<MenuModel> TopMenus { get; set; }

        /// <summary>
        /// 获取或设置左部菜单栏
        /// </summary>
        public List<MenuModel> LeftMenus { get; set; }

        /// <summary>
        /// 权限字符串
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// Gets or sets the last visit time.
        /// </summary>
        public DateTime LastVisitTime { get; set; }

        #endregion
    }
}