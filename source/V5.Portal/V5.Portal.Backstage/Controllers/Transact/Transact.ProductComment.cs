using System.Web.Mvc;

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System.Collections.Generic;
    using global::System.Text;    
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using V5.DataContract.Transact;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Transact;
    using V5.Service.Transact;

    public partial class TransactController
    {
        public PartialViewResult ProductComment()
        {
            return this.PartialView("ProductComment");
        }

        #region 商品评论操作方法

        public ActionResult QueryProductCommentWithPaging([DataSourceRequest] DataSourceRequest request, string statusForSearch, string userName, string productName, string fromDateTime, string toDateTime)
        {
            var service = new ProductCommentService();
            int rowCount, pageCount;
            string searchStr = this.BuildSearchString(statusForSearch, userName, productName, fromDateTime, toDateTime);

            var paging = new Paging(searchStr, request.Page, request.PageSize);
            var list = service.QueryWithPaging(paging, out pageCount, out rowCount);

            if (list == null)
            {
                return this.Json(null);
            }

            var modelList = new List<ProductCommentModel>();

            foreach (var comment in list)
            {
                var cmt = DataTransfer.Transfer<ProductCommentModel>(comment, typeof(Product_Comment));
                modelList.Add(cmt);
            }

            var dataSource = new DataSource() { Data = modelList, Total = rowCount };

            return this.Json(dataSource, JsonRequestBehavior.AllowGet);
        }

        private string BuildSearchString(string statusForSearch, string userName, string productName, string fromDateTime, string toDateTime)
        {
            var searchStringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And UserName like '%" + userName + "%'"
                        : " UserName like '%" + userName + "%'");
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And ProductName like '%" + productName + "%'"
                        : " ProductName like '%" + productName + "%'");
            }

            if (string.IsNullOrWhiteSpace(statusForSearch))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0 ? " And Status=" + 1 : " Status=" + 1); // 默认设置为未审核状态
            }
            else if (statusForSearch != "0")
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0 ? " And Status=" + statusForSearch : " Status=" + statusForSearch);
            }

            if (!string.IsNullOrWhiteSpace(fromDateTime))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And CreateTime >= '" + fromDateTime + "'"
                        : " CreateTime >= '" + fromDateTime + "'");
            }

            if (!string.IsNullOrWhiteSpace(toDateTime))
            {
                searchStringBuilder.Append(
                    searchStringBuilder.Length > 0
                        ? " And CreateTime <= '" + toDateTime + "'"
                        : " CreateTime <= '" + toDateTime + "'");
            }

            return searchStringBuilder.ToString();
        }

        public ActionResult GetCommentStatusItems()
        {
            List<SelectListItem> list = new List<SelectListItem>()
                                            {
                                                new SelectListItem() { Text = "通过", Value = "2" },
                                                new SelectListItem() { Text = "锁定", Value = "3" }
                                            };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommentSearchStatusItems()
        {
            List<SelectListItem> list = new List<SelectListItem>
                                            {
                                                new SelectListItem() { Text = "全部", Value = "0" },
                                                new SelectListItem() { Text = "未审核", Value = "1" },
                                                new SelectListItem() { Text = "已通过", Value = "2" },
                                                new SelectListItem() { Text = "已锁定", Value = "3" }
                                            };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModifyProductCommentStatus([DataSourceRequest] DataSourceRequest request, ProductCommentModel model)
        {
            var service = new ProductCommentService();

            service.ModifytCommentStatus(DataTransfer.Transfer<Product_Comment>(model, typeof(ProductCommentModel)));

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult RemoveProductComment([DataSourceRequest] DataSourceRequest request, int id)
        {
            var service = new ProductCommentService();
            service.Remove(id);

            return Json(null);
        } 

        #endregion

        #region 回复评论操作方法

        public ActionResult QueryByProductCommentID([DataSourceRequest] DataSourceRequest request, int pcId)
        {
            var service = new ProductCommentReplyService();
            var list = service.QueryByCommentID(pcId);

            if (list != null)
            {
                var modelList = new List<ProductCommentReplyModel>();

                foreach (var comment in list)
                {
                    var cmt = DataTransfer.Transfer<ProductCommentReplyModel>(comment, typeof(Product_Comment_Reply));
                    modelList.Add(cmt);
                }

                var dataSource = new DataSource() { Data = modelList };

                return Json(dataSource, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        public ActionResult RemoveProductCommentReply([DataSourceRequest] DataSourceRequest request, int id)
        {
            var service = new ProductCommentReplyService();
            service.Remove(id);

            return Json(null);
        } 

        #endregion
    }
}
