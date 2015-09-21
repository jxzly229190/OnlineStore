// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销数据访问类.
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
    /// 满就送促销数据访问类.
    /// </summary>
    public class PromoteMeetMoneyDA : IPromoteMeetMoneyDA
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
        /// 查询满就送促销列表
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
        /// 满就送列表
        /// </returns>
        public List<Promote_MeetMoney> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_MeetMoney>(paging, out pageCount, out totalCount, null);
        }
        
        /// <summary>
        /// 添加满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 满就送促销活动的编号.
        /// </returns>
        public int Insert(Promote_MeetMoney promoteMeetMoney, out SqlTransaction transaction)
        {
            if (promoteMeetMoney == null)
            {
                throw new ArgumentNullException("promoteMeetMoney");
            }

            this.SqlServer.BeginTransaction();
            transaction = this.SqlServer.Transaction;

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMeetMoney.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMeetMoney.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteMeetMoney.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMeetMoney.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         promoteMeetMoney.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteMeetMoney.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetMoney.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        public void Update(Promote_MeetMoney promoteMeetMoney, out SqlTransaction transaction)
        {
            if (promoteMeetMoney == null)
            {
                throw new ArgumentNullException("promoteMeetMoney");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetMoney.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMeetMoney.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMeetMoney.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteMeetMoney.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteMeetMoney.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMeetMoney.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         promoteMeetMoney.Description,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_Update", parameters, transaction);
        }

        /// <summary>
        /// 查询指定的满就送促销.
        /// </summary>
        /// <param name="id">
        /// 满额优惠编号.
        /// </param>
        /// <returns>
        /// Promote_MeetMoney的对象实例.
        /// </returns>
        public Promote_MeetMoney SelectByID(int id)
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
                "sp_Promote_MeetMoney_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_MeetMoney>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 修改促销活动状态.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号
        /// </param>
        /// <param name="status">
        /// 促销活动状态.
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetMoney_UpdateStatus", parameters, null);
        }

        /// <summary>
        /// 删除促销活动.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号
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
                "sp_Promote_MeetMoney_Delete",
                parameters,
                null);
        }

        #endregion
    }
}
