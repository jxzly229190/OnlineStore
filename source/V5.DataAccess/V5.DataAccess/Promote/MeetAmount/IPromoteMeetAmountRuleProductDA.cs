// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetAmountRuleProductDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠指定商品范围活动规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetAmount
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满件优惠指定商品范围活动规则数据访问接口.
    /// </summary>
    public interface IPromoteMeetAmountRuleProductDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满件优惠指定商品范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleProduct">
        /// Promote_MeetAmount_Rule_Product的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 指定商品活动的编号.
        /// </returns>
        int Insert(Promote_MeetAmount_Rule_Product promoteMeetAmountRuleProduct, SqlTransaction transaction);

        /// <summary>
        /// 修改满件优惠指定商品范围活动.
        /// </summary>
        /// <param name="promoteMeetAmountRuleProduct">
        /// Promote_MeetAmount_Rule_Product的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Update(Promote_MeetAmount_Rule_Product promoteMeetAmountRuleProduct, SqlTransaction transaction);

        /// <summary>
        /// 查询指定满件优惠活动的规则列表.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动的编号.
        /// </param>
        /// <returns>
        /// 满件优惠活动的规则列表.
        /// </returns>
        List<Promote_MeetAmount_Rule_Product> SelectByMeetAmountID(int meetAmountID);

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Delete(int id, SqlTransaction transaction);

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void DeleteByMeetAmountID(int meetAmountID, SqlTransaction transaction);

        #endregion
    }
}
