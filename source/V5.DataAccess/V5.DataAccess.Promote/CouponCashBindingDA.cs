// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponCashBindingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券绑定数据访问接口实现类.
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
    /// 现金券绑定数据访问接口实现类.
    /// </summary>
    public class CouponCashBindingDA : ICouponCashBindingDA
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
        /// 添加现金券绑定.
        /// </summary>
        /// <param name="couponCashBinding">
        /// Coupon_Cash_Binding的对象实例.
        /// </param>
        /// <param name="transaction">数据库事务，默认为Null</param>
        /// <returns>
        /// 现金券绑定的编号.
        /// </returns>
        public int Insert(Coupon_Cash_Binding couponCashBinding, SqlTransaction transaction = null)
        {
            if (couponCashBinding == null)
            {
                throw new ArgumentNullException("couponCashBinding");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponCashID",
                                         SqlDbType.Int,
                                         couponCashBinding.CouponCashID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         couponCashBinding.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "OrderID",
                                         SqlDbType.Int,
                                         couponCashBinding.OrderID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Number",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Number,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Password",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Password,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Cause",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Cause,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         couponCashBinding.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UseTime",
                                         SqlDbType.DateTime,
                                         couponCashBinding.UseTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BindingTime",
                                         SqlDbType.DateTime,
                                         couponCashBinding.BindingTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_Binding_Insert",
                parameters,
                transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

	    /// <summary>
        /// 添加现金券绑定.
        /// </summary>
        /// <param name="couponCashBinding">
        /// Coupon_Cash_Binding的对象实例.
        /// </param>
        public void Update(Coupon_Cash_Binding couponCashBinding)
        {
            if (couponCashBinding == null)
            {
                throw new ArgumentNullException("couponCashBinding");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponCashID",
                                         SqlDbType.Int,
                                         couponCashBinding.CouponCashID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         couponCashBinding.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "OrderID",
                                         SqlDbType.Int,
                                         couponCashBinding.OrderID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Number",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Number,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Password",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Password,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Cause",
                                         SqlDbType.VarChar,
                                         couponCashBinding.Cause,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         couponCashBinding.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UseTime",
                                         SqlDbType.DateTime,
                                         couponCashBinding.UseTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BindingTime",
                                         SqlDbType.DateTime,
                                         couponCashBinding.BindingTime,
                                         ParameterDirection.Input),
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Coupon_Cash_Update", parameters, null);
        }

        /// <summary>
        /// 查询现金券绑定列表
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
        /// 现金券绑定列表
        /// </returns>
        public List<Coupon_Cash_Binding> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Coupon_Cash_Binding>(paging, out pageCount, out totalCount, "sp_Paging");
        }

        /// <summary>
        /// 根据指定会员编号查询电子券列表.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="status">
        /// 状态（1：可用，2：不可用）
        /// </param>
        /// <returns>
        /// 电子券列表.
        /// </returns>
        public List<Coupon_Cash_Binding> SelectByUserID(int userID, int status)
        {
            if (userID <= 0)
            {
                throw new ArgumentNullException("userID");
            }

            if (status <= 0)
            {
                throw new ArgumentNullException("status");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         userID,
                                         ParameterDirection.Input),
                                          this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         status,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Cash_Binding_SelectByUserID",
                parameters,
                null);
            var list = dataReader.ToList<Coupon_Cash_Binding>();
            return list;
        }

        /// <summary>
        /// 可以使用指定电子券的商品.
        /// </summary>
        /// <param name="products">
        /// 可以使用电子券的商品(排除).
        /// </param>
        /// <param name="couponID">
        /// The coupon ID.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<int> SelectProducts(string products, int couponID)
        {
            //var parametres = new List<SqlParameter>
            //                     {
            //                         this.SqlServer.CreateSqlParameter(
            //                             "ProductIDS",
            //                             SqlDbType.NVarChar,
            //                             products,
            //                             ParameterDirection.Input),
            //                         this.SqlServer.CreateSqlParameter(
            //                             "CouponID",
            //                             SqlDbType.Int,
            //                             couponID,
            //                             ParameterDirection.Input)
            //                     };
            //var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_", parametres, null);
            //var list = dataReader.ToList<int>();
            //return list;

            throw new NotImplementedException();
        }

        #endregion
    }
}