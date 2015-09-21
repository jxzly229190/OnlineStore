// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRoleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色数据库访问类
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
    /// 系统角色数据库访问类
    /// </summary>
    public class SystemRoleDA : ISystemRoleDA
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
        /// 添加角色
        /// </summary>
        /// <param name="role">
        /// 角色对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 主键编号
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public int Insert(System_Role role, out SqlTransaction transaction)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         role.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Headcount",
                                         SqlDbType.Int,
                                         role.Headcount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         role.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.BeginTransaction();
                transaction = this.SqlServer.Transaction;

                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Role_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                this.SqlServer.RollbackTransaction();
                throw new Exception(exception.Message, exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的角色
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        public void DeleteByID(int roleID, SqlTransaction transaction)
        {
            if (roleID <= 0)
            {
                throw new ArgumentNullException("roleID");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         roleID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Role_DeleteRow", parameters, transaction);
            }
            catch (Exception exception)
            {
                this.SqlServer.RollbackTransaction();
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询角色列表
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
        /// 角色列表
        /// </returns>
        public List<System_Role> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                return this.SqlServer.Paging<System_Role>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询所有系统角色
        /// </summary>
        /// <returns>
        /// 角色列表
        /// </returns>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public List<System_Role> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Role_SelectAll", null, null);
                var list = dataReader.ToList<System_Role>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return null;
        }

        /// <summary>
        /// 查询所有系统角色以及角色相关的用户
        /// </summary>
        /// <returns>
        /// 角色列表
        /// </returns>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public List<System_Role_User> SelectAllWithUser()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Role_SelectAllWithUser", null, null);
                var list = dataReader.ToList<System_Role_User>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return null;
        }

        #endregion
    }
}