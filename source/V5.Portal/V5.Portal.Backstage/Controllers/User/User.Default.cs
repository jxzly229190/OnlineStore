// <copyright file="User.Default.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.User
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Text;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Microsoft.Ajax.Utilities;

    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.User;
    using V5.Service.User;

    /// <summary>
    /// 会员控制器部分类
    /// </summary>
    public partial class UserController
    {
        #region Constants and Fields

        /// <summary>
        /// 会员管理服务.
        /// </summary>
        private UserService userService;

        /// <summary>
        /// 会员收货地址服务.
        /// </summary>
        private UserReceiveAddressService userReceiveAddressService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 会员管理首页执行方法
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult Default()
        {
            return this.PartialView("Default");
        }

        /// <summary>
        /// 会员列表数据源.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelID">
        /// 会员等级编号.
        /// </param>
        /// <param name="userName">
        /// 会员名称.
        /// </param>
        /// <param name="email">
        /// 电子邮箱.
        /// </param>
        /// <param name="status">
        /// 会员状态.
        /// </param>
        /// <param name="mobile">
        /// 手机号码.
        /// </param>
        /// <param name="startTime">
        /// 搜索会员注册时间开始时间.
        /// </param>
        /// <param name="endTime">
        /// 搜索会员注册时间结束时间.
        /// </param>
        /// <param name="orderStartTime">
        /// 上次交易时间开始时间.
        /// </param>
        /// <param name="orderEndTime">
        /// 上次交易时间结束时间.
        /// </param>
        /// <param name="isHasOrder">
        /// 会员是否有成功订单.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult Query(
            [DataSourceRequest] DataSourceRequest request,
            string userLevelID,
            string userName,
            string email,
            string status,
            string mobile,
            string startTime,
            string endTime,
            string orderStartTime,
            string orderEndTime,
            string isHasOrder)
        {
            this.userService = new UserService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var stringBuilder = new StringBuilder();
            var condition = string.Empty;
            var objectName = string.Empty;

            if (!string.IsNullOrEmpty(userLevelID))
            {
                stringBuilder.Append(" [UserLevelID] = " + int.Parse(userLevelID));
            }

            if (!string.IsNullOrEmpty(userName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[LoginName] like '%" + userName + "%'");
            }

            switch (status)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[Status] = " + status);
                    break;
                case "2":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[Status] = 0");
                    break;
            }

            if (!string.IsNullOrEmpty(email))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[email] like '%" + email + "%'");
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[mobile] like '%" + mobile + "%'");
            }

            isHasOrder = string.IsNullOrEmpty(isHasOrder) ? "0" : isHasOrder;
            switch (isHasOrder)
            {
                case "0":
                    if (!string.IsNullOrEmpty(startTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_User_SelectAllInfo].[CreateTime] >= '" + startTime + "'");
                    }

                    if (!string.IsNullOrEmpty(endTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_User_SelectAllInfo].[CreateTime] <= '" + endTime + "'");
                    }

                    condition = stringBuilder.ToString();
                    objectName = "[view_User_SelectAllInfo]";
                    break;
                case "1":
                    if (!string.IsNullOrEmpty(startTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserHasOrder].[CreateTime] >= '" + startTime + "'");
                    }

                    if (!string.IsNullOrEmpty(endTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserHasOrder].[CreateTime] <= '" + endTime + "'");
                    }

                    if (!string.IsNullOrEmpty(orderStartTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserHasOrder].[OrderTime] >= '" + orderStartTime + "'");
                    }

                    if (!string.IsNullOrEmpty(orderEndTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserHasOrder].[OrderTime] <= '" + orderEndTime + "'");
                    }

                    condition = stringBuilder.ToString();
                    objectName = "[view_UserHasOrder]";
                    break;
                case "2":
                    if (!string.IsNullOrEmpty(startTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserNoHasOrder].[CreateTime] >= '" + startTime + "'");
                    }

                    if (!string.IsNullOrEmpty(endTime))
                    {
                        CheckCondition(stringBuilder);
                        stringBuilder.Append("[view_UserNoHasOrder].[CreateTime] <= '" + endTime + "'");
                    }

                    condition = stringBuilder.ToString();
                    objectName = "[view_UserNoHasOrder]";
                    break;
            }

            try
            {
                var paging = new Paging(objectName, null, null, condition, request.Page, request.PageSize);
                int pageCount;
                int totalCount;
                var list = this.userService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(null);
                }

                this.TempData["paging"] = paging;
                this.TempData["totalCount"] = totalCount;

                var modelList = new List<UserModel>();
                foreach (var user in list)
                {
                    modelList.Add(DataTransfer.Transfer<UserModel>(user, typeof(User)));
                }

                foreach (var model in modelList)
                {
                    model.StateName = model.Status == 0 ? "锁定帐号" : "解除锁定";
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加新的会员.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userModel">
        /// User的Model对象.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddUser([DataSourceRequest] DataSourceRequest request, UserModel userModel)
        {
            try
            {
                if (userModel != null)
                {
                    this.userService = new UserService();
                    var user = DataTransfer.Transfer<User>(userModel, typeof(UserModel));
                    userModel.ID = this.userService.AddUser(user);
                    if (userModel.ID <= 0)
                    {
                        return this.Json(string.Empty);
                    }

                    userModel.UserLevelName = "普通会员";
                    userModel.StateName = "锁定会员";
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功添加会员" + userModel.Email, "AddUser", Category.Info, Session.SessionID);
                    return this.Json(new[] { userModel }.ToDataSourceResult(request, this.ModelState));
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 会员的详细信息.
        /// </summary>
        /// <param name="id">
        /// 会员的编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            try
            {
                this.userService = new UserService();
                this.userReceiveAddressService = new UserReceiveAddressService();
                var userInfo = this.userService.QueryUserByID(id);
                if (userInfo == null)
                {
                    return null;
                }

                var user = DataTransfer.Transfer<UserModel>(userInfo, typeof(User));

	            if (user.Account == null)
	            {
		            user.Account=new UserAccountModel();
	            }

	            user.Account.Balance = userInfo.Balance;

                var userRecieveAddress = this.userReceiveAddressService.QueryDefaultReceiveAddressByUserID(user.ID);
                if (userRecieveAddress != null)
                {
                    user.DefaultAddress = userRecieveAddress.Address;
                    var postCode = this.userReceiveAddressService.QueryPostCodeByID(userRecieveAddress.ID);
                    user.PostCode = postCode ?? string.Empty;
                }

                user.Head = user.Head == null ? @"../../../Images/member-phone.gif" : string.Empty;
                user.StateName = user.Status == 1 ? "正常" : "锁定";
                return this.View("Detail", user);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 锁定会员帐号.
        /// </summary>
        /// <param name="id">
        /// 会员的ID编号.
        /// </param>
        public void UserLock(int id)
        {
            try
            {
                this.userService = new UserService();
                this.userService.ModifyUserStatus(id, 1);
                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功锁定会员" + id, "UserLock", Category.Info, Session.SessionID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 解锁会员帐号.
        /// </summary>
        /// <param name="id">
        /// 会员的编号.
        /// </param>
        public void UserUnlock(int id)
        {
            try
            {
                this.userService = new UserService();
                this.userService.ModifyUserStatus(id, 0);
                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功解锁会员" + id, "UserUnlock", Category.Info, Session.SessionID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 密码重置.
        /// </summary>
        /// <param name="id">
        /// 会员的编号.
        /// </param>
        public void ResetPassword(int id)
        {
            try
            {
                this.userService = new UserService();
                this.userService.ResetPassword(id);
                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功重置会员编号：" + id + "的密码", "AddUser", Category.Info, Session.SessionID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 导出结果.
        /// </summary>
        public void ExportUser()
        {
            this.userService = new UserService();

            var total = int.Parse(TempData["totalCount"].ToString());
            var paging = this.TempData["paging"] as Paging;
            this.TempData["paging"] = paging;
            this.TempData["totalCount"] = total;
            if (paging == null)
            {
                return;
            }

            paging.PageSize = total;

            List<User> list;
            try
            {
                int pageCount;
                int totalCount;
                list = this.userService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功导出会员列表", "ExportUser", Category.Info, Session.SessionID);
            var sb = new StringBuilder();
            sb.Append("编号\t");
            sb.Append("会员登录名\t");
            sb.Append("会员姓名\t");
            sb.Append("会员昵称\t");
            sb.Append("会员级别\t");
            sb.Append("电子邮箱\t");
            sb.Append("手机号码\t");
            sb.Append("注册时间\t\n");
            foreach (var userinfo in list)
            {
                sb.Append(userinfo.ID + "\t");
                sb.Append(userinfo.LoginName + "\t");
                sb.Append(userinfo.Name + "\t");
                sb.Append(userinfo.NickName + "\t");
                sb.Append(userinfo.UserLevelName + "\t");
                sb.Append(userinfo.Email + "\t");
                sb.Append(userinfo.Mobile + "\t");
                sb.Append(userinfo.CreateTime.ToString(CultureInfo.InvariantCulture) + "\t\n");
            }

            this.HttpContext.Response.ContentType = "application/x-excel";
            string fileName;
            var userAgent = this.Request.UserAgent;
            if (userAgent != null && userAgent.ToLower().IndexOf("msie", StringComparison.Ordinal) > -1)
            {
                fileName = HttpUtility.UrlEncode("会员列表") + "[" + DateTime.Now.ToString(CultureInfo.InvariantCulture)
                           + "].xls";
            }
            else
            {
                fileName = "会员列表" + "[" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "].xls";
            }

            this.HttpContext.Response.AddHeader("Content-Disposition", "attachment; fileName=\"" + fileName + "\"");
            this.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.Write(sb.ToString());
        }

        /// <summary>
        /// 验证邮箱是否重复.
        /// </summary>
        /// <param name="email">
        /// 邮箱地址.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult IsEmailExists(string email)
        {
            int isEmailExists;
            try
            {
                this.userService = new UserService();
                isEmailExists = this.userService.IsEmailExists(email);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return !string.IsNullOrEmpty(email) ? this.Json(isEmailExists, JsonRequestBehavior.AllowGet) : null;
        }

        /// <summary>
        /// 验证手机号码是否重复.
        /// </summary>
        /// <param name="mobile">
        /// 手机号码.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult IsMobileExists(string mobile)
        {
            int isMobileExists;
            try
            {
                this.userService = new UserService();
                isMobileExists = this.userService.IsMobileExists(mobile);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return !string.IsNullOrEmpty(mobile) ? this.Json(isMobileExists, JsonRequestBehavior.AllowGet) : null;
        }

        /// <summary>
        /// 验证登录名是否重复.
        /// </summary>
        /// <param name="loginName">
        /// 登录名.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult IsLoginNameExists(string loginName)
        {
            int isLoginNameExists;
            try
            {
                isLoginNameExists = this.userService.IsLoginNameExists(loginName);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return !string.IsNullOrEmpty(loginName) ? this.Json(isLoginNameExists, JsonRequestBehavior.AllowGet) : null;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// The check condition.
        /// </summary>
        /// <param name="stringBuilder">
        /// The string builder.
        /// </param>
        private static void CheckCondition(StringBuilder stringBuilder)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" And ");
            }
        }

        #endregion
    }
}
