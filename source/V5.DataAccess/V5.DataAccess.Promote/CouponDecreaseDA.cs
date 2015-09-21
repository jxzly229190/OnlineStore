// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券的数据访问类.
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
    /// 满减券的数据访问类.
    /// </summary>
    public class CouponDecreaseDA : ICouponDecreaseDA
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
        /// 添加满减券.
        /// </summary>
        /// <param name="couponDecrease">
        /// Coupon_Decrease的对象实例.
        /// </param>
        /// <returns>
        /// 满减券编号.
        /// </returns>
        public int InsertCouponDecrease(Coupon_Decrease couponDecrease)
        {
            if (couponDecrease == null)
            {
                throw new ArgumentNullException("couponDecrease");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         couponDecrease.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         couponDecrease.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "FaceValue",
                                         SqlDbType.Float,
                                         couponDecrease.FaceValue,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Float,
                                         couponDecrease.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "InitialNumber",
                                         SqlDbType.Int,
                                         couponDecrease.InitialNumber,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         couponDecrease.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         couponDecrease.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         couponDecrease.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         couponDecrease.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Coupon_Decrease_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询满减券列表
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
        /// 满减券列表
        /// </returns>
        public List<Coupon_Decrease> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Coupon_Decrease>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查询全部有效的满减券.
        /// </summary>
        /// <returns>
        /// 满减券列表.
        /// </returns>
        public List<Coupon_Decrease> SelectAllValidCouponDecrease()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Coupon_Decrease_SelectAll",
                null,
                null);
            return dataReader.ToList<Coupon_Decrease>();
        }

        /// <summary>
        /// 查询指定编号的满减券信息.
        /// </summary>
        /// <param name="ID">
        /// 编号.
        /// </param>
        /// <returns>
        /// Coupon_Decrease的对象实例.
        /// </returns>
        public Coupon_Decrease SelectCouponDecreaseByID(int ID)
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
                "sp_Coupon_Decrease_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Coupon_Decrease>();
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
                "sp_Coupon_Decrease_Update_InitialNumber",
                parameters,
                null);
        }

        /// <summary>
        /// 查询指定的满减券名称
        /// </summary>
        /// <param name="name">
        /// 满减券名称
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
                "sp_Coupon_Decrease_Exists_Name",
                parameters,
                null);
            return dataReader.HasRows ? 1 : 0;
        }

        #endregion
    }
}