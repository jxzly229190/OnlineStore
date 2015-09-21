// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台首页控制器类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Home
{
    using global::System;
    using global::System.Linq;
    using global::System.Web.Mvc;
    using global::System.Collections.Generic;
    using global::System.Web;
    using global::System.IO;
    using global::System.Drawing;
    using global::System.Drawing.Imaging;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;
    using V5.DataContract.Utility;
    using V5.Service.Utility;
    using V5.Library;
    using V5.Service.Product;

    /// <summary>
    /// 后台首页控制器类
    /// </summary>
    public class HomeController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
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
        /// PageView
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PageView()
        {
            return this.PartialView();
        }

        /// <summary>
        /// The query provinces.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryProvinces()
        {
            var list = MongoDBHelper.QueryProvinces();

            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null);
        }

        /// <summary>
        /// The query cities.
        /// </summary>
        /// <param name="provinceID">
        /// The Province ID.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCities(string provinceID)
        {
            var mongoDbHelper = new MongoDbStore<City>("Cities");

            int pageCount;
            var list = mongoDbHelper.List(1, 346, city => city.ID != 0, out pageCount);
            if (list == null)
            {
                return this.Json(null);
            }

            var result = list.Where(item => item.ProvinceID == Convert.ToInt32(provinceID));
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query counties.
        /// </summary>
        /// <param name="cityID">
        /// The city id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCounties(string cityID)
        {
            var mongoDbHelper = new MongoDbStore<County>("Counties");

            int pageCount;
            var list = mongoDbHelper.List(1, 2976, county => county.ID != 0, out pageCount);
            if (list == null)
            {
                return this.Json(null);
            }

            var result = list.Where(item => item.CityID == Convert.ToInt32(cityID));
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 系统访问量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult SystemVisitorCount(string startTime, string endTime, string condition)
        {
            List<System_Visitor> list = new SystemVisitorService().Query(Convert.ToDateTime(startTime), Convert.ToDateTime(endTime), condition);
            var query = from visitor in list
            select new
            {
                date = visitor.DepartDate,
                count = visitor.VisitorCount
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 系统访问PV
        /// </summary>
        /// <returns></returns>
        public int SystemVisitorPV()
        {
            return new SystemVisitorService().QueryPV();
        }

        /// <summary>
        /// The exit.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Exit()
        {
            try
            {
                var mongoDbStore = new MongoDbStore<SystemUserSession>("SystemUserSessions");
                mongoDbStore.Delete(item => item.SessionID == this.Session.SessionID);

                return this.Content("exit");
            }
            catch (Exception)
            {
                return this.Content("0");
            }
        }

        /// <summary>
        /// 获取促销管理菜单编号.
        /// </summary>
        private void GetTopMenuID()
        {
            var topMenu = this.SystemUserSession.TopMenus.Where(item => item.Name == "系统首页").FirstOrDefault();
            if (topMenu != null)
            {
                this.ViewBag.ParentID = topMenu.ID;
            }
        }
        
        /// <summary>
        /// 图片预览
        /// </summary>
        public void ImagePreview()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (file.ContentLength > 0 && file.ContentType.IndexOf("image/") >= 0)
                {
                    int width = Convert.ToInt32(Request.Form["width"]);
                    int height = Convert.ToInt32(Request.Form["height"]);

                    string path = "data:image/jpeg;base64," + Convert.ToBase64String(this.ResizeImg(file.InputStream, width, height).GetBuffer());

                    Response.Write(path);
                }
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public string UploadPicture(string type)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (file.ContentLength > 0 && file.ContentType.IndexOf("image/") >= 0)
                    {
                        string fileName = Guid.NewGuid().ToString();  // 文件名
                        string fileNameExt = Path.GetExtension(file.FileName);
                        string fileNameOld = Path.GetFileName(file.FileName);
                        string rootPath = this.Server.MapPath("~/Upload_V5/Advertise/");  // 图片保存根目录
                        string savePath = string.Empty; // 数据库保存地址
                        string path = Utils.CreateDirectory(rootPath, out savePath); // 图片文件保存地址
                        savePath = "/Upload_V5/Advertise/" + savePath;

                        var image = Image.FromStream(file.InputStream);
                        int width = image.Width ;
                        int height = image.Height;
                        int iwidth = 420;
                        int iheight = 240;                        
                        if (width > height)
                        {
                            if (width > iwidth)
                            {
                                height = Convert.ToInt32(iwidth * 1.0 / width * height);
                                width = iwidth;
                            }
                        }
                        else
                        {
                            if (height > iheight)
                            {
                                width = Convert.ToInt32(iheight * 1.0 / height * width);
                                height = iheight;
                            }
                        }

                        file.SaveAs(path + fileName + fileNameExt);
                        Imager.Thumbnail(new Bitmap(file.InputStream), width, height, path + fileName + "_1" + fileNameExt, fileNameExt);

                        return savePath + fileName + "_1" + fileNameExt;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return "其他错误";
        }
        
        public string RemovePicture(string path, int id)
        {
            try
            {
                string fileName = this.Server.MapPath("~") + (string.IsNullOrEmpty(path) ? "" : path);
                DeleteFile(fileName); //移除缩略图
                DeleteFile(fileName.Replace("/Thumbnail/", "/")); //移除原图

                fileName = this.Server.MapPath("~") + "..\\V5.Portal" + (string.IsNullOrEmpty(path) ? "" : path);
                DeleteFile(fileName); //移除缩略图
                DeleteFile(fileName.Replace("/Thumbnail/", "/")); //移除原图

                //删除图片空间内容
                new PictureService().RemovePictureByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return "1";
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage(HttpPostedFileBase upload, string typeName, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            var file = upload;
            if (file != null && file.ContentLength > 0)
            {
                string fileNameEx = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(fileNameEx))
                {
                    return Content(@"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"\", \"获取图片扩展名失败!\");</script></body></html>");
                }

                typeName = string.IsNullOrEmpty(typeName) ? "images" : typeName;

                string fileName = Guid.NewGuid().ToString();
                string rootPath = this.Server.MapPath("~/Upload_V5/" + typeName + "/");
                string savePath = string.Empty;
                string path = Utils.CreateDirectory(rootPath, out savePath);
                savePath += fileName + Path.GetExtension(file.FileName);
                savePath = "/Upload_V5/" + typeName + "/" + savePath;

                //保存图片
                file.SaveAs(path + fileName + fileNameEx);

                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + savePath + "\", \"上传成功!\");</script></body></html>";
            }

            return Content(result);
        }

        public static void DeleteFile(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="thumbnail"></param>
        /// <returns></returns>
        public static string CreateDirectory(string rootPath)
        {
            var path = rootPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + DateTime.Now.Day + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="ImgFile"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public MemoryStream ResizeImg(Stream ImgFile, int maxWidth, int maxHeight)
        {
            Image imgPhoto = Image.FromStream(ImgFile);

            decimal desiredRatio = 0.25M; //Math.Min((decimal)maxWidth / imgPhoto.Width, (decimal)maxHeight / imgPhoto.Height);
            int iWidth = (int)(imgPhoto.Width * desiredRatio);
            int iHeight = (int)(imgPhoto.Height * desiredRatio);

            Bitmap bmPhoto = new Bitmap(iWidth, iHeight);

            Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
            gbmPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(0, 0, imgPhoto.Width, imgPhoto.Height), GraphicsUnit.Pixel);

            MemoryStream ms = new MemoryStream();
            bmPhoto.Save(ms, ImageFormat.Jpeg);

            imgPhoto.Dispose();
            gbmPhoto.Dispose();
            bmPhoto.Dispose();

            return ms;
        }

        /// <summary>
        /// 获取站点URL
        /// </summary>
        /// <returns></returns>
        public string GetSiteUrl()
        {            
            return global::System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"].ToString();
        }

        #endregion
    }
}
