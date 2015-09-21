// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销活动规则数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetAmount
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetAmount;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满就送促销活动规则数据访问类.
    /// </summary>
    public class PromoteMeetAmountRuleDA : IPromoteMeetAmountRuleDA
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
        /// 查询指定满就送的促销规则.
        /// </summary>
        /// <param name="MeetAmountID">
        /// 满就送促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount_Rule对象实例的列表.
        /// </returns>
        public List<Promote_MeetAmount_Rule> SelectByMeetAmountID(int MeetAmountID)
        {
            if (MeetAmountID <= 0)
            {
                throw new ArgumentNullException("MeetAmountID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         MeetAmountID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_SelectByMeetAmountID", parameters, null);
            var list = dataReader.ToList<Promote_MeetAmount_Rule>();
            return list;
        }

        /// <summary>
        /// 添加满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetAmountRule">
        /// Promote_MeetAmount_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 规则的编号.
        /// </returns>
        public int Insert(Promote_MeetAmount_Rule promoteMeetAmountRule, SqlTransaction transaction)
        {
            if (promoteMeetAmountRule == null)
            {
                throw new ArgumentNullException("promoteMeetAmountRule");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsDiscount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteMeetAmountRule.Discount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetAmountRule">
        /// Promote_MeetAmount_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        public void Update(Promote_MeetAmount_Rule promoteMeetAmountRule, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsDiscount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteMeetAmountRule.Discount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRule.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRule.IsNoPostage,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="id">
        /// 满就送促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Delete(int id, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetAmount_Rule_DeleteRow",
                parameters,
                transaction);
        }

        #endregion
    }
}
