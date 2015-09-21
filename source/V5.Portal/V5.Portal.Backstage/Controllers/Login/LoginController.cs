// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台系统登录控制器类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Login
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web;
    using global::System.Web.Mvc;

    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Security;
    using V5.Library.Logger;
    using V5.Library.Storage.DB.NoSql;
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;

    /// <summary>
    /// 后台系统登录控制器类
    /// </summary>
    public class LoginController : Controller
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户服务类
        /// </summary>
        private SystemUserService systemUserService;

        /// <summary>
        /// The system menus.
        /// </summary>
        private List<System_Menu> systemMenus;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 获取登录安全码
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [OutputCache(Duration = 0)]
        public ActionResult GetSecurityCode()
        {
            string securityCode;
            var security = V5.Library.Security.SecurityCode.CreateSecurityCode(out securityCode);
            this.TempData[this.Session.SessionID] = securityCode;
            return this.File(security.ToArray(), @"image/jpeg");
        }

        /// <summary>
        /// 系统用户登陆执行方法
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 系统用户登陆执行方法
        /// </summary>
        /// <param name="loginName">
        /// 登录名
        /// </param>
        /// <param name="loginPassword">
        /// 登录密码
        /// </param>
        /// <param name="securityCode">
        /// 验证码
        /// </param>
        /// <param name="remember">
        /// The remember.
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult Index(string loginName, string loginPassword, string securityCode, string remember)
        {
            this.systemMenus = new SystemMenuService().QueryAll();
            try
            {
                var user = this.GetUserByLogin(loginName);
                if (user == null)
                {
                    return this.Content("2");
                }

                if (user.LoginPassword != Encrypt.HashBySHA1(loginName + loginPassword))
                {
                    if (user.LoginPassword == Encrypt.HashBySHA1(loginPassword))
                    {
                        var recevieid = new SystemUserService().UpdatePassWord(user.ID, Encrypt.HashBySHA1(loginName + loginPassword));
                        if (recevieid > 0)
                        {
                            Response.Write("<script type='text/javascript'>alert('用户密码升级完毕，请重新登录');</script>");
                            return this.Content("1");
                        }
                    }
                    else
                    {
                        return this.Content("2");
                    }
                }
                
                if (this.TempData[this.Session.SessionID] == null || this.TempData[this.Session.SessionID].ToString() != securityCode)
                {
                    return this.Content("1");
                }
                
                this.TempData[this.Session.SessionID] = null; // 重置验证码为空

                if (!string.IsNullOrEmpty(remember) && remember == "checked")
                {
                    var httpCookie = new HttpCookie("LoginName")
                                         {
                                             Value = loginName,
                                             Expires = DateTime.Now.AddDays(30)
                                         };

                    HttpContext.Response.Cookies.Add(httpCookie);
                }
                else
                {
                    if (HttpContext.Response.Cookies["LoginName"] != null)
                    {
                        HttpContext.Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(-30);
                    }
                }

                this.SetupSession(user);

                return this.Content("success");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 根据输入的登录信息获取用户对象
        /// </summary>
        /// <param name="loginName">
        /// 登录名
        /// </param>
        /// <returns>
        /// 系统用户对象
        /// </returns>
        private System_User GetUserByLogin(string loginName)
        {
            //日志
            LogUtils.Log("用户“" + loginName + "”登录", "GetUserByLogin", Category.Info, Session.SessionID);

            this.systemUserService = new SystemUserService();
            return this.systemUserService.QueryByLoginName(loginName);
        }

        /// <summary>
        /// The setup session.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        private void SetupSession(System_User user)
        {
            var topMenus = new List<MenuModel>();
            var leftMenus = new List<MenuModel>();
            var userRights = new SystemRightsService().QueryUserRight(user.ID);
            var systemMenuService = new SystemMenuService();
            var userTopMenus = systemMenuService.GetUserTopMenus(userRights);
            foreach (var systemMenu in userTopMenus)
            {
                topMenus.Add(DataTransfer.Transfer<MenuModel>(systemMenu, typeof(System_Menu)));
            }

            var userLeftMenus = systemMenuService.GetUserLeftMenus(userRights);
            foreach (var systemMenu in userLeftMenus)
            {
                leftMenus.Add(DataTransfer.Transfer<MenuModel>(systemMenu, typeof(System_Menu)));
            }

            var systemUserSession = new SystemUserSession
            {
                SessionID = this.Session.SessionID,
                SystemUserID = user.ID,
                // EmployeeID = user.ID, //暂时将EmployeeID设置为SystemUserId，未来将修改数据表，将EmployeeId改为SystemUserID
                Name = user.Name,
                LoginName = user.LoginName,
                RoleID = user.RoleID,
                TopMenus = topMenus,
                LeftMenus = leftMenus,
                Permissions = userRights,
                LastVisitTime = DateTime.Now
            };

            MongoDBHelper.RefreshSystemUserSession(systemUserSession);
        }

        #endregion
    }
}
