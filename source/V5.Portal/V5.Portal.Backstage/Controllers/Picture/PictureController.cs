// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The picture controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Picture
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Drawing;
    using global::System.IO;
    using global::System.Linq;
    using global::System.Web;
    using global::System.Web.Mvc;
    using Kendo.Mvc.UI;
    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Product;
    using V5.Service.Picture;
    using V5.Service.Product;

    /// <summary>
    /// The picture controller.
    /// </summary>
    public class PictureController : BaseController
    {
        /// <summary>
        /// The picture service.
        /// </summary>
        private PictureService pictureService;

        /// <summary>
        /// The product category service.
        /// </summary>
        private ProductCategoryService productCategoryService;

        /// <summary>
        /// The product brand service.
        /// </summary>
        private ProductBrandService productBrandService;

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.PictureCategories = this.GetPictureCategories(0, null);

            this.GetTopMenuID();

            return this.View("Index");
        }

        /// <summary>
        /// The selector.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Selector()
        {
            return this.View("Selector");
        }

        /// <summary>
        /// The selector for ck editor.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SelectorForCKEditor()
        {
            return this.View("SelectorForCKEditor");
        }

        /// <summary>
        /// 获取图片类别数据
        /// </summary>
        /// <param name="typeID">
        /// 类型编号（0：商品类别，1：商品品牌）
        /// </param>
        /// <param name="parent">
        /// 父级对象
        /// </param>
        /// <returns>
        /// 图片类别列表
        /// </returns>
        public List<PictureCategory> GetPictureCategories(int typeID, PictureCategory parent)
        {
            var pictureCategories = new List<PictureCategory>();

            this.productBrandService = new ProductBrandService();
            this.productCategoryService = new ProductCategoryService();

            object list;
            if (typeID == 0)
            {
                if (parent == null)
                {
                    list = this.productCategoryService.QueryCategoryByParentID(0);

                    var categories = list as List<Product_Category>;
                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            pictureCategories.Add(
                                new PictureCategory
                                {
                                    ID = category.ID,
                                    Text = category.CategoryName,
                                    Url = string.Empty,
                                    Level = 1
                                });
                        }
                    }
                }
                else
                {
                    list = this.productCategoryService.QueryCategoryByParentID(parent.ID);

                    var categories = list as List<Product_Category>;
                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            pictureCategories.Add(
                                new PictureCategory
                                {
                                    ID = category.ID,
                                    Text = category.CategoryName,
                                    Url = string.Empty,
                                    Level = 2
                                });
                        }
                    }
                }
            }

            if (typeID == 1 && parent != null)
            {
                if (parent.Level == 2)
                {
                    list = this.productBrandService.QueryProductBrandByCategoryID(parent.ID, 0);
                    var brands = list as List<Product_Brand>;
                    if (brands != null)
                    {
                        foreach (var brand in brands)
                        {
                            pictureCategories.Add(
                                new PictureCategory
                                {
                                    ID = brand.ID,
                                    Text = brand.BrandName,
                                    Url = string.Empty,
                                    Level = 3
                                });
                        }
                    }
                }

                if (parent.Level == 3)
                {
                    list = this.productBrandService.QueryProductBrandByParentID(parent.ID);
                    var brands = list as List<Product_Brand>;
                    if (brands != null)
                    {
                        foreach (var brand in brands)
                        {
                            pictureCategories.Add(
                                new PictureCategory
                                {
                                    ID = brand.ID,
                                    Text = brand.BrandName,
                                    Url = "Picture/" + brand.ID,
                                    Level = 4
                                });
                        }
                    }
                }
            }
            
            return pictureCategories;
        }
        
        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="pictureName">
        /// The picture name.
        /// </param>
        /// <param name="startTime">
        /// The start time.
        /// </param>
        /// <param name="endTime">
        /// The end time.
        /// </param>
        /// <param name="brandID">
        /// The brand id.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Query([DataSourceRequest] DataSourceRequest request, string pictureName, string startTime, string endTime, string brandID, string type)
        {
            this.pictureService = new PictureService();

            int pageCount;
            int totalCount;

            var condition = " IsDelete = 0 ";
            if (!string.IsNullOrEmpty(brandID))
            {
                switch (type)
                {
                    case "1":
                        condition += " and id in (select pictureid from product, product_picture where product_picture.productid = product.id and product.isdelete = 0 and product_picture.isdelete = 0 and product.ParentCategoryID = " + brandID + ")";
                        break;
                    case "2":
                        condition += " and id in (select pictureid from product, product_picture where product_picture.productid = product.id and product.isdelete = 0 and product_picture.isdelete = 0 and product.ProductCategoryID = " + brandID + ")";
                        break;
                    case "3":
                        condition += " and id in (select pictureid from product, product_picture where product_picture.productid = product.id and product.isdelete = 0 and product_picture.isdelete = 0 and product.ParentBrandID = " + brandID + ")";
                        break;
                    case "4":
                        condition += " and id in (select pictureid from product, product_picture where product_picture.productid = product.id and product.isdelete = 0 and product_picture.isdelete = 0 and product.ProductBrandID = " + brandID + ")";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(pictureName))
            {
                condition += " and [Name] like '%" + pictureName + "%' ";
            }

            if (!string.IsNullOrEmpty(startTime))
            {
                condition += " and [UploadTime] > '" + startTime + "' ";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                condition += " and [UploadTime] < '" + endTime + "' ";
            }

            var paging = new Paging("[Picture]", null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);
            var list = this.pictureService.Query(paging, out pageCount, out totalCount);

            var result = new DataSourceResult();
            if (list != null)
            {
                var modelList = new List<PictureModel>();

                foreach (var picture in list)
                {
                    picture.Path = Utils.GetProductImage(picture.Path, "1");
                    modelList.Add(
                        DataTransfer.Transfer<PictureModel>(picture, typeof(Picture)));
                }
                 
                result.Data = list;
                result.Total = totalCount;

                return this.Json(result);
            }

            result.Data = null;
            result.Total = 0;
            return this.Json(result);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="pictures">
        /// The pictures.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Remove(List<string> pictures)
        {
            AjaxResponse jsonResponse;

            try
            {
                this.pictureService = new PictureService();

                foreach (var picture in pictures)
                {
                    this.pictureService.RemovePictureByID(Convert.ToInt32(picture));
                }

                jsonResponse = new AjaxResponse(1);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(jsonResponse);
        }

        /// <summary>
        /// The modify picture category.
        /// </summary>
        /// <param name="pictures">
        /// The pictures.
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
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ModifyPictureCategory(List<string> pictures, string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID)
        {
            AjaxResponse jsonResponse;

            try
            {
                this.pictureService = new PictureService();

                foreach (var picture in pictures)
                {
                    this.pictureService.ModifyPictureCategory(Convert.ToInt32(picture), parentCategoryID, productCategoryID, parentBrandID, productBrandID);
                }

                jsonResponse = new AjaxResponse(1);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(jsonResponse);
        }
        
        /// <summary>
        /// The upload picture.
        /// </summary>
        /// <param name="pictureUpload">
        /// The picture Upload.
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
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UploadPicture(IEnumerable<HttpPostedFileBase> pictureUpload, string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID)
        {
            this.pictureService = new PictureService();

            if (pictureUpload == null)
            {
                return this.Json(string.Empty);
            }

            try
            {
                foreach (var file in pictureUpload)
                {
                    var displayName = Path.GetFileName(file.FileName);
                    if (displayName == null)
                    {
                        continue;
                    }

                    var fileName = Guid.NewGuid().ToString();  // 文件名
                    var rootPath = this.Server.MapPath("~/Upload_V5/Product/");  // 图片保存根目录
                    string savePath; // 数据库保存地址
                    var path = this.CreateDirectory(rootPath, out savePath); // 图片文件保存地址
                    savePath += fileName + Path.GetExtension(file.FileName);
                    savePath = "/Upload_V5/Product/" + savePath;

                    var stream = file.InputStream;
                    var image = Image.FromStream(stream);

                    var picture = new Picture
                                      {
                                          Name = Path.GetFileNameWithoutExtension(file.FileName),
                                          Type = Path.GetExtension(file.FileName),
                                          ParentCategoryID = Convert.ToInt32(parentCategoryID),
                                          ProductCategoryID = string.IsNullOrEmpty(productCategoryID) ? (int?)null : Convert.ToInt32(productCategoryID),
                                          ParentBrandID = string.IsNullOrEmpty(parentBrandID) ? (int?)null : Convert.ToInt32(parentBrandID),
                                          ProductBrandID = string.IsNullOrEmpty(productBrandID) ? (int?)null : Convert.ToInt32(productBrandID),
                                          Size = file.ContentLength,
                                          Width = image.Width,
                                          Height = image.Height,
                                          Path = savePath,
                                          ThumbnailPath = string.Empty,
                                          FileName = Guid.NewGuid().ToString(),
                                          Status = 1,
                                          CreateTime = DateTime.Now,
                                          UploadTime = DateTime.Now
                                      };

                    // 保存原图
                    file.SaveAs(path + fileName + picture.Type);
                    
                   // 生成对应缩略图
                    var pic = new ImageEditService();
                    pic.CreateProductPic(path + fileName + picture.Type, path);

                    picture.ID = this.pictureService.AddPicture(picture);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 创建目录.
        /// </summary>
        /// <param name="rootPath">
        /// 图片保存路径.
        /// </param>
        /// <param name="savePath">
        /// 数据库保存地址.
        /// </param>
        /// <returns>
        /// 图片路径.
        /// </returns>
        public string CreateDirectory(string rootPath, out string savePath)
        {
            var path = rootPath + DateTime.Today.ToString("yyyy") + "\\" + DateTime.Today.ToString("MMdd") + "\\";
            savePath = DateTime.Today.ToString("yyyy") + "/" + DateTime.Today.ToString("MMdd") + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// The query picture tree view.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryPictureTreeView(int? id)
        {
            this.productCategoryService = new ProductCategoryService();
            this.productBrandService = new ProductBrandService();

            int totalCount;
            int pageCount;

            var modelList = new List<dynamic>();

            if (id == null)
            {
                id = 0;
                var condition = string.Format("ParentID = {0} And Layer = 1 ", id);

                var paging = new Paging("[Product_Category]", null, "ID", condition, 1, 100);
                var list = this.productCategoryService.Query(paging, out pageCount, out totalCount);
                if (list != null)
                {
                    foreach (var category in list)
                    {
                        modelList.Add(
                            new { id = category.ID.ToString(), Name = category.CategoryName, hasChildren = true, type = 1 });
                    }
                }

                TempData["TreeviewType"] = 1;
            }
            else
            {
                if (Convert.ToInt32(TempData["TreeviewType"]) == 1)
                {
                    var condition = string.Format("ParentID = {0} And Layer = 2 ", id);

                    var paging = new Paging("[Product_Category]", null, "ID", condition, 1, 100);
                    var list = this.productCategoryService.Query(paging, out pageCount, out totalCount);
                    if (list != null)
                    {
                        foreach (var category in list)
                        {
                            modelList.Add(
                                new { id = category.ID.ToString(), Name = category.CategoryName, hasChildren = true, type = 2 });
                        }
                    }

                    TempData["TreeviewType"] = 2;
                }
                else if (Convert.ToInt32(TempData["TreeviewType"]) == 2)
                {
                    var condition = string.Format("ProductCategoryID = {0} And ParentID = 0 ", id);

                    var paging = new Paging("[Product_Brand]", null, "ID", condition, 1, 100);
                    var list = this.productBrandService.Query(paging, out pageCount, out totalCount);
                    if (list != null)
                    {
                        foreach (var brand in list)
                        {
                            modelList.Add(new { id = brand.ID.ToString(), Name = brand.BrandName, hasChildren = true, type = 3 });
                        }
                    }

                    TempData["TreeviewType"] = 3;
                }
                else if (Convert.ToInt32(TempData["TreeviewType"]) == 3)
                {
                    var condition = string.Format("ParentID = {0}", id);

                    var paging = new Paging("[Product_Brand]", null, "ID", condition, 1, 100);
                    var list = this.productBrandService.Query(paging, out pageCount, out totalCount);
                    if (list != null)
                    {
                        foreach (var brand in list)
                        {
                            modelList.Add(new { id = brand.ID.ToString(), Name = brand.BrandName, hasChildren = false, type = 4 });
                        }
                    }
                }
            }

            return this.Json(modelList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取头部菜单.
        /// </summary>
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "图片空间").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }
    }
}
