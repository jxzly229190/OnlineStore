// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemPermissionDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限数据库访问类
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
    /// 系统权限数据库访问类
    /// </summary>
    public class SystemPermissionDA : ISystemPermissionDA
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
        /// 添加权限
        /// </summary>
        /// <param name="permission">
        /// 权限对象
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
        public int Insert(System_Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         permission.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         permission.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Layer",
                                         SqlDbType.Int,
                                         permission.Layer,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         permission.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Permission_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的权限
        /// </summary>
        /// <param name="permissionID">
        /// 权限编号
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public void DeleteByID(int permissionID)
        {
            if (permissionID <= 0)
            {
                throw new ArgumentNullException("permissionID");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         permissionID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Permission_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 操作数据库异常
        /// </exception>
        public void Update(System_Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         permission.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         permission.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         permission.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Layer",
                                         SqlDbType.Int,
                                         permission.Layer,
                                         ParameterDirection.Input),
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Permission_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询权限列表
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
        /// 权限列表
        /// </returns>
        public List<System_Permission> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            

            try
            {
                return this.SqlServer.Paging<System_Permission>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询所有系统权限
        /// </summary>
        /// <returns>
        /// 系统权限列表
        /// </returns>
        /// <exception cref="Exception">
        /// 执行操作异常
        /// </exception>
        public List<System_Permission> SelectAll()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Permission_SelectAll", null, null);
                var list = dataReader.ToList<System_Permission>();
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
        /// 指定系统权限编号是否拥有子权限
        /// </summary>
        /// <param name="permissionID">
        /// 权限编号
        /// </param>
        /// <returns>
        /// 是否拥有子权限
        /// </returns>
        /// <exception cref="Exception">
        /// 执行操作异常
        /// </exception>
        public bool HasChildren(int permissionID)
        {
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         permissionID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ChildrenCount",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Permission_HasChildren", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return (int)parameters[1].Value > 0;
        }

        #endregion
    }
}