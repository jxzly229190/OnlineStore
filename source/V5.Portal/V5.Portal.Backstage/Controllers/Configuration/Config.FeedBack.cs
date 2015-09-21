using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using NPOI.SS.Formula.Functions;
using V5.DataContract.User;
using V5.Library;
using V5.Library.Logger;
using V5.Library.Storage.DB;
using V5.Portal.Backstage.Models.User;
using V5.Service.User;

namespace V5.Portal.Backstage.Controllers.Configuration
{
    public partial class ConfigController
    {
        #region 信息反馈服务对象
        /// <summary>
        /// 反馈信息
        /// </summary>
        private FeedBackService feedBackService;
        #endregion

        public PartialViewResult FeedBack()
        {
            return PartialView("FeedBack");
        }
        /// <summary>
        /// 查询用户反馈信息
        /// </summary>
        /// <param name="request">分页对象</param>
        /// <param name="gender">性别</param>
        /// <param name="type">类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>

        public ActionResult GetFeedBack([DataSourceRequest] DataSourceRequest request, int gender, int type, string keyword)
        {
            if (request.Page <= 0)
            {
                request.Page = 1;
            }
            if (request.PageSize == 0)
            {
                request.Page = 10;
            }
            var criteria = "ID>0";
            if (!string.IsNullOrEmpty(keyword))
            {

                criteria += " and ";

                criteria += "[Name] like '%" + keyword + "%' or [Content] like '%" + keyword + "%'";
            }
            if (gender != -1)
            {
                criteria += " and ";

                criteria += "[Gender]=" + gender + "";

            }
            if (type != 0)
            {

                criteria += " and ";

                criteria += "[Type]=" + type + "";
            }

            try
            {
                int pageCount;
                int totalCount;
                var paging = new Paging("[FeedBack]", null, "ID", criteria, request.Page, request.PageSize);
                this.feedBackService = new FeedBackService();
                var pagingFeedBack = this.feedBackService.Paging(paging, out pageCount, out totalCount);
                var modelList = new List<FeedBackModel>();
                if (pagingFeedBack != null && pagingFeedBack.Any())
                {
                    foreach (var feedBack in pagingFeedBack)
                    {
                        modelList.Add(DataTransfer.Transfer<FeedBackModel>(feedBack, typeof(FeedBack)));
                    }
                    var data = new DataSource()
                    {
                        Data = modelList,
                        Total = totalCount
                    };
                    return this.Json(data);
                }
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("查询出错", Category.Error, exception);
            }
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LookFeedBack(int id)
        {
            this.feedBackService = new FeedBackService();
            var model = this.feedBackService.QueryFeedBackByID(id);
            if (model != null)
            {
                var feedModel = new FeedBackModel
                {
                    ID = model.ID,
                    Name = model.Name,
                    Email = model.Email,
                    Content = model.Content,
                    Gender = model.Gender,
                    GjwNumber = model.GjwNumber,
                    ImgUrl = model.ImgUrl,
                    TelPhone = model.TelPhone,
                    Type = model.Type
                };
                return this.PartialView("LookFeedBack", feedModel);
            }
            return this.PartialView("LookFeedBack");
        }
    }
}