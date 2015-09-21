// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleFullDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠全网范围活动规则数据访问类.
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
    /// 满件优惠全网范围活动规则数据访问类.
    /// </summary>
    public class PromoteMeetAmountRuleFullDA : IPromoteMeetAmountRuleFullDA
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
        /// 添加满件优惠满件全网范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleFull">
        /// Promote_MeetAmount_Rule_Full的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 全网范围活动的编号.
        /// </returns>
        public int Insert(Promote_MeetAmount_Rule_Full promoteMeetAmountRuleFull, SqlTransaction transaction)
        {
            if (promoteMeetAmountRuleFull == null)
            {
                throw new ArgumentNullException("promoteMeetAmountRuleFull");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsDiscount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetAmount_Rule_Full_Insert",
                parameters,
                transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满件优惠全网范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleFull">
        /// Promote_MeetAmount_Rule_Full的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Update(Promote_MeetAmount_Rule_Full promoteMeetAmountRuleFull, SqlTransaction transaction)
        {
            if (promoteMeetAmountRuleFull == null)
            {
                throw new ArgumentNullException("promoteMeetAmountRuleFull");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleFull.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleFull.IsDiscount,
                                         ParameterDirection.Input),
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetAmount_Rule_Full_Update",
                parameters,
                transaction);
        }

        /// <summary>
        /// 查询指定满件优惠活动的规则列表.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动的编号.
        /// </param>
        /// <returns>
        /// 满件优惠活动的规则列表.
        /// </returns>
        public List<Promote_MeetAmount_Rule_Full> SelectByMeetAmountID(int meetAmountID)
        {
            if (meetAmountID <= 0)
            {
                throw new ArgumentNullException("meetAmountID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         meetAmountID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Full_Select", parameters, null);
            return dataReader.ToList<Promote_MeetAmount_Rule_Full>();
        }

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动促销规则编号.
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
                "sp_Promote_MeetAmount_Rule_Full_DeleteRow",
                parameters,
                transaction);
        }

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void DeleteByMeetAmountID(int meetAmountID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         meetAmountID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetAmount_Rule_Full_Delete",
                parameters,
                transaction);
        }

        #endregion
    }
}
