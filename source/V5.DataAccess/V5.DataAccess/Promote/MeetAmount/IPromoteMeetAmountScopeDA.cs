// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetAmountScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满额优惠活动范围数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetAmount
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满额优惠活动范围数据访问接口.
    /// </summary>
    public interface IPromoteMeetAmountScopeDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetAmountScope">
        /// Promote_MeetAmount_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满额优惠活动范围编号.
        /// </returns>
        int Insert(Promote_MeetAmount_Scope promoteMeetAmountScope, SqlTransaction transaction);

        /// <summary>
        /// 修改满额优惠活动范围.
        /// </summary>
        /// <param name="promoteMeetAmountScope">
        /// Promote_MeetAmount_Scope 的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Update(Promote_MeetAmount_Scope promoteMeetAmountScope, SqlTransaction transaction);

        /// <summary>
        /// 查询所有商品参加的促销.
        /// </summary>
        /// <returns>
        /// The <see cref="Promote_MeetAmount_Scope"/>.
        /// </returns>
        List<Promote_MeetAmount_Scope> SelectAll();

        /// <summary>
        /// 查询指定活动商品参加的促销.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满额优惠活动编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_MeetAmount_Scope"/>.
        /// </returns>
        Promote_MeetAmount_Scope SelectByMeetAmountID(int meetAmountID);

        #endregion
    }
}
