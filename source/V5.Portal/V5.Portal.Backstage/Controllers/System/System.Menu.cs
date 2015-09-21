// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Menu.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统菜单控制器部分类
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
    /// 系统菜单控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统菜单服务对象
        /// </summary>
        private SystemMenuService systemMenuService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 系统权限首页执行方法
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpGet]
        public PartialViewResult Menu()
        {
            return this.PartialView("Menu");
        }

        /// <summary>
        /// 添加一级菜单
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="menu">
        /// 菜单对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddMenu([DataSourceRequest] DataSourceRequest request, MenuModel menu)
        {
            try
            {
                if (menu != null)
                {
                    menu.ParentID = 0;
                    menu.Layer = 1;

                    this.systemMenuService = new SystemMenuService();

                    var systemMenu = DataTransfer.Transfer<System_Menu>(menu, typeof(MenuModel));
                    systemMenu.ID = this.systemMenuService.AddMenu(systemMenu);

                    if (systemMenu.ID > 0)
                    {
                        return this.Json(new[] { systemMenu }.ToDataSourceResult(request, this.ModelState));
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
        /// 添加二级菜单
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="menu">
        /// 权限对象
        /// </param>
        /// <param name="menuID">
        /// 上级菜单编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddMenuSecond([DataSourceRequest] DataSourceRequest request, MenuModel menu, int menuID)
        {
            try
            {
                if (menu != null)
                {
                    menu.ParentID = menuID;
                    menu.Layer = 2;

                    this.systemMenuService = new SystemMenuService();

                    var systemMenu = DataTransfer.Transfer<System_Menu>(menu, typeof(MenuModel));
                    systemMenu.ID = this.systemMenuService.AddMenu(systemMenu);

                    if (systemMenu.ID > 0)
                    {
                        return this.Json(new[] { systemMenu }.ToDataSourceResult(request, this.ModelState));
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
        /// 移除指定编号的菜单
        /// </summary>
        /// <param name="id">
        /// 菜单编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult RemoveMenu(int id)
        {
            try
            {
                this.systemMenuService = new SystemMenuService();
                this.systemMenuService.RemoveMenuByID(id);

                return this.RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改系统菜单
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="menu">
        /// 菜单对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult ModifyMenu([DataSourceRequest] DataSourceRequest request, MenuModel menu)
        {
            if (menu == null || !this.ModelState.IsValid)
            {
                return this.Json(new[] { menu }.ToDataSourceResult(request, this.ModelState));
            }

            try
            {
                this.systemMenuService = new SystemMenuService();

                var systemMenu = DataTransfer.Transfer<System_Menu>(menu, typeof(MenuModel));
                this.systemMenuService.ModifyMenu(systemMenu);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(new[] { menu }.ToDataSourceResult(request, this.ModelState));
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
        public ActionResult QueryMenu([DataSourceRequest] DataSourceRequest request)
        {
            this.systemMenuService = new SystemMenuService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Menu]", null, "ID", string.Format("ParentID = {0}", 0), request.Page, request.PageSize);

            var list = this.systemMenuService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var menuModels = new List<MenuModel>();
            foreach (var systemMenu in list)
            {
                menuModels.Add(DataTransfer.Transfer<MenuModel>(systemMenu, typeof(System_Menu)));
            }

            var result = new DataSourceResult { Data = menuModels, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询子权限列表
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="menuID">
        /// 上级权限编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public ActionResult QueryMenuByID([DataSourceRequest] DataSourceRequest request, int menuID)
        {
            this.systemMenuService = new SystemMenuService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Menu]", null, "ID", string.Format("ParentID = {0}", menuID), request.Page, request.PageSize);

            var list = this.systemMenuService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var menuModels = new List<MenuModel>();
            foreach (var systemMenu in list)
            {
                menuModels.Add(DataTransfer.Transfer<MenuModel>(systemMenu, typeof(System_Menu)));
            }

            var result = new DataSourceResult { Data = menuModels, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
