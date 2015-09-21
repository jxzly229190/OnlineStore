// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   电子券使用范围数据访问类.
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
    /// 电子券使用范围数据访问类.
    /// </summary>
    public class CouponScopeDA : ICouponScopeDA
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
        /// 添加电子券使用范围.
        /// </summary>
        /// <param name="couponScope">
        /// Coupon_Scope的对象实例.
        /// </param>
        /// <returns>
        ///  电子券使用范围编号.
        /// </returns>
        public int Inseret(Coupon_Scope couponScope)
        {
            if (couponScope == null)
            {
                throw new ArgumentNullException("couponScope");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponID",
                                         SqlDbType.Int,
                                         couponScope.CouponID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponTypeID",
                                         SqlDbType.Int,
                                         couponScope.CouponTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ScopeType",
                                         SqlDbType.Int,
                                         couponScope.ScopeType,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TargetTypeID",
                                         SqlDbType.Int,
                                         couponScope.TargetTypeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         couponScope.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Coupon_Scope_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询指定编号的电子券使用范围列表.
        /// </summary>
        /// <param name="couponID">
        /// 电子券编号.
        /// </param>
        /// <param name="couponType">
        /// 电子券类型（0：现金券，1：满减券）.
        /// </param>
        /// <returns>
        /// Coupon_Scope的对象实例的列表.
        /// </returns>
        public List<Coupon_Scope> SelectByCouponID(int couponID, int couponType)
        {
            if (couponID <= 0)
            {
                throw new ArgumentNullException("couponID");
            }

            if (couponType < 0)
            {
                throw new ArgumentNullException("couponType");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponID",
                                         SqlDbType.Int,
                                         couponID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponTypeID",
                                         SqlDbType.Int,
                                         couponType,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Scope_SelectByCouponID",
                parameters,
                null);
            var list = dataReader.ToList<Coupon_Scope>();
            return list;
        }

        #endregion
    }
}
