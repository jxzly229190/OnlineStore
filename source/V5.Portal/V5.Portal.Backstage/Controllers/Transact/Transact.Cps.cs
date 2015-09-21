// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transact.Cps.cs" company="(C) 2013 www.gjw.com. All rights reserved.">
//   www.gjw.com
// </copyright>
// <summary>
//   Defines the TransactController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Product;
    using V5.DataContract.Transact;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Transact;
    using V5.Service.Product;
    using V5.Service.Transact;

    /// <summary>
    /// 交易控制器部分类.
    /// </summary>
    public partial class TransactController
    {
        #region Constants and Fields

        /// <summary>
        /// Cps服务对象.
        /// </summary>
        private CpsService cpsService;

        /// <summary>
        /// Cps佣金比例服务对象
        /// </summary>
        private CpsCommissionRatioService cpsCommissionRatioService;

        /// <summary>
        /// 商品类别服务对象.
        /// </summary>
        private ProductCategoryService productCategoryService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult Cps()
        {
            return this.PartialView("Cps");
        }

        /// <summary>
        /// 添加Cps合作平台.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cpsModel">
        /// CpsModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCps([DataSourceRequest] DataSourceRequest request, CpsModel cpsModel)
        {
            try
            {
                if (cpsModel != null)
                {
                    this.cpsService = new CpsService();

                    var cps = DataTransfer.Transfer<Cps>(cpsModel, typeof(CpsModel));

                    cpsModel.ID = this.cpsService.AddCps(cps);

                    if (cpsModel.ID > 0)
                    {
                        return this.Json(new[] { cpsModel }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加Cps时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// 查询Cps合作平台信息列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCps([DataSourceRequest] DataSourceRequest request)
        {
            this.cpsService = new CpsService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int totalCount;
            List<Cps> list;
            try
            {
                var paging = new Paging("[Cps]", null, "ID", null, request.Page, request.PageSize);

                int pageCount;
                list = this.cpsService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list == null)
            {
                return this.View();
            }

            var modelList = new List<CpsModel>();
            foreach (var cps in list)
            {
                modelList.Add(DataTransfer.Transfer<CpsModel>(cps, typeof(Cps)));
            }

            var result = new DataSourceResult { Data = modelList, Total = totalCount };
            return this.Json(result);
        }

        /// <summary>
        /// 查询所有CPS信息
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryAllCps([DataSourceRequest] DataSourceRequest request)
        {
            List<Cps> list;
            try
            {
                this.cpsService = new CpsService();

                list = this.cpsService.QueryAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list == null)
            {
                return this.Json(null, JsonRequestBehavior.AllowGet);
            }

            var modelList = new List<CpsModel>();
            foreach (var cps in list)
            {
                modelList.Add(DataTransfer.Transfer<CpsModel>(cps, typeof(Cps)));
            }

            return this.Json(modelList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改Cps合作平台信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cpsModel">
        /// CPS的模型.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ModifyCps([DataSourceRequest] DataSourceRequest request, CpsModel cpsModel)
        {
            try
            {
                if (cpsModel != null)
                {
                    this.cpsService = new CpsService();

                    var userLevel = DataTransfer.Transfer<Cps>(cpsModel, typeof(CpsModel));

                    this.cpsService.Modify(userLevel);
                }

                return this.Json(new[] { cpsModel }.ToDataSourceResult(request, this.ModelState));
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// 添加佣金比例信息
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="cpsCommissionRatioModel">
        /// CpsCommissionRatioModel的对象实例.
        /// </param>
        /// <param name="cps_ID">
        /// CPS合作平台编号.
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult AddCommissionRatio([DataSourceRequest] DataSourceRequest request, CpsCommissionRatioModel cpsCommissionRatioModel, string cps_ID)
        {
            try
            {
                if (cpsCommissionRatioModel != null)
                {
                    cpsCommissionRatioModel.CpsID = int.Parse(cps_ID);

                    this.cpsCommissionRatioService = new CpsCommissionRatioService();

                    var cpsCommissionRatio = DataTransfer.Transfer<Cps_CommissionRatio>(
                        cpsCommissionRatioModel,
                        typeof(CpsCommissionRatioModel));

                    cpsCommissionRatioModel.ID = this.cpsCommissionRatioService.Add(cpsCommissionRatio);

                    if (cpsCommissionRatioModel.ID > 0)
                    {
                        cpsCommissionRatio =
                            this.cpsCommissionRatioService.SelectCommissionRatioByID(cpsCommissionRatioModel.ID);
                        cpsCommissionRatioModel = DataTransfer.Transfer<CpsCommissionRatioModel>(
                            cpsCommissionRatio,
                            typeof(Cps_CommissionRatio));
                        return this.Json(new[] { cpsCommissionRatioModel }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加Cps佣金信息时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// 查询佣金比例信息.
        /// </summary>
        /// <param name="cpsID">
        /// CPS合作平台编号.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCommissionRatio(string cpsID, [DataSourceRequest] DataSourceRequest request)
        {
            List<Cps_CommissionRatio> list;
            try
            {
                this.cpsCommissionRatioService = new CpsCommissionRatioService();
                list = this.cpsCommissionRatioService.QueryCommissionRatioByCpsID(int.Parse(cpsID));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modellist = new List<CpsCommissionRatioModel>();
                foreach (var cpsCommissionRatio in list)
                {
                    modellist.Add(
                        DataTransfer.Transfer<CpsCommissionRatioModel>(cpsCommissionRatio, typeof(Cps_CommissionRatio)));
                }

                var result = new DataSourceResult { Data = list };
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// 修改佣金比例信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cpsCommissionRatioModel">
        /// The cps commission ratio model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ModifyCommissionRatio([DataSourceRequest] DataSourceRequest request, CpsCommissionRatioModel cpsCommissionRatioModel)
        {
            try
            {
                if (cpsCommissionRatioModel != null)
                {
                    this.cpsCommissionRatioService = new CpsCommissionRatioService();

                    var cpsCommissionRatio = DataTransfer.Transfer<Cps_CommissionRatio>(cpsCommissionRatioModel, typeof(CpsCommissionRatioModel));

                    this.cpsCommissionRatioService.Modify(cpsCommissionRatio);
                }

                return this.Json(new[] { cpsCommissionRatioModel }.ToDataSourceResult(request, this.ModelState));
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// 获取商品类别列表下拉框.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QuerySelectListItems()
        {
            List<Product_Category> list;
            try
            {
                this.productCategoryService = new ProductCategoryService();
                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 1", 1, 30);
                list = this.productCategoryService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var selectListItems = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "请选择" } };
                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = category.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = category.CategoryName,
                                             };

                    selectListItems.Add(selectListItem);
                }

                selectListItems[2].Selected = true;
                return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null);
        }

        #endregion
    }
}
