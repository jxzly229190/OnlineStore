using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using V5.DataContract.Advertise;
using V5.Library;
using V5.Library.Logger;
using V5.Library.Storage.DB;
using V5.Portal.Backstage.Models.Advertise;
using V5.Service.Advertise;
using System.Data.SqlClient;

namespace V5.Portal.Backstage.Controllers.Advertise
{
    public partial class AdvertiseController
    {
        #region Partiview

        /// <summary>
        /// 广告配置部分视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AdvertiseConfig()
        {
            return this.PartialView();
        }

        /// <summary>
        /// 批量导入LP
        /// </summary>
        /// <returns></returns>
        public PartialViewResult BatchLeadLp()
        {
            return this.PartialView();
        }

        /// <summary>
        /// 产品查询视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SelectProduct()
        {
            return this.PartialView();
        }

        /// <summary>
        /// 批量导入产品
        /// </summary>
        /// <returns></returns>
        public PartialViewResult BatchLeadProduct()
        {
            return this.PartialView();
        }

        /// <summary>
        /// 修改配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult ModifiyConfig(int id)
        {
            this.advertiseConfigService = new AdvertiseConfigService();
            var model = this.advertiseConfigService.QueryID(id);
            return this.PartialView(model);
        }

        /// <summary>
        /// 添加配置节点
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddLP()
        {
            return PartialView();
        }

        #endregion

        #region Action

        /// <summary>
        /// 根据PID获取节点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TreeNodeFirst(int pid)
        {
            this.advertiseConfigService = new AdvertiseConfigService();
            var treefistNode = this.advertiseConfigService.QueryPid(pid);
            return Json(treefistNode);
        }

        /// <summary>
        /// 获限所有的节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TreeNodes()
        {
            this.advertiseConfigService = new AdvertiseConfigService();
            var treeNodes = this.advertiseConfigService.QueryAll();
            return Json(treeNodes);
        }

        /// <summary>
        /// 根据PID获限节点
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult GetNodeByPId([DataSourceRequest] DataSourceRequest request, int pid)
        {
            var condition = " PID=" + pid + "";
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 5;
            }
            try
            {
                int pageCount;
                var paging = new Paging("[Advertise_Config]", null, "ID", condition, request.Page, request.PageSize, "IsOrder", 0, true);
                this.advertiseConfigService = new AdvertiseConfigService();
                int totalCount;
                var treeNodes = this.advertiseConfigService.Paging(paging, out pageCount, out totalCount);
                var modelList = new List<AdvertiseConfigModel>();
                if (treeNodes != null && treeNodes.Any())
                {
                    foreach (var advertise in treeNodes)
                    {
                        modelList.Add(DataTransfer.Transfer<AdvertiseConfigModel>(advertise, typeof(Advertise_Config)));
                    }
                    var data = new DataSource()
                    {
                        Data = modelList,
                        Total = totalCount,
                    };
                    return this.Json(data);
                }
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("查询出错", Category.Error, exception);
            }
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 初始化Lp树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InitLpTree()
        {
            this.advertiseConfigService = new AdvertiseConfigService();
            var list = this.advertiseConfigService.QueryLPTree();
            return this.Json(list);
        }

