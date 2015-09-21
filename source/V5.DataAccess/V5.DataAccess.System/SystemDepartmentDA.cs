// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemDepartmentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统部门数据库访问类
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
    /// 系统部门数据库访问类
    /// </summary>
    public class SystemDepartmentDA : ISystemDepartmentDA
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
        /// 增加部门
        /// </summary>
        /// <param name="department">
        /// 部门对象
        /// </param>
        /// <returns>
        /// 主键编号
        /// </returns>
        public int Insert(System_Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         department.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Principal",
                                         SqlDbType.NVarChar,
                                         department.Principal,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PrincipalMobile",
                                         SqlDbType.NVarChar,
                                         department.PrincipalMobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Headcount",
                                         SqlDbType.Int,
                                         department.Headcount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         department.Description ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         department.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Department_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - Insert", exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的部门
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        public void DeleteByID(int departmentID)
        {
            if (departmentID <= 0)
            {
                throw new ArgumentNullException("departmentID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         departmentID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Department_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">
        /// 部门对象
        /// </param>
        public void Update(System_Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         department.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         department.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Principal",
                                         SqlDbType.NVarChar,
                                         department.Principal,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PrincipalMobile",
                                         SqlDbType.NVarChar,
                                         department.PrincipalMobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Headcount",
                                         SqlDbType.Int,
                                         department.Headcount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         department.Description,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Department_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - Update", exception);
            }
        }

        /// <summary>
        /// 更新部门人数
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        public void UpdateHeadcount(int departmentID)
        {
            if (departmentID <= 0)
            {
                throw new ArgumentNullException("departmentID");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         departmentID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Department_Update_Headcount", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - UpdateHeadcount", exception);
            }
        }

        /// <summary>
        /// 查询指定编号的部门
        /// </summary>
        /// <param name="departmentID">
        /// 部门编号
        /// </param>
        /// <returns>
        /// System_Department 的实例
        /// </returns>
        public System_Department SelectByID(int departmentID)
        {
            if (departmentID <= 0)
            {
                throw new ArgumentNullException("departmentID");
            }

            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         departmentID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Department_SelectRow", parameters, null);
                var list = dataReader.ToList<System_Department>();
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - SelectByID", exception);
            }

            return null;
        }

        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns>
        /// 部门对象列表
        /// </returns>
        public List<System_Department> SelectAll()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Department_SelectAll", null, null);
                var list = dataReader.ToList<System_Department>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemDepartmentDA - SelectAll", exception);
            }

            return null;
        }

        #endregion
    }
}