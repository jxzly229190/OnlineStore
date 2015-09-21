// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetGiveIntegralDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件送积分促销规则数据访问.
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
    ///  满足条件送积分促销规则数据访问.
    /// </summary>
    public class PromoteMeetGiveIntegralDA : IPromoteMeetGiveIntegralDA
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
        /// 添加满足条件送积分促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveIntegral">
        /// Promote_Meet_GiveIntegral的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满足条件送积分促销规则编号.
        /// </returns>
        public int Insert(Promote_Meet_GiveIntegral promoteMeetGiveIntegral, SqlTransaction transaction)
        {
            if (promoteMeetGiveIntegral == null)
            {
                throw new ArgumentNullException("promoteMeetGiveIntegral");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.ID,
                                         ParameterDirection.Output)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_GiveIntegral_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满足条件送积分促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveIntegral">
        /// Promote_Meet_GiveIntegral的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void UpdateByRuleID(Promote_Meet_GiveIntegral promoteMeetGiveIntegral, SqlTransaction transaction)
        {
            if (promoteMeetGiveIntegral == null)
            {
                throw new ArgumentNullException("promoteMeetGiveIntegral");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.MeetRuleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetTypeID",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.MeetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         promoteMeetGiveIntegral.Integral,
                                         ParameterDirection.Input)
                                 };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Meet_GiveIntegral_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除满足条件送积分促销规则.
        /// </summary>
        /// <param name="id">
        /// 满足条件送积分促销规则编号.
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
                "sp_Promote_Meet_GiveIntegral_DeleteRow",
                parameters,
                transaction);
        }

        /// <summary>
        /// 删除满足条件送积分促销规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void DeleteByRuleID(int ruleID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         ruleID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Meet_GiveIntegral_Delete",
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
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public bool IsExist(int ruleID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetRuleID",
                                         SqlDbType.Int,
                                         ruleID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_Meet_GiveIntegral_Select",
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
