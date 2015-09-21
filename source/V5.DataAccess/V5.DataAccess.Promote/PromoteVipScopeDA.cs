// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteVipScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销商品数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员促销商品数据访问类.
    /// </summary>
    public class PromoteVipScopeDA : IPromoteVipScopeDA
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
        /// 查询指定活动的商品.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <returns>
        /// 活动商品列表.
        /// </returns>
        public List<Promote_Vip_Scope> SelectByPromoteVipID(int id)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteVipID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_Vip_SelectByPromoteVipID",
                parameters,
                null);
            return dataReader.ToList<Promote_Vip_Scope>();
        }

        /// <summary>
        /// 添加活动商品.
        /// </summary>
        /// <param name="promoteVipScope">
        /// 活动商品实体.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Promote_Vip_Scope promoteVipScope, SqlTransaction transaction)
        {
            if (promoteVipScope == null)
            {
                throw new ArgumentNullException("promoteVipScope");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteVipID",
                                         SqlDbType.Int,
                                         promoteVipScope.PromoteVipID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.NVarChar,
                                         promoteVipScope.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteVipScope.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Vip_Scope_Insert",
                parameters,
                transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改活动商品.
        /// </summary>
        /// <param name="promoteVipScope">
        /// 活动商品实体.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Update(Promote_Vip_Scope promoteVipScope, SqlTransaction transaction)
        {
            if (promoteVipScope == null)
            {
                throw new ArgumentNullException("promoteVipScope");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteVipID",
                                         SqlDbType.Int,
                                         promoteVipScope.PromoteVipID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.NVarChar,
                                         promoteVipScope.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteVipScope.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Vip_Scope_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除指定活动商品.
        /// </summary>
        /// <param name="id">
        /// 活动商品编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Delete(int id, SqlTransaction transaction)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteVipID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Vip_Scope_Delete",
                parameters,
                transaction);
        }

        /// <summary>
        /// 当前商品中已参加会员促销的商品.
        /// </summary>
        /// <param name="productIDs">
        /// The product i ds.
        /// </param>
        /// <param name="promoteVipID">
        /// The promote Vip ID.
        /// </param>
        /// <returns>
        /// 当前商品中已参加限时抢购的商品信息.
        /// </returns>
        public List<ProductSearchResult> SelectByPromoteProduct(string productIDs, int promoteVipID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductIDs",
                                         SqlDbType.Text,
                                         productIDs,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "PromoteVipID",
                                         SqlDbType.Int,
                                         promoteVipID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_Vip_Scope_SelectByProductStr", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<ProductSearchResult>();
        }
        #endregion
    }
}
