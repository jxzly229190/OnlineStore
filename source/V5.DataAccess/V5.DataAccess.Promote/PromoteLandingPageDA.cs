// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteLandingPageDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   LP（LandingPage）数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// LP（LandingPage）数据访问类.
    /// </summary>
    public class PromoteLandingPageDA : IPromoteLandingPageDA
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
        /// 添加Promote_LandingPage.
        /// </summary>
        /// <param name="landingPage">
        /// Promote_LandingPage对象实例.
        /// </param>
        /// <returns>
        /// Promote_LandingPage编号.
        /// </returns>
        public int Insert(Promote_LandingPage landingPage)
        {
            if (landingPage == null)
            {
                throw new ArgumentNullException("landingPage");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.VarChar,
                                         landingPage.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         landingPage.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         landingPage.StartTime,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         landingPage.EndTime,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "Link",
                                         SqlDbType.VarChar,
                                         landingPage.Link,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "Content",
                                         SqlDbType.Text,
                                         landingPage.Content,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         landingPage.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MasterPicture",
                                         SqlDbType.VarChar,
                                         landingPage.MasterPicture,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture01",
                                         SqlDbType.VarChar,
                                         landingPage.Picture01,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture02",
                                         SqlDbType.VarChar,
                                         landingPage.Picture02,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture03",
                                         SqlDbType.VarChar,
                                         landingPage.Picture03,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture04",
                                         SqlDbType.VarChar,
                                         landingPage.Picture04,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture05",
                                         SqlDbType.VarChar,
                                         landingPage.Picture05,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
								 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_LandingPage_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 更新Promote_LandingPage.
        /// </summary>
        /// <param name="landingPage">
        /// Promote_LandingPage对象实例.
        /// </param>
        public void Update(Promote_LandingPage landingPage)
        {
            if (landingPage == null)
            {
                throw new ArgumentNullException("landingPage");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         landingPage.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PID",
                                         SqlDbType.Int,
                                         landingPage.PID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.VarChar,
                                         landingPage.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         landingPage.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         landingPage.StartTime,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         landingPage.EndTime,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "Link",
                                         SqlDbType.VarChar,
                                         landingPage.Link,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "Content",
                                         SqlDbType.Text,
                                         landingPage.Content,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MasterPicture",
                                         SqlDbType.VarChar,
                                         landingPage.MasterPicture,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture01",
                                         SqlDbType.VarChar,
                                         landingPage.Picture01,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture02",
                                         SqlDbType.VarChar,
                                         landingPage.Picture02,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture03",
                                         SqlDbType.VarChar,
                                         landingPage.Picture03,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture04",
                                         SqlDbType.VarChar,
                                         landingPage.Picture04,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Picture05",
                                         SqlDbType.VarChar,
                                         landingPage.Picture05,
                                         ParameterDirection.Input)
								 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_LandingPage_Update", parameters, null);
        }

        /// <summary>
        /// 查询所有Promote_LandingPage.
        /// </summary>
        /// <returns>
        /// Promote_LandingPage列表.
        /// </returns>
        public List<Promote_LandingPage> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_LandingPage_SelectAll",
                null,
                null);
            return dataReader.ToList<Promote_LandingPage>();
        }

        /// <summary>
        /// 查询指定编号的LP.
        /// </summary>
        /// <param name="ID">
        /// 编号.
        /// </param>
        /// <returns>
        /// Promote_LandingPage对象实例.
        /// </returns>
        public Promote_LandingPage SelectRowByID(int ID)
        {
            if (ID <= 0)
            {
                throw new ArgumentNullException("ID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         ID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_LandingPage_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_LandingPage>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 查询LP列表（编码、父级编码、名称、制作人）.
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
        /// LP列表
        /// </returns>
        public List<Promote_LandingPage> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_LandingPage>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 删除指定编号的LP信息.
        /// </summary>
        /// <param name="ID">
        /// LP编号.
        /// </param>
        public void Delete(int ID)
        {
            if (ID <= 0)
            {
                throw new ArgumentNullException("ID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         ID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_LandingPage_Delete",
                parameters,
                null);
        }

        #endregion
    }
}
