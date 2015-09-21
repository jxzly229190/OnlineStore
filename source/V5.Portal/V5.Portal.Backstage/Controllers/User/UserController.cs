// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员控制类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.User
{
    using global::System.Linq;
    using global::System.Web.Mvc;

    using V5.Portal.Backstage.Filters;

    /// <summary>
    /// 会员控制类.
    /// </summary>
    [V5ExceptionFilter]
    public partial class UserController : BaseController
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
        /// 获取会员管理菜单编号
        /// </summary>
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "会员管理").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }

        #endregion
    }
}
