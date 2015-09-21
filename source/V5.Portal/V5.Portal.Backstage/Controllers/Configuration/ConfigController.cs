using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Backstage.Controllers.Configuration
{
    using V5.Portal.Backstage.Filters;
    using V5.Service.Product;

    /// <summary>
    /// 配置控制器类
    /// </summary>
    [V5ExceptionFilter]
    public partial class ConfigController : BaseController
    {
        public ActionResult Index()
        {
            GetTopMenuID();
            var product = new ProductService().QueryByID(3);
            return View();
        }

        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "配置管理").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }
    }
}
