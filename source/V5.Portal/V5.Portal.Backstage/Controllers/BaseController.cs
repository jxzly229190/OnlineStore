// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The base controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers
{
    using global::System;
    using global::System.Text;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using V5.Library;
    using V5.Library.Storage.DB.NoSql;
    using V5.Service.System;
    using V5.Library.Logger;

    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : Controller
    {
        #region Constants and Fields

        /// <summary>
        /// The system user session.
        /// </summary>
        private SystemUserSession systemUserSession;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the system user session.
        /// </summary>
        public SystemUserSession SystemUserSession
        {
            get
            {
                return this.systemUserSession ?? (this.systemUserSession = new SystemUserSession());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string resourceKey = string.Empty;
            string resourceDescription = string.Empty;
            var mongoDbStore = new MongoDbStore<SystemUserSession>("SystemUserSessions");
            systemUserSession = mongoDbStore.Single(item => item.SessionID == Session.SessionID);

            if (systemUserSession == null)
            {
                HandleSessionLost(filterContext);
            }
            else
            {
                // todo: 会话失效判断
                //this.HandleSessionState(filterContext, mongoDbStore);

                var systemRightService = new SystemRightsService();
                resourceKey = this.GetResourceKey(filterContext);
                resourceDescription = systemRightService.GetResourceDescriptionByKey(resourceKey);
                if (!systemRightService.ValidateRight(resourceKey, this.systemUserSession.Permissions))
                {
                    if (!this.ValidateAjaxRequest(filterContext))
                    {
                        filterContext.Result =
                            this.Content("<script type='text/javascript'>alert('对不起，您没有此操作权限！');</script>");
                    }
                    else
                    {
	                    Response.StatusCode = 610;
                        filterContext.Result = this.Json(new AjaxResponse(-403, "无操作权限"), JsonRequestBehavior.AllowGet);
                    }

                    LogUtils.Log(
                        "无操作权限" + resourceDescription,
                        "OnActionExecuting",
                        Category.Info,
                        systemUserSession.SessionID,
                        systemUserSession.SystemUserID,
                        "Enter");
                }
            }

            if (systemUserSession == null)
            {
                LogUtils.Log("未登录", "OnActionExecuting");
            }
            else
            {
                LogUtils.Log(
                    "用户“" + systemUserSession.Name + "”，正在操作：" + resourceDescription,
                    "OnActionExecuting",
                    Category.Info,
                    systemUserSession.SessionID,
                    systemUserSession.SystemUserID,
                    "Enter");
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// The on action executed.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (systemUserSession == null)
            {
                LogUtils.Log("未登录", "OnActionExecuted");
            }
            else
            {
                string resourceKey = GetResourceKey(filterContext);
                string resourceDescription = new SystemRightsService().GetResourceDescriptionByKey(resourceKey);
                LogUtils.Log(
                    "用户“" + systemUserSession.Name + "”，操作完毕：" + resourceDescription,
                    "OnActionExecuted",
                    Category.Info,
                    this.systemUserSession.SessionID,
                    this.systemUserSession.SystemUserID,
                    "Enter");
            }

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 处理会话状态
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <param name="mongoDbStore">
        /// The mongo db store.
        /// </param>
        private void HandleSessionState(
            ActionExecutingContext filterContext,
            MongoDbStore<SystemUserSession> mongoDbStore)
        {
            var timeSpan = DateTime.Now.Subtract(this.systemUserSession.LastVisitTime);

            if (timeSpan.TotalMinutes > 30)
            {
                mongoDbStore.Delete(item => item.SessionID == this.Session.SessionID);
                this.HandleSessionLost(filterContext);
            }

            this.systemUserSession.LastVisitTime = DateTime.Now;
            MongoDBHelper.RefreshSystemUserSession(this.systemUserSession);
        }

        /// <summary>
        /// 处理会话失效方法
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        private void HandleSessionLost(ActionExecutingContext filterContext)
        {
            if (!this.ValidateAjaxRequest(filterContext))
            {
                filterContext.Result = this.RedirectToRoute("Default", new { Controller = "Login", Action = "Index" });
            }
            else
            {
                filterContext.Result = this.Json(new AjaxResponse(-401, "会话失效"), JsonRequestBehavior.AllowGet);
                filterContext.HttpContext.Response.StatusCode = 600;
                //filterContext.HttpContext.Response.Write("会话失效,请重新登录");
            }
        }

        /// <summary>
        /// 判断是否为Ajax请求
        /// </summary>
        /// <param name="filterContext">参数</param>
        /// <returns>是否</returns>
        private bool ValidateAjaxRequest(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取资源Key
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetResourceKey(ActionExecutingContext filterContext)
        {
            var isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"];
            var method = this.ControllerContext.RequestContext.HttpContext.Request.HttpMethod;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            return new SystemRightsService().BuildResourceKey(controller, actionName, method); // system.user.get
        }

        /// <summary>
        /// 获取资源Key
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetResourceKey(ActionExecutedContext filterContext)
        {
            var isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"];
            var method = this.ControllerContext.RequestContext.HttpContext.Request.HttpMethod;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            return new SystemRightsService().BuildResourceKey(controller, actionName, method); // system.user.get
        }

        #region  解决MVC返回Json中日期格式问题 /Date('123123123')/格式

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new CustomJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new CustomJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }

        public new JsonResult Json(object data)
        {
            return new CustomJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 提供自定义日期格式 Json.
    /// </summary> 
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (this.Data != null)
            {
                // 这里使用自定义日期格式，默认是ISO8601格式
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" };
                response.ContentType = "application/json";
                response.Write(JsonConvert.SerializeObject(this.Data, Formatting.Indented, timeConverter));
            }
        }
    }
}
