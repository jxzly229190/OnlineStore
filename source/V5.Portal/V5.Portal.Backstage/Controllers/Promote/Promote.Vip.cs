// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.Vip.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销部分控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Promote;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote;
    using V5.Portal.Backstage.Utils;
    using V5.Service.Promote;

    /// <summary>
    /// 促销部分控制器.
    /// </summary>
    public partial class PromoteController
    {
        /// <summary>
        /// The promote vip service.
        /// </summary>
        private PromoteVipService promoteVipService;

        /// <summary>
        /// 会员促销.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult Vip()
        {
            return this.PartialView("Vip");
        }

        /// <summary>
        /// 会员促销编辑页.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult EditVip()
        {
            return this.PartialView("VipEdit");
        }

        /// <summary>
        /// 会员促销.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="promoteName">
        /// 活动名称.
        /// </param>
        /// <param name="promoteStatus">
        /// 活动状态.
        /// </param>
        /// <param name="startStartTime">
        /// 活动开始时间范围起点时间.
        /// </param>
        /// <param name="startEndTime">
        /// 活动开始时间范围结点时间.
        /// </param>
        /// <param name="endStartTime">
        /// 活动结束时间范围起点时间.
        /// </param>
        /// <param name="endEndTime">
        /// 活动结束时间范围结点时间.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryVip(
            [DataSourceRequest] DataSourceRequest request,
            string promoteName,
            string promoteStatus,
            string startStartTime,
            string startEndTime,
            string endStartTime,
            string endEndTime)
        {
            this.promoteMeetMoneyService = new PromoteMeetMoneyService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            int totalCount;
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(promoteName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[Name] like '%" + promoteName + "%'");
            }

            if (!string.IsNullOrEmpty(startStartTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[StartTime] >= '" + startStartTime + "'");
            }

            if (!string.IsNullOrEmpty(startEndTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[StartTime] <= '" + startEndTime + "'");
            }

            if (!string.IsNullOrEmpty(endStartTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[EndTime] >= '" + endStartTime + "'");
            }

            if (!string.IsNullOrEmpty(endEndTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[EndTime] <= '" + endEndTime + "'");
            }

            switch (promoteStatus)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[StartTime] > '" + DateTime.Now + "'");
                    break;
                case "2":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("'" + DateTime.Now + "' Between [StartTime] And [EndTime] And [Status] in (1,2) ");
                    break;
                case "3":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("([EndTime] < '" + DateTime.Now + "' or [Status] = 3)");
                    break;
            }

            CheckCondition(stringBuilder);
            stringBuilder.Append("[IsDelete] = 0");
            var condition = stringBuilder.ToString();
            List<Promote_Vip> list;
            try
            {
                var paging = new Paging(
                    "[Promote_Vip]",
                    null,
                    "[ID]",
                    condition,
                    request.Page,
                    request.PageSize,
                    "[CreateTime]",
                    1);
                int pageCount;
                list = new PromoteVipService().Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<PromoteVipModel>();
                foreach (var coupon in list)
                {
                    modelList.Add(DataTransfer.Transfer<PromoteVipModel>(coupon, typeof(Promote_Vip)));
                }

                foreach (var model in modelList)
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

                    model.OperateText = this.GetVipOperationHtml(
                        model.ID,
                        model.Status,
                        model.StartTime,
                        model.EndTime);
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 添加会员促销.
        /// </summary>
        /// <param name="promoteVipModel">
        /// 会员促销实体.
        /// </param>
        /// <param name="scopes">
        /// The scopes.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddVip(PromoteVipModel promoteVipModel, int[] scopes)
        {
            try
            {
                var sb = new StringBuilder();
                this.promoteVipService = new PromoteVipService();
                promoteVipModel.EmployeeID = this.SystemUserSession.EmployeeID;
                promoteVipModel.Status = 1;

                var promoteVip = DataTransfer.Transfer<Promote_Vip>(promoteVipModel, typeof(PromoteVipModel));

                var promoteVipScopeList = new List<Promote_Vip_Scope>();
                foreach (var scope in scopes)
                {
                    var promoteVipScope = new Promote_Vip_Scope { ProductID = scope };
                    promoteVipScopeList.Add(promoteVipScope);
                    sb.Append(scope + ",");
                }
                
                // 验证相同商品是否参与其他促销
                var products = this.VerifyPromote(0, sb.ToString());
                if (!string.IsNullOrEmpty(products))
                {
                    return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                }

                promoteVip.Scopes = promoteVipScopeList;
                this.promoteVipService.Add(promoteVip);
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "添加失败！" + exception.Message));
            }

            return this.Json(new AjaxResponse(1, "添加成功！"));
        }

        /// <summary>
        /// 修改会员促销.
        /// </summary>
        /// <param name="promoteVipModel">
        /// 会员促销实体.
        /// </param>
        /// <param name="scopes">
        /// The scopes.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult ModifyVip(PromoteVipModel promoteVipModel, int[] scopes)
        {
            try
            {
                var sb = new StringBuilder();
                this.promoteVipService = new PromoteVipService();
                promoteVipModel.EmployeeID = this.SystemUserSession.EmployeeID;
                promoteVipModel.Status = 1;

                var promoteVip = DataTransfer.Transfer<Promote_Vip>(promoteVipModel, typeof(PromoteVipModel));

                var promoteVipScopeList = new List<Promote_Vip_Scope>();
                foreach (var scope in scopes)
                {
                    var promoteVipScope = new Promote_Vip_Scope { ProductID = scope };
                    promoteVipScopeList.Add(promoteVipScope); 
                    sb.Append(scope + ",");
                }

                // 验证相同商品是否参与其他促销
                var products = this.VerifyPromote(promoteVipModel.ID, sb.ToString());
                if (!string.IsNullOrEmpty(products))
                {
                    return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                }

                promoteVip.Scopes = promoteVipScopeList;
                this.promoteVipService.Modify(promoteVip);

                return this.Json(new AjaxResponse(1, "修改成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "添加失败！" + exception.Message));
            }
        }

        /// <summary>
        /// 暂停/恢复/停止指定的的促销活动.
        /// </summary>
        /// <param name="id">
        /// 促销活动编号.
        /// </param>
        /// <param name="status">
        /// 活动状态（1：正常,2:暂停,3：停止）.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult ChangesVipStatus(int id, int status)
        {
            try
            {
                if (status == 1)
                {
                    var sb = new StringBuilder();
                    this.promoteVipService = new PromoteVipService();
                    var scopes = this.promoteVipService.QueryScopeByID(id);
                    if (scopes != null)
                    {
                        foreach (var scope in scopes)
                        {
                            sb.Append(scope.ProductID + ",");
                        }
                        
                        // 验证相同商品是否参与其他促销
                        var products = this.VerifyPromote(id, sb.ToString());
                        if (!string.IsNullOrEmpty(products))
                        {
                            return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                        }
                    }
                }

                this.promoteVipService = new PromoteVipService();
                this.promoteVipService.ModifyStatus(id, status);
                return this.Json(new AjaxResponse(1, "操作成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "操作失败：" + exception.Message));
            }
        }

        /// <summary>
        /// 删除指定的的促销活动.
        /// </summary>
        /// <param name="id">
        /// 促销活动编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RemoveVip(int id)
        {
            try
            {
                this.promoteVipService = new PromoteVipService();
                this.promoteVipService.Remove(id);
                return this.Json(new AjaxResponse(1, "操作成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "操作失败：" + exception.Message));
            }
        }

        /// <summary>
        /// 查询指定的会员促销.
        /// </summary>
        /// <param name="id">
        /// 会员促销编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult QueryVipByID(int id)
        {
            try
            {
                this.promoteVipService = new PromoteVipService();
                var promoteVip = this.promoteVipService.QueryByID(id);
                promoteVip.Scopes = this.promoteVipService.QueryScopeByID(id);
                return this.Json(promoteVip);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 获取操作文本字符串.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="startTime">
        /// The start time.
        /// </param>
        /// <param name="endTime">
        /// The end time.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetVipOperationHtml(int id, int status, DateTime startTime, DateTime endTime)
        {
            var sb = new StringBuilder();
            var permissionUtility = new PermissionUtility(this.SystemUserSession.SessionID);
            if (endTime > DateTime.Now)
            {
                switch (status)
                {
                    case 1:
                        if (startTime > DateTime.Now)
                        {
                            sb.Append(
                                "<input type='button' class='k-button k-grid-edit' name='" + id
                                + "' value='编辑' onclick='promotevip.Edit(this,null)' style='"
                                + permissionUtility.GetDisplayAttribute("modifyvip", "Promote", "POST")["style"]
                                + "'/>");
                        }

                        sb.Append(
                            "<input type='button' class='k-button k-grid-suspend' name='" + id
                            + "'  value='暂停' onclick='promotevip.Suspend(this)' style='"
                            + permissionUtility.GetDisplayAttribute("suspendvip", "Promote", "POST")["style"]
                            + "'/>");
                        sb.Append(
                            "<input type='button' class='k-button k-grid-stop' name='" + id
                            + "'  value='停止' onclick='promotevip.Stop(this)' style='"
                            + permissionUtility.GetDisplayAttribute("stopvip", "Promote", "POST")["style"] + "'/>");
                        break;
                    case 2:
                        if (startTime > DateTime.Now)
                        {
                            sb.Append(
                                "<input type='button' class='k-button k-grid-edit' name='" + id
                                + "' value='编辑' onclick='promotevip.Edit(this,null)' style='"
                                + permissionUtility.GetDisplayAttribute("modifyvip", "Promote", "POST")["style"]
                                + "'/>");
                        }

                        sb.Append(
                            "<input type='button' class='k-button k-grid-suspend' name='" + id
                            + "'  value='恢复' onclick='promotevip.Suspend(this)' style='"
                            + permissionUtility.GetDisplayAttribute("recovervip", "Promote", "POST")["style"]
                            + "'/>");
                        sb.Append(
                            "<input type='button' class='k-button k-grid-stop' name='" + id
                            + "'  value='停止' onclick='promotevip.Stop(this)' style='"
                            + permissionUtility.GetDisplayAttribute("stopvip", "Promote", "POST")["style"] + "'/>");
                        break;
                    case 3:
                       break;
                }
            }

            sb.Append(
                "<input type='button' class='k-button k-grid-detail' name='" + id
                + "' value='查看' onclick='promotevip.Detail(this)' style='"
                + permissionUtility.GetDisplayAttribute("vipDetail", "Promote", "Get")["style"] + "'/>");
            sb.Append(
                "<input type='button' class='k-button k-grid-delete' name='" + id
                + "' value='删除' onclick='promotevip.Delete(this)' style='"
                + permissionUtility.GetDisplayAttribute("Removevip", "Promote", "POST")["style"] + "'/>");

            return sb.ToString();
        }

        /// <summary>
        /// 验证商品参加重复促销.
        /// </summary>
        /// <param name="promoteVipID">
        /// The promote vip id.
        /// </param>
        /// <param name="productIDs">
        /// The product i ds.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string VerifyPromote(int promoteVipID, string productIDs)
        {
            if (string.IsNullOrEmpty(productIDs))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            this.promoteVipService = new PromoteVipService();
            var list = this.promoteVipService.QueryByPromoteProduct(productIDs, promoteVipID);

            if (list == null)
            {
                return string.Empty;
            }

            foreach (var productSearchResult in list)
            {
                sb.Append(productSearchResult.Name + ";");
            }

            return sb.ToString();
        }
    }
}
