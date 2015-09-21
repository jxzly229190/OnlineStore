// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetGiveGiftDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件送礼物促销规则数据访问类.
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
    /// 满足条件送礼物促销规则数据访问类.
    /// </summary>
    public class PromoteMeetGiveGiftDA : IPromoteMeetGiveGiftDA
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
        /// 添加满足条件送礼物促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveGift">
        /// Promote_Meet_GiveGift的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 添加满足条件送礼物促销规则的编号.
        /// </returns>
        public int Insert(Promote_Meet_GiveGift promoteMeetGiveGift, SqlTransaction transaction)
        {
            if (promoteMeetGiveGift == null)
            {
                throw new ArgumentNullException("promoteMeetGiveGift");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.ScopeTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.ID,
                                         ParameterDirection.Output)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_GiveGift_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 添加满足条件送礼物促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveGift">
        /// Promote_Meet_GiveGift的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void UpdateByRuleID(Promote_Meet_GiveGift promoteMeetGiveGift, SqlTransaction transaction)
        {
            if (promoteMeetGiveGift == null)
            {
                throw new ArgumentNullException("promoteMeetGiveGift");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.ScopeTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMeetGiveGift.ProductID,
                                         ParameterDirection.Input)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_GiveGift_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除满足条件送礼物促销规则.
        /// </summary>
        /// <param name="id">
        /// 满足条件送礼物促销规则编号.
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
                "sp_Promote_Meet_GiveGift_DeleteRow",
                parameters,
                transaction);
        }

        /// <summary>
        /// 删除满足条件送礼物促销规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <param name="meetTypeID">
        /// 促销类型（0：满就送，1：满件优惠）
        /// </param>
        /// <param name="scopeID">
        /// 范围编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void DeleteByRuleID(int ruleID, int meetTypeID, int scopeID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         ruleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         scopeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         meetTypeID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Meet_GiveGift_Delete",
                parameters,
                transaction);
        }

        /// <summary>
        /// 检查是否存在指定规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <returns>
        /// true:存在，false:不存在.
        /// </returns>
        /// <param name="meetTypeID">
        /// 促销类型（0：满就送，1：满件优惠）
        /// </param>
        /// <param name="scopeID">
        /// 范围编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public bool IsExist(int ruleID, int meetTypeID, int scopeID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         ruleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         scopeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         meetTypeID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_Meet_GiveGift_Select",
                parameters,
                transaction);

            var hasRow = dataReader.HasRows;

            if (transaction == null)
            {
                dataReader.Close();
            }

            return hasRow;
        }

        #endregion
    }
}
