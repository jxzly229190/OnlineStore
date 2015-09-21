// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseBindingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券绑定数据访问接口实现类.
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
    /// 满减券绑定数据访问接口实现类.
    /// </summary>
    public class CouponDecreaseBindingDA : ICouponDecreaseBindingDA
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
        /// 添加满减券绑定.
        /// </summary>
        /// <param name="couponDecreaseBinding">
        /// Coupon_Cash_Binding的对象实例.
        /// </param>
        /// <param name="transaction">数据库事务，默认为Null</param>
        /// <returns>
        /// 满减券绑定编号.
        /// </returns>
        public int Insert(Coupon_Decrease_Binding couponDecreaseBinding, SqlTransaction transaction = null)
        {
            if (couponDecreaseBinding == null)
            {
                throw new ArgumentNullException("couponDecreaseBinding");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CouponDecreaseID",
                                         SqlDbType.Int,
                                         couponDecreaseBinding.CouponDecreaseID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         couponDecreaseBinding.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "OrderID",
                                         SqlDbType.Int,
                                         couponDecreaseBinding.OrderID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Number",
                                         SqlDbType.VarChar,
                                         couponDecreaseBinding.Number,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Password",
                                         SqlDbType.VarChar,
                                         couponDecreaseBinding.Password,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Cause",
                                         SqlDbType.VarChar,
                                         couponDecreaseBinding.Cause,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         couponDecreaseBinding.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UseTime",
                                         SqlDbType.DateTime,
                                         couponDecreaseBinding.UseTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BindingTime",
                                         SqlDbType.DateTime,
                                         couponDecreaseBinding.BindingTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Coupon_Decrease_Binding_Insert",
                parameters,
                transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询满减券绑定列表
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
        /// 满减券绑定列表
        /// </returns>
        public List<Coupon_Decrease_Binding> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Coupon_Decrease_Binding>(paging, out pageCount, out totalCount, null);
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
        public List<Coupon_Decrease_Binding> SelectByUserID(int userID, int status)
        {
            return null;
        }

        #endregion
    }
}