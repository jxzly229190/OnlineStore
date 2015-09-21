// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMuchBottledRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销规则数据访问类.
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
    /// 多瓶装促销规则数据访问类.
    /// </summary>
    public class PromoteMuchBottledRuleDA : IPromoteMuchBottledRuleDA
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
        /// 添加多瓶装活动规则.
        /// </summary>
        /// <param name="promoteMuchBottledRule">
        /// Promote_MuchBottled_Rule的对象实例.
        /// </param>
        /// <returns>
        /// 多瓶装活动规则的编号.
        /// </returns>
        public int Insert(Promote_MuchBottled_Rule promoteMuchBottledRule)
        {
            if (promoteMuchBottledRule == null)
            {
                throw new ArgumentNullException("promoteMuchBottledRule");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MuchBottledID",
                                         SqlDbType.Int,
                                         promoteMuchBottledRule.MuchBottledID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMuchBottledRule.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Quantity",
                                         SqlDbType.Int,
                                         promoteMuchBottledRule.Quantity,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UnitPrice",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.UnitPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DiscountAmount",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.DiscountAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalMoney",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.TotalMoney,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ImageUrl",
                                         SqlDbType.NVarChar,
                                         promoteMuchBottledRule.ImageUrl,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDefault",
                                         SqlDbType.Bit,
                                         promoteMuchBottledRule.IsDefault,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_Rule_Insert",
                parameters,
                null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询多瓶装促销规则列表
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
        /// 多瓶装促销规则列表
        /// </returns>
        public List<Promote_MuchBottled_Rule> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_MuchBottled_Rule>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 更新多瓶装促销规则.
        /// </summary>
        /// <param name="promoteMuchBottledRule">
        /// Promote_MuchBottled_Rule的对象实例.
        /// </param>
        public void Update(Promote_MuchBottled_Rule promoteMuchBottledRule)
        {
            if (promoteMuchBottledRule == null)
            {
                throw new ArgumentNullException("promoteMuchBottledRule");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMuchBottledRule.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMuchBottledRule.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Quantity",
                                         SqlDbType.Int,
                                         promoteMuchBottledRule.Quantity,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UnitPrice",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.UnitPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DiscountAmount",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.DiscountAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalMoney",
                                         SqlDbType.Float,
                                         promoteMuchBottledRule.TotalMoney,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ImageUrl",
                                         SqlDbType.NVarChar,
                                         promoteMuchBottledRule.ImageUrl,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDefault",
                                         SqlDbType.Bit,
                                         promoteMuchBottledRule.IsDefault,
                                         ParameterDirection.Input)
                                 };
            this.sqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_Rule_Update",
                parameters,
                null);
        }

        /// <summary>
        /// 查询指顶多瓶装促销的规则列表.
        /// </summary>
        /// <param name="muchBottledID">
        /// 多瓶装促销的编号.
        /// </param>
        /// <returns>
        /// 规则列表.
        /// </returns>
        public List<Promote_MuchBottled_Rule> SelectByMuchBottledID(int muchBottledID)
        {
            if (muchBottledID <= 0)
            {
                throw new ArgumentNullException("muchBottledID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MuchBottledID",
                                         SqlDbType.Int,
                                         muchBottledID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_Rule_SelectByMuchBottledID",
                parameters,
                null);
            return dataReader.ToList<Promote_MuchBottled_Rule>();
        }

        #endregion
    }
}
