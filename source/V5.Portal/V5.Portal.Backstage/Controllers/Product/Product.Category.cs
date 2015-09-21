// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.Category.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类别控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Product;

    /// <summary>
    /// 商品类别控制器部分类
    /// </summary>
    public partial class ProductController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The category.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Category([DataSourceRequest] DataSourceRequest request)
        {
            return this.PartialView("Category");
        }

        /// <summary>
        /// The add category.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productCategoryModel">
        /// The product category model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult AddCategory([DataSourceRequest] DataSourceRequest request, ProductCategoryModel productCategoryModel)
        {
            try
            {
                if (productCategoryModel != null)
                {
                    var productCategory = DataTransfer.Transfer<Product_Category>(
                        productCategoryModel,
                        typeof(ProductCategoryModel));

                    productCategory.ParentID = 0;
                    productCategory.IsGjw = true;
                    productCategory.IsDisplay = true;
                    productCategory.Layer = 1;
                    productCategory.Sorting = null;

                    productCategoryModel.ID = this.ProductCategoryService.AddProductCategory(productCategory);
                    if (productCategoryModel.ID > 0)
                    {
                        return Json(new[] { productCategoryModel }.ToDataSourceResult(request, ModelState));
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
        /// The add sub category.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productCategoryModel">
        /// The product category model.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult AddSubCategory([DataSourceRequest] DataSourceRequest request, ProductCategoryModel productCategoryModel, int categoryID)
        {
            try
            {
                if (productCategoryModel != null)
                {
                    var productCategory = DataTransfer.Transfer<Product_Category>(
                        productCategoryModel,
                        typeof(ProductCategoryModel));

                    productCategory.ParentID = categoryID;
                    productCategory.IsGjw = true;
                    productCategory.IsDisplay = true;
                    productCategory.Layer = 2;
                    productCategory.Sorting = null;

                    productCategoryModel.ID = this.ProductCategoryService.AddProductCategory(productCategory);

                    if (productCategoryModel.ID > 0)
                    {
                        return Json(new[] { productCategoryModel }.ToDataSourceResult(request, ModelState));
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
        /// The remove category.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult RemoveCategory(int id, FormCollection collection)
        {
            try
            {
                this.ProductCategoryService.RemoveProductCategoryByID(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// The modify category.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productCategoryModel">
        /// The product category model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ModifyCategory([DataSourceRequest] DataSourceRequest request, ProductCategoryModel productCategoryModel)
        {
            if (productCategoryModel == null || !this.ModelState.IsValid)
            {
                return this.Json(new[] { productCategoryModel }.ToDataSourceResult(request, this.ModelState));
            }

            try
            {
                var productCategory = DataTransfer.Transfer<Product_Category>(
                        productCategoryModel,
                        typeof(ProductCategoryModel));

                this.ProductCategoryService.ModifyProductCategory(productCategory);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(new[] { productCategoryModel }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// The query category.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCategory([DataSourceRequest] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            DataSourceResult result;

            try
            {
                var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 0 and layer = 1 ", request.Page, request.PageSize);
                int totalCount;
                int pageCount;
                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.View();
                }

                var modelList = new List<ProductCategoryModel>();
                foreach (var product in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductCategoryModel>(product, typeof(Product_Category)));
                }

                result = new DataSourceResult { Data = modelList, Total = totalCount };
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(result);
        }

        /// <summary>
        /// The query sub category.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QuerySubCategory(int categoryID, [DataSourceRequest] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            DataSourceResult result;
            try
            {
                var codition = string.Format("ParentID = {0} and layer = 2 ", categoryID);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", codition, request.Page, request.PageSize);

                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(new DataSourceResult { Data = null, Total = 0 });
                }

                var modelList = new List<ProductCategoryModel>();
                foreach (var product in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductCategoryModel>(product, typeof(Product_Category)));
                }

                result = new DataSourceResult { Data = modelList, Total = totalCount };
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(result);
        }

        /// <summary>
        /// The query select list items.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QuerySelectListItems()
        {
            List<SelectListItem> selectListItems;
            try
            {
                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 0", 1, 10);
                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(null);
                }

                selectListItems = new List<SelectListItem> { new SelectListItem { Value = "-1", Text = "购酒网" } };
                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = category.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = category.CategoryName,
                                             };

                    selectListItems.Add(selectListItem);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query category list items.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCategorySelectListItems()
        {
            List<SelectListItem> selectListItems;
            try
            {
                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 0", 1, 10);
                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                selectListItems = new List<SelectListItem>();
                if (list == null)
                {
                    return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
                }

                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = category.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = category.CategoryName,
                                             };

                    selectListItems.Add(selectListItem);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query select list items by parent id.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCategorySelectListItemsByParentID(string parentID)
        {
            if (string.IsNullOrEmpty(parentID))
            {
                return null;
            }

            List<SelectListItem> selectListItems;
            try
            {
                var condtion = string.Format("ParentID = {0}", parentID);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", condtion, 1, 100);

                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);

                selectListItems = new List<SelectListItem> { new SelectListItem { Text = "全部", Value = "-1", Selected = true } };
                if (list == null)
                {
                    return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
                }

                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = category.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = category.CategoryName,
                                             };

                    selectListItems.Add(selectListItem);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query sub category select list items.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QuerySubCategorySelectListItems(string parentID)
        {
            if (string.IsNullOrEmpty(parentID))
            {
                return null;
            }

            List<SelectListItem> selectListItems;
            try
            {
                var condtion = string.Format("ParentID = {0}", parentID);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", condtion, 1, 100);
                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);

                selectListItems = new List<SelectListItem> { new SelectListItem { Value = "-1", Text = "请选择" } };
                if (list == null)
                {
                    return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
                }

                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                    {
                        Value = category.ID.ToString(CultureInfo.InvariantCulture),
                        Text = category.CategoryName,
                    };

                    selectListItems.Add(selectListItem);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
