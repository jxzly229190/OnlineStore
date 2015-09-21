// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Permission.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.System;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;

    /// <summary>
    /// 系统权限控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统权限服务对象
        /// </summary>
        private SystemPermissionService systemPermissionService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 系统权限首页执行方法
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpGet]
        public PartialViewResult Permission()
        {
            return this.PartialView("Permission");
        }

        /// <summary>
        /// 添加一级权限
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddPermission([DataSourceRequest] DataSourceRequest request, PermissionModel permission)
        {
            try
            {
                if (permission != null)
                {
                    permission.ParentID = 0;
                    permission.Layer = 1;
                    permission.CreateTime = DateTime.Now;

                    this.systemPermissionService = new SystemPermissionService();

                    var sysPermission = DataTransfer.Transfer<System_Permission>(permission, typeof(PermissionModel));
                    sysPermission.ID = this.systemPermissionService.AddPermission(sysPermission);

                    if (sysPermission.ID > 0)
                    {
                        return this.Json(new[] { sysPermission }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.View();
        }

        /// <summary>
        /// 添加二级权限
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <param name="permissionID">
        /// 上级权限编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddPermissionSecond([DataSourceRequest] DataSourceRequest request, PermissionModel permission, int permissionID)
        {
            try
            {
                if (permission != null)
                {
                    permission.ParentID = permissionID;
                    permission.Layer = 2;
                    permission.CreateTime = DateTime.Now;

                    this.systemPermissionService = new SystemPermissionService();

                    var sysPermission = DataTransfer.Transfer<System_Permission>(permission, typeof(PermissionModel));
                    sysPermission.ID = this.systemPermissionService.AddPermission(sysPermission);

                    if (sysPermission.ID > 0)
                    {
                        return this.Json(new[] { sysPermission }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加权限时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// 添加三级权限
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <param name="permissionID">
        /// 上级权限编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddPermissionThird([DataSourceRequest] DataSourceRequest request, PermissionModel permission, int permissionID)
        {
            try
            {
                if (permission != null)
                {
                    permission.ParentID = permissionID;
                    permission.Layer = 3;
                    permission.CreateTime = DateTime.Now;

                    this.systemPermissionService = new SystemPermissionService();

                    var sysPermission = DataTransfer.Transfer<System_Permission>(permission, typeof(PermissionModel));
                    sysPermission.ID = this.systemPermissionService.AddPermission(sysPermission);

                    if (sysPermission.ID > 0)
                    {
                        return this.Json(new[] { sysPermission }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加权限时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// 移除指定编号的权限
        /// </summary>
        /// <param name="id">
        /// 权限编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult RemovePermission(int id)
        {
            try
            {
                this.systemPermissionService = new SystemPermissionService();
                this.systemPermissionService.RemovePermissionByID(id);

                return this.RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改系统权限
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult ModifyPermission([DataSourceRequest] DataSourceRequest request, PermissionModel permission)
        {
            if (permission == null || !this.ModelState.IsValid)
            {
                return this.Json(new[] { permission }.ToDataSourceResult(request, this.ModelState));
            }

            try
            {
                this.systemPermissionService = new SystemPermissionService();

                var systemPermission = DataTransfer.Transfer<System_Permission>(permission, typeof(PermissionModel));
                this.systemPermissionService.ModifyPermission(systemPermission);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(new[] { permission }.ToDataSourceResult(request, this.ModelState));
        }

        /// <summary>
        /// 查询系统权限列表
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult QueryPermission([DataSourceRequest] DataSourceRequest request)
        {
            this.systemPermissionService = new SystemPermissionService();

            int pageCount;
            int totalCount;

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var paging = new Paging("[System_Permission]", null, "ID", string.Format("ParentID = {0}", 0), request.Page, request.PageSize);

            var list = this.systemPermissionService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var permissionModels = new List<PermissionModel>();

            foreach (var systemPermission in list)
            {
                permissionModels.Add(DataTransfer.Transfer<PermissionModel>(systemPermission, typeof(System_Permission)));
            }

            var result = new DataSourceResult { Data = permissionModels, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询子权限列表
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="permissionID">
        /// 上级权限编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult QueryPermissionByID([DataSourceRequest] DataSourceRequest request, int permissionID)
        {
            this.systemPermissionService = new SystemPermissionService();

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Permission]", null, "ID", string.Format("ParentID = {0}", permissionID), request.Page, request.PageSize);

            var list = this.systemPermissionService.Query(paging, out pageCount, out totalCount);

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (list == null)
            {
                return this.View();
            }

            var permissionModels = new List<PermissionModel>();

            foreach (var systemPermission in list)
            {
                permissionModels.Add(DataTransfer.Transfer<PermissionModel>(systemPermission, typeof(System_Permission)));
            }

            var result = new DataSourceResult { Data = permissionModels, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The query select list.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryPermissionTreeViewItems(int? id)
        {
            if (id == null)
            {
                id = 0;
            }

            this.systemPermissionService = new SystemPermissionService();

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Permission]", null, "ID", string.Format("ParentID = {0}", id), 1, 1000);

            var list = this.systemPermissionService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.Json(null);
            }

            var modelList = new List<dynamic>();
            foreach (var systemPermission in list)
            {
                modelList.Add(new { id = systemPermission.ID, Name = systemPermission.Name, hasChildren = this.systemPermissionService.HasChildren(systemPermission.ID) });
            }

            return this.Json(modelList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询一级权限选择项列表
        /// </summary>
        /// <returns>
        /// 一级权限选择项列表
        /// </returns>
        public ActionResult QueryPermissionSelectListItems()
        {
            this.systemPermissionService = new SystemPermissionService();

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Permission]", null, "ID", string.Format("ParentID = {0}", 0), 1, 1000);

            var list = this.systemPermissionService.Query(paging, out pageCount, out totalCount);
            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null);
        }

        /// <summary>
        /// 查询子权限选择项列表
        /// </summary>
        /// <param name="permissionID">
        /// The permission ID.
        /// </param>
        /// <returns>
        /// 子权限选择项列表
        /// </returns>
        public ActionResult QuerySubPermissionSelectListItems(int permissionID)
        {
            this.systemPermissionService = new SystemPermissionService();

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Permission]", null, "ID", string.Format("ParentID = {0}", permissionID), 1, 1000);

            var list = this.systemPermissionService.Query(paging, out pageCount, out totalCount);
            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null);
        }

        #endregion
    }
}
