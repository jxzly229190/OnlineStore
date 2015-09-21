// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Role.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色控制器部分类
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
    /// 系统角色控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统角色服务对象
        /// </summary>
        private SystemRoleService systemRoleService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 系统角色首页执行方法
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Role()
        {
            return this.PartialView("Role");
        }

        /// <summary>
        /// 添加系统角色执行方法
        /// </summary>
        /// <param name="checkedItems">
        /// 权限编号字符串
        /// </param>
        /// <param name="roleName">
        /// 角色名称
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行操作异常
        /// </exception>
        [HttpPost]
        public ActionResult AddRole(string checkedItems, string roleName)
        {
            try
            {
                var rolePermissions = new List<System_Role_Permission>();
                if (string.IsNullOrEmpty(checkedItems))
                {
                    rolePermissions = null;
                }
                else
                {
                    checkedItems = checkedItems.Substring(1, checkedItems.Length - 1);

                    var permissionIDs = checkedItems.Split(',');
                    foreach (var permissionID in permissionIDs)
                    {
                        rolePermissions.Add(new System_Role_Permission { PermissionID = Convert.ToInt32(permissionID) });
                    }
                }

                this.systemRoleService = new SystemRoleService();

                var systemRole = new System_Role { Name = roleName, Headcount = 0, CreateTime = DateTime.Now };
                systemRole.ID = this.systemRoleService.AddRole(systemRole, rolePermissions);

                return this.Content("1");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 移除系统角色执行方法
        /// </summary>
        /// <param name="id">
        /// 角色编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpPost]
        public ActionResult RemoveRole(int id)
        {
            try
            {
                this.systemRoleService = new SystemRoleService();
                this.systemRoleService.RemoveRoleByID(id);
                return this.RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询系统角色列表
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <returns>
        /// 执行结果
        /// </returns>
        public ActionResult QueryRole([DataSourceRequest] DataSourceRequest request)
        {
            this.systemRoleService = new SystemRoleService();

            int pageCount;
            int totalCount;

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var paging = new Paging("[System_Role]", null, "ID", null, request.Page, request.PageSize);

            var list = this.systemRoleService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var roleModels = new List<RoleModel>();
            foreach (var systemRole in list)
            {
                roleModels.Add(DataTransfer.Transfer<RoleModel>(systemRole, typeof(System_Role)));
            }

            var result = new DataSourceResult { Data = roleModels, Total = totalCount };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询所有系统角色
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public JsonResult QueryRoleSelectListItems()
        {
            this.systemRoleService = new SystemRoleService();

            var list = this.systemRoleService.QueryAll();
            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询所有系统角色和角色相关的用户
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        public JsonResult QueryRoleWithUser()
        {
            this.systemRoleService = new SystemRoleService();

            var list = this.systemRoleService.QueryAllWithUser();
            return list != null ? this.Json(list, JsonRequestBehavior.AllowGet) : this.Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