        /// <summary>
        /// 执添加操作
        /// </summary>
        /// <param name="pid">父节点标识</param>
        /// <param name="name">标识</param>
        /// <param name="url">url</param>
        /// <param name="description">描述</param>
        /// <param name="imgpath">图片路径</param>
        /// <param name="imagePath"></param>
        /// <param name="source">来源</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="thumbnailImagePath"></param>
        /// <param name="indexId"></param>
        /// <param name="imageId"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public ActionResult InsertAdvertiseConfig(string pid, string name, string url, string description, string imagePath, string source, string indexId, int width, int height, string thumbnailImagePath, string imageId, string backgroundColor, bool isParent, int filter)
        {
            var advertiseConfig = new Advertise_Config();
            if (!string.IsNullOrEmpty(pid))
            {
                int result;
                int.TryParse(pid, out result);
                advertiseConfig.PID = result;
            }
            advertiseConfig.Name = name;
            advertiseConfig.URL = url;
            advertiseConfig.Description = V5.Library.Utils.UnEscape(description);
            advertiseConfig.ImagePath = imagePath;
            advertiseConfig.Source = source;
            advertiseConfig.CreateTime = DateTime.Now;
            advertiseConfig.IndexID = string.IsNullOrEmpty(indexId) ? -1 : Convert.ToInt32(indexId);
            advertiseConfig.Width = width;
            advertiseConfig.Height = height;
            advertiseConfig.ThumbnailImagePath = thumbnailImagePath;
            advertiseConfig.ImageID = string.IsNullOrEmpty(imageId) ? -1 : Convert.ToInt32(imageId);
            advertiseConfig.BackgroundColor = backgroundColor;
            advertiseConfig.filter = filter;
            advertiseConfig.isParent = isParent;
            this.advertiseConfigService = new AdvertiseConfigService();
            this.advertiseConfigService.Insert(advertiseConfig);
            return Json(new AjaxResponse { Data = 1, Message = "添加成功" });
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <param name="name">标题</param>
        /// <param name="url">URL</param>
        /// <param name="description">描述</param>
        /// <param name="imgpath">图片路径</param>
        /// <param name="imagePath"></param>
        /// <param name="source">来源</param>
        /// <param name="height"></param>
        /// <param name="thumbnailImagePath"></param>
        /// <param name="indexId"></param>
        /// <param name="width"></param>
        /// <param name="imageId"></param>
        /// <param name="backgroundColor"></param>
        public ActionResult UpdateConfig(int id, string name, string url, string description, string imagePath, string source, string indexId, int width, int height, string thumbnailImagePath, string imageId, string backgroundColor, bool isParent, int filter)
        {
            var model = new Advertise_Config()
            {
                ID = id,
                Name = name,
                URL = url,
                Description = V5.Library.Utils.UnEscape(description),
                ImagePath = imagePath,
                Source = source,
                IndexID = string.IsNullOrEmpty(indexId) ? -1 : Convert.ToInt32(indexId),
                Width = width,
                Height = height,
                ThumbnailImagePath = thumbnailImagePath,
                ImageID = string.IsNullOrEmpty(imageId) ? -1 : Convert.ToInt32(imageId),
                BackgroundColor = backgroundColor,
                filter = filter,
                isParent = isParent
            };
            this.advertiseConfigService = new AdvertiseConfigService();
            if (this.advertiseConfigService.Update(model) > 0)
            {
                return Json(new AjaxResponse { Data = 1, Message = "修改成功" });
            }
            return Json(new AjaxResponse { Data = 0, Message = "修改失败" });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ActionResult FilterSetting(int id, int filter)
        {
            new AdvertiseConfigService().UpdateFilter(id, filter);
            return Json(new AjaxResponse { State = 1, Message = "设定成功" });
        }
                
        /// <summary>
        /// 刷新主页
        /// </summary>
        /// <returns></returns>
        public void RefreshHome()
        {
            StaticHtmlHelper.RefreshHome();
        }

        /// <summary>
        /// 批量添加操作
        /// </summary>
        /// <param name="batchJson"></param>
        [HttpPost]
        public void BatchAddConfig(string batchJson)
        {
            this.advertiseConfigService = new AdvertiseConfigService();
            SqlTransaction transaction = null;
            string[] str = batchJson.Split(',');
            var list = new List<Advertise_Config>();
            for (int i = 0; i < str.Length; i++)
            {
                string[] arr_value = str[i].Split(':');
                if (arr_value.Length < 2) continue;
                list.Add(new Advertise_Config { PID = int.Parse(arr_value[0]), Name = arr_value[1] });
            }
            this.advertiseConfigService.BatchInsert(list, null);
            Response.Write("导入成功");
        }

        /// <summary>
        /// 批量删除操作
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public ActionResult DeleteRow(string array)
        {
            if (!string.IsNullOrEmpty(array))
            {
                this.advertiseConfigService = new AdvertiseConfigService();
                string[] str = array.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    advertiseConfigService.DeleteRow(int.Parse(str[i]));
                }
            }
            return Json(new AjaxResponse { Data = 1, Message = "删除成功" });
        }

        /// <summary>
        /// 向上排序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult IsUpOrder(int id, int pid)
        {
            try
            {
                var ReturenValue = new AdvertiseConfigService().UpdateIsOrder(id, pid);
                if (ReturenValue > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "向上排序" });
                }
            }
            catch (Exception exception)
            {

                throw new ArgumentException(exception.Message);
            }
            return Json(new AjaxResponse { Data = 0, Message = "排序不成功" });
        }

        #endregion
    }
}
