// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemEmployeeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统员工数据库访问类
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
    /// 系统员工数据库访问类
    /// </summary>
    public class SystemEmployeeDA : ISystemEmployeeDA
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
        /// 添加员工
        /// </summary>
        /// <param name="employee">
        /// 员工对象
        /// </param>
        /// <returns>
        /// 主键编号
        /// </returns>
        public int Insert(System_Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }
            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "DepartmentID",
                                         SqlDbType.Int,
                                         employee.DepartmentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CountyID",
                                         SqlDbType.Int,
                                         employee.CountyID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IdentityCard",
                                         SqlDbType.VarChar,
                                         employee.IdentityCard,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IdentityCardAddress",
                                         SqlDbType.NVarChar,
                                         employee.IdentityCardAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BankCard",
                                         SqlDbType.VarChar,
                                         employee.BankCard ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         employee.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Age",
                                         SqlDbType.Int,
                                         employee.Age,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Gender",
                                         SqlDbType.NVarChar,
                                         employee.Gender,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Mobile",
                                         SqlDbType.VarChar,
                                         employee.Mobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "HomeAddress",
                                         SqlDbType.NVarChar,
                                         employee.HomeAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         employee.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         employee.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Employee_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemEmployeeDA - Insert", exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的员工
        /// </summary>
        /// <param name="employeeID">
        /// 员工编号
        /// </param>
        public void DeleteByID(int employeeID)
        {
            if (employeeID <= 0)
            {
                throw new ArgumentNullException("employeeID");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         employeeID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Employee_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemEmployeeDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="employee">
        /// 员工对象
        /// </param>
        public void Update(System_Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         employee.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DepartmentID",
                                         SqlDbType.Int,
                                         employee.DepartmentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CountyID",
                                         SqlDbType.Int,
                                         employee.CountyID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IdentityCard",
                                         SqlDbType.VarChar,
                                         employee.IdentityCard,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IdentityCardAddress",
                                         SqlDbType.NVarChar,
                                         employee.IdentityCardAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BankCard",
                                         SqlDbType.VarChar,
                                         employee.BankCard ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         employee.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Age",
                                         SqlDbType.Int,
                                         employee.Age,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Gender",
                                         SqlDbType.NVarChar,
                                         employee.Gender,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Mobile",
                                         SqlDbType.VarChar,
                                         employee.Mobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "HomeAddress",
                                         SqlDbType.NVarChar,
                                         employee.HomeAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         employee.Status,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Employee_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemEmployeeDA - Update", exception);
            }
        }

        /// <summary>
        /// 查询指定编号的员工
        /// </summary>
        /// <param name="employeeID">
        /// 员工编号
        /// </param>
        /// <returns>
        /// System_Employee 的实例
        /// </returns>
        public System_Employee SelectByID(int employeeID)
        {
            if (employeeID <= 0)
            {
                throw new ArgumentNullException("employeeID");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         employeeID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Employee_SelectRow", parameters, null);
                var list = dataReader.ToList<System_Employee>();
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemEmployeeDA - SelectByID", exception);
            }

            return null;
        }

        /// <summary>
        /// 查询员工列表
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
        /// 员工列表
        /// </returns>
        public List<System_Employee> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                return this.SqlServer.Paging<System_Employee>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询所有员工
        /// </summary>
        /// <returns>
        /// 员工列表
        /// </returns>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public List<System_Employee> SelectAll()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_Employee_SelectAll", null, null);
                var list = dataReader.ToList<System_Employee>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemEmployeeDA - SelectAll", exception);
            }

            return null;
        }

        #endregion
    }
}