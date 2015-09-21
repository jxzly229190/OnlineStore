using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V5.Portal.Backstage.Filters;
using V5.Service.Advertise;
using V5.Service.Promote;
using V5.Service.User;

namespace V5.Portal.Backstage.Controllers.Advertise
{
    public partial class AdvertiseController : BaseController
    {
        //
        // GET: /Advertise/
        #region Constants and Fields

        /// <summary>
        /// 广告服务对象
        /// </summary>
        private AdvertiseConfigService advertiseConfigService;
        /// <summary>
        /// LP服务类对象
        /// </summary>
        private PromoteLandingPageService promotelandPageService;
        /// <summary>
        /// 反馈信息
        /// </summary>
        private FeedBackService feedBackService;

        #endregion
        public ActionResult Index()
        {
            GetTopMenuID();
            return View();
        }
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "广告配置").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }
    }
}
