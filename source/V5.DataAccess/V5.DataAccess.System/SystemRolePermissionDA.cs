// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRolePermissionDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色权限关系数据库访问类
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
    /// 系统角色权限关系数据库访问类
    /// </summary>
    public class SystemRolePermissionDA : ISystemRolePermissionDA
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
        /// 添加角色权限关系
        /// </summary>
        /// <param name="rolePermission">
        /// 角色权限关系对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        public void Insert(System_Role_Permission rolePermission, SqlTransaction transaction)
        {
            if (rolePermission == null)
            {
                throw new ArgumentNullException("rolePermission");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PermissionID",
                                         SqlDbType.Int,
                                         rolePermission.PermissionID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         rolePermission.RoleID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Role_Permission_Insert", parameters, transaction);
            }
            catch (Exception exception)
            {
                this.SqlServer.RollbackTransaction();
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 删除指定角色编号的角色权限关系
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        public void DeleteByID(int roleID, out SqlTransaction transaction)
        {
            if (roleID <= 0)
            {
                throw new ArgumentNullException("roleID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         roleID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.BeginTransaction();
                transaction = this.SqlServer.Transaction;

                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Role_Permission_DeleteRow", parameters, transaction);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询指定角色编号的角色权限关系列表
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <returns>
        /// 角色权限关系列表
        /// </returns>
        public List<System_Role_Permission> SelectByRoleID(int roleID)
        {
            if (roleID <= 0)
            {
                throw new ArgumentNullException("roleID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         roleID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Role_Permission_SelectByRoleID", parameters, null);
                var list = dataReader.ToList<System_Role_Permission>();
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