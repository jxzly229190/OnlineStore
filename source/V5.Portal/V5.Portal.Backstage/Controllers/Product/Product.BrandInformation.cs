using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using V5.DataContract.Configuration;
using V5.DataContract.Product;
using V5.Library;
using V5.Library.Storage.DB;
using V5.Service.Configuration;
using V5.Service.Product;

namespace V5.Portal.Backstage.Controllers.Product
{
    public partial class ProductController
    {
        private ConfigPageService configPageService;
        //
        // GET: /Product.BrandDescription/

        public PartialViewResult BrandInformation()
        {
            return PartialView("BrandInformation");
        }

        public ActionResult GetBrandCategory()
        {
            this.productBrandService = new ProductBrandService();
            var brandList = this.productBrandService.QueryBrandTree();
            return Json(brandList);
        }

        /// <summary>
        /// 根据ID查商品品牌信息
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public ActionResult GetBrandInfoByBrandID(int brandId)
        {
            try
            {
                this.brandInformationService = new BrandInformationService();
                var brand = this.brandInformationService.QueryByBrandID(brandId);
                if (brand != null)
                {
                    return Json(new AjaxResponse { Data = brand, Message = "", State = 1 });
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return Json(new AjaxResponse { Data = 0, Message = "没有找到相关的记录", State = 0 });
        }

        /// <summary>
        /// 根据返回值判断是执行添加还是修改操作 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Introduce"></param>
        /// <param name="brandId"></param>
        /// <param name="logo"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyAndInsert(string title, string Introduce, int brandId, string logo, string product)
        {
            try
            {
                this.brandInformationService = new BrandInformationService();
                var brand = this.brandInformationService.QueryByBrandID(brandId);

                Brand_Information model;
                if (brand != null)
                {
                    model = new Brand_Information
                     {
                         BrandID = brandId,
                         Introduce = Introduce,
                         Title = title,
                         Logo = !string.IsNullOrEmpty(logo) ? logo : brand.Logo,
                         ProductID = product
                     };
                    if (brandInformationService.UpdateByBrandID(model) > 0)
                    {
                        return Json(new AjaxResponse { Data = 1, Message = "修改成功", State = 1 });
                    }
                }
                else
                {
                    model = new Brand_Information
                    {
                        BrandID = brandId,
                        Introduce = Introduce,
                        Title = title,
                        Logo = logo,
                        ProductID = product,
                    };
                    if (brandInformationService.Insert(model) > 0)
                    {
                        return Json(new AjaxResponse { Data = 1, Message = "添加成功", State = 1 });
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return Json(new AjaxResponse { Data = 0, Message = "出错了", State = 0 });
        }
        /// <summary>
        /// 修 改Logo
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyLogo(int brandId, string logo)
        {
            this.brandInformationService = new BrandInformationService();
            var returnValue = this.brandInformationService.QueryByBrandID(brandId);
            if (returnValue != null)
            {
                if (brandInformationService.UpdateLogo(brandId, logo) > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "保存成功", State = 1 });
                }
            }
            else
            {
                var model = new Brand_Information
                {
                    BrandID = brandId,
                    Logo = logo,
                    Introduce = "NULL",
                    ProductID = "NULL",
                    Title = "NULL"
                };
                if (brandInformationService.Insert(model) > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "保存成功", State = 1 });
                }
            }
            return Json(new AjaxResponse { Data = 0, Message = "保存失败", State = 0 });
        }

        /// <summary>
        /// 修 改产品Id
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyProduct(int brandId, string product)
        {
            this.brandInformationService = new BrandInformationService();
            var returnValue = this.brandInformationService.QueryByBrandID(brandId);
            if (returnValue != null)
            {
                if (brandInformationService.UpdateProductString(brandId, product) > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "保存成功", State = 1 });
                }
            }
            else
            {
                var model = new Brand_Information
                {
                    BrandID = brandId,
                    Logo = "",
                    Introduce = "NULL",
                    ProductID = product,
                    Title = "NULL"
                };
                if (brandInformationService.Insert(model) > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "保存成功", State = 1 });
                }
            }
            return Json(new AjaxResponse { Data = 0, Message = "保存失败", State = 0 });
        }

        /// <summary>
        /// 获取链接的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public ActionResult QueryBrandLinkSource([DataSourceRequest] DataSourceRequest request, int brandId)
        {
            try
            {
                string condidtion = "Type=4 And PID=" + brandId.ToString() + "";
                var paging = new Paging("Config_Page", null, "ID", condidtion, request.Page, request.PageSize);
                int pageCount;
                int totalCount;
                this.configPageService = new ConfigPageService();
                var list = this.configPageService.Paging(paging, out pageCount, out totalCount);
                var data = new DataSource
                {
                    Data = list,
                    Total = totalCount,
                    TotalPages = pageCount
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加链接
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="Source"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertLink(int brandId, string Source, string Name)
        {
            var model = new Config_Page
            {
                PID = brandId,
                Name = Name,
                Source = Source,
                Type = 4,
                Content = "content"
            };
            try
            {
                this.configPageService = new ConfigPageService();
                var returnVaue = this.configPageService.Insert(model);
                if (returnVaue > 0)
                {
                    return Json("添加成功");
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message, exception);
            }

            return Json(string.Empty);
        }

        /// <summary>
        /// 修改链接
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ModifyLink([DataSourceRequest] DataSourceRequest request, Config_Page config)
        {
            if (config != null && ModelState.IsValid)
            {
                this.configPageService = new ConfigPageService();
                this.configPageService.UpdateLink(config);
            }
            return Json(new[] { config }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// 删除除操作
        /// </summary>
        /// <param name="request"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, Config_Page product)
        {
            try
            {
                if (product != null)
                {
                    this.configPageService = new ConfigPageService();
                    if (this.configPageService.DeleteRow(product.ID) > 0)
                    {
                        return Json(new[] { product }.ToDataSourceResult(request, ModelState));

                    }
                }

            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message, exception);
            }

            return Json(string.Empty);

        }

    }
}
