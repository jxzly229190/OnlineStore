// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.GroupBuylist.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The channel controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Channel
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Channel;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Channel;
    using V5.Service.Channel;

    /// <summary>
    /// 团购频道部分控制器
    /// </summary>
    public partial class ChannelController
    {
        #region  Public Methods and Operators

        /// <summary>
        /// The group by.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult GroupBuylist()
        {
            return this.PartialView("GroupBuylist");
        }

        /// <summary>
        /// 团购商品
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="groupBuy">
        /// 团购商品对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult AddGroupBuy([DataSourceRequest] DataSourceRequest request, ChannelGroupBuyModel groupBuy)
        {
            try
            {
                if (groupBuy != null)
                {
                    this.channelGroupBuyService = new ChannelGroupBuyService();
                    var channelGroupBuy = DataTransfer.Transfer<Channel_GroupBuy>(groupBuy, typeof(ChannelGroupBuyModel));
                    channelGroupBuy.ID = this.channelGroupBuyService.Insert(channelGroupBuy);
                    if (channelGroupBuy.ID > 0)
                    {
                        return Json(new[] { channelGroupBuy }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.View();
        }

        /// <summary>
        /// 条件查询  //1全部2进行中3停止4未开始
        /// </summary>
        /// <param name="request">查询数据对象</param>
        /// <param name="name">团购名称</param>
        /// <param name="timestart">开始时间</param>
        /// <param name="timestart1">开始时间1</param>
        /// <param name="timeEnd">结束时间</param>
        /// <param name="timeEnd1">结束时间1</param>
        /// <param name="status">状态</param>
        /// <param name="userLevel">用户级别</param>
        /// <param name="showLevel">显示级别</param>
        /// <returns></returns>
        public ActionResult QueryViewGroupBuyProduct(
            [DataSourceRequest] DataSourceRequest request,
            string name,
            string timestart,
            string timestart1,
            string timeEnd,
            string timeEnd1,
            string status,
            string userLevel,
            string showLevel)
        {
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }
            var condition = new StringBuilder();


            if (!string.IsNullOrEmpty(name))
            {
                CheckCondition(condition);
                condition.Append("[ProductName] like '%" + name + "%'");
            }

            if (!string.IsNullOrEmpty(timestart))
            {
                CheckCondition(condition);
                condition.Append("[StartTime] >'" + timestart + "'");
            }
            //if (!string.IsNullOrEmpty(timeEnd))
            //{
            //    CheckCondition(condition);
            //    condition.Append("[EndTime] >'" + timeEnd + "'");
            //}

            if (!string.IsNullOrEmpty(userLevel))
            {
                CheckCondition(condition);
                condition.Append("[LevelID]=" + userLevel + string.Empty);
            }
            if (!string.IsNullOrEmpty(showLevel))
            {
                CheckCondition(condition);
                condition.Append("[ShowLevel]=" + showLevel + string.Empty);
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "1")
                {
                    CheckCondition(condition);
                    condition.Append("Status!=0");
                    return this.ViewPaging(request, condition.ToString());
                }

                if (status == "2")
                {
                    CheckCondition(condition);
                    condition.Append("Status=2");
                    return this.ViewPaging(request, condition.ToString());
                }
                if (status == "3")
                {
                    CheckCondition(condition);
                    condition.Append("Status=3");
                    return this.ViewPaging(request, condition.ToString());
                }
                if (status == "4")
                {
                    CheckCondition(condition);
                    condition.Append("Status=4");
                    return this.ViewPaging(request, condition.ToString());
                }
            }

            return this.ViewPaging(request, condition.ToString());
        }


        public JsonResult ViewPaging([DataSourceRequest] DataSourceRequest request, string condition)
        {
            int totalCount;
            List<View_GroupBuy_Product> list;
            this.channelGroupBuyService = new ChannelGroupBuyService();
            try
            {
                var paging = new Paging("view_GroupBuy_Product", null, "ProductId", condition, request.Page, request.PageSize);
                int pageCount;
                list = this.channelGroupBuyService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            var modelList = new List<ViewGroupBuyProductModel>();
            if (list != null)
            {
                foreach (var viewGroupBuyProduct in list)
                {
                    modelList.Add(DataTransfer.Transfer<ViewGroupBuyProductModel>(viewGroupBuyProduct, typeof(View_GroupBuy_Product)));

                }
            }

            var data = new DataSource
            {
                Data = modelList,
                Total = totalCount
            };
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        public void TransforSuspend(string productId, string statusName)
        {

            if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(statusName))
            {
                //1全部2进行中3停止4未开始5暂停
                var status = 0;
                switch (statusName)
                {
                    case "暂停":
                        status = 4;
                        break;
                    case "恢复":
                        status = 2;
                        break;
                    case "停止":
                        status = 3;
                        break;
                }

                try
                {
                    this.channelGroupBuyService = new ChannelGroupBuyService();
                    var returnValue = channelGroupBuyService.UpdateStatus(int.Parse(productId), status);
                    if (returnValue > 0)
                    {
                        Response.Write(statusName + "成功");
                    }

                    Response.End();
                }
                catch (Exception exception)
                {

                    throw new Exception(exception.Message, exception);
                }
            }
        }

        /// <summary>
        /// 根据ProductID查询商品的团购信息
        /// </summary>
        /// <param name="productId">ProductID</param>
        /// <returns></returns>
        public PartialViewResult QueryChannelGroupByProductId(string productId)
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(productId))
            {
                condition = "ProductId=" + productId + string.Empty;
            }
            try
            {
                int totalCount = -1;
                int totalPage = -1;
                this.channelGroupBuyService = new ChannelGroupBuyService();
                var paging = new Paging("view_GroupBuy_Product", null, "ProudtcId", condition, 1, 1);
                var list = this.channelGroupBuyService.Query(paging, out totalCount, out totalPage);
                var ModelList = new List<ViewGroupBuyProductModel>();
                if (list != null && list.Any())
                {
                    foreach (var viewGroupBuyProduct in list)
                    {
                        ModelList.Add(DataTransfer.Transfer<ViewGroupBuyProductModel>(viewGroupBuyProduct, typeof(View_GroupBuy_Product)));
                    }
                }
                ViewGroupBuyProductModel singleModel = ModelList.FirstOrDefault();
                return this.PartialView("QueryChannelGroupByProductId", singleModel);
            }
            catch (Exception exception)
            {

                throw new ArgumentNullException(exception.Message, exception);
            }

        }

        [HttpPost]
        public void DeleteGrouBuyProductId(string productId)
        {
            int pid = -1;
            if (!string.IsNullOrEmpty(productId))
            {
                int.TryParse(productId, out pid);
            }
            this.channelGroupBuyService = new ChannelGroupBuyService();
            var result = channelGroupBuyService.DeleteGrouBuyProductId(pid);
            if (result > 0)
            {
                Response.Write("删除成功");
            }
        }

        #endregion
    }
}
