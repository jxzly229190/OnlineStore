using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Backstage.Controllers
{
    public class ToolsController : Controller
    {
        //
        // GET: /Tools/

        public ActionResult Index()
        {
            return View("SystemTools");
        }

		public ActionResult RefreshMongoDB()
		{
			try
			{
				MongoDBHelper.RefreshCollection(RefreshCollectionName.CountyData);

				MongoDBHelper.RefreshCollection(RefreshCollectionName.Resources);
				return this.Content("成功");
			}
			catch
			{
				return this.Content("失败");
			}
		}
    }
}
