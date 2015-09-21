// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetDiscountDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件打折促销规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.PromoteMeetAmount
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataAccess.Promote.IPromoteMeetAmount;
    using V5.DataContract.Promote.PromoteMeetAmount;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满足条件打折促销规则数据访问接口.
    /// </summary>
    public class PromoteMeetDiscountDA : IPromoteMeetDiscountDA
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
        /// 添加满足条件打折促销规则.
        /// </summary>
        /// <param name="promoteMeetDiscount">
        /// Promote_Meet_Discount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满足条件打折促销规则编号.
        /// </returns>
        public int Insert(Promote_Meet_Discount promoteMeetDiscount, SqlTransaction transaction)
        {
            if (promoteMeetDiscount == null)
            {
                throw new ArgumentNullException("promoteMeetDiscount");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.ScopeTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteMeetDiscount.Discount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.ID,
                                         ParameterDirection.Output)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_Discount_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满足条件打折促销规则.
        /// </summary>
        /// <param name="promoteMeetDiscount">
        /// Promote_Meet_Discount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void UpdateByRuleID(Promote_Meet_Discount promoteMeetDiscount, SqlTransaction transaction)
        {
            if (promoteMeetDiscount == null)
            {
                throw new ArgumentNullException("promoteMeetDiscount");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeTypeID",
                                         SqlDbType.Int,
                                         promoteMeetDiscount.ScopeTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteMeetDiscount.Discount,
                                         ParameterDirection.Input)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_Discount_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除满足条件打折促销规则.
        /// </summary>
        /// <param name="id">
        /// 满足条件打折促销规则编号.
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
                "sp_Promote_Meet_Discount_DeleteRow",
                parameters,
                transaction);
        }

        /// <summary>
        /// 删除满足条件打折促销规则.
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
                "sp_Promote_Meet_Discount_Delete",
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
                "sp_Promote_Meet_Discount_Select",
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
