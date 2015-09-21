// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.MeetMoney.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销管理部分控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Promote.MeetMoney;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote.MeetMoney;
    using V5.Portal.Backstage.Utils;
    using V5.Service.Promote;

    /// <summary>
    /// 促销管理部分控制器.
    /// </summary>
    public partial class PromoteController
    {
        #region Constants and Fields

        /// <summary>
        /// 满就送促销服务对象.
        /// </summary>
        private PromoteMeetMoneyService promoteMeetMoneyService;

        /// <summary>
        /// 满就送数据访问服务对象.
        /// </summary>
        private PromoteMeetMoneyRuleService promoteMeetMoneyRuleService;

        /// <summary>
        /// 满就送范围商品数据访问服务对象.
        /// </summary>
        private PromoteMeetMoneyScopeService promoteMeetMoneyScopeService;

        #endregion

        #region Public Methods and Operators
        
        /// <summary>
        /// 满就送.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult MeetMoney()
        {
            return this.PartialView("MeetMoney");
        }

        /// <summary>
        /// 满额优惠编辑
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult MeetMoneyEdit()
        {
            return this.PartialView("MeetMoneyEdit");
        }

        /// <summary>
        /// 满额优惠详细页
        /// </summary>
        /// <param name="id">满额优惠编号</param>
        /// <returns>The <see cref="PartialViewResult"/></returns>
        public JsonResult MeetMoneyDetail(int id)
        {
            try
            {
                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                this.promoteMeetMoneyRuleService = new PromoteMeetMoneyRuleService();
                this.promoteMeetMoneyScopeService = new PromoteMeetMoneyScopeService();
                var promoteMeetMoney = this.promoteMeetMoneyService.QueryByID(id);
                var promoteMeetMoneyModel = DataTransfer.Transfer<PromoteMeetMoneyModel>(
                    promoteMeetMoney,
                    typeof(Promote_MeetMoney));
                var list = this.promoteMeetMoneyRuleService.QueryByMeetMoneyID(promoteMeetMoney.ID);
                var listmodel = new List<PromoteMeetMoneyRuleModel>();
                foreach (var promoteMeetMoneyRule in list)
                {
                    listmodel.Add(
                        DataTransfer.Transfer<PromoteMeetMoneyRuleModel>(
                            promoteMeetMoneyRule,
                            typeof(Promote_MeetMoney_Rule)));
                }

                var promoteMeetMoneyScope = this.promoteMeetMoneyScopeService.QueryByMeetMoneyID(promoteMeetMoney.ID);
                var promoteMeetMoneyScopeModel = DataTransfer.Transfer<PromoteMeetMoneyScopeModel>(
                    promoteMeetMoneyScope,
                    typeof(Promote_MeetMoney_Scope));
                promoteMeetMoneyModel.MeetMoneyScope = promoteMeetMoneyScopeModel;
                promoteMeetMoneyModel.PromoteMeetMoneyRuleModelsList = listmodel;
                return this.Json(promoteMeetMoneyModel);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 满额优惠.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="moneyName">
        /// 活动名称.
        /// </param>
        /// <param name="moneyStatus">
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
        public JsonResult QueryMeetMoney(
            [DataSourceRequest] DataSourceRequest request,
            string moneyName,
            string moneyStatus,
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
            if (!string.IsNullOrEmpty(moneyName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[Name] like '%" + moneyName + "%'");
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

            switch (moneyStatus)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[StartTime] > '" + DateTime.Now + "'");
                    break;
                case "2":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("'" + DateTime.Now + "' Between [StartTime] And [EndTime]");
                    break;
                case "3":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("([EndTime] < '" + DateTime.Now + "' or [Status] = 3)");
                    break;
            }

            CheckCondition(stringBuilder);
            stringBuilder.Append("[IsDelete] = 0");
            var condition = stringBuilder.ToString();
            List<Promote_MeetMoney> list;
            try
            {
                var paging = new Paging(
                    "[Promote_MeetMoney]",
                    null,
                    "[ID]",
                    condition,
                    request.Page,
                    request.PageSize,
                    "[CreateTime]",
                    1);
                int pageCount;
                list = this.promoteMeetMoneyService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<PromoteMeetMoneyModel>();
                foreach (var coupon in list)
                {
                    modelList.Add(DataTransfer.Transfer<PromoteMeetMoneyModel>(coupon, typeof(Promote_MeetMoney)));
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

                    model.StatusText = this.GetMoneyOperationHtml(
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
        /// 添加满就送.
        /// </summary>
        /// <param name="promoteMeetMoneyModel">
        /// 满就送活动名称.
        /// </param>
        /// <param name="meetMoneyScopeModel">
        /// 满就送活动范围
        /// </param>
        /// <param name="contents">
        /// 活动规则.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddMeetMoney(
            PromoteMeetMoneyModel promoteMeetMoneyModel,
            PromoteMeetMoneyScopeModel meetMoneyScopeModel,
            List<PromoteMeetMoneyRuleModel> contents)
        {
            try
            {
                // 验证相同商品是否参与其他促销
                var products = this.VerifyPromote(meetMoneyScopeModel.Scope, 0, 0);
                if (!string.IsNullOrEmpty(products))
                {
                    return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                }

                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                promoteMeetMoneyModel.EmployeeID = this.SystemUserSession.EmployeeID;
                promoteMeetMoneyModel.Status = 1;

                var promoteMeetMoney = DataTransfer.Transfer<Promote_MeetMoney>(
                    promoteMeetMoneyModel,
                    typeof(PromoteMeetMoneyModel));

                promoteMeetMoney.MeetMoneyScope = DataTransfer.Transfer<Promote_MeetMoney_Scope>(
                    meetMoneyScopeModel,
                    typeof(PromoteMeetMoneyScopeModel));

                var promoteMeetMoneyRuleList = new List<Promote_MeetMoney_Rule>();
                foreach (var promoteMeetMoneyRuleModel in contents)
                {
                    promoteMeetMoneyRuleList.Add(
                        DataTransfer.Transfer<Promote_MeetMoney_Rule>(
                            promoteMeetMoneyRuleModel,
                            typeof(PromoteMeetMoneyRuleModel)));
                }

                promoteMeetMoney.MeetMoneyRules = promoteMeetMoneyRuleList;
                this.promoteMeetMoneyService.Add(promoteMeetMoney);
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "添加失败！" + exception.Message));
            }

            return this.Json(new AjaxResponse(1, "添加成功！"));
        }

        /// <summary>
        /// 显示修改页面.
        /// </summary>
        /// <param name="meetMoneyID">
        /// The meet Money ID.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult ModifyMeetMoneyShow(string meetMoneyID)
        {
            PromoteMeetMoneyModel promoteMeetMoneyModel;
            try
            {
                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                var promoteMeetMoney = this.promoteMeetMoneyService.QueryByID(int.Parse(meetMoneyID));
                promoteMeetMoneyModel = DataTransfer.Transfer<PromoteMeetMoneyModel>(
                    promoteMeetMoney,
                    typeof(Promote_MeetMoney));
                if (promoteMeetMoney != null)
                {
                    this.promoteMeetMoneyScopeService = new PromoteMeetMoneyScopeService();
                    var meetMoneyScope = this.promoteMeetMoneyScopeService.QueryByMeetMoneyID(int.Parse(meetMoneyID));
                    this.promoteMeetMoneyRuleService = new PromoteMeetMoneyRuleService();
                    var list = this.promoteMeetMoneyRuleService.QueryByMeetMoneyID(promoteMeetMoney.ID);
                    var promoteMeetMoneyList = new List<PromoteMeetMoneyRuleModel>();
                    foreach (var moneyRule in list)
                    {
                        promoteMeetMoneyList.Add(
                            DataTransfer.Transfer<PromoteMeetMoneyRuleModel>(moneyRule, typeof(Promote_MeetMoney_Rule)));
                    }

                    promoteMeetMoneyModel.MeetMoneyScope =
                        DataTransfer.Transfer<PromoteMeetMoneyScopeModel>(
                            meetMoneyScope,
                            typeof(Promote_MeetMoney_Scope));
                    promoteMeetMoneyModel.PromoteMeetMoneyRuleModelsList = promoteMeetMoneyList;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(promoteMeetMoneyModel);
        }

        /// <summary>
        /// 修改满就送.
        /// </summary>
        /// <param name="promoteMeetMoneyModel">
        /// The promote Meet Money Model.
        /// </param>
        /// <param name="meetMoneyScopeModel">
        /// The meet Money Scope Model.
        /// </param>
        /// <param name="contents">
        /// 活动规则.
        /// </param>
        /// <param name="removeRuleId">
        /// 删除的活动规则编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult ModifyMeetMoney(
            PromoteMeetMoneyModel promoteMeetMoneyModel,
            PromoteMeetMoneyScopeModel meetMoneyScopeModel,
            List<PromoteMeetMoneyRuleModel> contents,
            string[] removeRuleId)
        {
            try
            {
                // 验证相同商品是否参与其他促销
                var products = this.VerifyPromote(meetMoneyScopeModel.Scope, 1, promoteMeetMoneyModel.ID);
                if (!string.IsNullOrEmpty(products))
                {
                    return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                }

                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                promoteMeetMoneyModel.EmployeeID = this.SystemUserSession.EmployeeID;
                promoteMeetMoneyModel.Status = 1;

                var promoteMeetMoney = DataTransfer.Transfer<Promote_MeetMoney>(
                    promoteMeetMoneyModel,
                    typeof(PromoteMeetMoneyModel));
                meetMoneyScopeModel.MeetMoneyID = promoteMeetMoneyModel.ID;
                promoteMeetMoney.MeetMoneyScope = DataTransfer.Transfer<Promote_MeetMoney_Scope>(
                    meetMoneyScopeModel,
                    typeof(PromoteMeetMoneyScopeModel));

                var promoteMeetMoneyRuleList = new List<Promote_MeetMoney_Rule>();
                foreach (var promoteMeetMoneyRuleModel in contents)
                {
                    promoteMeetMoneyRuleList.Add(
                        DataTransfer.Transfer<Promote_MeetMoney_Rule>(
                            promoteMeetMoneyRuleModel,
                            typeof(PromoteMeetMoneyRuleModel)));
                }

                promoteMeetMoney.MeetMoneyRules = promoteMeetMoneyRuleList;
                this.promoteMeetMoneyService.Modify(promoteMeetMoney, removeRuleId);
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "设置失败：" + exception.Message));
            }

            return this.Json(new AjaxResponse(1, "设置成功"));
        }

        /// <summary>
        /// 暂停/恢复/停止指定的的促销活动.
        /// </summary>
        /// <param name="meetMoneyID">
        /// 促销活动编号.
        /// </param>
        /// <param name="status">
        /// 活动状态（1：正常,2:暂停,3：停止）.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult ChangesMeetMoneyStatus(string meetMoneyID, string status)
        {
            try
            {
                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                this.promoteMeetMoneyService.ModifyStatus(int.Parse(meetMoneyID), int.Parse(status));
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
        /// <param name="meetMoneyID">
        /// 促销活动编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RemoveMeetMoney(string meetMoneyID)
        {
            try
            {
                this.promoteMeetMoneyService = new PromoteMeetMoneyService();
                this.promoteMeetMoneyService.Remove(int.Parse(meetMoneyID));
                return this.Json(new AjaxResponse(1, "操作成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "操作失败：" + exception.Message));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 根据活动状态和结束时间获取相应的操作按钮.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="startTime">
        /// The start Time.
        /// </param>
        /// <param name="endTime">
        /// The end time.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMoneyOperationHtml(int id, int status, DateTime startTime, DateTime endTime)
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
                                + "' value='编辑' onclick='meetMoney.MeetMoneyEdit(this,null)' style='"
                                + permissionUtility.GetDisplayAttribute("modifymeetmoney", "Promote", "POST")["style"]
                                + "'/>");
                        }

                        sb.Append(
                            "<input type='button' class='k-button k-grid-suspend' name='" + id
                            + "'  value='暂停' onclick='meetMoney.MeetMoneySuspend(this)' style='"
                            + permissionUtility.GetDisplayAttribute("suspendmeetmoney", "Promote", "POST")["style"]
                            + "'/>");
                        sb.Append(
                            "<input type='button' class='k-button k-grid-stop' name='" + id
                            + "'  value='停止' onclick='meetMoney.MeetMoneyStop(this)' style='"
                            + permissionUtility.GetDisplayAttribute("stopmeetmoney", "Promote", "POST")["style"] + "'/>");
                        break;
                    case 2:
                        if (startTime > DateTime.Now)
                        {
                            sb.Append(
                                "<input type='button' class='k-button k-grid-edit' name='" + id
                                + "' value='编辑' onclick='meetMoney.MeetMoneyEdit(this,null)' style='"
                                + permissionUtility.GetDisplayAttribute("modifymeetmoney", "Promote", "POST")["style"]
                                + "'/>");
                        }

                        sb.Append(
                            "<input type='button' class='k-button k-grid-suspend' name='" + id
                            + "'  value='恢复' onclick='meetMoney.MeetMoneySuspend(this)' style='"
                            + permissionUtility.GetDisplayAttribute("recovermeetmoney", "Promote", "POST")["style"]
                            + "'/>");
                        sb.Append(
                            "<input type='button' class='k-button k-grid-stop' name='" + id
                            + "'  value='停止' onclick='meetMoney.MeetMoneyStop(this)' style='"
                            + permissionUtility.GetDisplayAttribute("stopmeetmoney", "Promote", "POST")["style"] + "'/>");
                        break;
                    case 3:
                        sb.Append(
                            "<input type='button' class='k-button k-grid-stop' name='" + id
                            + "'  value='重做' onclick='meetMoney.MeetMoneyEdit(this,1)' style='"
                            + permissionUtility.GetDisplayAttribute("addmeetmoney", "Promote", "POST")["style"] + "'/>");
                        break;
                }
            }
            else
            {
                sb.Append(
                    "<input type='button' class='k-button k-grid-stop' name='" + id
                    + "'  value='重做' onclick='meetMoney.MeetMoneyEdit(this,1)' style='"
                    + permissionUtility.GetDisplayAttribute("addmeetmoney", "Promote", "POST")["style"] + "'/>");
            }

            sb.Append(
                "<input type='button' class='k-button k-grid-detail' name='" + id
                + "' value='查看' onclick='meetMoney.MeetMoneyDetail(this)' style='"
                + permissionUtility.GetDisplayAttribute("MeetMoneyDetail", "Promote", "Get")["style"] + "'/>");
            sb.Append(
                "<input type='button' class='k-button k-grid-delete' name='" + id
                + "' value='删除' onclick='meetMoney.MeetMoneyDelete(this)' style='"
                + permissionUtility.GetDisplayAttribute("RemoveMeetMoney", "Promote", "POST")["style"] + "'/>");

            return sb.ToString();
        }

        /// <summary>
        /// 验证相同商品是否参与其他促销.
        /// </summary>
        /// <param name="scope">
        /// 促销商品 0、1字符串.
        /// </param>
        /// <param name="type">
        /// 类型：（0：添加新的促销活动，1：满额优惠，2：满件优惠）.
        /// </param>
        /// <param name="typeID">
        /// The type ID.
        /// </param>
        /// <returns>
        /// 已参加活动的商品.
        /// </returns>
        private string VerifyPromote(string scope, int type, int typeID)
        {
            // 是否已参加促销活动
            #region 方案一：分组验证 

            /*
            StringBuilder sb = new StringBuilder();
            
            var nowscope = new ArrayList();
            for (var i = 0; i * 50 < scope.Length; i++)
            {
                nowscope.Add(scope.Substring(i * 50, (i + 1) * 50));
            }
            
            // 查询现有促销的商品
            this.promoteMeetMoneyScopeService = new PromoteMeetMoneyScopeService();
            var list = this.promoteMeetMoneyScopeService.QueryAll();

            foreach (var promoteMeetMoneyScope in list)
            {
                var group = new ArrayList();
                for (var i = 0; i * 50 < promoteMeetMoneyScope.Scope.Length; i++)
                {
                    group.Add(promoteMeetMoneyScope.Scope.Substring(i * 50, (i + 1) * 50));
                }

                var count = group.Count < nowscope.Count ? group.Count : nowscope.Count; // 取值小的 
                for (int i = 0; i < count; i++)
                {
                    var j = Convert.ToUInt64(group[i].ToString(), 2) & Convert.ToUInt64(nowscope[i].ToString(), 2);
                    if (j > 0)
                    {

                    }
                }
            }
            */

            #endregion

            #region 方案二：Product添加字段活动结束时间 标识字段

            /*
            // 获取所选商品编号
            var sb = new StringBuilder();
            for (int i = 0; i < scope.Length; i++)
            {
                if (scope.Substring(i, 1) == "1")
                {
                    sb.Append(i + 1 + ",");
                }
            }

            var productStr = sb.ToString(0, sb.Length - 1);

            // 在所选商品中查找已参加促销活动的商品
            this.productService = new ProductService();
            var list = this.productService.QueryPromoteProducts(productStr);
            if (list == null)
            {
                return string.Empty;
            }

            var productstr = new StringBuilder();
            foreach (var product in list)
            {
                productstr.Append(product.Name + ",");
            }
             */

            #endregion

            #region 方案三  
            var productItems = new ArrayList(); // 已参加活动的商品
            var sb = new StringBuilder(); // 选择的商品已参加活动

            // 查询现有满额促销的商品
            this.promoteMeetMoneyScopeService = new PromoteMeetMoneyScopeService();
            var meetMoneyList = this.promoteMeetMoneyScopeService.QueryAll();
            
            // 修改时需去除本身的促销商品
            if (type == 1)
            {
                meetMoneyList.Remove(meetMoneyList.Find(m => m.MeetMoneyID == typeID));
            }

            foreach (var meetMoneyScope in meetMoneyList)
            {
                var products = meetMoneyScope.Scope.ToCharArray();
                for (var i = 0; i < products.Length; i++)
                {
                    if (products[i] == '1')
                    {
                        productItems.Add(i);
                    }
                }
            }

            // 查询现有满件促销的商品
            this.promoteMeetAmountScopeService = new PromoteMeetAmountScopeService();
            var meetAmountList = this.promoteMeetAmountScopeService.QueryAll();

            // 修改时需去除本身的促销商品
            if (type == 2)
            {
                meetAmountList.Remove(meetAmountList.Find(m => m.MeetAmountID == typeID));
            }

            foreach (var meetAmountScope in meetAmountList)
            {
                var products = meetAmountScope.Scope.ToCharArray();
                for (var i = 0; i < products.Length; i++)
                {
                    if (products[i] == '1')
                    {
                        productItems.Add(i);
                    }
                }
            }

            var newProducts = scope.ToCharArray();
            if (newProducts.Length > 0)
            {
                for (var i = 0; i < newProducts.Length; i++)
                {
                    if (newProducts[i] == '1' && productItems.Contains(i))
                    {
                        sb.Append(i + ",");
                    }
                }
            }

            #endregion

            return sb.ToString();
        }

        [HttpPost]
        public ActionResult QueryCounponItems(int pageIndex, int pageSize, string sortField, string sortOrder, string type, string counponName)
        {
            int totalCount, pageCount;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[Status] = 2");

            if (!string.IsNullOrEmpty(counponName))
            {
                stringBuilder.Append(string.Format(" And [Name] like '%{0}%' ", counponName));
            }

            var condition = stringBuilder.ToString();
            var paging = new Paging("view_Coupon_Paging", null, "ID", condition, pageIndex, pageSize, "CreateTime", 1);
            var searchResult = new CouponCashService().Query(paging, out pageCount, out totalCount);
            return this.Json(new { data = searchResult, total = totalCount });
        }
        
        #endregion
    }
}
