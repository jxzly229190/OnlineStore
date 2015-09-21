// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetMoneyScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满额优惠活动范围数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满额优惠活动范围数据访问接口.
    /// </summary>
    public interface IPromoteMeetMoneyScopeDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetMoneyScope">
        /// Promote_MeetMoney_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满额优惠活动范围编号.
        /// </returns>
        int Insert(Promote_MeetMoney_Scope promoteMeetMoneyScope, SqlTransaction transaction);

        /// <summary>
        /// 修改满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetMoneyScope">
        /// Promote_MeetMoney_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Update(Promote_MeetMoney_Scope promoteMeetMoneyScope, SqlTransaction transaction);

        /// <summary>
        /// 查询所有商品参加的促销.
        /// </summary>
        /// <returns>
        /// The <see cref="Promote_MeetMoney_Scope"/>.
        /// </returns>
        List<Promote_MeetMoney_Scope> SelectAll();

        /// <summary>
        /// 查询指定活动商品参加的促销.
        /// </summary>
        /// <param name="meetMoneyID">
        /// The meet Money ID.
        /// </param>
        /// <returns>
        /// 参加促销的活动商品.
        /// </returns>
        Promote_MeetMoney_Scope SelectByMeetMoneyID(int meetMoneyID);

        #endregion
    }
}
