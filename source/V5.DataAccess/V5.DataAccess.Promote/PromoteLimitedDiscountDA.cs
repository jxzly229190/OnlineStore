// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteLimitedDiscountDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   限时促销促销活动数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 限时促销促销活动数据访问类.
    /// </summary>
    public class PromoteLimitedDiscountDA : IPromoteLimitedDiscountDA
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
        /// 添加限时打折促销活动.
        /// </summary>
        /// <param name="promoteLimitedDiscount">
        /// Promote_Limited_Discount的对象实例.
        /// </param>
        /// <returns>
        /// 限时打折促销活动的编号.
        /// </returns>
        public int Insert(Promote_Limited_Discount promoteLimitedDiscount)
        {
            if (promoteLimitedDiscount == null)
            {
                throw new ArgumentNullException("promoteLimitedDiscount");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.sqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteLimitedDiscount.ProductID,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteLimitedDiscount.Name,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.Discount,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "DiscountPrice",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.DiscountPrice,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "TotalQuantity",
                                         SqlDbType.Int,
                                         promoteLimitedDiscount.TotalQuantity,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "LimitedBuyQuantity",
                                         SqlDbType.Int,
                                         promoteLimitedDiscount.LimitedBuyQuantity,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "IsOnlinePayment",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsOnlinePayment,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteLimitedDiscount.StartTime,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteLimitedDiscount.EndTime,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         promoteLimitedDiscount.CreateTime,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         promoteLimitedDiscount.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };
            this.sqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Limited_Discount_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询限时打折促销列表
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
        /// 限时打折列表
        /// </returns>
        public List<Promote_Limited_Discount> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_Limited_Discount>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 修改限时抢购促销.
        /// </summary>
        /// <param name="promoteLimitedDiscount">
        /// Promote_Limited_Discount的对象的实例.
        /// </param>
        public void Update(Promote_Limited_Discount promoteLimitedDiscount)
        {
            if (promoteLimitedDiscount == null)
            {
                throw new ArgumentNullException("promoteLimitedDiscount");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteLimitedDiscount.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteLimitedDiscount.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Discount",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.Discount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DiscountPrice",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.DiscountPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "LimitedBuyQuantity",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.LimitedBuyQuantity,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalQuantity",
                                         SqlDbType.Float,
                                         promoteLimitedDiscount.TotalQuantity,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsOnlinePayment",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsOnlinePayment,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNewUser",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsNewUser,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMobileValidate",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsMobileValidate,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsUseCoupon",
                                         SqlDbType.Bit,
                                         promoteLimitedDiscount.IsUseCoupon,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteLimitedDiscount.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteLimitedDiscount.EndTime,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_Limited_Discount_Update",
                parameters,
                null);
        }

        /// <summary>
        /// 修改活动状态.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <param name="status">
        /// 状态数值（1：正常，2，暂停，3，停止）.
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_Limited_Discount_UpdateStatus", parameters, null);
        }

        /// <summary>
        /// 删除指定活动.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
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
                "sp_Promote_Limited_Discount_Delete",
                parameters,
                null);
        }

        /// <summary>
        /// 当前商品中已参加限时抢购的商品.
        /// </summary>
        /// <param name="productIDs">
        /// The product i ds.
        /// </param>
        /// <returns>
        /// 当前商品中已参加限时抢购的商品信息.
        /// </returns>
        public List<ProductSearchResult> SelectByPromoteProduct(string productIDs)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductIDs",
                                         SqlDbType.Text,
                                         productIDs,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectByPromoteProduct", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<ProductSearchResult>();
        }

        /// <summary>
        /// 查询指定编号的限时打折.
        /// </summary>
        /// <param name="id">
        /// 限制打折编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_Limited_Discount"/>.
        /// </returns>
        public Promote_Limited_Discount SelectByID(int id)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_Limited_Discount_SelectRow", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = dataReader.ToList<Promote_Limited_Discount>();
            return list.Count > 0 ? list[0] : null;
        }

        #endregion
    }
}
