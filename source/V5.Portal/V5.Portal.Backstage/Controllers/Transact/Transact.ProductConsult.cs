// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transact.ProductConsult.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using V5.DataContract.Transact;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Transact;
    using V5.Service.Transact;

    /// <summary>
    /// The transact controller.
    /// </summary>
    public partial class TransactController
    {
        public PartialViewResult ProductConsult()
        {
            return this.PartialView("ProductConsult");
        }

        public ActionResult QueryConsultWithoutReply([DataSourceRequest] DataSourceRequest request, ProductConsultSearchModel searchModel, string fromDateTime, string toDateTime)
        {
            var service =new ProductConsultService();
            string searchStr = this.BuildConsultSearchString(searchModel, fromDateTime, toDateTime);
            if (string.IsNullOrEmpty(searchStr))
            {
                searchStr = "replyid is null";
            }
            else
            {
                searchStr += " And replyid is null";
            }

            int rowCount = 0, pageCount = 0;
            var paging = new Paging(searchStr, request.Page, request.PageSize);
            var list = service.QueryConsult(paging, out pageCount, out rowCount);

            if (list != null)
            {
                var modelList = new List<ProductConsultModel>();

                foreach (var consult in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductConsultModel>(consult, typeof(Product_Consult)));
                }

                var dataSource = new DataSource() { Data = modelList, Total = rowCount, TotalPages = pageCount };

                return Json(dataSource, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        public ActionResult QueryConsultReplies([DataSourceRequest] DataSourceRequest request, ProductConsultSearchModel searchModel, string fromDateTime, string toDateTime)
        {
            var service = new ProductConsultService();
            string searchStr = this.BuildConsultSearchString(searchModel, fromDateTime, toDateTime);
            int rowCount = 0, pageCount  = 0;
            var paging = new Paging(searchStr, request.Page, request.PageSize);
            var list = service.QueryConsultReplies(paging, out pageCount, out rowCount);

            if (list != null)
            {
                var modelList = new List<ProductConsultModel>();

                foreach (var consult in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductConsultModel>(consult, typeof(Product_Consult)));
                }

                var dataSource = new DataSource() { Data = modelList, Total = rowCount, TotalPages = pageCount };

                return Json(dataSource, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        private string BuildConsultSearchString(ProductConsultSearchModel model, string fromDateTime, string toDateTime)
        {
            StringBuilder sb=new StringBuilder();
            const string selectStr = " productID in (Select ID ProductID from Product where ";

            #region 如果用户选了类别
            
            if (model.ParentCategoryID > 0 && model.CategoryID > 0)
            {
                // 若选择了二级分类
                if (model.BrandId > 0)
                {
                    sb.Append("ProductBrandID in (").Append(model.BrandId).Append(")");
                }
                else
                {
                    sb.Append("ProductCategoryID in (").Append(model.CategoryID).Append(")");
                }
            }
            else if (model.ParentCategoryID > 0 && model.CategoryID <= 0)
            {
                // 若没有选择二级分类
                var service = new ProductConsultService();
                var list = service.QuerySubCategoriesByParentId(model.ParentCategoryID);
                sb.Append("ProductCategoryID in (");
                if (list != null && list.Count > 0)
                {
                    // 有一级分类和二级分类
                    foreach (var category in list)
                    {
                        sb.Append(category.ID).Append(",");
                    }

                    sb.Remove(sb.Length - 1, 1); // 去除最后一个","
                }
                else
                {
                    // 只有一级分类，没有二级分类，则将0作为子分类的ID号
                    sb.Append("0");
                }

                sb.Append(")"); // 添加”)“结束
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, selectStr).Append(")"); // 加上头部和尾部
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                sb.Append(
                    sb.Length > 0
                        ? " And ProductName like '%" + model.ProductName + "%'"
                        : " ProductName like '%" + model.ProductName + "%'");
            }

            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                sb.Append(
                    sb.Length > 0
                        ? " And UserName like '%" + model.UserName + "%'"
                        : " UserName like '%" + model.UserName + "%'");
            }

            if (!string.IsNullOrWhiteSpace(fromDateTime))
            {
                sb.Append(
                    sb.Length > 0
                        ? " And ConsultTime >= '" + fromDateTime + "'"
                        : " ConsultTime >= '" + fromDateTime + "'");
            }

            if (!string.IsNullOrWhiteSpace(toDateTime))
            {
                sb.Append(
                    sb.Length > 0
                        ? " And ConsultTime <= '" + toDateTime + "'"
                        : " ConsultTime <= '" + toDateTime + "'");
            }

            return sb.ToString();
        }

        public ActionResult ReplyProductConsult([DataSourceRequest] DataSourceRequest request, ProductConsultModel model)
        {
            var service = new ProductConsultService();
            service.ReplyConsult(DataTransfer.Transfer<Product_Consult>(model, typeof(ProductConsultModel)));

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ModifyConsultReply([DataSourceRequest] DataSourceRequest request, ProductConsultModel model)
        {
            var service = new ProductConsultService();
            service.ModifyConsultReply(DataTransfer.Transfer<Product_Consult>(model, typeof(ProductConsultModel)));

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult RemoveProductConsultReply([DataSourceRequest] DataSourceRequest request, ProductConsultModel model)
        {
            var service = new ProductConsultService();
            service.RemoveConsultReply(model.ID);
            return Json(null);
        }

        public ActionResult RemoveProductConsult([DataSourceRequest] DataSourceRequest request, ProductConsultModel model)
        {
            var service = new ProductConsultService();
            service.RemoveConsult(model.ID);
            return Json(null);
        }

        public ActionResult GetParentProductCategories()
        {
            var service = new ProductConsultService();
            var listCategories = service.QueryParentProductCategories();
            return Json(listCategories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductCategoriesByParentId(string ParentCategoryID)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var service = new ProductConsultService();
            var listCategories = service.QuerySubCategoriesByParentId(Convert.ToInt32(ParentCategoryID));

            if (listCategories == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            foreach (var productCategory in listCategories)
            {
                var item = new SelectListItem()
                               {
                                   Text = productCategory.CategoryName,
                                   Value = productCategory.ID.ToString()
                               };
                items.Add(item);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductBrandByCategoryId(string CategoryId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var service = new ProductConsultService();
            var listCategories = service.QueryProductBrandByCategoryId(Convert.ToInt32(CategoryId));
            if (listCategories == null) return Json(null,JsonRequestBehavior.AllowGet);
            foreach (var productCategory in listCategories)
            {
                var item = new SelectListItem()
                               {
                                   Text = productCategory.BrandName,
                                   Value = productCategory.ID.ToString()
                               };
                items.Add(item);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}