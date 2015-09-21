// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemDepartmentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统部门数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;

    /// <summary>
    /// 系统部门数据库访问接口
    /// </summary>
    public interface ISystemDepartmentDA
    {
        #region Public Methods and Operators

        int Insert(System_Department department);

        void DeleteByID(int departmentID);

        void Update(System_Department department);

        void UpdateHeadcount(int departmentID);

        System_Department SelectByID(int departmentID);

        List<System_Department> SelectAll();

        #endregion
    }
}