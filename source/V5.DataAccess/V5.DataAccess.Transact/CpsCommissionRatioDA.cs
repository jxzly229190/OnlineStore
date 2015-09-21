// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsCommissionRatioDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   CPS佣金比例数据访问类.
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
    /// CPS佣金比例数据访问类.
    /// </summary>
    public class CpsCommissionRatioDA : ICpsCommissionRatioDA
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
        /// 添加CPS佣金信息.
        /// </summary>
        /// <param name="cpsCommissionRatio">
        /// Cps_CommissionRatio的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Cps_CommissionRatio cpsCommissionRatio)
        {
            if (cpsCommissionRatio == null)
            {
                throw new ArgumentNullException("cpsCommissionRatio");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CpsID",
                                         SqlDbType.Int,
                                         cpsCommissionRatio.CpsID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         cpsCommissionRatio.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CommissionRatio",
                                         SqlDbType.Float,
                                         cpsCommissionRatio.CommissionRatio,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         cpsCommissionRatio.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Cps_CommissionRatio_Insert",
                parameters,
                null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 更新CPS拥挤比例.
        /// </summary>
        /// <param name="cpsCommissionRatio">
        /// The cps_ commission ratio.
        /// </param>
        public void Updata(Cps_CommissionRatio cpsCommissionRatio)
        {
            if (cpsCommissionRatio == null)
            {
                throw new ArgumentNullException("cpsCommissionRatio");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         cpsCommissionRatio.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CpsID",
                                         SqlDbType.Int,
                                         cpsCommissionRatio.CpsID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         cpsCommissionRatio.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CommissionRatio",
                                         SqlDbType.Float,
                                         cpsCommissionRatio.CommissionRatio,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         cpsCommissionRatio.CreateTime,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_cps_CommissionRatio_Update",
                parameters,
                null);
        }

        /// <summary>
        /// 查询指定的CPS佣金.
        /// </summary>
        /// <param name="condition">
        /// 查询的条件.
        /// </param>
        /// <param name="totalCount">
        /// The total Count.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        public List<Cps_CommissionRatio> SelectCommissionRatioByCpsID(string condition, out int totalCount)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "tableName",
                                         SqlDbType.VarChar,
                                         "Cps_CommissionRatio",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageIndex",
                                         SqlDbType.Int,
                                         1,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageSize",
                                         SqlDbType.Int,
                                         10,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageColumn",
                                         SqlDbType.VarChar,
                                         "ID",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "columns",
                                         SqlDbType.VarChar,
                                         null,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "condition",
                                         SqlDbType.NVarChar,
                                         condition,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "orderBy",
                                         SqlDbType.VarChar,
                                         null,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "totalCount",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Paging",
                parameters,
                null);
            if (!dataReader.HasRows)
            {
                totalCount = 0;
                return null;
            }

            var list = dataReader.ToList<Cps_CommissionRatio>();
            totalCount = (int)parameters.Find(parameter => parameter.ParameterName == "totalCount").Value;

            return list;
        }

        /// <summary>
        /// 查询指定的CPS佣金.
        /// </summary>
        /// <param name="cpsID">
        /// 查询的条件CpsID.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        public List<Cps_CommissionRatio> SelectCommissionRatioByCpsID(int cpsID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "cpsID",
                                         SqlDbType.NVarChar,
                                         cpsID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Cps_CommissionRatio_SelectByCpsID",
                parameters,
                null);
            var list = dataReader.ToList<Cps_CommissionRatio>();
            return list.Count > 0 ? list : null;
        }

        /// <summary>
        /// 查询指定编号的佣金比例信息.
        /// </summary>
        /// <param name="ID">
        /// 编码.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio的对象实例.
        /// </returns>
        public Cps_CommissionRatio SelectCommissionRatioByID(int ID)
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
                "sp_Cps_CommissionRatio_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Cps_CommissionRatio>();
            return list.Count > 0 ? list[0] : null;
        }

        #endregion
    }
}