// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.Attribute.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NPOI.SS.Formula.Functions;

namespace V5.Portal.Backstage.Controllers.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Product;
    using V5.Service.Product;

    /// <summary>
    /// 商品控制器.
    /// </summary>
    public partial class ProductController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The attribute.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Attribute()
        {
            return this.PartialView("Attribute");
        }

        /// <summary>
        /// The add product attr.
        /// </summary>
        /// <param name="productCateID">
        /// The product cate id.
        /// </param>
        /// <param name="productAttrName">
        /// The product attr name.
        /// </param>
        /// <param name="inputType"></param>
        /// <param name="dataType"></param>
        /// <param name="length"></param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAttribute(string productCateID, string productAttrName, string inputType, string dataType, int length)
        {
            try
            {
                if (string.IsNullOrEmpty(productCateID) || string.IsNullOrEmpty(productAttrName))
                {
                    return Content("2");
                }

                var attr = new Product_Attribute
                {
                    ProductCategoryID = Convert.ToInt32(productCateID),
                    AttributeName = productAttrName,
                    InputType = inputType,
                    DataType = dataType,
                    DataLength = length
                };

                this.ProductAttributeService.AddProductAttribute(attr);

                return Content("1");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The modify product attr.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productAttributeModel">
        /// The product attribute model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ModifyAttribute([DataSourceRequest] DataSourceRequest request,
            ProductAttributeModel productAttributeModel)
        {
            if (productAttributeModel == null || !this.ModelState.IsValid)
            {
                return this.Json(new[] { productAttributeModel }.ToDataSourceResult(request, this.ModelState));
            }

            try
            {
                var productAttr = DataTransfer.Transfer<Product_Attribute>(
                    productAttributeModel,
                    typeof(ProductAttributeModel));

                this.ProductAttributeService.ModifyProductAttribute(productAttr);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(new[] { productAttributeModel }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// The remove product attr.
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
        public ActionResult RemoveAttribute(int id, FormCollection collection)
        {
            try
            {
                this.ProductAttributeService.RemoveProductAttributeByID(id);

                return RedirectToAction("Attribute");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The query product attribute.
        /// </summary>
        /// <param name="categoryID">
        /// The category ID.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryAttribute(string categoryID, [DataSourceRequest] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            DataSourceResult result;
            try
            {
                var condition = "1 = 1";
                if (!string.IsNullOrEmpty(categoryID) && categoryID != "-1")
                {
                    condition += string.Format(" and [ProductCategoryID] = {0} ", categoryID);
                }

                var paging = new Paging("[Product_Attribute]", null, "ID", condition, request.Page, request.PageSize);
                int pageCount;
                int totalCount;
                var list = this.ProductAttributeService.Query(paging, out pageCount, out totalCount);
                result = new DataSourceResult();

                if (list != null)
                {
                    var modelList = new List<ProductAttributeModel>();
                    foreach (var product in list)
                    {
                        modelList.Add(
                            DataTransfer.Transfer<ProductAttributeModel>(product, typeof(Product_Attribute)));
                    }

                    result.Data = modelList;
                    result.Total = totalCount;

                    return Json(result);
                }

                result.Data = null;
                result.Total = 0;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(result);
        }

        /// <summary>
        /// The add attribute value.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="productAttributeValueModel">
        /// The product attribute value model.
        /// </param>
        /// <param name="productAttrID">
        /// The product attr id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAttributeValue(string attributeValue, int Sorting, int isDefault, string productAttrID)
        {
            try
            {
                var productAttrValue = new Product_AttributeValue
                {
                    AttributeValue = attributeValue,
                    AttributeID = Convert.ToInt32(productAttrID),
                    Sorting = Sorting,
                    IsDefault = isDefault,

                };
                //if (productAttributeValueModel != null)
                //{
                //    var productAttrValue = DataTransfer.Transfer<Product_AttributeValue>(
                //        productAttributeValueModel,
                //        typeof(ProductAttributeValueModel));
                //    productAttrValue.AttributeID = Convert.ToInt32(productAttrID);
                //    productAttributeValueModel.ID =
                this.ProductAttributeValueService.AddProductAttributeValue(productAttrValue);
                // }

                return Json("");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary/>
        /// The product attribute value model.
        /// 
        /// <param name="attributeValue"></param>
        /// <param name="Sorting"></param>
        /// <param name="isDefault"></param>
        /// <param name="attributeID"></param>
        /// <param name="id"></param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ModifyAttributeValue(string attributeValue, int Sorting, int isDefault, int attributeID, int id)
        {
            try
            {

                var productAttrValue = new Product_AttributeValue
                {
                    AttributeID = attributeID,
                    AttributeValue = attributeValue,
                    CreateTime = DateTime.Now,
                    ID = id,
                    IsDefault = isDefault,
                    Sorting = Sorting
                };

                this.ProductAttributeValueService.ModifyProductAttributeValue(productAttrValue);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(new AjaxResponse { Data = 1, Message = "修改成功" });
        }

        /// <summary>
        /// The remove product attr value.
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
        public ActionResult RemoveAttributeValue(int id, FormCollection collection)
        {
            try
            {
                this.ProductAttributeValueService.RemoveProductAttributeValueByID(id);

                return RedirectToAction("Attribute");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The query product attribute value.
        /// </summary>
        /// <param name="productAttrID">
        /// The product attr id.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryAttributeValue(string productAttrID, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result;
            try
            {
                int totalCount;
                int pageCount;

                if (request.PageSize == 0)
                {
                    request.PageSize = 10;
                }

                string condition = string.Format("AttributeID = {0}", productAttrID);

                var paging = new Paging("[Product_AttributeValue]", null, "ID", condition, request.Page,
                    request.PageSize);

                var list = this.ProductAttributeValueService.Query(paging, out pageCount, out totalCount);
                result = new DataSourceResult();

                if (list != null)
                {
                    var modelList = new List<ProductAttributeValueModel>();

                    foreach (var product in list)
                    {
                        modelList.Add(
                            DataTransfer.Transfer<ProductAttributeValueModel>(product, typeof(Product_AttributeValue)));
                    }

                    result.Data = modelList;
                    result.Total = totalCount;
                    return Json(result);
                }

                result.Data = null;
                result.Total = 0;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return Json(result);
        }

        /// <summary>
        /// 查询属性和属性值
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string QueryAttributeAndAttributeValue(string categoryId)
        {
            return string.IsNullOrEmpty(categoryId) ? "" : this.ProductAttributeService.QueryByCategoryId(categoryId);
        }

        public ActionResult AddAttributeValue([DataSourceRequest] DataSourceRequest request, ProductAttributeModel model)
        {
            return Json("");
        }

        #endregion
    }
}
