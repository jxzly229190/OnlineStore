using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using V5.Library.Storage.DB.NoSql;
    using V5.Portal.Filters;
    using V5.Portal.Models;

    public class MongoDemoController : Controller
    {
        //
        // GET: /MongoDemo/

        public ActionResult Index()
        {
            var list = new List<PageModel>()
                           {
                               new PageModel()
                                   {
                                       PageKey = "index",
                                       PageContent = "IndexContent_original",
                                       LastUpdateTime = DateTime.Now
                                   },
                               new PageModel()
                                   {
                                       PageKey = "productList",
                                       PageContent = "ProductList_original",
                                       LastUpdateTime = DateTime.Now
                                   }
                           };

            foreach (var pageModel in list)
            {
                //MongoDBHelper.Update(pageModel);
            }

            var newList = MongoDBHelper.List<PageModel>(p => p.PageKey != null);

            return View(newList);
        }

        [AllowAnonymous]
        public ActionResult Update(string indexContent, string productContent)
        {
			//if (!string.IsNullOrWhiteSpace(indexContent))
			//    //MongoDBHelper.Update(
			//    //    new PageModel() { PageKey = "index", PageContent = indexContent, LastUpdateTime = DateTime.Now });

			//if (!string.IsNullOrWhiteSpace(productContent))
			//    //MongoDBHelper.Update(
			//    //    new PageModel()
			//    //        {
			//    //            PageKey = "productList",
			//    //            PageContent = productContent,
			//    //            LastUpdateTime = DateTime.Now
			//    //        });

            return this.View(MongoDBHelper.List<PageModel>(p => p.PageKey != null));
        }
    }
}
