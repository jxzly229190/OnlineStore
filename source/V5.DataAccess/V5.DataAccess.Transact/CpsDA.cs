// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact;
    using V5.Library.Storage.DB;

    /// <summary>
    /// Cps数据访问类.
    /// </summary>
    public class CpsDA : ICpsDA
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
        /// 添加CPS信息.
        /// </summary>
        /// <param name="cps">
        /// The cps.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Cps cps)
        {
            if (cps == null)
            {
                throw new ArgumentNullException("cps");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         cps.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserName",
                                         SqlDbType.NVarChar,
                                         cps.UserName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "URL",
                                         SqlDbType.NVarChar,
                                         cps.URL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Linkman",
                                         SqlDbType.NVarChar,
                                         cps.Linkman,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Mobile",
                                         SqlDbType.VarChar,
                                         cps.Mobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Tel",
                                         SqlDbType.VarChar,
                                         cps.Tel,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Email",
                                         SqlDbType.VarChar,
                                         cps.Email,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "QQ",
                                         SqlDbType.VarChar,
                                         cps.QQ,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Company",
                                         SqlDbType.NVarChar,
                                         cps.Company,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CompanyAddress",
                                         SqlDbType.NVarChar,
                                         cps.CompanyAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ZipCode",
                                         SqlDbType.VarChar,
                                         cps.ZipCode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TrackingURL",
                                         SqlDbType.VarChar,
                                         cps.TrackingURL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         cps.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Cps_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询CPS列表
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
        /// CPS列表
        /// </returns>
        public List<Cps> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Cps>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 更新Cps信息.
        /// </summary>
        /// <param name="cps">
        /// Cps的对象实例.
        /// </param>
        public void Update(Cps cps)
        {
            if (cps == null)
            {
                throw new ArgumentNullException("cps");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.NVarChar,
                                         cps.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         cps.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserName",
                                         SqlDbType.NVarChar,
                                         cps.UserName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "URL",
                                         SqlDbType.NVarChar,
                                         cps.URL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Linkman",
                                         SqlDbType.NVarChar,
                                         cps.Linkman,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Mobile",
                                         SqlDbType.VarChar,
                                         cps.Mobile,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Tel",
                                         SqlDbType.VarChar,
                                         cps.Tel,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Email",
                                         SqlDbType.VarChar,
                                         cps.Email,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "QQ",
                                         SqlDbType.VarChar,
                                         cps.QQ,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Company",
                                         SqlDbType.NVarChar,
                                         cps.Company,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CompanyAddress",
                                         SqlDbType.NVarChar,
                                         cps.CompanyAddress,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ZipCode",
                                         SqlDbType.VarChar,
                                         cps.ZipCode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TrackingURL",
                                         SqlDbType.VarChar,
                                         cps.TrackingURL,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         cps.CreateTime,
                                         ParameterDirection.Input),
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Cps_Update", parameters, null);
        }

        /// <summary>
        /// 查询所有CPS信息
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Cps> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Cps_SelectAll",
                null,
                null);
            var list = dataReader.ToList<Cps>();
            return list.Count > 0 ? list : null;
        }

        #endregion
    }
}