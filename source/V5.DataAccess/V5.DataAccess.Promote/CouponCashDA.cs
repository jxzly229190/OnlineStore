// --------------------------------------------------------------------------------------------------------------------
// <copyright file="couponCashDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券数据访问类.
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
    /// 现金券数据访问类.
    /// </summary>
    public class CouponCashDA : ICouponCashDA
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
        /// 添加现金券信息.
        /// </summary>
        /// <param name="couponCash">
        /// 现金券Coupon_Cash的对象实例.
        /// </param>
        /// <returns>
        /// 现金券的编号.
        /// </returns>
        public int InsertCoupon_Cash(Coupon_Cash couponCash)
        {
            if (couponCash == null)
            {
                throw new ArgumentNullException("couponCash");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         couponCash.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         couponCash.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "FaceValue",
                                         SqlDbType.Float,
                                         couponCash.FaceValue,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "InitialNumber",
                                         SqlDbType.Int,
                                         couponCash.InitialNumber,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         couponCash.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         couponCash.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         couponCash.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         couponCash.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Coupon_Cash_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询现金券列表
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
        /// 现金券列表
        /// </returns>
        public List<Coupon_Cash> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Coupon_Cash>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查询所有有效的现金券.
        /// </summary>
        /// <returns>
        /// 现金券列表.
        /// </returns>
        public List<Coupon_Cash> SelectAllValidCouponCash()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_SelectAll",
                null,
                null);
            return dataReader.ToList<Coupon_Cash>();
        }

        /// <summary>
        /// 查询优惠券
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Coupon_Query> CouponPaging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Coupon_Query>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查询指定编号的现金券信息.
        /// </summary>
        /// <param name="ID">
        /// 编号.
        /// </param>
        /// <returns>
        /// Coupon_Cash的对象实例.
        /// </returns>
        public Coupon_Cash SelectCouponCashByID(int ID)
        {
            if (ID <= 0)
            {
                throw new ArgumentNullException("ID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         ID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Coupon_Cash>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 增加现金券初始数量
        /// </summary>
        /// <param name="ID">
        /// 现金券编号
        /// </param>
        /// <param name="initialNumber">
        /// 更新的初始数量.
        /// </param>
        public void UpdateInitialNumberByID(int ID, int initialNumber)
        {
            if (ID <= 0)
            {
                throw new ArgumentNullException("ID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "InitialNumber",
                                         SqlDbType.Int,
                                         initialNumber,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_Update_InitialNumber",
                parameters,
                null);
        }

        /// <summary>
        /// 查询指定的现金券名称
        /// </summary>
        /// <param name="name">
        /// 现金券名称
        /// </param>
        /// <returns>
        /// 0：没有符合条件的结果、1：有符合条件的结果
        /// </returns>
        public int IsNameExists(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         name,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_Exists_Name",
                parameters,
                null);
            return dataReader.HasRows ? 1 : 0;
        }

        #endregion
    }
}