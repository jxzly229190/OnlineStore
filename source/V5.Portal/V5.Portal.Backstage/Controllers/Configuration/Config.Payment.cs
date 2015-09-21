namespace V5.Portal.Backstage.Controllers.Configuration
{
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Configuration;
    using V5.Library;
    using V5.Portal.Backstage.Models.Configuration;
    using V5.Service.Configuration;

    public partial class ConfigController
    {

        #region Payment Organization

        public PartialViewResult Payment()
        {
            return this.PartialView("Payment");
        }

        public ActionResult QueryPaymentOrganization([DataSourceRequest] DataSourceRequest request, int paymentTypeId)
        {
            var service = new ConfigPaymentOrganizationService();

            var list = service.QueryAll();
            if (list != null)
            {
                var modelList = new List<ConfigPaymentOrganizationModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigPaymentOrganizationModel>(
                            config,
                            typeof(Config_Payment_Organization)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        public ActionResult QueryByPaymentTypeId([DataSourceRequest] DataSourceRequest request, int typeID)
        {
            var service = new ConfigPaymentOrganizationService();

            var list = service.QueryByPaymentTypeId(typeID);
            if (list != null)
            {
                var modelList = new List<ConfigPaymentOrganizationModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigPaymentOrganizationModel>(
                            config,
                            typeof(Config_Payment_Organization)));
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        public ActionResult AddPaymentOrganization([DataSourceRequest] DataSourceRequest request, ConfigPaymentOrganizationModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigPaymentOrganizationService();

                    var paymentOrganization = DataTransfer.Transfer<Config_Payment_Organization>(
                        model,
                        typeof(ConfigPaymentOrganizationModel));

                    model.ID = service.Add(paymentOrganization);
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemovePaymentOrganization(int id, FormCollection collection)
        {
            try
            {
                var service = new ConfigPaymentOrganizationService();

                service.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifyPaymentOrganization([DataSourceRequest] DataSourceRequest request, ConfigPaymentOrganizationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigPaymentOrganizationService();

                var paymentOrganization = DataTransfer.Transfer<Config_Payment_Organization>(
                    model,
                    typeof(ConfigPaymentOrganizationModel));

                service.Modify(paymentOrganization);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// 查询所有支付类别
        /// </summary>
        /// <returns></returns>
        public JsonResult QueryTypeSelectItems()
        {
            List<ConfigPaymentTypeModel> modelList = new List<ConfigPaymentTypeModel>();

            var paymentTypeService = new ConfigPaymentTypeService();

            var list = paymentTypeService.QueryAll();
            foreach (var config in list)
            {
                modelList.Add(DataTransfer.Transfer<ConfigPaymentTypeModel>(
                                config,
                                typeof(Config_Payment_Type)));
            }
            return Json(modelList, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region Payment Type
        public ActionResult QueryPaymentType([DataSourceRequest] DataSourceRequest request)
        {
            var service = new ConfigPaymentTypeService();

            var list = service.QueryAll();
            if (list != null)
            {
                var modelList = new List<ConfigPaymentTypeModel>();

                foreach (var config in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<ConfigPaymentTypeModel>(
                            config,
                            typeof(Config_Payment_Type)));
                }

                for (var i = 0; i < modelList.Count; i++)
                {
                    SetPaymentMethod(modelList[i]);
                }

                return Json(modelList.ToDataSourceResult(request));
            }

            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPaymentType([DataSourceRequest] DataSourceRequest request, ConfigPaymentTypeModel model)
        {
            try
            {
                if (model != null)
                {
                    var service = new ConfigPaymentTypeService();

                    var product = DataTransfer.Transfer<Config_Payment_Type>(
                        model,
                        typeof(ConfigPaymentTypeModel));

                    model.ID = service.Add(product);
                }

                SetPaymentMethod(model);

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch
            {
                return View();
            }
        }

        private static void SetPaymentMethod(ConfigPaymentTypeModel model)
        {
            if (model.PaymentMethodID == 0)
            {
                model.PaymentMethodName = "在线支付";
            }
			else if (model.PaymentMethodID == 1)
            {
                model.PaymentMethodName = "货到付款";
            }
			else if (model.PaymentMethodID == 2)
			{
				model.PaymentMethodName = "货到付款(Pos刷卡)";
			}
        }

        [HttpPost]
        public void RemovePaymentType(int id)
        {
            try
            {
                var service = new ConfigPaymentTypeService();

                service.Remove(id);

                Response.Write("恭喜，删除成功");
            }
            catch
            {
                Response.Write("删除失败");
            }
        }

        [HttpPost]
        public ActionResult ModifyPaymentType([DataSourceRequest] DataSourceRequest request, ConfigPaymentTypeModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new ConfigPaymentTypeService();

                var paymentMethod = DataTransfer.Transfer<Config_Payment_Type>(
                    model,
                    typeof(ConfigPaymentTypeModel));

                SetPaymentMethod(model);

                service.Modify(paymentMethod);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// 获取配送方式
        /// </summary>
        /// <param name="PaymentMethodID"></param>
        /// <returns></returns>
        public JsonResult QueryMethodList()
        {
           
            var modelList = new List<PaymentMethodModel>();
            
            //配送支付方式：0-在线支付，1-货到付款
            modelList.Add(new PaymentMethodModel() { PaymentMethodName = "在线支付", PaymentMethodId = 0 });
            modelList.Add(new PaymentMethodModel() { PaymentMethodName = "货到付款", PaymentMethodId = 1 });
			modelList.Add(new PaymentMethodModel() { PaymentMethodName = "货到付款（Pos刷卡）", PaymentMethodId = 2 });

            return this.Json(modelList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}