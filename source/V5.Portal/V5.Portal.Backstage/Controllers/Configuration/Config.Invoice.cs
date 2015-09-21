
namespace V5.Portal.Backstage.Controllers.Configuration
{
    using global::System.Collections.Generic;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Configuration;
    using V5.Library;
    using V5.Portal.Backstage.Models.Configuration;
    using V5.Service.Configuration;

    public partial class ConfigController
    {
        public PartialViewResult InvoiceType()
        {
            return this.PartialView("InvoiceType");
        }

        public PartialViewResult InvoiceContent()
        {
            
            return this.PartialView("InvoiceContent");
        }

        #region Invoice Type 操作方法

        /// <summary>
        /// The query invoice type.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult QueryInvoiceType(DataSourceRequest request)
        {
            var service = new ConfigInvoiceTypeSevice();

            var list = service.QueryAll();
            if (list != null)
            {
                var modelList = new List<ConfigInvoiceTypeModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigInvoiceTypeModel>(
                            config,
                            typeof(Config_Invoice_Type)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        /// <summary>
        /// The query invoice type.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult QueryInvoiceType()
        {
            var service = new ConfigInvoiceTypeSevice();

            var list = service.QueryAll();
            if (list != null)
            {
                var modelList = new List<ConfigInvoiceTypeModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigInvoiceTypeModel>(
                            config,
                            typeof(Config_Invoice_Type)));
                }

                return Json(modelList);
            }

            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddInvoiceType([DataSourceRequest] DataSourceRequest request, ConfigInvoiceTypeModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigInvoiceTypeSevice();

                    var invoice = DataTransfer.Transfer<Config_Invoice_Type>(
                        model,
                        typeof(ConfigInvoiceTypeModel));

                    model.ID = service.Add(invoice);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveInvoiceType(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigInvoiceTypeSevice();

                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifyInvoiceType([DataSourceRequest] DataSourceRequest request, ConfigInvoiceTypeModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigInvoiceTypeSevice();

                var cost = DataTransfer.Transfer<Config_Invoice_Type>(
                    model,
                    typeof(ConfigInvoiceTypeModel));

                service.Modify(cost);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Invoice Content 操作方法
        public ActionResult QueryInvoiceContent([DataSourceRequest] DataSourceRequest request)
        {
			//var service = new ConfigInvoiceContentSevice();

	        var list = new List<string>();
            if (list != null)
            {
                var modelList = new List<ConfigInvoiceContentModel>();

				//foreach (var config in list)
				//{
				//    modelList.Add(
				//        DataTransfer.Transfer<ConfigInvoiceContentModel>(
				//            config,
				//            typeof(Config_Invoice_Content)));
				//}
				
				modelList.Add(new ConfigInvoiceContentModel()
					              {
						              Name = "酒水",
									  ID = 0
					              });

				modelList.Add(new ConfigInvoiceContentModel()
				{
					Name = "食品",
					ID = 1
				});

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        /// <summary>
        /// The query invoice content.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult QueryInvoiceContent()
        {
			//var service = new ConfigInvoiceContentSevice();

			   var modelList = new List<ConfigInvoiceContentModel>();

			//    foreach (var config in list)
			//    {
			//        modelList.Add(
			//            DataTransfer.Transfer<ConfigInvoiceContentModel>(
			//                config,
			//                typeof(Config_Invoice_Content)));
			//    }

			modelList.Add(new ConfigInvoiceContentModel()
			{
				Name = "酒水",
				ID = 0
			});

			modelList.Add(new ConfigInvoiceContentModel()
			{
				Name = "食品",
				ID = 1
			});
               
			return Json(modelList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddInvoiceContent([DataSourceRequest] DataSourceRequest request, ConfigInvoiceContentModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigInvoiceContentSevice();

                    var invoice = DataTransfer.Transfer<Config_Invoice_Content>(
                        model,
                        typeof(ConfigInvoiceContentModel));

                    model.ID = service.Add(invoice);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveInvoiceContent(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigInvoiceContentSevice();

                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifyInvoiceContent([DataSourceRequest] DataSourceRequest request, ConfigInvoiceContentModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigInvoiceContentSevice();

                var cost = DataTransfer.Transfer<Config_Invoice_Content>(
                    model,
                    typeof(ConfigInvoiceContentModel));

                service.Modify(cost);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}