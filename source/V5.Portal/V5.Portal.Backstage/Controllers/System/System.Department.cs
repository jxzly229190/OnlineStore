// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Department.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统部门控制器部分类
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
    using V5.Portal.Backstage.Models.System;
    using V5.Service.System;

    /// <summary>
    /// 系统部门控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// 系统部门服务对象
        /// </summary>
        private SystemDepartmentService systemDepartmentService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Department()
        {
            return this.PartialView("Department");
        }

        /// <summary>
        /// 添加系统部门
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="departmentModel">
        /// 部门模型对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult AddDepartment([DataSourceRequest] DataSourceRequest request, DepartmentModel departmentModel)
        {
            try
            {
                if (departmentModel != null)
                {
                    this.systemDepartmentService = new SystemDepartmentService();

                    var systemDepartment = DataTransfer.Transfer<System_Department>(departmentModel, typeof(DepartmentModel));
                    systemDepartment.CreateTime = DateTime.Now;

                    departmentModel.ID = this.systemDepartmentService.AddDepartment(systemDepartment);

                    if (departmentModel.ID > 0)
                    {
                        return this.Json(new[] { departmentModel }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加部门时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// 移除指定编号的系统部门
        /// </summary>
        /// <param name="id">
        /// 系统部门编号
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        [HttpGet]
        public ActionResult RemoveDepartment(int id)
        {
            return this.View();
        }

        /// <summary>
        /// 移除指定编号的系统部门
        /// </summary>
        /// <param name="id">
        /// 系统部门编号
        /// </param>
        /// <param name="collection">
        /// 表单集合
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult RemoveDepartment(int id, FormCollection collection)
        {
            try
            {
                this.systemDepartmentService = new SystemDepartmentService();
                this.systemDepartmentService.RemoveDepartmentByID(id);

                return this.RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改系统部门
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <param name="departmentModel">
        /// 部门模型对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        [HttpPost]
        public ActionResult ModifyDepartment([DataSourceRequest] DataSourceRequest request, DepartmentModel departmentModel)
        {
            if (departmentModel != null && this.ModelState.IsValid)
            {
                try
                {
                    this.systemDepartmentService = new SystemDepartmentService();

                    var backstageDepartment = DataTransfer.Transfer<System_Department>(
                        departmentModel,
                        typeof(DepartmentModel));

                    this.systemDepartmentService.ModifyDepartment(backstageDepartment);
                }
                catch (Exception exception)
                {
                    throw new Exception("删除部门时发生错误", exception);
                }
            }

            return this.Json(new[] { departmentModel }.ToDataSourceResult(request, this.ModelState));
        }

        /// <summary>
        /// 查询所有系统部门
        /// </summary>
        /// <param name="request">
        /// 数据源请求信息对象
        /// </param>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法异常
        /// </exception>
        public ActionResult QueryAllDepartment([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                this.systemDepartmentService = new SystemDepartmentService();

                var list = this.systemDepartmentService.QueryAll();
                if (list != null)
                {
                    var modelList = new List<DepartmentModel>();

                    foreach (var backstageDepartment in list)
                    {
                        modelList.Add(DataTransfer.Transfer<DepartmentModel>(backstageDepartment, typeof(System_Department)));
                    }

                    return this.Json(modelList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("查询所有部门时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// The select list items.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// 执行方法结果
        /// </exception>
        [HttpGet]
        public JsonResult QueryDepartmentSelectListItems()
        {
            try
            {
                this.systemDepartmentService = new SystemDepartmentService();

                var list = this.systemDepartmentService.QueryAll();
                if (list != null)
                {
                    return this.Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("查询部门选择列表出现错误", exception);
            }

            return this.Json(null);
        }

        #endregion
    }
}
