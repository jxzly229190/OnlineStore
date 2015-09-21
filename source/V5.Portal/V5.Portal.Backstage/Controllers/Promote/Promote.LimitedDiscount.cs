// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.LimitedDiscount.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销管理部分控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Promote;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote;
    using V5.Service.Promote;

    /// <summary>
    /// 促销管理部分控制器.
    /// </summary>
    public partial class PromoteController
    {
        #region Constants and Fields

        /// <summary>
        /// The promote limited discount service.
        /// </summary>
        private PromoteLimitedDiscountService promoteLimitedDiscountService; 

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 限时打折促销.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult LimitedDiscount()
        {
            return this.PartialView("LimitedDiscount");
        }

        /// <summary>
        /// 限时抢购.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="promoteName">
        /// 限时抢购促销名称.
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
        /// <param name="searchStatus">
        /// The search Status.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryLimitedDiscount(
           [DataSourceRequest] DataSourceRequest request,
           string promoteName,
           string productName,
           string startStartTime,
           string startEndTime,
           string endStartTime,
           string endEndTime,
            string searchStatus)
        {
            this.promoteLimitedDiscountService = new PromoteLimitedDiscountService();
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

            if (!string.IsNullOrEmpty(productName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[ProductName] like '%" + productName + "%'");
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

            switch (searchStatus)
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

            var condition = stringBuilder.ToString();
            List<Promote_Limited_Discount> list;
            try
            {
                var paging = new Paging(
                    "[view_Promote_Limited_Discount]",
                    null,
                    "[ID]",
                    condition,
                    request.Page,
                    request.PageSize,
                    "[CreateTime]",
                    1);
                int pageCount;
                list = this.promoteLimitedDiscountService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<PromoteLimitedDiscountModel>();
                foreach (var limitedDiscount in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<PromoteLimitedDiscountModel>(limitedDiscount, typeof(Promote_Limited_Discount)));
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 添加限时促销活动.
        /// </summary>
        /// <param name="promoteName">
        /// 促销活动名称.
        /// </param>
        /// <param name="promoteStartTime">
        /// 活动开始时间.
        /// </param>
        /// <param name="promoteEndTime">
        /// 活动结束时间.
        /// </param>
        /// <param name="productArry">
        /// 商品编号集合.
        /// </param>
        /// <param name="discountArry">
        /// 商品打折数集合.
        /// </param>
        /// <param name="discountPriceArry">
        /// 商品折后价集合.
        /// </param>
        /// <param name="limitedBuyNumArry">
        /// 每人限购数集合.
        /// </param>
        /// <param name="totalNumArry">
        /// 活动商品总数集合.
        /// </param>
        /// <param name="isOnlinePayArry">
        /// 是否仅支持在线支付集合.
        /// </param>
        /// <param name="useCouponArry">
        /// 能否使用优惠券集合.
        /// </param>
        /// <param name="newUserArry">
        /// 是否限制为新会员集合.
        /// </param>
        /// <param name="mobileverifyArry">
        /// 是否需要手机验证集合.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddLimitedDiscount(
            string promoteName,
            string promoteStartTime,
            string promoteEndTime,
            string productArry,
            string discountArry,
            string discountPriceArry,
            string limitedBuyNumArry,
            string totalNumArry,
            string isOnlinePayArry,
            string useCouponArry,
            string newUserArry,
            string mobileverifyArry)
        {
            try
            {
                // 验证相同商品是否参与其他促销
                var products = this.VerifyPromote(productArry);
                if (!string.IsNullOrEmpty(products))
                {
                    return this.Json(new AjaxResponse(0, "以下商品已参加其他促销活动：" + products));
                }

                var promoteLimitedDiscount = new Promote_Limited_Discount
                                                 {
                                                     Name = promoteName,
                                                     StartTime = DateTime.Parse(promoteStartTime),
                                                     EndTime = DateTime.Parse(promoteEndTime),
                                                     CreateTime = DateTime.Now,
                                                     IsNewUser = false,
                                                     IsMobileValidate = false,
                                                     IsUseCoupon = true
                                                 };
                var productIDArry = productArry.Split(',');
                var discountNumArry = discountArry.Split(',');
                var discountedArry = discountPriceArry.Split(',');
                var limitedBuyArry = limitedBuyNumArry.Split(',');
                var totalArry = totalNumArry.Split(',');
                var isOnlineArry = isOnlinePayArry.Split(',');

                for (int i = 0; i < productIDArry.Length; i++)
                {
                    if (productIDArry[i].Trim() != string.Empty)
                    {
                        promoteLimitedDiscount.ProductID = int.Parse(productIDArry[i]);
                        promoteLimitedDiscount.Discount = double.Parse(discountNumArry[i]);
                        promoteLimitedDiscount.DiscountPrice = double.Parse(discountedArry[i]);
                        promoteLimitedDiscount.LimitedBuyQuantity = int.Parse(limitedBuyArry[i]);
                        promoteLimitedDiscount.TotalQuantity = int.Parse(totalArry[i]);
                        promoteLimitedDiscount.IsOnlinePayment = isOnlineArry[i] == "1";
                        promoteLimitedDiscount.Status = 1;

                        this.promoteLimitedDiscountService = new PromoteLimitedDiscountService();
                        promoteLimitedDiscount.ID = this.promoteLimitedDiscountService.Add(promoteLimitedDiscount);
                    }
                }

                return this.Json(new AjaxResponse(1, "设置成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 修改促销活动信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="promoteLimitedDiscountModel">
        /// PromoteLimitedDiscountModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyLimitedDiscount([DataSourceRequest] DataSourceRequest request, PromoteLimitedDiscountModel promoteLimitedDiscountModel)
        {
            try
            {
                if (promoteLimitedDiscountModel != null)
                {
                    this.promoteLimitedDiscountService = new PromoteLimitedDiscountService();

                    var promoteLimitedDiscount =
                        DataTransfer.Transfer<Promote_Limited_Discount>(
                            promoteLimitedDiscountModel,
                            typeof(PromoteLimitedDiscountModel));

                    this.promoteLimitedDiscountService.Modify(promoteLimitedDiscount);
                }

                return this.Json(new[] { promoteLimitedDiscountModel }.ToDataSourceResult(request, this.ModelState));
            }
            catch
            {
                return this.Json(string.Empty);
            }
        }

        #endregion

        #region

        /// <summary>
        /// 验证相同商品是否参与其他促销.
        /// </summary>
        /// <param name="productIDs">
        /// The product I Ds.
        /// </param>
        /// <returns>
        /// 已参加活动的商品.
        /// </returns>
        private string VerifyPromote(string productIDs)
        {
            if (string.IsNullOrEmpty(productIDs))
            {
                return "";
            }

            var sb = new StringBuilder();
            this.promoteLimitedDiscountService = new PromoteLimitedDiscountService();
            var list = this.promoteLimitedDiscountService.QueryByPromoteProduct(productIDs);
            if (list != null)
            {
                foreach (var productSearchResult in list)
                {
                    sb.Append(productSearchResult.Name + ";");
                }

                return sb.ToString();
            }

            return "";
        }

        #endregion
    }
}
