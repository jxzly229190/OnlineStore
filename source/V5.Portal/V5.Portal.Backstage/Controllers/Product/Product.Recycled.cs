// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.Recycled.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Product;
    using V5.Service.Product;

    /// <summary>
    /// The product controller.
    /// </summary>
    public partial class ProductController
    {
        /// <summary>
        /// The query product sold out.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productName">
        /// The product name.
        /// </param>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <param name="parentCategoryID">
        /// The parent category id.
        /// </param>
        /// <param name="productCategoryID">
        /// The product category id.
        /// </param>
        /// <param name="parentBrandID">
        /// The parent brand id.
        /// </param>
        /// <param name="productBrandID">
        /// The product brand id.
        /// </param>
        /// <param name="minPrice">
        /// The min price.
        /// </param>
        /// <param name="maxPrice">
        /// The max price.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryProductRecycled([DataSourceRequest] DataSourceRequest request, string productName, string barcode, string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID, string minPrice, string maxPrice)
        {
            int totalCount;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[Status] = 4");

            if (!string.IsNullOrEmpty(productName))
            {
                stringBuilder.Append(string.Format(" And [Name] like '%{0}%' ", productName));
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stringBuilder.Append(string.Format(" And [Barcode] like '%{0}%' ", barcode));
            }

            if (!string.IsNullOrEmpty(parentCategoryID) && parentCategoryID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ParentCategoryID] = {0} ", parentCategoryID));
            }

            if (!string.IsNullOrEmpty(productCategoryID) && productCategoryID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ProductCategoryID] = {0} ", productCategoryID));
            }

            if (!string.IsNullOrEmpty(parentBrandID) && parentBrandID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ParentBrandID] = {0} ", parentBrandID));
            }

            if (!string.IsNullOrEmpty(productBrandID) && productBrandID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ProductBrandID] = '{0}' ", productBrandID));
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                stringBuilder.Append(string.Format(" And [GoujiuPrice] >= '{0}' ", minPrice));
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                stringBuilder.Append(string.Format(" And [GoujiuPrice] <= '{0}' ", maxPrice));
            }

            var condition = stringBuilder.ToString();
            var paging = new Paging("view_Product_Paging", null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);

            var productSearchResultModelList = new List<ProductSearchResultModel>();

            try
            {
                int pageCount;
                var searchResult = this.ProductService.Query(paging, out pageCount, out totalCount);
                if (searchResult != null)
                {
                    foreach (var productSearchResult in searchResult)
                    {
                        var item = DataTransfer.Transfer<ProductSearchResultModel>(
                            productSearchResult,
                            typeof(ProductSearchResult));

                        productSearchResultModelList.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            var result = new DataSourceResult { Data = productSearchResultModelList, Total = totalCount };

            return this.Json(result);
        }

        /// <summary>
        /// The put shelf.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Restore(string productID)
        {
            AjaxResponse ajaxResponse;

            try
            {
                this.ProductService.ModifyProductStatus(productID, 3);

                ajaxResponse = new AjaxResponse(1);
            }
            catch (global::System.Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(ajaxResponse);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Remove(string productID)
        {
            AjaxResponse ajaxResponse;

            try
            {
                this.ProductService.RemoveByID(productID);

                ajaxResponse = new AjaxResponse(1);
            }
            catch (global::System.Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(ajaxResponse);
        }
    }
}