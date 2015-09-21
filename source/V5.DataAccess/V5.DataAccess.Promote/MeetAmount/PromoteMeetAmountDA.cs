// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠促销活动数据访问类
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
    /// 满件优惠促销活动数据访问类
    /// </summary>
    public class PromoteMeetAmountDA : IPromoteMeetAmountDA
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
        /// 添加满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满件优惠的编号.
        /// </returns>
        public int Insert(Promote_MeetAmount promoteMeetAmount, out SqlTransaction transaction)
        {
            if (promoteMeetAmount == null)
            {
                throw new ArgumentNullException("promoteMeetAmount");
            }

            this.SqlServer.BeginTransaction();
            transaction = this.SqlServer.Transaction;

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMeetAmount.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMeetAmount.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteMeetAmount.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMeetAmount.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         promoteMeetAmount.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteMeetAmount.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetAmount.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Update(Promote_MeetAmount promoteMeetAmount, out SqlTransaction transaction)
        {
            if (promoteMeetAmount == null)
            {
                throw new ArgumentNullException("promoteMeetAmount");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetAmount.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMeetAmount.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMeetAmount.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetAmount.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteMeetAmount.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMeetAmount.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteMeetAmount.Description,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Update", parameters, transaction);
        }

        /// <summary>
        /// 启动、停止满件优惠.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动编号.
        /// </param>
        /// <param name="status">
        /// 活动状态（1：可用，2：停用）.
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_UpdateStatus", parameters, null);
        }

        /// <summary>
        /// 查询满件优惠列表
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
        /// 满件优惠列表
        /// </returns>
        public List<Promote_MeetAmount> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_MeetAmount>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查询指定的满件优惠活动.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount.
        /// </returns>
        public Promote_MeetAmount SelectByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

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
                "sp_Promote_MeetAmount_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_MeetAmount>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 删除促销活动.
        /// </summary>
        /// <param name="id">
        /// 满件促销活动的编号
        /// </param>
        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

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
                "sp_Promote_MeetAmount_DeleteRow",
                parameters,
                null);
        }

        #endregion
    }
}
