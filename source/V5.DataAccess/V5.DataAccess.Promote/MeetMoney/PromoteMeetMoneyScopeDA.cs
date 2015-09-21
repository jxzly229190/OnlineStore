// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满额优惠活动范围数据访问接口.
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
    /// 满额优惠活动范围数据访问接口.
    /// </summary>
    public class PromoteMeetMoneyScopeDA : IPromoteMeetMoneyScopeDA
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
        /// 添加满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetMoneyScope">
        /// Promote_MeetMoney_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满额优惠活动范围编号.
        /// </returns>
        public int Insert(Promote_MeetMoney_Scope promoteMeetMoneyScope, SqlTransaction transaction)
        {
            if (promoteMeetMoneyScope == null)
            {
                throw new ArgumentNullException("promoteMeetMoneyScope");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetMoneyID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyScope.MeetMoneyID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Scope",
                                         SqlDbType.VarChar,
                                         promoteMeetMoneyScope.Scope,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetMoney_Scope_Insert",
                parameters,
                null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetMoneyScope">
        /// Promote_MeetMoney_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Update(Promote_MeetMoney_Scope promoteMeetMoneyScope, SqlTransaction transaction)
        {
            if (promoteMeetMoneyScope == null)
            {
                throw new ArgumentNullException("promoteMeetMoneyScope");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetMoneyID",
                                         SqlDbType.Int,
                                         promoteMeetMoneyScope.MeetMoneyID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Scope",
                                         SqlDbType.VarChar,
                                         promoteMeetMoneyScope.Scope,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Scope_Update", parameters, transaction);
        }

        /// <summary>
        /// 查询所有商品参加的促销.
        /// </summary>
        /// <returns>
        /// The <see cref="Promote_MeetMoney_Scope"/>.
        /// </returns>
        public List<Promote_MeetMoney_Scope> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_MeetMoney_Scope_SelectAll",
                null,
                null);
            return dataReader.ToList<Promote_MeetMoney_Scope>();
        }

        /// <summary>
        /// 查询指定活动商品参加的促销.
        /// </summary>
        /// <param name="meetMoneyID">
        /// The meet Money ID.
        /// </param>
        /// <returns>
        /// 参加促销的活动商品.
        /// </returns>
        public Promote_MeetMoney_Scope SelectByMeetMoneyID(int meetMoneyID)
        {
            if (meetMoneyID <= 0)
            {
                throw new ArgumentNullException("meetMoneyID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetMoneyID",
                                         SqlDbType.Int,
                                         meetMoneyID,
                                         ParameterDirection.Input)
                                 }; 

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_MeetMoney_Scope_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_MeetMoney_Scope>();
            return list.Count > 0 ? list[0] : null;
        }

        #endregion
    }
}
