// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.MessageEmail.cs" company="www.gjw.com">
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
    using global::System.Configuration;
    using global::System.Globalization;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

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
        /// 邮件信息服务对象.
        /// </summary>
        private UserMessageEmailService userMessageEmailService;

        /// <summary>
        /// 邮件信息记录服务对象.
        /// </summary>
        private UserMessageSendRecordService userMessageSendRecordService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 邮件信息执行方法
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult MessageEmail()
        {
            return this.PartialView("MessageEmail");
        }

        /// <summary>
        /// 添加邮件信息.
        /// </summary>
        /// <param name="userMessageEmailModel">
        /// The user message email model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult AddMessageEmail(UserMessageEmailModel userMessageEmailModel)
        {
            AjaxResponse ajaxResponse;
            try
            {
                if (userMessageEmailModel != null)
                {
                    userMessageEmailModel.EmployeeID = this.SystemUserSession.EmployeeID;
                    userMessageEmailModel.CreateTime = DateTime.Now;
                    this.userMessageEmailService = new UserMessageEmailService();

                    var userMessageEmail = DataTransfer.Transfer<User_Message_Email>(userMessageEmailModel, typeof(UserMessageEmailModel));

                    userMessageEmailModel.ID = this.userMessageEmailService.Add(userMessageEmail);
                    ajaxResponse = new AjaxResponse(1, "添加成功！", userMessageEmailModel);
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功添加邮件信息", "AddMessageEmail", Category.Info, Session.SessionID);
                    return this.Json(ajaxResponse);
                }

                ajaxResponse = new AjaxResponse(-1, "添加失败！");
                return this.Json(ajaxResponse);
            }
            catch (Exception exception)
            {
                ajaxResponse = new AjaxResponse(-1, exception.Message);
                LogUtils.Log(
                    "用户" + this.SystemUserSession.LoginName + "添加邮件信息错误:" + exception.Message,
                    "AddMessageEmail",
                    Category.Error,
                    Session.SessionID);
                return this.Json(ajaxResponse);
            }
        }

        /// <summary>
        /// 查询电子邮件列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryMessageEmail([DataSourceRequest] DataSourceRequest request)
        {
            this.userMessageEmailService = new UserMessageEmailService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var condition = "[IsDelete]=0";
            var paging = new Paging(
                "[User_Message_Email]",
                null,
                "ID",
                condition,
                request.Page,
                request.PageSize,
                "CreateTime",
                1);

            try
            {
                int pageCount;
                int totalCount;
                var list = this.userMessageEmailService.Query(paging, out pageCount, out totalCount);
                if (list != null)
                {
                    var modelList = new List<UserMessageEmailModel>();
                    foreach (var messageEmail in list)
                    {
                        var model = DataTransfer.Transfer<UserMessageEmailModel>(
                            messageEmail,
                            typeof(User_Message_Email));
                        model.StatusName = model.Status == 0 ? "正常" : "停止";
                        modelList.Add(model);
                    }

                    var result = new DataSourceResult { Data = modelList, Total = totalCount };
                    return this.Json(result);
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 更改邮件信息.
        /// </summary>
        /// <param name="userMessageEmailModel">
        /// The user message email model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ModifyMessageEmail(UserMessageEmailModel userMessageEmailModel)
        {
            AjaxResponse ajaxResponse;
            try
            {
                if (userMessageEmailModel != null)
                {
                    this.userMessageEmailService = new UserMessageEmailService();

                    var userMessageEmail = DataTransfer.Transfer<User_Message_Email>(
                        userMessageEmailModel,
                        typeof(UserMessageEmailModel));

                    this.userMessageEmailService.Modify(userMessageEmail);
                    userMessageEmailModel.StatusName = userMessageEmailModel.Status == 0 ? "正常" : "停止";
                    ajaxResponse = new AjaxResponse(1, "修改成功！", userMessageEmailModel);
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功修改邮件信息", "ModifyMessageEmail", Category.Info, Session.SessionID);
                    return this.Json(ajaxResponse);
                }

                ajaxResponse = new AjaxResponse(-1, "添加失败！");
                return this.Json(ajaxResponse);
            }
            catch (Exception exception)
            {
                ajaxResponse = new AjaxResponse(-1, exception.Message);
                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "修改邮件信息：" + exception.Message, "ModifyMessageEmail", Category.Error, Session.SessionID);
                return this.Json(ajaxResponse);
            }
        }

        /// <summary>
        /// 发送电子邮件.
        /// </summary>
        /// <param name="emailID">
        /// 邮件编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult SendEmail(int emailID)
        {
            this.userMessageEmailService = new UserMessageEmailService();
            var userMessageEmail = this.userMessageEmailService.QueryByID(emailID);
            this.userService = new UserService();
            var paging = this.TempData["paging"] as Paging;
            this.TempData["paging"] = paging;
            if (paging == null)
            {
                return this.Json(new AjaxResponse(0, "请选择会员！"));
            }

            List<string> list;
            try
            {
                list = this.userService.QueryEmail(paging);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list == null)
            {
                return this.Json(new AjaxResponse(0, "请选择会员！"));
            }

            try
            {
                var sendEmailFrom = ConfigurationManager.AppSettings["SendEmailFrom"];
                var sendEmailServer = ConfigurationManager.AppSettings["SendEmailServer"];
                var sendEmailPassword = ConfigurationManager.AppSettings["SendEmailPassword"];
                var email = new Email(
                    sendEmailServer,
                    sendEmailFrom,
                    sendEmailPassword,
                    userMessageEmail.Title,
                    userMessageEmail.Content,
                    true) { ToList = list };

                var emailservice = new EmailService(email);
                emailservice.SendBySmtp();
                LogUtils.Log(
                    "用户" + this.SystemUserSession.LoginName + "成功发送邮件信息",
                    "SendEmail",
                    Category.Info,
                    this.Session.SessionID);
                this.userMessageSendRecordService = new UserMessageSendRecordService();
                var userMessageSendRecord = new User_Message_SendRecord
                                                {
                                                    EmployeeID =
                                                        this.SystemUserSession.EmployeeID,
                                                    MessageID = userMessageEmail.ID,
                                                    MessageTypeID = 1,
                                                    UserCount = list.Count,
                                                    CreateTime = DateTime.Now
                                                };
                userMessageSendRecord.ID = this.userMessageSendRecordService.Add(userMessageSendRecord);

                return this.Json(new AjaxResponse(1, "发送成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 删除指定邮件信息.
        /// </summary>
        /// <param name="id">
        /// 邮件编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RemoveEmail(int id)
        {
            try
            {
                this.userMessageEmailService = new UserMessageEmailService();
                this.userMessageEmailService.RemoveByID(id);
                return this.Json(new AjaxResponse(1, "删除成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 查询所有邮件列表.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QuerySelectEmailListItems()
        {
            List<User_Message_Email> list;
            try
            {
                this.userMessageEmailService = new UserMessageEmailService();
                list = this.userMessageEmailService.QueryAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var items = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "请选择" } };
                foreach (var email in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = email.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = email.Name,
                                             };
                    items.Add(selectListItem);
                }

                return this.Json(items, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query select status list item.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryStatus()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem { Text = "正常", Value = "0" },
                    new SelectListItem { Text = "作废", Value = "1" }
                };

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
