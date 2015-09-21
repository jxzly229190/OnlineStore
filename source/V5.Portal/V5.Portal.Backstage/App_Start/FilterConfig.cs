// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   过滤器配置类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.App_Start
{
    using System.Web.Mvc;

    /// <summary>
    /// 过滤器配置类
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注册全局的过滤器
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}