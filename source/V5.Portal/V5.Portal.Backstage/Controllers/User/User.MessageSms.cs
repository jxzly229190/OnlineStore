// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.MessageSms.cs" company="www.gjw.com">
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
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Sms;

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
        /// 短信信息服务对象.
        /// </summary>
        private UserMessageSmsService userMessageSmsService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The message sms.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult MessageSms()
        {
            return this.PartialView("MessageSms");
        }

        /// <summary>
        /// 添加短信信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userMessageSmsModel">
        /// UserMessageSmsModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult AddMessageSms([DataSourceRequest] DataSourceRequest request, UserMessageSmsModel userMessageSmsModel)
        {
            try
            {
                if (userMessageSmsModel != null)
                {
                    userMessageSmsModel.EmployeeID = this.SystemUserSession.EmployeeID;
                    this.userMessageSmsService = new UserMessageSmsService();

                    var userMessageSms = DataTransfer.Transfer<User_Message_Sms>(userMessageSmsModel, typeof(UserMessageSmsModel));

                    userMessageSmsModel.ID = this.userMessageSmsService.Add(userMessageSms);
                    userMessageSmsModel.StatusName = userMessageSmsModel.Status == 0 ? "正常" : "停止";
                }

                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功添加短信信息", "AddMessageSms", Category.Info, Session.SessionID);
                return this.Json(new[] { userMessageSmsModel }.ToDataSourceResult(request, this.ModelState));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询电子短信列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryMessageSms([DataSourceRequest] DataSourceRequest request)
        {
            this.userMessageSmsService = new UserMessageSmsService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int totalCount;
            var condition = "[IsDelete]=0";

            var paging = new Paging("[User_Message_Sms]", null, "ID", condition, request.Page, request.PageSize);

            List<User_Message_Sms> list;
            try
            {
                int pageCount;
                list = this.userMessageSmsService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<UserMessageSmsModel>();
                foreach (var messageSms in list)
                {
                    var model = DataTransfer.Transfer<UserMessageSmsModel>(messageSms, typeof(User_Message_Sms));
                    model.StatusName = model.Status == 0 ? "正常" : "停止";
                    modelList.Add(model);
                }

                var result = new DataSourceResult { Data = modelList, Total = totalCount };
                return this.Json(result);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 更改短信信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userMessageSmsModel">
        /// UserMessageSmsModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyMessageSms([DataSourceRequest] DataSourceRequest request, UserMessageSmsModel userMessageSmsModel)
        {
            try
            {
                if (userMessageSmsModel != null)
                {
                    this.userMessageSmsService = new UserMessageSmsService();

                    var userMessageSms = DataTransfer.Transfer<User_Message_Sms>(
                        userMessageSmsModel,
                        typeof(UserMessageSmsModel));

                    this.userMessageSmsService.Modify(userMessageSms);
                    userMessageSmsModel.StatusName = userMessageSmsModel.Status == 0 ? "正常" : "停止";
                }

                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功修改短信信息", "ModifyMessageSms", Category.Info, Session.SessionID);
                return this.Json(new[] { userMessageSmsModel }.ToDataSourceResult(request, this.ModelState));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 删除指定短信信息.
        /// </summary>
        /// <param name="id">
        /// 信息编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RemoveSms(int id)
        {
            try
            {
                this.userMessageSmsService = new UserMessageSmsService();
                this.userMessageSmsService.RemoveByID(id);
                return this.Json(new AjaxResponse(1, "删除成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 发送短信.
        /// </summary>
        /// <param name="smsID">
        /// 短信编号.
        /// </param>
        public void SendSms(int smsID)
        {
            this.userMessageSmsService = new UserMessageSmsService();
            var userMessageSms = this.userMessageSmsService.QueryByID(smsID);
            this.userService = new UserService();
            var paging = this.TempData["paging"] as Paging;
            this.TempData["paging"] = paging;
            if (paging == null)
            {
                return;
            }

            var list = this.userService.QueryMobile(paging);
            if (list != null)
            {
                try
                {
                    var sm = new ShortMessage { ReceiveMobiles = list, Content = userMessageSms.Content };
                    sm.Send();
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功发送短信", "SendSms", Category.Info, Session.SessionID);
                    Response.Write("发送成功！");
                }
                catch (Exception exception)
                {
                    Response.Write("发送失败！");
                    throw new Exception(exception.Message);
                }

                try
                {
                    this.userMessageSendRecordService = new UserMessageSendRecordService();
                    var userMessageSendRecord = new User_Message_SendRecord
                                                    {
                                                        EmployeeID =
                                                            this.SystemUserSession.EmployeeID,
                                                        MessageID = userMessageSms.ID,
                                                        MessageTypeID = 2,
                                                        UserCount = list.Count,
                                                        CreateTime = DateTime.Now
                                                    };
                    userMessageSendRecord.ID = this.userMessageSendRecordService.Add(userMessageSendRecord);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
        }

        /// <summary>
        /// 查询所有短信列表.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QuerySelectSmsListItems()
        {
            List<User_Message_Sms> list;
            try
            {
                this.userMessageSmsService = new UserMessageSmsService();
                list = this.userMessageSmsService.QueryAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
           
            if (list != null)
            {
                var items = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "请选择" } };
                foreach (var sms in list)
                {
                    var selectListItem = new SelectListItem
                    {
                        Value = sms.ID.ToString(CultureInfo.InvariantCulture),
                        Text = sms.Name,
                    };
                    items.Add(selectListItem);
                }

                return this.Json(items, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
