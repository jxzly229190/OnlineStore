// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.User.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Security;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;
    using V5.Library.Logger;
    using V5.Library.Storage.DB.NoSql;

    /// <summary>
    /// 系统用户控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户服务类
        /// </summary>
        private SystemUserService systemUserService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 系统用户首页执行方法
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpGet]
        public new PartialViewResult User()
        {
            return this.PartialView("User");
        }

        /// <summary>
        /// 添加系统用户执行方法
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="user">
        /// 系统用户模型对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        public ActionResult AddUser([DataSourceRequest] DataSourceRequest request, UserModel user)
        {
            try
            {
                if (user != null)
                {
                    this.systemUserService = new SystemUserService();

                    user.Status = 1;
                    user.CreateTime = DateTime.Now;

                    var sysUser = DataTransfer.Transfer<System_User>(user, typeof(UserModel));
                    sysUser.LoginPassword = Encrypt.HashBySHA1(sysUser.LoginName + sysUser.LoginPassword);
                    sysUser.ID = this.systemUserService.AddUser(sysUser);

                    if (sysUser.ID > 0)
                    {
                        return this.Json(new[] { sysUser }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.View();
        }

        /// <summary>
        /// 移除系统用户执行方法
        /// </summary>
        /// <param name="id">
        /// 系统用户编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult RemoveUser(int id)
        {
            try
            {
                this.systemUserService = new SystemUserService();

                this.systemUserService.RemoveUserByID(id);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// 修改系统用户执行方法
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="user">
        /// 用户模型对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult ModifyUser([DataSourceRequest] DataSourceRequest request, UserModel user)
        {
            if (user != null && this.ModelState.IsValid)
            {
                this.systemUserService = new SystemUserService();

                var systemUser = DataTransfer.Transfer<System_User>(
                    user,
                    typeof(UserModel));
                
                this.systemUserService.ModifyUser(systemUser);

                //判断用户密码是否被修改过
                var userModel = this.systemUserService.QueryByLoginName(systemUser.LoginName);
                if (userModel != null && userModel.LoginPassword != systemUser.LoginPassword)
                {
                    var loginPassword = Encrypt.HashBySHA1(systemUser.LoginName + systemUser.LoginPassword);
                    LogUtils.Log("用户“" + systemUser.LoginName + "”修改密码", "ModifyUser", Category.Info, Session.SessionID);
                    this.systemUserService.UpdatePassWord(systemUser.ID, loginPassword);
                }
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        /// <summary>
        /// 查询系统用户列表执行方法
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult QueryUser([DataSourceRequest] DataSourceRequest request)
        {
            this.systemUserService = new SystemUserService();

            int pageCount;
            int totalCount;

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var paging = new Paging("[System_User]", null, "ID", null, request.Page, request.PageSize);

            var list = this.systemUserService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var modelList = new List<UserModel>();
            foreach (var systemRole in list)
            {
                modelList.Add(DataTransfer.Transfer<UserModel>(systemRole, typeof(System_User)));
            }

            var result = new DataSourceResult { Data = modelList, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The is user exists.
        /// </summary>
        /// <param name="loginName">
        /// The login Name.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult IsLoginNameExists(string loginName)
        {
            if (!string.IsNullOrEmpty(loginName))
            {
                return this.Json(this.systemUserService.IsLoginNameExists(loginName), JsonRequestBehavior.AllowGet);
            }

            return null;
        }
        #endregion
    }
}
