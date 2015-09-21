// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleBrandDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠商品品牌范围活动规则数据访问类.
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
    /// 满件优惠商品品牌范围活动规则数据访问类.
    /// </summary>
    public class PromoteMeetAmountRuleBrandDA : IPromoteMeetAmountRuleBrandDA
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
        /// 添加满件优惠商品品牌范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleBrand">
        /// Promote_MeetAmount_Rule_Brand.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 优惠商品品牌范围活动的编号.
        /// </returns>
        public int Insert(Promote_MeetAmount_Rule_Brand promoteMeetAmountRuleBrand, SqlTransaction transaction)
        {
            if (promoteMeetAmountRuleBrand == null)
            {
                throw new ArgumentNullException("promoteMeetAmountRuleBrand");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ParentBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ProductBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsDiscount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsNoPostage,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ID,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Brand_Insert", parameters, transaction);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 修改满件优惠商品品牌范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleBrand">
        /// Promote_MeetAmount_Rule_Brand.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Update(Promote_MeetAmount_Rule_Brand promoteMeetAmountRuleBrand, SqlTransaction transaction)
        {
            if (promoteMeetAmountRuleBrand == null)
            {
                throw new ArgumentNullException("promoteMeetAmountRuleBrand");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ID,
                                         ParameterDirection.Input),
                                    this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.PromoteMeetAmountID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ParentBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.ProductBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MeetAmount",
                                         SqlDbType.Int,
                                         promoteMeetAmountRuleBrand.MeetAmount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDiscount",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsDiscount,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsGiveGift",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsGiveGift,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsNoPostage",
                                         SqlDbType.Bit,
                                         promoteMeetAmountRuleBrand.IsNoPostage,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Brand_Update", parameters, transaction);
        }

        /// <summary>
        /// 查询指定满件优惠活动的规则列表.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动的编号.
        /// </param>
        /// <returns>
        /// 满件优惠活动的规则列表.
        /// </returns>
        public List<Promote_MeetAmount_Rule_Brand> SelectByMeetAmountID(int meetAmountID)
        {
            if (meetAmountID <= 0)
            {
                throw new ArgumentNullException("meetAmountID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         meetAmountID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Promote_MeetAmount_Rule_Brand_Select", parameters, null);
            return dataReader.ToList<Promote_MeetAmount_Rule_Brand>();
        }

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void Delete(int id, SqlTransaction transaction)
        {
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
                "sp_Promote_MeetAmount_Rule_Brand_DeleteRow",
                parameters,
                transaction);
        }

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        public void DeleteByMeetAmountID(int meetAmountID, SqlTransaction transaction)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteMeetAmountID",
                                         SqlDbType.Int,
                                         meetAmountID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MeetAmount_Rule_Brand_Delete",
                parameters,
                transaction);
        }

        #endregion
    }
}
