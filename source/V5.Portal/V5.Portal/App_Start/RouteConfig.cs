namespace V5.Portal
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Brand", "{brand}.htm", new { controller = "Brand", action = "Index" });

			routes.MapRoute("Product", "Product/{action}-id-{id}.htm", new { controller = "Product", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("CustomAPI", "api/{action}.aspx", new { controller = "api", action = "api", id = UrlParameter.Optional });

			routes.MapRoute("CPS", "tracert/{action}.aspx", new { controller = "cps", action = "cps", id = UrlParameter.Optional });

            routes.MapRoute("HtmlDefault", "{controller}/{action}/{id}.html", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}