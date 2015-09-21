// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelController.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Th团购频道控制器类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Controllers.Channel
{
    using global::System.Text;
    using global::System.Web.Mvc;

    using V5.Portal.Backstage.Filters;
    using V5.Service.Channel;
    using V5.Service.Product;
    using V5.Service.User;

    /// <summary>
    /// 团购频道控制器类
    /// </summary>
    [V5ExceptionFilter]
    public partial class ChannelController : BaseController
    {
        #region Constants and Fields

        /// <summary>
        /// 团购频道服务对象
        /// </summary>
        private ChannelGroupBuyService channelGroupBuyService;

        /// <summary>
        /// 商品服务对象 
        /// </summary>
        private ProductService productService;

        /// <summary>
        /// 会员服务对象
        /// </summary>
        private UserLevelService userLevelService;
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 团购频道首页
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.GetTopMenuID();
            return this.View("Index");
        }

        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "团购管理").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }


        /// <summary>
        /// The check condition.
        /// </summary>
        /// <param name="stringBuilder">
        /// The string builder.
        /// </param>
        private static void CheckCondition(StringBuilder stringBuilder)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" And ");
            }
        }
        #endregion
    }
}
