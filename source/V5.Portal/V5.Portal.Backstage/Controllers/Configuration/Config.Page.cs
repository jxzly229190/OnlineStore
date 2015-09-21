using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NPOI.HSSF.Model;
using NPOI.SS.Formula.Functions;
using V5.DataContract.Configuration;
using V5.Library;
using V5.Library.Logger;
using V5.Library.Storage.DB;
using V5.Portal.Backstage.Models.Configuration;
using V5.Service.Configuration;

namespace V5.Portal.Backstage.Controllers.Configuration
{
    public partial class ConfigController
    {
        #region Ctor 基础数据服务对象

        private ConfigPageService configPageService;
        #endregion
        #region PartialView
        /// <summary>
        /// 帮助视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ConfigHelper()
        {
            return PartialView();
        }
        /// <summary>
        /// 最新公告视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult NewannouncementView()
        {
            return PartialView();
        }
        /// <summary>
        /// 促销信息
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PromoteMessageView()
        {
            return PartialView();
        }

        #endregion

        #region Action
        /// <summary>
        /// 加载帮助节点
        /// </summary>
        /// <returns></returns>
        public JsonResult HelperTree()
        {
            this.configPageService = new ConfigPageService();
            var list = configPageService.Query(1);
            var modelList = new List<dynamic>();
            foreach (var item in list)
            {
                var model = new { ID = item.ID, PID = item.PID, Type = item.Type, Name = item.Name, isParent = (item.PID == 0 ? true : false) };
                modelList.Add(model);
            }
            return Json(modelList);
        }
        /// <summary>
        /// 加载最新公告节点
        /// </summary>
        /// <returns></returns>
        public ActionResult Announcement([DataSourceRequest] DataSourceRequest request)
        {
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }
            try
            {
                var condition = "[Type]=2";
                int pageCount;
                int totalCount;
                this.configPageService = new ConfigPageService();
                var paging = new Paging("[Config_Page]", null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);
                var list = this.configPageService.Paging(paging, out pageCount, out totalCount);

                var data = new DataSource
                {
                    Data = list,
                    Total = totalCount
                };
                return Json(data);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("查询出错", Category.Error, exception);
            }
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);


        }
        /// <summary>
        /// 加载促销信息节点
        /// </summary>
        /// <returns></returns>
        public ActionResult PromoteMessage([DataSourceRequest] DataSourceRequest request)
        {
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }
            try
            {
                var condition = "[Type]=3";
                int pageCount;
                int totalCount;
                this.configPageService = new ConfigPageService();
                var paging = new Paging("[Config_Page]", null, "ID", condition, request.Page, request.PageSize, "CreateTime", 1);
                var list = this.configPageService.Paging(paging, out pageCount, out totalCount);

                var data = new DataSource
                {
                    Data = list,
                    Total = totalCount
                };
                return Json(data);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("查询出错", Category.Error, exception);
            }
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 帮助内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult helperContent(int id)
        {
            this.configPageService = new ConfigPageService();
            var list = configPageService.QueryByID(id);
            return Json(list);
        }

        public ActionResult AddContent(string name)
        {
            var model = new Config_Page
            {
                PID = 0,
                Content = "一级节点",
                Name = name,
                Source = "source",
                Type = 1
            };
            var configService = new ConfigPageService();
            if (configService.Insert(model) > 0)
            {
                return Json(new AjaxResponse { State = 1, Message = "添加目录成功" });
            }
            return Json(new AjaxResponse { State = 0, Message = "添加止录失败" });

        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="pid"></param>
        /// <param name="Content">内容</param>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyContent(int ID, string Name, int pid, string Content)
        {
            this.configPageService = new ConfigPageService();

            if (ID == -1)
            {
                var model = new Config_Page
                {
                    Name = Name,
                    Content = Content,
                    Type = 1,
                    PID = pid,
                    Source = "default"
                };
                if (configPageService.Insert(model) > 0)
                {
                    return Json(new AjaxResponse { Data = 1, Message = "添加成功" });
                }
            }
            var received = this.configPageService.UpdateContent(ID, Content, Name);
            if (received > 0)
            {
                // todo : 需要类型参数
                return Json(new AjaxResponse { Data = 1, Message = "修改成功" });
            }
            return Json(string.Empty);
        }

        /// <summary>
        /// 添加最新消息内容
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Content"></param>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InserNewAnnouncetContent(string Name, string Content)
        {
            var model = new Config_Page
            {
                PID = 29,
                Type = 2,
                Name = Name,
                Content = Content,
                Source = "default"
            };
            this.configPageService = new ConfigPageService();
            var recevieId = this.configPageService.Insert(model);
            if (recevieId > 0)
            {
                return Json(new AjaxResponse { Data = 1, Message = "添加成功" });
            }
            return Json(new AjaxResponse { Data = 0, Message = "操作失败" });

        }

        /// <summary>
        /// 添加促销信息内容
        /// </summary>

        /// <param name="Name"></param>
        /// <param name="Content"></param>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertPromoteMessage(string Name, string Content)
        {
            var model = new Config_Page
            {
                PID = 31,
                Type = 3,
                Name = Name,
                Content = Content,
                Source = "default"
            };
            this.configPageService = new ConfigPageService();
            var recevieId = this.configPageService.Insert(model);
            if (recevieId > 0)
            {
                return Json(new AjaxResponse { Data = 1, Message = "添加成功" });
            }
            return Json(new AjaxResponse { Data = 0, Message = "操作失败" });

        }
        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="ID"></param>
        public ActionResult DeleteRow(int ID)
        {
            this.configPageService = new ConfigPageService();
            var receiveId = this.configPageService.DeleteRow(ID);
            if (receiveId > 0)
            {
                return Json(new AjaxResponse { Data = 1, Message = "删除成功" });
            }
            return Json(string.Empty);
        }

        public ActionResult QueryContentById(int id)
        {
            this.configPageService = new ConfigPageService();
            var list = configPageService.QueryByID(id);
            if (list != null)
            {
                return Json(list);
            }
            return Json(string.Empty);
        }

        #endregion
    }
}