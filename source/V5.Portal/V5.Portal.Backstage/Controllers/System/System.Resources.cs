using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Backstage.Controllers.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;

    public partial class SystemController
    {
        /// <summary>
        /// 系统资源对象
        /// </summary>
        private SystemResourcesService systemResourcesService;
        
        /// <summary>
        /// 查询所有的系统资源
        /// </summary>
        /// <returns></returns>
        public JsonResult QueryAll()
        {
            this.systemResourcesService = new SystemResourcesService();

            var list = this.systemResourcesService.QueryAll();
            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null,JsonRequestBehavior.AllowGet);
        } 
    }
}
