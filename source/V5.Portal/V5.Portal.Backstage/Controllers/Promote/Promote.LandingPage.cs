// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.LandingPage.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   LP管理部分控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Drawing;
    using global::System.Drawing.Imaging;
    using global::System.IO;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Promote;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote;
    using V5.Service.Promote;

    /// <summary>
    /// LP管理部分控制器.
    /// </summary>
    public partial class PromoteController
    {
        #region Constants and Fields

        /// <summary>
        /// LP服务对象.
        /// </summary>
        private PromoteLandingPageService promoteLandingPageService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// LP管理.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult LandingPage()
        {
            return this.PartialView("LandingPage");
        }

        /// <summary>
        /// The landing page edit.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult LandingPageEdit()
        {
            return this.PartialView("LandingPageEdit");
        }

        /// <summary>
        /// LP活动列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="filterYear">
        /// The filter Year.
        /// </param>
        /// <param name="filterMonth">
        /// The filter Month.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryLandingPage(
            [DataSourceRequest] DataSourceRequest request,
            string filterYear,
            string filterMonth)
        {
            this.promoteLandingPageService = new PromoteLandingPageService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var stringBuilder = new StringBuilder();
            
            if (!string.IsNullOrEmpty(filterYear))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("DATEDIFF(yyyy,CreateTime,'" + filterYear + "')=0");
            }

            if (!string.IsNullOrEmpty(filterMonth))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("DATEDIFF(mm,CreateTime,'" + filterMonth + "-1')=0"); // (-1补全日期)
            }

            CheckCondition(stringBuilder);
            stringBuilder.Append("[IsDelete] = 0");
            var condition = stringBuilder.ToString();
            try
            {
                var paging = new Paging("[Promote_LandingPage]", null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);

                int pageCount;
                int totalCount;
                var list = this.promoteLandingPageService.QueryList(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(null);
                }

                var modelList = new List<PromoteLandingPageModel>();
                foreach (var userLevelPrice in list)
                {
                    var model = DataTransfer.Transfer<PromoteLandingPageModel>(
                        userLevelPrice,
                        typeof(Promote_LandingPage));
                    if (model.EndTime > DateTime.Now)
                    {
                        switch (model.Status)
                        {
                            case 1:
                                model.StatusName = "正常";
                                break;
                            case 2:
                                model.StatusName = "暂停";
                                break;
                            case 3:
                                model.StatusName = "停止";
                                break;
                        }
                    }
                    else
                    {
                        model.StatusName = "过期";
                    }

                    modelList.Add(model);
                }

                var result = new DataSourceResult { Data = modelList, Total = totalCount };
                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加LP信息.
        /// </summary>
        /// <param name="landingPageModel">
        /// The landing page model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddLandingPage(PromoteLandingPageModel landingPageModel)
        {
            try
            {
                this.promoteLandingPageService = new PromoteLandingPageService();
                var promoteLandingPage = DataTransfer.Transfer<Promote_LandingPage>(
                    landingPageModel,
                    typeof(PromoteLandingPageModel));
                promoteLandingPage.EmployeeID = this.SystemUserSession.EmployeeID;
                promoteLandingPage.Status = 1;
                this.promoteLandingPageService.Add(promoteLandingPage);
                return this.Json(new AjaxResponse(1, "添加成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 修改LP信息.
        /// </summary>
        /// <param name="landingPageModel">
        /// The landing page model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ModifyLandingPage(PromoteLandingPageModel landingPageModel)
        {
            try
            {
                this.promoteLandingPageService = new PromoteLandingPageService();
                var promoteLandingPage = DataTransfer.Transfer<Promote_LandingPage>(
                    landingPageModel,
                    typeof(PromoteLandingPageModel));
                promoteLandingPage.EmployeeID = this.SystemUserSession.EmployeeID;
                this.promoteLandingPageService.Modify(promoteLandingPage);

                return this.Json(new AjaxResponse(1, "修改成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 查询指定的LP活动.
        /// </summary>
        /// <param name="landingPageId">
        /// The landing page id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult QueryLandingPageByID(string landingPageId)
        {
            try
            {
                this.promoteLandingPageService = new PromoteLandingPageService();
                var promoteLandingPage = this.promoteLandingPageService.Query(int.Parse(landingPageId));
                var landingPageModel = DataTransfer.Transfer<PromoteLandingPageModel>(
                    promoteLandingPage,
                    typeof(Promote_LandingPage));
                return this.Json(landingPageModel);
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 删除指定的LP活动.
        /// </summary>
        /// <param name="landingPageId">
        /// The landing page id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RemoveLandingPage(string landingPageId)
        {
            try
            {
                this.promoteLandingPageService = new PromoteLandingPageService();
                this.promoteLandingPageService.Remove(int.Parse(landingPageId));
               
                return this.Json(new AjaxResponse(1, "删除成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 加载LP管理树
        /// </summary>
        /// <returns>LP管理树</returns>
        public JsonResult QueryAllLandingPage()
        {
            List<Promote_LandingPage> list = new PromoteLandingPageService().QueryAll();
            var query = from p in list select new { p.ID, p.PID, p.Name };
            return this.Json(query, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}
