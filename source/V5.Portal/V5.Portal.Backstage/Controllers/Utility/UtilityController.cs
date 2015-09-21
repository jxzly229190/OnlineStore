using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V5.Service.Utility;

namespace V5.Portal.Backstage.Controllers.Utility
{
    public partial class UtilityController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
