using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using V5.Library.Logger;

using System.IO;
using System.Threading;

namespace V5.Portal
{
    using V5.Library;
    using V5.Portal.Models;

	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected static Thread refresh;
        protected static object obj = new object();

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            StaticContentRewrite();
        }

        /// <summary>
        /// 处理静态发布内容
        /// </summary>
        private void StaticContentRewrite()
        {
            if (Context.Request.Path == "/Home/Index" || Context.Request.FilePath == "/" || Context.Request.FilePath.StartsWith("/index.htm", StringComparison.OrdinalIgnoreCase))
            {
                if (File.Exists(Server.MapPath("~/index.htm")))
                {
                    Context.RewritePath("~/index.htm");
                }
            }
        }

	    protected void Application_Start()
	    {
		    AreaRegistration.RegisterAllAreas();

		    //WebApiConfig.Register(GlobalConfiguration.Configuration);
		    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

		    //RegisterRoutes(RouteTable.Routes);
		    RouteTable.Routes.RouteExistingFiles = false;
		    RouteConfig.RegisterRoutes(RouteTable.Routes);

		    BundleConfig.RegisterBundles(BundleTable.Bundles);
		    AuthConfig.RegisterAuth();
	    }

	    protected void Session_Start(object sender, EventArgs e)
	    {
            UserSessionManager.Start();
            lock (obj)
            {
                if (refresh == null)
                {
                    refresh = new Thread(new ThreadStart(Refresh.Init));
                    refresh.IsBackground = true;
                    refresh.Name = "线程名称：" + Utils.HostName + "[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                    refresh.Start();
                    LogUtils.Log(refresh.Name + ", 00. 刷新服务已创建" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Create", Category.Fatal);
                }
                else if (!refresh.IsAlive)
                {
                    refresh = new Thread(new ThreadStart(Refresh.Init));
                    refresh.IsBackground = true;
                    refresh.Name = "线程名称：" + Utils.HostName + "[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                    refresh.Start();
                    LogUtils.Log(refresh.Name + ", 00. 刷新服务已重启" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.ReStart", Category.Fatal);
                }
            }

            ////从MongoDB里面获取用户信息 统计用户访问量
            //var visitorKey = this.Request.Cookies.Get(ConstantParams.VisitorKeyName);
            //var session = MongoDBHelper.GetModel<UserSession>(u => u.SessionId == Session.SessionID);
            //if (session == null)
            //{
            //    return;
            //}
            //var visitorService = new SystemVisitorService();
            //var visitor = new System_Visitor
            //{
            //    SessionID = session.SessionId,
            //    UserName = session.ShowName ?? "匿名用户",
            //    IP4Address = Request.UserHostAddress,
            //    EndTime = DateTime.Now.AddHours(24 - DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute)
            //};
            //if (visitorService.Insert(visitor) > 0)
            //{
            //}
            //else
            //{
            //    this.Response.Redirect("/Utility/Error");
            //}
        }

        void Session_End(object sender, EventArgs e)
        {
            ////用户离开时出
            //var visitorService = new SystemVisitorService();
            //var visitor = new System_Visitor
            //{
            //    SessionID = Session.SessionID
            //};
            //visitorService.Update(visitor);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //将错误写入日志
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            if (httpException != null && httpException.GetHashCode() == 404)
            {
                LogUtils.Log("程序执行错误，错误消息：" + httpException.Message + ";错误堆栈：" + httpException.StackTrace,
                        "公共错误处理:Application_Error",
                        Category.Error);
            }
        }
    }
}