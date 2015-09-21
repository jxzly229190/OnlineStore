// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemMenuDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统菜单数据库访问类
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
    /// 系统菜单数据库访问类
    /// </summary>
    public class SystemMenuDA : ISystemMenuDA
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
        /// 添加菜单
        /// </summary>
        /// <param name="menu">
        /// 菜单对象
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
        public int Insert(System_Menu menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PermissionID",
                                         SqlDbType.Int,
                                         menu.PermissionID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         menu.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Layer",
                                         SqlDbType.Int,
                                         menu.Layer,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         menu.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "URL",
                                         SqlDbType.VarChar,
                                         menu.URL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         menu.Sorting,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         menu.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Menu_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemMenuDA - Insert", exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的菜单
        /// </summary>
        /// <param name="menuID">
        /// 菜单编号
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public void DeleteByID(int menuID)
        {
            if (menuID <= 0)
            {
                throw new ArgumentNullException("menuID");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         menuID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Menu_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemMenuDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu">
        /// 菜单对象
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public void Update(System_Menu menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         menu.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PermissionID",
                                         SqlDbType.Int,
                                         menu.PermissionID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         menu.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Layer",
                                         SqlDbType.Int,
                                         menu.Layer,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         menu.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "URL",
                                         SqlDbType.VarChar,
                                         menu.URL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         menu.Sorting,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Menu_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemMenuDA - Update", exception);
            }
        }

        /// <summary>
        /// 查询菜单列表
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
        /// 菜单列表
        /// </returns>
        public List<System_Menu> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            

            try
            {
                return this.SqlServer.Paging<System_Menu>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询所有系统菜单
        /// </summary>
        /// <returns>
        /// 系统菜单列表
        /// </returns>
        /// <exception cref="Exception">
        /// 执行操作异常
        /// </exception>
        public List<System_Menu> SelectAll()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Menu_SelectAll", null, null);
                if (!dataReader.HasRows)
                {
                    return null;
                }

                return dataReader.ToList<System_Menu>();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询指定角色编号的菜单列表
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <returns>
        /// 菜单列表
        /// </returns>
        /// <exception cref="Exception">
        /// 执行操作异常
        /// </exception>
        public List<System_Menu> SelectByRoleID(int roleID)
        {
            

            try
            {
                var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         roleID,
                                         ParameterDirection.Input)
                                 };

                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Menu_SelectByRoleID", parameters, null);
                if (!dataReader.HasRows)
                {
                    return null;
                }

                return dataReader.ToList<System_Menu>();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion
    }
}