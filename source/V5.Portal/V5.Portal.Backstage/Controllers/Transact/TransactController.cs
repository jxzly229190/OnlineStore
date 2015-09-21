// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   交易控制器类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System.Linq;
    using global::System.Web.Mvc;

    using V5.Portal.Backstage.Filters;

    /// <summary>
    /// 交易控制器类.
    /// </summary>
    [V5ExceptionFilter]
    public partial class TransactController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.GetTopMenuID();
            return this.View();
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// 获取交易管理菜单编号
        /// </summary>
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "交易管理").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
                this.ViewBag.TopMenuURL = topMenu.URL;
            }
        }

        #endregion
    }
}
