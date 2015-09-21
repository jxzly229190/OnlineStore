// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemLogDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统日志表
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;
    using global::System.Data;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The ISystemLogDA interface.
    /// </summary>
    public interface ISystemLogDA
    {                
        /// <summary>
        /// 删除系统日志
        /// </summary>
        /// <param name="rights">
        /// 系统日志对象
        /// </param>
        void DeleteByID(int logID);

        /// <summary>
        /// 查询系统日志
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        System_Log SelectByID(int logID);

        /// <summary>
        /// 查询系统日志
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<System_Log> Paging(Paging paging, out int pageCount, out int totalCount);
    }
}