// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   The base controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Text;
    using V5.Portal.Attributes;
    using V5.Portal.Common;
    using V5.Portal.Models;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : Controller
    {
        protected UserSession UserSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ActionAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true); // 判断Action上的标签
            var ctrAttrs = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true); // 判断Control上的标签            
            if (ctrAttrs.Length > 0 || ActionAttrs.Length > 0)
            {
                if (!UserSessionManager.IsLogin)
                {
                    this.HandleSessionLost(filterContext);
                }
            }

            UserSession = UserSessionManager.UserSession;
            
            base.OnActionExecuting(filterContext);
            return;

            /**
             * 流程：
             * 1. 检查是否存在VisitorKey
             *      1.1 若存在，则证明此用户不是第一访问，MNDB中有可能存在会话信息
             *          1.1.1 根据Visitorkey查找UserSession信息
             *              1.1.1.1 若有，则检查会话是否过期，若过期，则删除原会话并创建新的会话。
             *              1.1.1.2 若无，则为过期会话，则创建新的会话
             *       1.2 VisitorKey不存在，则证明此用户为第一访问，创建新会话。             * 
             * 
            
            var ActionAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true); // 判断Action上的标签
            var ctrAttrs = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true); // 判断Control上的标签            
            if (ctrAttrs.Length > 0 || ActionAttrs.Length > 0)
            {
                var userID = this.GetUserID();
                if (userID == 0)
                {
                    this.HandleSessionLost(filterContext);
                }
            }

            var httpSessionStateBase = this.HttpContext.Session;
            if (httpSessionStateBase != null)
            {
                var visitorKey = this.Request.Cookies.Get(ConstantParams.VisitorKeyName);
				if (visitorKey != null)
                {
					//todo: 处理用户会话
	                this.UserSession = HttpRuntime.Cache[visitorKey.Value] as UserSession;
                    //this.UserSession = MongoDBHelper.GetModel<UserSession>(u => u.VisitorKey == visitorKey.Value);
                    
                    if (this.UserSession == null)
                    {
                        // 过期，更新会话
                        this.UserSession = new UserSession
                                               {
                                                   SessionId = httpSessionStateBase.SessionID,
                                                   VisitorKey = visitorKey.Value
                                               };
                    }

                    var timeSpan = (DateTime.Now - this.UserSession.LastVisitTime).TotalMilliseconds;

                    // 过期，更新会话会
                    if (timeSpan > this.UserSession.ValidTime)
                    {
                        this.UserSession = new UserSession
                                               {
                                                   SessionId = httpSessionStateBase.SessionID,
                                                   VisitorKey = visitorKey.Value
                                               };
                    }

                    this.UserSession.LastVisitTime = DateTime.Now;
                    MongoDBHelper.UpdateModel(this.UserSession, u => u.VisitorKey == this.UserSession.VisitorKey);
					HttpRuntime.Cache[this.UserSession.VisitorKey] = this.UserSession;
                }
                else
                {
                    // 第一次访问用户，设置VisitorKey
                    this.UserSession = new UserSession
                                           {
                                               SessionId = httpSessionStateBase.SessionID,
                                               VisitorKey = Guid.NewGuid().ToString(),
                                               LastVisitTime = DateTime.Now
                                           };
                    MongoDBHelper.UpdateModel(this.UserSession, u => u.VisitorKey == this.UserSession.VisitorKey);
	                HttpRuntime.Cache[this.UserSession.VisitorKey] = this.UserSession;
                    this.Response.AppendCookie(
                        new HttpCookie(ConstantParams.VisitorKeyName, this.UserSession.VisitorKey));
                }
            }

            base.OnActionExecuting(filterContext);
            */
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
                filterContext.Result = this.RedirectToRoute(
                    "Default",
                    new
                        {
                            Controller = "Login",
                            Action = "Index",
                            backurl = Request.Url != null ? Request.Url.PathAndQuery : null
                        });
            }
            else
            {
				filterContext.HttpContext.Response.StatusCode = 600;
                filterContext.Result = this.Content(Request.UrlReferrer != null ? Request.UrlReferrer.PathAndQuery : null);
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
        /// The is login.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLoginHtml()
        {
            return UserSessionManager.IsLogin ?
                "<a href='#' class='login-name'>" + UserSessionManager.ShowName + "</a><a id='loginout' href='javascript:Navigation.LoginOut();'>退出</a>"
                : "<a class='login-btn' href='/Login/index' target='_self'>登录</a><a class='reg-btn' href='/Login/register' target='_self'>快速注册</a>";
        }

        /// <summary>
        ///  获取登录会员的编号.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetUserID()
        {
            return UserSessionManager.UserID;
            /*
            var visitorkey = this.Request.Cookies.Get(ConstantParams.VisitorKeyName);
			if (visitorkey != null && HttpRuntime.Cache[visitorkey.Value] != null)
            {
				var userSession = HttpRuntime.Cache[visitorkey.Value] as UserSession;
                //var userSession = MongoDBHelper.GetModel<UserSession>(u => u.VisitorKey == visitorkey.Value);
                if (userSession != null)
                {
                    var timeSpan = (DateTime.Now - userSession.LastVisitTime).TotalMilliseconds;
                    if (timeSpan < userSession.ValidTime)
                    {
                        return userSession.UserID;
                    }
                }
            }

            return 0;
            */
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