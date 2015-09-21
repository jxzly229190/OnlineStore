// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetAmountRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetAmount
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满就送促销规则数据访问接口.
    /// </summary>
    public interface IPromoteMeetAmountRuleDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 查询指定满就送的促销规则.
        /// </summary>
        /// <param name="MeetAmountID">
        /// 满就送促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount_Rule对象实例的列表.
        /// </returns>
        List<Promote_MeetAmount_Rule> SelectByMeetAmountID(int MeetAmountID);

        /// <summary>
        /// 添加满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetAmountRule">
        /// Promote_MeetAmount_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 规则的编号.
        /// </returns>
        int Insert(Promote_MeetAmount_Rule promoteMeetAmountRule, SqlTransaction transaction);

        /// <summary>
        /// 修改满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetAmountRule">
        /// Promote_MeetAmount_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        void Update(Promote_MeetAmount_Rule promoteMeetAmountRule, SqlTransaction transaction);

        /// <summary>
        /// 删除指定的促销规则.
        /// </summary>
        /// <param name="id">
        /// 满就送促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Delete(int id, SqlTransaction transaction);

        #endregion
    }
}
