// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web;

namespace V5.Portal.Backstage
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;
    using V5.Portal.Backstage.App_Start;
    using V5.Service.System;
    using V5.Library.Logger;

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Public Methods and Operators

        /// <summary>
        /// 应用程序启动触发事件
        /// </summary>
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // 加载 省/市/区 数据到 MongoDB
            LoadProvinces();
            LoadCities();
            LoadCounties();

            // 添加资源信息
            LoadResource();

            // 日志
            LogUtils.Log("系统启动");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //日志
            LogUtils.Log("会话启动", "Session_Start", Category.Info, Session.SessionID);
        }

        /// <summary>
        /// 会话结束触发事件
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Session_End(object sender, EventArgs e)
        {
            //日志
            LogUtils.Log("会话销毁", "Session_End", Category.Info, Session.SessionID);
            var mongoDbHelper = new MongoDbStore<SystemUserSession>("SystemUserSessions");
            mongoDbHelper.Delete(systemUserSession => systemUserSession.SessionID == Session.SessionID);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ((HttpException)ex).GetHashCode() == 404)
            {
                Response.Redirect("/Error");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load provinces to MongoDb
        /// </summary>
        private static void LoadProvinces()
        {
			var mongoDbStore = new MongoDbStore<Province>("Provinces");
			mongoDbStore.Delete(item => item.ID != 0);

			var systemHomeService = new SystemHomeService();
			var provinces = systemHomeService.QueryProvinces();

			foreach (var province in provinces)
			{
				mongoDbStore.Insert(province);
			}
        }

        /// <summary>
        /// The load permissions.
        /// </summary>
        private static void LoadResource()
        {
            //var mongoDbStore = new MongoDbStore<System_Resources>("Resources");
            //mongoDbStore.Delete(item => item.ID != 0);

            //var systemResourcesService = new SystemResourcesService();
            //var resources = systemResourcesService.QueryAll();

            //foreach (var resource in resources)
            //{
            //    mongoDbStore.Insert(resource);
            //}

            MongoDBHelper.RefreshResource();
        }

        /// <summary>
        /// Load cities to MongoDb
        /// </summary>
        private static void LoadCities()
        {
            var mongoDbStore = new MongoDbStore<City>("Cities");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemHomeService = new SystemHomeService();
            var cities = systemHomeService.QueryCities();

            foreach (var city in cities)
            {
                mongoDbStore.Insert(city);
            }
        }

        /// <summary>
        /// Load counties to MongoDb
        /// </summary>
        private static void LoadCounties()
        {
            var mongoDbStore = new MongoDbStore<County>("Counties");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemHomeService = new SystemHomeService();
            var counties = systemHomeService.QueryCounties();
            foreach (var county in counties)
            {
                mongoDbStore.Insert(county);
            }
        }

        #endregion
    }
}