// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemEmployeeService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统员工用户服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统员工用户服务类
    /// </summary>
    public class SystemEmployeeService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统员工数据库访问对象
        /// </summary>
        private readonly ISystemEmployeeDA systemEmployeeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemEmployeeService"/> class.
        /// </summary>
        public SystemEmployeeService()
        {
            this.systemEmployeeDA = new DAFactorySystem().CreateSystemEmployeeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="employee">
        /// 员工对象
        /// </param>
        /// <returns>
        /// 员工编号
        /// </returns>
        public int AddEmployee(System_Employee employee)
        {
            return this.systemEmployeeDA.Insert(employee);
        }

        /// <summary>
        /// 删除指定编号的员工
        /// </summary>
        /// <param name="employeeID">
        /// 员工编号
        /// </param>
        public void RemoveEmployeeByID(int employeeID)
        {
            this.systemEmployeeDA.DeleteByID(employeeID);
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="employee">
        /// 员工对象
        /// </param>
        public void ModifyEmployee(System_Employee employee)
        {
            this.systemEmployeeDA.Update(employee);
        }

        /// <summary>
        /// 查询指定编号的员工
        /// </summary>
        /// <param name="employeeID">
        /// 员工编号
        /// </param>
        /// <returns>
        /// 员工对象
        /// </returns>
        public System_Employee QueryByID(int employeeID)
        {
            return this.systemEmployeeDA.SelectByID(employeeID);
        }

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 员工列表
        /// </returns>
        public List<System_Employee> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemEmployeeDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询所有员工列表
        /// </summary>
        /// <returns>
        /// 员工列表
        /// </returns>
        public List<System_Employee> QueryAll()
        {
            return this.systemEmployeeDA.SelectAll();
        }

        #endregion
    }
}