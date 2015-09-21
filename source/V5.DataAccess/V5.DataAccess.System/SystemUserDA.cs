// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemUserDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统用户数据库访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;

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
    public class SystemUserDA : ISystemUserDA
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
        /// 添加系统用户
        /// </summary>
        /// <param name="user">
        /// 系统用户对象
        /// </param>
        /// <returns>
        /// 主键编号
        /// </returns>
        public int Insert(System_User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         user.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         user.RoleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "LoginName",
                                         SqlDbType.VarChar,
                                         user.LoginName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "LoginPassword",
                                         SqlDbType.VarChar,
                                         user.LoginPassword,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         user.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         user.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         user.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_User_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemUserDA - Insert", exception);
            }

            return id;
        }

        /// <summary>
        /// 删除指定编号的用户
        /// </summary>
        /// <param name="userID">
        /// 用户编号
        /// </param>
        public void DeleteByID(int userID)
        {
            if (userID <= 0)
            {
                throw new ArgumentNullException("userID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         userID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_User_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemUserDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="user">
        /// 系统用户对象
        /// </param>
        public void Update(System_User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         user.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         user.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         user.RoleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         user.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "LoginName",
                                         SqlDbType.NVarChar,
                                         user.LoginName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         user.Status,
                                         ParameterDirection.Input)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_User_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemUserDA - Update", exception);
            }
        }

        /// <summary>
        /// 查询指定编号的系统用户
        /// </summary>
        /// <param name="loginName">
        /// 登录名
        /// </param>
        /// <returns>
        /// 系统用户对象
        /// </returns>
        public System_User SelectByLoginName(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                throw new ArgumentNullException("loginName");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "LoginName",
                                         SqlDbType.VarChar,
                                         loginName,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_User_SelectRow", parameters, null);
                var list = dataReader.ToList<System_User>();
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemUserDA - SelectByID", exception);
            }

            return null;
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
        public List<System_User> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                return this.SqlServer.Paging<System_User>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// The is user name exists.
        /// </summary>
        /// <param name="loginName">
        /// The login name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int IsLoginNameExists(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                throw new ArgumentNullException("loginName");
            }

            var parameters = new List<SqlParameter>
                                 {
                                         this.SqlServer.CreateSqlParameter(
                                         "LoginName",
                                         SqlDbType.NVarChar,
                                         loginName,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_System_User_Exists_LoginName", parameters, null);
                return dataReader.HasRows ? 1 : 0;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion

        /// <summary>
        /// 修改密码
        /// </summary
        /// <param name="userId">用户ID</param>
        /// <param name="loginpassword">密码</param>
        /// <returns></returns>
        public int UpdatePassWord(int userId, string loginpassword)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter("ID", SqlDbType.Int,userId , ParameterDirection.Input),
                this.SqlServer.CreateSqlParameter("LoginPassword", SqlDbType.NVarChar, loginpassword,
                    ParameterDirection.Input)
            };
            try
            {
                int retrunValue = this.sqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_User_Update_Password", parameters,
                     null);
                return retrunValue;
            }
            catch (Exception exception)
            {
                throw new ArgumentNullException(exception.Message, exception);
            }
        }
    }
}