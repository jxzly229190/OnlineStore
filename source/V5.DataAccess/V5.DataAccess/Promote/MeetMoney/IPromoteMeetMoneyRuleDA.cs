// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetMoneyRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满就送促销规则数据访问接口.
    /// </summary>
    public interface IPromoteMeetMoneyRuleDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 查询指定满就送的促销规则.
        /// </summary>
        /// <param name="meetMoneyID">
        /// 满就送促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetMoney_Rule对象实例的列表.
        /// </returns>
        List<Promote_MeetMoney_Rule> SelectByMeetMoneyID(int meetMoneyID);

        /// <summary>
        /// 添加满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetMoneyRule">
        /// Promote_MeetMoney_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 规则的编号.
        /// </returns>
        int Insert(Promote_MeetMoney_Rule promoteMeetMoneyRule, SqlTransaction transaction);

        /// <summary>
        /// 修改满就送促销规则.
        /// </summary>
        /// <param name="promoteMeetMoneyRule">
        /// Promote_MeetMoney_Rule的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        void Update(Promote_MeetMoney_Rule promoteMeetMoneyRule, SqlTransaction transaction);

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
