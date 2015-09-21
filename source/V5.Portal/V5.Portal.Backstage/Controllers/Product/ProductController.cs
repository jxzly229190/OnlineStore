// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.  
// </copyright>
// <summary>
//   The product controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Net;
    using global::System.Text;
    using global::System.Web.Mvc;

    using V5.DataContract.Product;
    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Library.Storage.DB.NoSql;
    using V5.Portal.Backstage.Models.Product;
    using V5.Service.Product;

    /// <summary>
    /// 商品控制器类.
    /// </summary>
    public partial class ProductController : BaseController
    {
        #region Constants and Fields

        /// <summary>
        /// 商品服务对象
        /// </summary>
        private ProductService productService;

        /// <summary>
        /// 商品图片服务对象
        /// </summary>
        private ProductPictureService productPictureService;

        /// <summary>
        /// 商品类别服务对象
        /// </summary>
        private ProductCategoryService productCategoryService;

        /// <summary>
        /// 商品品牌服务对象
        /// </summary>
        private ProductBrandService productBrandService;

        /// <summary>
        /// 商品属性服务对象
        /// </summary>
        private ProductAttributeService productAttributeService;

        /// <summary>
        /// 商品属性值服务对象
        /// </summary>
        private ProductAttributeValueService productAttributeValueService;

        /// <summary>
        /// 商品属性值设置服务对象
        /// </summary>
        private ProductAttributeValueSetService productAttributeValueSetService;
        /// <summary>
        /// 商品品牌信息服务对象
        /// </summary>
        private BrandInformationService brandInformationService;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the product service.
        /// </summary>
        public ProductService ProductService
        {
            get
            {
                return this.productService ?? (new ProductService());
            }
        }

        /// <summary>
        /// Gets the product picture service.
        /// </summary>
        public ProductPictureService ProductPictureService
        {
            get
            {
                return this.productPictureService ?? (new ProductPictureService());
            }
        }

        /// <summary>
        /// Gets the product category service.
        /// </summary>
        public ProductCategoryService ProductCategoryService
        {
            get
            {
                return this.productCategoryService ?? (new ProductCategoryService());
            }
        }

        /// <summary>
        /// Gets the product brand service.
        /// </summary>
        public ProductBrandService ProductBrandService
        {
            get
            {
                return this.productBrandService ?? (new ProductBrandService());
            }
        }

        /// <summary>
        /// Gets the product attribute service.
        /// </summary>
        public ProductAttributeService ProductAttributeService
        {
            get
            {
                return this.productAttributeService ?? (new ProductAttributeService());
            }
        }

        /// <summary>
        /// Gets the product attribute value service.
        /// </summary>
        public ProductAttributeValueService ProductAttributeValueService
        {
            get
            {
                return this.productAttributeValueService ?? (new ProductAttributeValueService());
            }
        }

        /// <summary>
        /// Gets the product attribute value set service.
        /// </summary>
        public ProductAttributeValueSetService ProductAttributeValueSetService
        {
            get
            {
                return this.productAttributeValueSetService ?? (new ProductAttributeValueSetService());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 商品首页.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.GetTopMenuID();

            return this.View();
        }

        /// <summary>
        /// The sold out.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult SoldOut()
        {
            return this.PartialView("SoldOut");
        }

        /// <summary>
        /// The on sale.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult OnSale()
        {
            return this.PartialView("OnSale");
        }

        /// <summary>
        /// The recycled.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult Recycled()
        {
            return this.PartialView("Recycled");
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Add()
        {
            return this.PartialView("Add");
        }

        /// <summary>
        /// 获取商品属性网页代码
        /// </summary>
        /// <param name="categoryID">
        /// 商品类别编号
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult GetAttributeHtml(string categoryID)
        {
            var productAttributes = this.GetAttributes(categoryID);
            var stringBuilder = new StringBuilder();
            if (productAttributes == null)
            {
                return this.Content(stringBuilder.ToString());
            }

            stringBuilder.Append("<table>");

            int index = 0;
            foreach (var productAttribute in productAttributes)
            {
                index++;
                if (productAttribute.ProductAttributeValues == null)
                {
                    continue;
                }

                if (index == 1)
                {
                    stringBuilder.Append("<tr style='height: 30px;'>");
                }

                stringBuilder.Append("<td style='text-align: right; width: 120px;'>&nbsp;&nbsp;&nbsp;&nbsp;");
                stringBuilder.Append(productAttribute.AttributeName + "：");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='width: 150px;'><select width='130' style=' font-family: 微软雅黑; width: 130px;' name='ProductAttribute'>");
                stringBuilder.Append("<option value ='-1'>  请  选  择  </option>");

                foreach (var item in productAttribute.ProductAttributeValues)
                {
                    stringBuilder.Append("<option value ='" + productAttribute.ID + "_" + item.ID + "'>" + item.AttributeValue + "</option>");
                }

                stringBuilder.Append("</select></td>");

                if (index == 4)
                {
                    stringBuilder.Append("</tr>");
                    index = 0;
                }
            }

            stringBuilder.Append("<tr>");
            stringBuilder.Append("</table>");

            return this.Content(stringBuilder.ToString());
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="productAttributeValueSets">
        /// The product attribute value sets.
        /// </param>
        /// <param name="pictures">
        /// The pictures.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ProductModel product, List<ProductAttributeValueSetModel> productAttributeValueSets, List<string> pictures, string masterPictureID)
        {
            try
            {
                if (this.VerifyBarcode(product.ID, product.Name, product.Barcode))
                {
                    return this.Json(new AjaxResponse(0, "商品名称或条形码已存在！"));
                }

                var productObj = DataTransfer.Transfer<Product>(product, typeof(ProductModel));
                var productAttributeValueSetList = new List<Product_AttributeValueSet>();
                if (productAttributeValueSets != null && productAttributeValueSets.Count > 0)
                {
                    foreach (var productAttributeValueSetModel in productAttributeValueSets)
                    {
                        var item = DataTransfer.Transfer<Product_AttributeValueSet>(productAttributeValueSetModel, typeof(ProductAttributeValueSetModel));
                        productAttributeValueSetList.Add(item);
                    }
                }

                productObj.CreateTime = DateTime.Now;
                var productID = this.ProductService.Add(productObj, productAttributeValueSetList, pictures, masterPictureID);
                return this.Json(new AjaxResponse(1, "添加成功！"));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="productAttributeValueSets">
        /// The product attribute value sets.
        /// </param>
        /// <param name="pictures">
        /// The pictures.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modify(ProductModel product, List<ProductAttributeValueSetModel> productAttributeValueSets, List<string> pictures, string masterPictureID)
        {
            try
            {
                if (this.VerifyBarcode(product.ID, product.Name, product.Barcode))
                {
                    return this.Json(new AjaxResponse(0, "商品名称或条形码已存在！"));
                }

                var productObj = DataTransfer.Transfer<Product>(product, typeof(ProductModel));
                var productAttributeValueSetList = new List<Product_AttributeValueSet>();
                if (productAttributeValueSets != null && productAttributeValueSets.Count > 0)
                {
                    foreach (var productAttributeValueSetModel in productAttributeValueSets)
                    {
                        var item = DataTransfer.Transfer<Product_AttributeValueSet>(productAttributeValueSetModel, typeof(ProductAttributeValueSetModel));
                        productAttributeValueSetList.Add(item);
                    }
                }

                this.ProductService.Modify(productObj, productAttributeValueSetList, pictures, masterPictureID);
                return this.Json(new AjaxResponse(1, "修改成功！"));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The get attribute html for modify.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult GetAttributeHtmlForModify(string categoryID, string productID)
        {
            var stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(categoryID) || string.IsNullOrEmpty(productID))
            {
                return this.Content(stringBuilder.ToString());
            }

            var productAttributes = this.GetAttributes(categoryID);
            var productAttributeValueSets = this.ProductAttributeValueSetService.QueryByProductID(productID);

            if (productAttributes == null)
            {
                return this.Content(stringBuilder.ToString());
            }

            stringBuilder.Append("<table>");

            int index = 0;
            foreach (var productAttribute in productAttributes)
            {
                index++;
                if (productAttribute.ProductAttributeValues == null)
                {
                    continue;
                }

                if (index == 1)
                {
                    stringBuilder.Append("<tr style='height: 30px;'>");
                }

                stringBuilder.Append("<td style='text-align: right; width: 120px;'>&nbsp;&nbsp;&nbsp;&nbsp;");
                stringBuilder.Append(productAttribute.AttributeName + "：");
                stringBuilder.Append("</td>");
                stringBuilder.Append(
                    "<td style='width: 150px;'><select width='130' style=' font-family: 微软雅黑; width: 130px;' name='ProductAttribute'>");
                stringBuilder.Append("<option value ='-1'>  请  选  择  </option>");

                foreach (var item in productAttribute.ProductAttributeValues)
                {
                    string selected = string.Empty;

                    if (productAttributeValueSets != null)
                    {
                        foreach (var productAttributeValueSet in productAttributeValueSets)
                        {
                            if (productAttributeValueSet.AttributeID == productAttribute.ID
                                && productAttributeValueSet.AttributeValueID == item.ID)
                            {
                                selected = "selected='selected'";
                                break;
                            }
                        }
                    }
                    var optionString = "<option value ='" + productAttribute.ID + "_" + item.ID + "' " + selected + ">" + item.AttributeValue + "</option>";
                    stringBuilder.Append(optionString);
                }

                stringBuilder.Append("</select></td>");

                if (index == 4)
                {
                    stringBuilder.Append("</tr>");
                    index = 0;
                }
            }

            stringBuilder.Append("<tr>");
            stringBuilder.Append("</table>");

            return this.Content(stringBuilder.ToString());
        }

        /// <summary>
        /// The get picture html.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult GetPictureHtml(string productID)
        {
            if (string.IsNullOrEmpty(productID)) return null;

            var list = this.ProductPictureService.QueryByProductID(productID);
            if (list == null || list.Count == 0) return null;

            var query = from p in list
                        select new
                        {
                            PictureID = p.PictureID,
                            Path = Utils.GetProductImage(p.Path, "2"),
                            IsMaster = p.IsMaster
                        };

            return this.Json(query);
        }

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Modify(string productID)
        {
            var productModifyModel = new ProductModifyModel();

            if (!string.IsNullOrEmpty(productID))
            {
                productModifyModel = new ProductModifyModel();

                var product = this.ProductService.QueryByID(productID);
                var productAttributeValueSets = this.ProductAttributeValueSetService.QueryByProductID(productID);
                var productPictures = this.ProductPictureService.QueryByProductID(productID);

                var productAttributeValueSetModels = new List<ProductAttributeValueSetModel>();
                if (productAttributeValueSetModels != null && productAttributeValueSetModels.Count > 0)
                {
                    foreach (var productAttributeValueSet in productAttributeValueSets)
                    {
                        var item = DataTransfer.Transfer<ProductAttributeValueSetModel>(
                            productAttributeValueSet,
                            typeof(Product_AttributeValueSet));
                        productAttributeValueSetModels.Add(item);
                    }
                }
                var productPictureModels = new List<ProductPictureModel>();
                if (productPictures != null && productPictures.Count > 0)
                {
                    foreach (var productPicture in productPictures)
                    {
                        var item = DataTransfer.Transfer<ProductPictureModel>(productPicture, typeof(Product_Picture));
                        productPictureModels.Add(item);
                    }
                }
                productModifyModel.Product = DataTransfer.Transfer<ProductModel>(product, typeof(Product));
                productModifyModel.ProductAttributeValueSetModels = productAttributeValueSetModels;
                productModifyModel.ProductPictures = productPictureModels;
            }

            return this.PartialView("Modify", productModifyModel);
        }

        /// <summary>
        /// The limited buy area.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult LimitedBuyArea()
        {
            return this.View("LimitedBuyArea");
        }

        /// <summary>
        /// The get limited buy area.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult GetLimitedBuyArea()
        {
            return this.Content(this.GenerateLimitedBuyArea());
        }

        /// <summary>
        /// 获取一级商品类别信息
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult GetProductParentCategory()
        {
            int totalCount;
            int pageCount;

            var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 0 and layer = 1 ", 1, 10);
            var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
            if (list == null || list.Count == 0) return null;

            var query = from p in list
                        select new
                        {
                            id = p.ID,
                            name = p.CategoryName
                        };
            return this.Json(query);
        }

        /// <summary>
        /// 获取二级商品类别信息
        /// </summary>
        /// <param name="parentCategoryID">
        /// 一级商品类别编号
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult GetProductSubCategory(string parentCategoryID)
        {
            int totalCount;
            int pageCount;

            var condition = string.Format("ParentID = {0} And layer = 2 ", parentCategoryID);
            var paging = new Paging("[Product_Category]", null, "ID", condition, 1, 100);
            var list = this.ProductCategoryService.Query(paging, out pageCount, out totalCount);
            if (list == null || list.Count == 0) return null;

            var query = from p in list
                        select new
                        {
                            id = p.ID,
                            name = p.CategoryName
                        };
            return this.Json(query);
        }

        /// <summary>
        /// 获取一级商品品牌信息
        /// </summary>
        /// <param name="subCategoryID">
        /// 二级类别编号
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult GetProductParentBrand(string subCategoryID)
        {
            int totalCount;
            int pageCount;

            var condition = string.Format("ProductCategoryID = {0} and ParentID = 0 and layer = 1 ", subCategoryID);
            var paging = new Paging("[Product_Brand]", null, "ID", condition, 1, 1000);
            var list = this.ProductBrandService.Query(paging, out pageCount, out totalCount);
            if (list == null || list.Count == 0) return null;

            var query = from p in list
                        select new
                        {
                            id = p.ID,
                            name = p.BrandName
                        };
            return this.Json(query);
        }

        /// <summary>
        /// 获取二级商品品牌信息
        /// </summary>
        /// <param name="parentBrandID">
        /// 一级品牌编号
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public JsonResult GetProductSubBrand(string parentBrandID)
        {
            int totalCount;
            int pageCount;

            var condition = string.Format("ParentID = {0} and layer = 2 ", parentBrandID);
            var paging = new Paging("[Product_Brand]", null, "ID", condition, 1, 10000);
            var list = this.ProductBrandService.Query(paging, out pageCount, out totalCount);
            if (list == null || list.Count == 0) return null;

            var query = from p in list
                        select new
                        {
                            id = p.ID,
                            name = p.BrandName
                        };
            return this.Json(query);
        }

        public JsonResult GetProductTree()
        {
            List<Product_Tree> list = this.ProductService.QueryAllTree();
            if (list == null || list.Count == 0) return null;
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品最大编号.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [HttpPost]
        public int GetProductCount()
        {
            try
            {
                return this.ProductService.QueryProductCount();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取头部菜单.
        /// </summary>
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "商品管理").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }

        /// <summary>
        /// 获取商品属性列表
        /// </summary>
        /// <param name="categoryID">
        /// 商品类别编号
        /// </param>
        /// <returns>
        /// 商品属性列表
        /// </returns>
        private IEnumerable<Product_Attribute> GetAttributes(string categoryID)
        {



            var condition = "1 = 1";
            if (!string.IsNullOrEmpty(categoryID))
            {
                condition += " and [ProductCategoryID] = '" + categoryID + "' ";
            }

            int totalCount;
            int pageCount;

            var paging = new Paging("[Product_Attribute]", null, "ID", condition, 1, 1000);
            var productAttributes = this.ProductAttributeService.Query(paging, out pageCount, out totalCount);

            if (productAttributes != null)
            {
                foreach (var productAttribute in productAttributes)
                {
                    if (productAttribute == null)
                    {
                        continue;
                    }

                    paging = new Paging("[Product_AttributeValue]", null, "[ID]", string.Format("AttributeID = {0}", productAttribute.ID), 1, 1000);
                    productAttribute.ProductAttributeValues = this.ProductAttributeValueService.Query(paging, out pageCount, out totalCount);
                }
            }
            return productAttributes;
        }

        /// <summary>
        /// The generate limited buy area.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateLimitedBuyArea()
        {
            int pageCount;

            var mongoDbHelperForProvince = new MongoDbStore<Province>("Provinces");
            var mongoDbHelperForCity = new MongoDbStore<City>("Cities");
            var mongoDbHelperForCounty = new MongoDbStore<County>("Counties");

            var provinces = mongoDbHelperForProvince.List(1, 34, province => province.ID != 0, out pageCount);
            var cities = mongoDbHelperForCity.List(1, 346, city => city.ID != 0, out pageCount);
            var counties = mongoDbHelperForCounty.List(1, 2976, county => county.ID != 0, out pageCount);

            var province1 = new List<string> { "北京", "上海", "天津", "重庆", "河北" };
            var province2 = new List<string> { "山西", "河南", "辽宁", "吉林", "黑龙江" };
            var province3 = new List<string> { "内蒙古", "江苏", "山东", "安徽", "浙江" };
            var province4 = new List<string> { "福建", "湖北", "湖南", "广东", "广西" };
            var province5 = new List<string> { "江西", "四川", "海南", "贵州", "云南" };
            var province6 = new List<string> { "西藏", "陕西", "甘肃", "青海", "宁夏" };
            var province7 = new List<string> { "新疆", "台湾", "香港", "澳门" };

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<div class='smc'>");

            #region Generate Province One Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province1[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        if (cityList.Count == 1)
                        {
                            var countyList = counties.Where(item => item.CityID == cityList[0].ID);

                            stringBuilder.Append("<div class='city'><ul>");
                            foreach (var county in countyList)
                            {
                                stringBuilder.Append(string.Format("<li title='{0}'>", county.Name));
                                stringBuilder.Append(
                                    string.Format(
                                        "<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}",
                                        county.ID,
                                        county.Name));
                                stringBuilder.Append(string.Format("</li>"));
                            }

                            stringBuilder.Append("</ul></div>");
                        }
                        else
                        {
                            stringBuilder.Append("<div class='city'><ul>");
                            foreach (var city in cityList)
                            {
                                stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                                stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                                stringBuilder.Append(string.Format("</li>"));
                            }

                            stringBuilder.Append("</ul></div>");
                        }
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Two Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province2[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Three Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province3[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Four Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province4[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Five Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province5[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Six Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 5; i++)
            {
                var provinceName = province6[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.AppendLine();

            #region Generate Province Seven Html

            stringBuilder.Append("<div class='item'>");

            for (var i = 0; i < 4; i++)
            {
                var provinceName = province7[i];
                if (string.IsNullOrEmpty(provinceName))
                {
                    continue;
                }

                stringBuilder.Append(
                    i < 2
                        ? "<div class='i-item' data-widget='area'>"
                        : "<div class='i-item right' data-widget='area'>");

                var province = provinces.Find(item => item.Name == provinceName);
                if (province != null)
                {
                    stringBuilder.Append(string.Format("<div class='province' title='{0}'>", provinceName));
                    stringBuilder.Append(string.Format("<div data-widget='area-check' data-id='{0}' class='input selected'>", province.ID));
                    stringBuilder.Append("<span>√</span>");
                    stringBuilder.Append("</div>");
                    stringBuilder.Append(string.Format("{0} (<span class='red' data-widget='area-num'>0</span>)<em></em>", provinceName));
                    stringBuilder.Append("</div>");

                    var cityList = cities.Where(item => item.ProvinceID == province.ID).ToList();
                    if (cityList.Count > 0)
                    {
                        stringBuilder.Append("<div class='city'><ul>");
                        foreach (var city in cityList)
                        {
                            stringBuilder.Append(string.Format("<li title='{0}'>", city.Name));
                            stringBuilder.Append(string.Format("<input checked='checked' data-widget='area-list' autocomplete='off' data-id='{0}' type='checkbox'>{1}", city.ID, city.Name));
                            stringBuilder.Append(string.Format("</li>"));
                        }

                        stringBuilder.Append("</ul></div>");
                    }
                }

                stringBuilder.Append("</div>");
            }

            stringBuilder.Append("</div>");

            #endregion

            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 判断商品是否存在相同的名称或条形码.
        /// </summary>
        /// <param name="id">
        /// 商品编号.
        /// </param>
        /// <param name="name">
        /// 商品名称.
        /// </param>
        /// <param name="barcode">
        /// 商品条形码.
        /// </param>
        /// <returns>
        /// 有重复数据true,没有false.
        /// </returns>
        private bool VerifyBarcode(int id, string name, string barcode)
        {
            var count = new ProductService().VerifyProduct(id, name, barcode);

            return count > 0;
        }

        #endregion

        #region 限购区域

        public ActionResult SaveLimitedBuyArea(int productId, string AreaID, int AreaType)
        {
            if (string.IsNullOrEmpty(AreaID))
            {
                return Json(new AjaxResponse { State = 0, Message = "限购区域不能为空" });
            }
            var model = new Product_LimitedBuy_Area
            {
                ProductID = productId,
                AreaID = AreaID,
                AreaType = AreaType
            };
            var ProductLimitedBuyAreaService = new ProductLimitedBuyAreaService();
            if (ProductLimitedBuyAreaService.SelectByProductId(productId).FirstOrDefault() != null)
            {
                if (ProductLimitedBuyAreaService.UpdateByProductId(AreaID, productId) > 0)
                {
                    return Json(new AjaxResponse { State = 1, Message = "修改成功" });
                }
            }
            else
            {
                if (ProductLimitedBuyAreaService.Insert(model) > 0)
                    return Json(new AjaxResponse { State = 1, Message = "添加成功" });
            }
            return Json(new AjaxResponse { State = 0, Message = "操作失败" });
        }

        public string LoadLimitedArea(int productId)
        {
            var ProductLimitedBuyAreaService = new ProductLimitedBuyAreaService();
            var list = ProductLimitedBuyAreaService.SelectByProductId(productId);
            var productLimitedBuyArea = list.FirstOrDefault();
            if (productLimitedBuyArea != null && productLimitedBuyArea.AreaID.Length > 11)
                return productLimitedBuyArea.AreaID.Substring(11);
            return string.Empty;
        }

        public ActionResult BatchAddLimitedArea(string batchId, string content)
        {
            var ProductLimitedBuyAreaService = new ProductLimitedBuyAreaService();
            if (string.IsNullOrEmpty(batchId) && string.IsNullOrEmpty(content))
            {
                return Json(new AjaxResponse { State = 0, Message = "请选择商品或地区" });
            }
            if (!string.IsNullOrEmpty(batchId))
            {
                string[] strId = batchId.Split(',');
                var model = new Product_LimitedBuy_Area();
                foreach (var s in strId)
                {
                    if (ProductLimitedBuyAreaService.SelectByProductId(int.Parse(s)).FirstOrDefault() != null)
                    {
                        ProductLimitedBuyAreaService.UpdateByProductId(content, int.Parse(s));
                    }
                    else
                    {
                        model.ProductID = int.Parse(s);
                        model.AreaID = content;
                        model.AreaType = 1;
                        ProductLimitedBuyAreaService.Insert(model);
                    }
                }
            }

            return Json(new AjaxResponse { State = 1, Message = "设置成功" });
        }
        
        #endregion
    }
}