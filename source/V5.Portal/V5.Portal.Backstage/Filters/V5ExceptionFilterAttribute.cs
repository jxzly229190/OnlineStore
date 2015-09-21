// --------------------------------------------------------------------------------------------------------------------
// <copyright file="V5ExceptionFilterAttribute.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   V5 异常过滤器特性类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Filters
{
    using System.Web.Mvc;

    using V5.Library.Logger;

    /// <summary>
    /// V5 异常过滤器特性类
    /// </summary>
    public class V5ExceptionFilterAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            LogUtils.Log(filterContext.Exception.Message);
            //TextLogger.Instance.Log(filterContext.Exception.Message, Category.Error);
        }
    }
}