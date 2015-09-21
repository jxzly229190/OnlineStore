// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NightFairController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the NightFairController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// 夜市.
    /// </summary>
    public class NightFairController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 今夜明星1.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMainStar()
        {
            return string.Empty;
        }

        /// <summary>
        /// 今夜明星2.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMinorStar()
        {
            return string.Empty;
        }

        /// <summary>
        /// 白酒热卖.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetBaijiu()
        {
            return string.Empty;
        }

        /// <summary>
        /// 红酒热卖.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHongjiu()
        {
            return string.Empty;
        }

        /// <summary>
        /// 洋酒热卖.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetYangjiu()
        {
            return string.Empty;
        }

        /// <summary>
        /// 黄酒热卖.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHuangjiu()
        {
            return string.Empty;
        }
    }
}
