// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.MuchBottled.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销管理控制类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.IO;
    using global::System.Text;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Promote;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote;
    using V5.Service.Promote;

    /// <summary>
    /// 促销管理控制类.
    /// </summary>
    public partial class PromoteController
    {
        #region Constants and Fields

        /// <summary>
        /// 多瓶装促销服务对象.
        /// </summary>
        private PromoteMuchBottledService promoteMuchBottledService;

        /// <summary>
        /// 多瓶装促销规则服务对象.
        /// </summary>
        private PromoteMuchBottledRuleService promoteMuchBottledRuleService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 多瓶装促销局部试图.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult MuchBottled()
        {
            return this.PartialView("MuchBottled");
        }

        /// <summary>
        /// 多瓶装促销.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="promoteName">
        /// 多瓶装促销名称.
        /// </param>
        /// <param name="startStartTime">
        /// 活动开始时间范围起始时间.
        /// </param>
        /// <param name="startEndTime">
        /// 活动开始时间范围结束时间.
        /// </param>
        /// <param name="endStartTime">
        /// 活动结束时间范围起始时间.
        /// </param>
        /// <param name="endEndTime">
        /// 活动结束时间范围结束时间.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryMuchBottled(
            [DataSourceRequest] DataSourceRequest request,
            string promoteName,
            string startStartTime,
            string startEndTime,
            string endStartTime,
            string endEndTime)
        {
            this.promoteMuchBottledService = new PromoteMuchBottledService();
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

            var condition = stringBuilder.ToString();
            List<Promote_MuchBottled> list;
            try
            {
                var paging = new Paging("[view_Promote_MuchBottled]", null, "[ID]", condition, request.Page, request.PageSize);
                int pageCount;
                list = this.promoteMuchBottledService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            
            if (list != null)
            {
                var modelList = new List<PromoteMuchBottledModel>();
                foreach (var muchBottled in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<PromoteMuchBottledModel>(muchBottled, typeof(Promote_MuchBottled)));
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 多瓶装促销规则.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="muchBottledID">
        /// 多瓶装促销活动编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryMuchBottledRule([DataSourceRequest] DataSourceRequest request, int muchBottledID)
        {
            this.promoteMuchBottledRuleService = new PromoteMuchBottledRuleService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            int totalCount;
            List<Promote_MuchBottled_Rule> list;
            try
            {
                string condition = "[MuchBottledID]=" + muchBottledID;
                int pageCount;
                var paging = new Paging("[Promote_MuchBottled_Rule]", null, "[ID]", condition, request.Page, request.PageSize);
                list = this.promoteMuchBottledRuleService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<PromoteMuchBottledRuleModel>();
                foreach (var muchBottledRule in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<PromoteMuchBottledRuleModel>(muchBottledRule, typeof(Promote_MuchBottled_Rule)));
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.View();
        }

        /// <summary>
        /// 添加新的多瓶装促销活动信息.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="gjwPrice">
        /// 商品购酒网价格.
        /// </param>
        /// <param name="promoteName">
        /// 多瓶装促销活动的名称.
        /// </param>
        /// <param name="isOnlinePayment">
        /// 活动是否仅支持在线支付.
        /// </param>
        /// <param name="startTime">
        /// 活动开始时间.
        /// </param>
        /// <param name="endTime">
        /// 活动结束时间.
        /// </param>
        /// <param name="isDisplayTime">
        /// 是否显示时间.
        /// </param>
        /// <param name="property">
        /// 活动的规则名称列表.
        /// </param>
        /// <param name="number">
        /// 活动的规则数量列表.
        /// </param>
        /// <param name="price">
        /// 活动规则促销价格列表.
        /// </param>
        /// <param name="imgstr">
        /// 缩略图列表.
        /// </param>
        /// <param name="isDefault">
        /// 是否默认列表
        /// </param>
        [HttpPost]
        public void AddMuchBottled(
            string productID,
            string gjwPrice,
            string promoteName,
            bool isOnlinePayment,
            string startTime,
            string endTime,
            bool isDisplayTime,
            string property,
            string number,
            string price,
            string imgstr,
            string isDefault)
        {
            try
            {
                this.promoteMuchBottledService = new PromoteMuchBottledService();
                var isExists = this.promoteMuchBottledService.QueryByProductID(int.Parse(productID));
                if (isExists)
                {
                    Response.Write("该商品已参加多瓶装促销！");
                    return;
                }

                var promoteMuchBottled = new Promote_MuchBottled
                                             {
                                                 EmployeeID = this.SystemUserSession.EmployeeID,
                                                 ProductID = int.Parse(productID),
                                                 Name = promoteName,
                                                 IsOnlinePayment = isOnlinePayment,
                                                 StartTime = DateTime.Parse(startTime),
                                                 EndTime = DateTime.Parse(endTime),
                                                 IsDisplayTime = isDisplayTime,
                                                 CreateTime = DateTime.Now
                                             };

                promoteMuchBottled.ID = this.promoteMuchBottledService.Add(promoteMuchBottled);

                this.AddMuchBottledRule(
                    promoteMuchBottled.ID,
                    double.Parse(gjwPrice),
                    property,
                    number,
                    price,
                    imgstr,
                    isDefault);
                
                Response.Write("设置成功！");
            }
            catch (Exception exception)
            {
                Response.Write("设置失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加多瓶装促销的规则.
        /// </summary>
        /// <param name="muchBottledID">
        /// 多瓶装活动的编号.
        /// </param>
        /// <param name="goujiuPrice">
        /// 商品购酒网价格.
        /// </param>
        /// <param name="property">
        /// 活动的规则名称列表.
        /// </param>
        /// <param name="number">
        /// 活动的规则数量列表.
        /// </param>
        /// <param name="price">
        /// 活动规则促销价格列表.
        /// </param>
        /// <param name="imgstr">
        /// 缩略图列表.
        /// </param>
        /// <param name="isDefault">
        /// 是否默认列表
        /// </param>
        public void AddMuchBottledRule(
            int muchBottledID,
            double goujiuPrice,
            string property,
            string number,
            string price,
            string imgstr,
            string isDefault)
        {
            var ruleName = property.Split(',');
            var ruleNum = number.Split(',');
            var rulePrice = price.Split(',');
            var ruleisDefault = isDefault.Split(',');
            var ruleImg = imgstr.Split(',');
            var promoteMuchBottledRule = new Promote_MuchBottled_Rule { MuchBottledID = muchBottledID };
            for (int i = 0; i < ruleName.Length; i++)
            {
                if (ruleName[i].Trim() != string.Empty)
                {
                    promoteMuchBottledRule.Name = ruleName[i].Trim();
                    promoteMuchBottledRule.Quantity = int.Parse(ruleNum[i].Trim());
                    promoteMuchBottledRule.UnitPrice = double.Parse(rulePrice[i].Trim());
                    promoteMuchBottledRule.TotalMoney = promoteMuchBottledRule.UnitPrice
                                                        * promoteMuchBottledRule.Quantity;
                    promoteMuchBottledRule.DiscountAmount = (goujiuPrice - int.Parse(rulePrice[i].Trim()))
                                                            * int.Parse(ruleNum[i].Trim());
                    promoteMuchBottledRule.IsDefault = ruleisDefault[i] == "1";
                    promoteMuchBottledRule.ImageUrl = "Images/pro/" + ruleImg[i];

                    this.promoteMuchBottledRuleService = new PromoteMuchBottledRuleService();
                    promoteMuchBottledRule.ID = this.promoteMuchBottledRuleService.Add(promoteMuchBottledRule);
                }
            }
        }
        
        /// <summary>
        /// 多瓶装促销修改页面.
        /// </summary>
        /// <param name="muchBottledID">
        /// 促销活动编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult GetMuchBottledInfoByID(string muchBottledID)
        {
            if (string.IsNullOrEmpty(muchBottledID))
            {
                return this.Json(null);
            }

            try
            {
                this.promoteMuchBottledService = new PromoteMuchBottledService();
                var promoteMuchBottled = this.promoteMuchBottledService.QueryByID(int.Parse(muchBottledID));
                var promoteMuchBottledModel = DataTransfer.Transfer<PromoteMuchBottledModel>(
                    promoteMuchBottled,
                    typeof(Promote_MuchBottled));

                this.promoteMuchBottledRuleService = new PromoteMuchBottledRuleService();
                var promoteMuchBottledRuleList =
                    this.promoteMuchBottledRuleService.QueryByMuchBottledID(int.Parse(muchBottledID));

                if (promoteMuchBottledRuleList != null)
                {
                    var modelList = new List<PromoteMuchBottledRuleModel>();
                    foreach (var muchBottledRule in promoteMuchBottledRuleList)
                    {
                        modelList.Add(
                            DataTransfer.Transfer<PromoteMuchBottledRuleModel>(
                                muchBottledRule,
                                typeof(Promote_MuchBottled_Rule)));
                    }

                    promoteMuchBottledModel.PromoteMuchBottledRuleModels = modelList;
                }

                return this.Json(promoteMuchBottledModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改多瓶装促销的详细信息.
        /// </summary>
        /// <param name="muchBottledID">
        /// 多瓶装促销活动的编号.
        /// </param>
        /// <param name="isOnlinePayment">
        /// 是否仅支持在线支付.
        /// </param>
        /// <param name="endTime">
        /// 活动结束时间.
        /// </param>
        /// <param name="isDisplayTime">
        /// 是否显示时间.
        /// </param>
        /// <param name="goujiuPrice">
        /// 商品购酒网价格.
        /// </param>
        /// <param name="property">
        /// 活动的规则名称列表.
        /// </param>
        /// <param name="number">
        /// 活动的规则数量列表.
        /// </param>
        /// <param name="price">
        /// 活动规则促销价格列表.
        /// </param>
        /// <param name="imgstr">
        /// 缩略图列表.
        /// </param>
        /// <param name="isDefault">
        /// 是否默认列表
        /// </param>
        [HttpPost]
        public void ModifyMuchBottled(
            string muchBottledID,
            bool isOnlinePayment,
            string endTime,
            bool isDisplayTime,
            double goujiuPrice,
            string property,
            string number,
            string price,
            string imgstr,
            string isDefault)
        {
            try
            {
                var promoteMuchbottled = new Promote_MuchBottled
                {
                    ID = int.Parse(muchBottledID),
                    EmployeeID = this.SystemUserSession.EmployeeID,
                    IsOnlinePayment = isOnlinePayment,
                    EndTime = DateTime.Parse(endTime),
                    IsDisplayTime = isDisplayTime
                };
                this.promoteMuchBottledService = new PromoteMuchBottledService();
                this.promoteMuchBottledService.Update(promoteMuchbottled);

                var ruleName = property.Split(',');
                var ruleNum = number.Split(',');
                var rulePrice = price.Split(',');
                var ruleisDefault = isDefault.Split(',');
                var ruleImg = imgstr.Split(',');

                this.promoteMuchBottledRuleService = new PromoteMuchBottledRuleService();
                var promoteMuchBottledRuleList =
                    this.promoteMuchBottledRuleService.QueryByMuchBottledID(int.Parse(muchBottledID));

                var promoteMuchBottledRule = new Promote_MuchBottled_Rule();
                for (int i = 0; i < promoteMuchBottledRuleList.Count; i++)
                {
                    if (ruleName[i].Trim() != string.Empty)
                    {
                        promoteMuchBottledRule.ID = promoteMuchBottledRuleList[i].ID;
                        promoteMuchBottledRule.Name = ruleName[i].Trim();
                        promoteMuchBottledRule.Quantity = int.Parse(ruleNum[i].Trim());
                        promoteMuchBottledRule.UnitPrice = double.Parse(rulePrice[i].Trim());
                        promoteMuchBottledRule.TotalMoney = promoteMuchBottledRule.UnitPrice
                                                            * promoteMuchBottledRule.Quantity;
                        promoteMuchBottledRule.DiscountAmount = (goujiuPrice - int.Parse(rulePrice[i].Trim()))
                                                                * int.Parse(ruleNum[i].Trim());
                        promoteMuchBottledRule.IsDefault = ruleisDefault[i] == "1";
                        promoteMuchBottledRule.ImageUrl = ruleImg[i];

                        this.promoteMuchBottledRuleService.Modify(promoteMuchBottledRule);
                    }
                }

                if (promoteMuchBottledRuleList.Count < ruleName.Length)
                {
                    for (int i = promoteMuchBottledRuleList.Count; i < ruleName.Length; i++)
                    {
                        if (ruleName[i].Trim() != string.Empty)
                        {
                            promoteMuchBottledRule.Name = ruleName[i].Trim();
                            promoteMuchBottledRule.Quantity = int.Parse(ruleNum[i].Trim());
                            promoteMuchBottledRule.UnitPrice = double.Parse(rulePrice[i].Trim());
                            promoteMuchBottledRule.TotalMoney = promoteMuchBottledRule.UnitPrice
                                                                * promoteMuchBottledRule.Quantity;
                            promoteMuchBottledRule.DiscountAmount = (goujiuPrice - int.Parse(rulePrice[i].Trim()))
                                                                    * int.Parse(ruleNum[i].Trim());
                            promoteMuchBottledRule.IsDefault = ruleisDefault[i] == "1";
                            promoteMuchBottledRule.ImageUrl = ruleImg[i];

                            promoteMuchBottledRule.ID = this.promoteMuchBottledRuleService.Add(promoteMuchBottledRule);
                        }
                    }
                } // 如果修改的数量大于已存在的数量则添加新的规则 
            }
            catch (Exception exception)
            {
                Response.Write("修改失败！");
                throw new Exception(exception.Message, exception);
            }
            
            Response.Write("修改成功！");
        }

        #endregion

        #region 上传图片

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ContentResult SavePicture(HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    var fileName = Path.GetFileName(img.FileName);
                    if (fileName != null)
                    {
                        var physicalPath = Path.Combine(this.Server.MapPath("~/Images/pro"), fileName);
                        img.SaveAs(physicalPath);
                    }
                }

                return Content(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="fileNames">
        /// The file names.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ContentResult RemovePicture(string fileNames)
        {
            try
            {
                if (fileNames != null)
                {
                    var fileName = Path.GetFileName(fileNames);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/pro"), fileName);

                    if (global::System.IO.File.Exists(physicalPath))
                    {
                        global::System.IO.File.Delete(physicalPath);
                    }
                }

                return Content(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check condition.
        /// </summary>
        /// <param name="stringBuilder">
        /// The string builder.
        /// </param>
        private static void CheckCondition(StringBuilder stringBuilder)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" And ");
            }
        }

        #endregion
    }
}
