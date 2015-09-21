namespace V5.Portal.Backstage.Controllers.Configuration
{
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Configuration;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Configuration;
    using V5.Service.Configuration;

    public partial class ConfigController
    {
        #region Render View Method
        public PartialViewResult Delivery()
        {
            return this.PartialView("Delivery");
        }

        public PartialViewResult DeliveryMethod()
        {
            return this.PartialView("DeliveryMethod");
        }
        #endregion

        #region Corporation
        /// <summary>
        /// 获取所有的合作快递公司信息
        /// </summary>
        public ActionResult QueryCorporation([DataSourceRequest] DataSourceRequest request)
        {
            var service = new ConfigDeliveryCorporationService();

            var list = service.QueryAllConfigDeliveryCorporations();
            if (list != null)
            {
                var modelList = new List<ConfigDeliveryCorporationModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigDeliveryCorporationModel>(
                            config,
                            typeof(Config_Delivery_Corporation)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCorporation([DataSourceRequest] DataSourceRequest request, ConfigDeliveryCorporationModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigDeliveryCorporationService();

                    var product = DataTransfer.Transfer<Config_Delivery_Corporation>(
                        model,
                        typeof(ConfigDeliveryCorporationModel));

                    model.ID = service.Add(product);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 删除配送公司
        /// </summary>
        /// <param name="id">
        /// 配送公司ID
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult RemoveCorporation(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigDeliveryCorporationService();

                //this.productService.RemoveDepartmentByID(id);
                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 更新配送公司
        /// </summary>
        [HttpPost]
        public ActionResult ModifyCorporation([DataSourceRequest] DataSourceRequest request, ConfigDeliveryCorporationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigDeliveryCorporationService();

                var corporation = DataTransfer.Transfer<Config_Delivery_Corporation>(
                    model,
                    typeof(ConfigDeliveryCorporationModel));

                service.Modify(corporation);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// 查询所有 合作快递公司
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public JsonResult QuerySelectListItems()
        {
            var service = new ConfigDeliveryCorporationService();

            var list = service.QueryAllConfigDeliveryCorporations();
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null);
        }
        #endregion

        #region Cost

        public ActionResult QueryCost([DataSourceRequest] DataSourceRequest request)
        {
            var service = new ConfigDeliveryCostService();

            var list = service.QueryAllConfigDeliveryCosts();
            if (list != null)
            {
                var modelList = new List<ConfigDeliveryCostModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigDeliveryCostModel>(
                            config,
                            typeof(Config_Delivery_Cost)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCost([DataSourceRequest] DataSourceRequest request, ConfigDeliveryCostModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigDeliveryCostService();

                    var product = DataTransfer.Transfer<Config_Delivery_Cost>(
                        model,
                        typeof(ConfigDeliveryCostModel));

                    model.ID = service.Add(product);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveCost(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigDeliveryCostService();

                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifyCost([DataSourceRequest] DataSourceRequest request, ConfigDeliveryCostModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigDeliveryCostService();

                var cost = DataTransfer.Transfer<Config_Delivery_Cost>(
                    model,
                    typeof(ConfigDeliveryCostModel));

                service.Modify(cost);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        /// <summary>
        /// 根据快递公司查询分页快递费用列表.
        /// </summary>
        /// <param name="corporationId">
        /// 快递公司ID
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryByCorporationIDWithPaging(int corporationId, [DataSourceRequest] DataSourceRequest request)
        {
            var service = new ConfigDeliveryCostService();
            int rowCount = 0, pageCount = 0;
            var page = new Paging(" [DeliveryCorporationID]=" + corporationId, request.Page, request.PageSize);
            var list = service.Query(page, out pageCount, out rowCount);

            if (list != null)
            {
                var modelList = new List<ConfigDeliveryCostModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigDeliveryCostModel>(
                            config,
                            typeof(Config_Delivery_Cost)));
                }

                var dataSource = new DataSource() { Data = modelList, Total = rowCount, TotalPages = pageCount };

                return Json(dataSource, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        #endregion

        #region Method
        public ActionResult QueryMethod([DataSourceRequest] DataSourceRequest request)
        {
            var service = new ConfigDeliveryMethodService();

            var list = service.QueryAll();
            if (list != null)
            {
                var modelList = new List<ConfigDeliveryMethodModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigDeliveryMethodModel>(
                            config,
                            typeof(Config_Delivery_Method)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.Json(string.Empty);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddMethod([DataSourceRequest] DataSourceRequest request, ConfigDeliveryMethodModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigDeliveryMethodService();

                    var method = DataTransfer.Transfer<Config_Delivery_Method>(
                        model,
                        typeof(ConfigDeliveryMethodModel));

                    model.ID = service.Add(method);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveMethod(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigDeliveryMethodService();

                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifyMethod([DataSourceRequest] DataSourceRequest request, ConfigDeliveryMethodModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigDeliveryMethodService();

                var method = DataTransfer.Transfer<Config_Delivery_Method>(
                    model,
                    typeof(ConfigDeliveryMethodModel));

                service.Modify(method);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}
