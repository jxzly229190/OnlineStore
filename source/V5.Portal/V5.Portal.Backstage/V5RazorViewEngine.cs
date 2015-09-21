// --------------------------------------------------------------------------------------------------------------------
// <copyright file="V5RazorViewEngine.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   V5 Razor 视图引擎类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage
{
    using System.Web.Mvc;

    /// <summary>
    /// V5 Razor 视图引擎类
    /// </summary>
    public class V5RazorViewEngine : RazorViewEngine
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="V5RazorViewEngine"/> class.
        /// </summary>
        public V5RazorViewEngine()
        {
            this.MasterLocationFormats = new[]
                                             {
                                                 "~/Views/system/{1}/{0}.cshtml",
                                                 "~/Views/user/{1}/{0}.cshtml",
                                                 "~/Views/product/{1}/{0}.cshtml",
                                                 "~/Views/configuration/{1}/{0}.cshtml",
                                                 "~/Views/transact/{1}/{0}.cshtml"
                                             };

            this.ViewLocationFormats = this.MasterLocationFormats;
            this.PartialViewLocationFormats = this.ViewLocationFormats;
        }

        #endregion
    }
}