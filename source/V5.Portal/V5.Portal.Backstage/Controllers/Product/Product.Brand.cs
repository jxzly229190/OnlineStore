// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.Brand.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品品牌内部类.
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
    /// 商品品牌内部类.
    /// </summary>
    public partial class ProductController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The brand.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult Brand()
        {
            return this.PartialView("Brand");
        }

        /// <summary>
        /// The add brand.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentBrandID">
        /// The parent brand id.
        /// </param>
        /// <param name="brandLayer">
        /// The brand layer.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productBrandModel">
        /// The product brand model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult AddBrand(int categoryID, int parentBrandID, int brandLayer, [DataSourceRequest] DataSourceRequest request, ProductBrandModel productBrandModel)
        {
            if (productBrandModel == null)
            {
                return this.View();
            }

            try
            {
                var productBrand = DataTransfer.Transfer<Product_Brand>(
                        productBrandModel,
                        typeof(ProductBrandModel));

                productBrand.ProductCategoryID = categoryID;
                productBrand.ParentID = parentBrandID;
                productBrand.Layer = brandLayer;

                productBrandModel.ID = this.ProductBrandService.AddProductBrand(productBrand);
                if (productBrandModel.ID > 0)
                {
                    return this.Json(new[] { productBrandModel }.ToDataSourceResult(request, this.ModelState));
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.View();
        }

        /// <summary>
        /// The remove brand.
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
        public ActionResult RemoveBrand(int id, FormCollection collection)
        {
            try
            {
                this.ProductBrandService.RemoveProductBrandByID(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// The modify brand.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productBrandModel">
        /// The product brand model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ModifyBrand([DataSourceRequest] DataSourceRequest request, ProductBrandModel productBrandModel)
        {
            if (productBrandModel == null || !this.ModelState.IsValid)
            {
                return this.Json(new[] { productBrandModel }.ToDataSourceResult(request, this.ModelState));
            }

            try
            {
                var productBrand = DataTransfer.Transfer<Product_Brand>(
                        productBrandModel,
                        typeof(ProductBrandModel));

                this.ProductBrandService.ModifyBrandCategory(productBrand);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(new[] { productBrandModel }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// The query parent brand.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryBrandCategory(string categoryParentID, [DataSourceRequest] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            DataSourceResult result;
            try
            {
                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", "Layer = 2 And ParentID = " + categoryParentID, request.Page, request.PageSize);

                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.View();
                }

                var modelList = new List<ProductCategoryModel>();

                foreach (var brand in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductCategoryModel>(brand, typeof(Product_Category)));
                }

                result = new DataSourceResult
                             {
                                 Data = list,
                                 Total = totalCount
                             };
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(result);
        }

        /// <summary>
        /// The query sub brand.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentBrandID">
        /// The parent brand id.
        /// </param>
        /// <param name="brandLayer">
        /// The brand layer.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QuerySubBrand(int categoryID, int parentBrandID, int brandLayer, [DataSourceRequest] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            DataSourceResult result;
            try
            {
                var codition = string.Format(
                        "ProductCategoryID = {0} and ParentID = {1} and layer={2}",
                        categoryID,
                        parentBrandID,
                        brandLayer);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Brand]", null, "ID", codition, request.Page, request.PageSize);
                var list = this.ProductBrandService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(new DataSourceResult { Data = null, Total = 0 });
                }

                var modelList = new List<ProductBrandModel>();
                foreach (var brand in list)
                {
                    modelList.Add(DataTransfer.Transfer<ProductBrandModel>(brand, typeof(Product_Brand)));
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
        /// The query categoryt list items.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCategorytListItems()
        {
            List<SelectListItem> selectListItems;
            try
            {
                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Category]", null, "ID", "layer = 2", 1, 100);
                var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(null);
                }

                selectListItems = new List<SelectListItem>();
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
        /// The query brand select list items.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryBrandSelectListItems(string categoryID)
        {
            if (string.IsNullOrEmpty(categoryID))
            {
                return null;
            }

            List<SelectListItem> selectListItems;
            try
            {
                selectListItems = new List<SelectListItem> { new SelectListItem { Value = "-1", Text = "请选择" } };

                var codition = string.Format("ProductCategoryID = {0} and layer = 1 ", categoryID);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Brand]", null, "ID", codition, 1, 30);
                var list = this.ProductBrandService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
                }

                foreach (var brand in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = brand.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = brand.BrandName,
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
        /// The query sub brand select list items.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QuerySubBrandSelectListItems(string parentID)
        {
            if (string.IsNullOrEmpty(parentID))
            {
                return null;
            }

            List<SelectListItem> selectListItems;
            try
            {
                selectListItems = new List<SelectListItem> { new SelectListItem { Value = "-1", Text = "请选择" } };

                var codition = string.Format("parentID = {0} and layer = 2 ", parentID);

                int totalCount;
                int pageCount;
                var paging = new Paging("[Product_Brand]", null, "ID", codition, 1, 30);
                var list = this.ProductBrandService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(selectListItems, JsonRequestBehavior.AllowGet);
                }

                foreach (var brand in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = brand.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = brand.BrandName,
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
        /// 查询商品大类、品牌（一级品牌）
        /// </summary>
        /// <returns></returns>
        public JsonResult QueryBrandTree()
        {
            return this.Json(this.ProductBrandService.QueryBrandTree(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
