// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemLogDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统日志类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统用户数据库访问类
    /// </summary>
    public class SystemLogDA : ISystemLogDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Public Methods and Operators
        
        /// <summary>
        /// 删除指定编号的用户
        /// </summary>
        /// <param name="userID">
        /// 用户编号
        /// </param>
        public void DeleteByID(int logID)
        {
            if (logID <= 0)
            {
                throw new ArgumentNullException("logID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         logID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Log_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemLogDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 查询用户列表
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
        /// 用户列表
        /// </returns>
        public List<System_Log> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                return this.SqlServer.Paging<System_Log>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询指定编号的系统日志
        /// </summary>
        /// <param name="logID">
        /// 日志编码
        /// </param>
        /// <returns>
        /// 系统日志对象
        /// </returns>
        public System_Log SelectByID(int logID)
        {
            if (logID <= 0)
            {
                throw new ArgumentNullException("logID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.VarChar,
                                         logID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Log_SelectRow", parameters, null);
                var list = dataReader.ToList<System_Log>();
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemLogDA - SelectByID", exception);
            }

            return null;
        }

        #endregion
    }
}