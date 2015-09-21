// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemDepartmentService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统部门服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;

    /// <summary>
    /// 系统部门服务类
    /// </summary>
    public class SystemDepartmentService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统部门数据库访问对象
        /// </summary>
        private readonly ISystemDepartmentDA systemDepartmentDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemDepartmentService"/> class.
        /// </summary>
        public SystemDepartmentService()
        {
            this.systemDepartmentDA = new DAFactorySystem().CreateSystemDepartmentDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">
        /// 部门对象
        /// </param>
        /// <returns>
        /// 部门编号
        /// </returns>
        public int AddDepartment(System_Department department)
        {
            return this.systemDepartmentDA.Insert(department);
        }

        /// <summary>
        /// 删除指定编号的部门
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        public void RemoveDepartmentByID(int departmentID)
        {
            this.systemDepartmentDA.DeleteByID(departmentID);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">
        /// 部门对象
        /// </param>
        public void ModifyDepartment(System_Department department)
        {
            this.systemDepartmentDA.Update(department);
        }

        /// <summary>
        /// 更新部门人数
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        public void ModifyDepartmentHeadcount(int departmentID)
        {
            this.systemDepartmentDA.UpdateHeadcount(departmentID);
        }

        /// <summary>
        /// 查询指定编号的部门
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        /// <returns>
        /// 部门对象
        /// </returns>
        public System_Department QueryByID(int departmentID)
        {
            return this.systemDepartmentDA.SelectByID(departmentID);
        }

        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns>
        /// 部门列表
        /// </returns>
        public List<System_Department> QueryAll()
        {
            return this.systemDepartmentDA.SelectAll();
        }

        #endregion
    }
}