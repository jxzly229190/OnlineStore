// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.Employee.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统员工控制器部分类
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
    /// 系统员工控制器部分类
    /// </summary>
    public partial class SystemController
    {
        #region Constants and Fields

        /// <summary>
        /// The backstage employee service.
        /// </summary>
        private SystemEmployeeService systemEmployeeService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Employee()
        {
            return this.PartialView("Employee");
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="employeeModel">
        /// The employee model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// s
        /// </exception>
        [HttpPost]
        public ActionResult AddEmployee([DataSourceRequest] DataSourceRequest request, EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    this.systemEmployeeService = new SystemEmployeeService();

                    var backstageEmployee = DataTransfer.Transfer<System_Employee>(
                        employeeModel,
                        typeof(EmployeeModel));

                    employeeModel.ID = this.systemEmployeeService.AddEmployee(backstageEmployee);

                    if (employeeModel.ID > 0)
                    {
                        return this.Json(new[] { employeeModel }.ToDataSourceResult(request, this.ModelState));
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("添加员工时发生错误", exception);
            }

            return this.View();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult RemoveEmployee(int id)
        {
            return this.View();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult RemoveEmployee(int id, FormCollection collection)
        {
            try
            {
                this.systemEmployeeService = new SystemEmployeeService();

                this.systemEmployeeService.RemoveEmployeeByID(id);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="employeeModel">
        /// The employee model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ModifyEmployee([DataSourceRequest] DataSourceRequest request, EmployeeModel employeeModel)
        {
            if (employeeModel != null && this.ModelState.IsValid)
            {
                this.systemEmployeeService = new SystemEmployeeService();

                var backstageEmployee = DataTransfer.Transfer<System_Employee>(
                    employeeModel,
                    typeof(EmployeeModel));

                this.systemEmployeeService.ModifyEmployee(backstageEmployee);
            }

            return this.Json(new[] { employeeModel }.ToDataSourceResult(request, this.ModelState));
        }

        /// <summary>
        /// The editing inline_ read.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="departmentID">
        /// The department ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryEmployeeByDepartmentID([DataSourceRequest] DataSourceRequest request, int departmentID)
        {
            this.systemEmployeeService = new SystemEmployeeService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int pageCount;
            int totalCount;

            var paging = new Paging("[System_Employee]", null, "ID", string.Format("DepartmentID = {0}", departmentID), request.Page, request.PageSize);

            var list = this.systemEmployeeService.Query(paging, out pageCount, out totalCount);
            if (list == null)
            {
                return this.View();
            }

            var modelList = new List<EmployeeModel>();
            foreach (var backstageEmployee in list)
            {
                modelList.Add(DataTransfer.Transfer<EmployeeModel>(backstageEmployee, typeof(System_Employee)));
            }

            var result = new DataSourceResult { Data = modelList, Total = totalCount };

            return this.Json(result);
        }

        /// <summary>
        /// The query select list items.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryEmployeeSelectListItems()
        {
            this.systemEmployeeService = new SystemEmployeeService();

            var list = this.systemEmployeeService.QueryAll();
            if (list != null)
            {
                return this.Json(list, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null);
        }

        /// <summary>
        /// The query employee status.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryEmployeeStatus()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem { Text = "在职", Value = "0" },
                    new SelectListItem { Text = "离职", Value = "1" }
                };

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
