// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemEmployeeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统员工数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统员工数据库访问接口
    /// </summary>
    public interface ISystemEmployeeDA
    {
        #region Public Methods and Operators

        int Insert(System_Employee employee);

        void DeleteByID(int employeeID);

        void Update(System_Employee employee);

        System_Employee SelectByID(int employeeID);

        List<System_Employee> Paging(Paging paging, out int pageCount, out int totalCount);

        List<System_Employee> SelectAll();

        #endregion
    }
}