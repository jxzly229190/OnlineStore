// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Log.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统日志控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;

    /// <summary>
    /// 系统日志控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统角色服务对象
        /// </summary>
        private SystemLogService systemLogService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 系统日志首页执行方法
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Log()
        {
            return this.PartialView("Log");
        }
        
        /// <summary>
        /// 移除系统日志执行方法
        /// </summary>
        /// <param name="logID">
        /// 日志编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult RemoveLog(int ID)
        {
            try
            {
                this.systemLogService = new SystemLogService();
                this.systemLogService.RemoveByID(ID);
                return this.RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        
        /// <summary>
        /// 查询系统用户列表执行方法
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult QueryLog([DataSourceRequest] DataSourceRequest request)
        {
            this.systemLogService = new SystemLogService();

            int pageCount;
            int totalCount;

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var paging = new Paging("[System_Log]", null, "ID", null, request.Page, request.PageSize);

            var list = this.systemLogService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var modelList = new List<System_Log>();
            foreach (var systemRole in list)
            {
                modelList.Add(DataTransfer.Transfer<System_Log>(systemRole, typeof(System_Log)));
            }

            var result = new DataSourceResult { Data = modelList, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
