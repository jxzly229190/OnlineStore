// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   路由规则配置类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// 路由规则配置类
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 注册路由规则
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "user",
            //    url: "user/{controller}/{action}/{id}",
            //    defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "V5.Portal.Backstage.Controllers.User" }
            //    );

            //routes.MapRoute(
            //    name: "Transact",
            //    url: "transact/{controller}/{action}/{id}",
            //    defaults: new { controller = "Transact", action = "Index", id = UrlParameter.Optional });

            //routes.MapRoute(
            //    name: "system",
            //    url: "system/{controller}/{action}/{id}",
            //    defaults: new { controller = "Department", action = "Index", id = UrlParameter.Optional });

            //routes.MapRoute(
            //    name: "product",
            //    url: "product/{controller}/{action}/{id}",
            //    defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "V5.Portal.Backstage.Controllers.Product" }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}