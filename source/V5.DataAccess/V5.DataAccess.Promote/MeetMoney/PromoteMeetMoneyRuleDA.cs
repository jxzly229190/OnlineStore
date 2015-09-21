// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销活动规则数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满就送促销活动规则数据访问类.
    /// </summary>
    public class PromoteMeetMoneyRuleDA : IPromoteMeetMoneyRuleDA
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
        /// <param name="meetMoneyID">
        /// 满就送促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetMoney_Rule对象实例的列表.
        /// </returns>
        public List<Promote_MeetMoney_Rule> SelectByMeetMoneyID(int meetMoneyID)
        {
            if (meetMoneyID <= 0)
            {
                throw new ArgumentNullException("meetMoneyID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetMoneyID",
                                         SqlDbType.Int,
                                         meetMoneyID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Rule_SelectByMeetMoneyID", parameters, null);
            var list = dataReader.ToList<Promote_MeetMoney_Rule>();
            return list;
        }

        /// <summary>
        /// 添加满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetMoneyRule">
        /// Promote_MeetMoney_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 规则的编号.
        /// </returns>
        public int Insert(Promote_MeetMoney_Rule promoteMeetMoneyRule, SqlTransaction transaction)
        {
            if (promoteMeetMoneyRule == null)
            {
                throw new ArgumentNullException("promoteMeetMoneyRule");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetMoneyID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.PromoteMeetMoneyID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetMoney",
                                         SqlDbType.Float,
                                         promoteMeetMoneyRule.MeetMoney,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoCeiling",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsNoCeiling,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDecreaseCash",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsDecreaseCash,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DecreaseCash",
                                         SqlDbType.Float,
                                         promoteMeetMoneyRule.DecreaseCash,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveIntegral",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveIntegral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponType",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.CouponType,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.CouponID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Rule_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetMoneyRule">
        /// Promote_MeetMoney_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        public void Update(Promote_MeetMoney_Rule promoteMeetMoneyRule, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.ID,
                                         ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                         "MeetMoney",
                                         SqlDbType.Float,
                                         promoteMeetMoneyRule.MeetMoney,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoCeiling",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsNoCeiling,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDecreaseCash",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsDecreaseCash,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DecreaseCash",
                                         SqlDbType.Float,
                                         promoteMeetMoneyRule.DecreaseCash,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveIntegral",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveIntegral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetMoneyRule.IsGiveCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponType",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.CouponType,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyRule.CouponID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Rule_Update", parameters, transaction);
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
                "sp_Promote_MeetMoney_Rule_DeleteRow",
                parameters,
                transaction);
        }

        #endregion
    }
}
