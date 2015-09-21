// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteVipDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销数据访问类.
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
    /// 会员促销数据访问类.
    /// </summary>
    public class PromoteVipDA : IPromoteVipDA
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
        /// 查询会员促销列表
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
        /// 会员促销列表
        /// </returns>
        public List<Promote_Vip> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_Vip>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查询指定的会员促销.
        /// </summary>
        /// <param name="id">
        /// 会员促销编号.
        /// </param>
        /// <returns>
        /// Promote_Vip的对象实例.
        /// </returns>
        public Promote_Vip SelectByID(int id)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_Vip_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_Vip>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 添加会员促销活动.
        /// </summary>
        /// <param name="promoteVip">
        /// Promote_Vip的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 会员促销活动的编号.
        /// </returns>
        public int Insert(Promote_Vip promoteVip, out SqlTransaction transaction)
        {
            if (promoteVip == null)
            {
                throw new ArgumentNullException("promoteVip");
            }

            this.SqlServer.BeginTransaction();
            transaction = this.SqlServer.Transaction;

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteVip.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteVip.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteVip.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteVip.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteVip.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteVip.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteVip.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         promoteVip.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteVip.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteVip.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Vip_Insert",
                parameters,
                transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改会员促销活动.
        /// </summary>
        /// <param name="promoteVip">
        /// Promote_Vip的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        public void Update(Promote_Vip promoteVip, out SqlTransaction transaction)
        {
            if (promoteVip == null)
            {
                throw new ArgumentNullException("promoteVip");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteVip.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteVip.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteVip.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteVip.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteVip.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteVip.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteVip.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteVip.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteVip.Description,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Vip_Update", parameters, transaction);
        }

        /// <summary>
        /// 删除会员促销活动.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        public void Delete(int id, out SqlTransaction transaction)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Vip_DeleteRow", parameters, transaction);
        }

        /// <summary>
        /// 删除会员促销活动.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <param name="status">
        /// 状态.
        /// </param>
        public void UpdateStatus(int id, int status)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         status,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Vip_UpdateStatus", parameters, null);
        }

        #endregion
    }
}
