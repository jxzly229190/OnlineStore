// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetGiveGiftDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件送礼物促销规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满足条件送礼物促销规则数据访问接口.
    /// </summary>
    public interface IPromoteMeetGiveGiftDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满足条件送礼物促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveGift">
        /// Promote_Meet_GiveGift的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 添加满足条件送礼物促销规则的编号.
        /// </returns>
        int Insert(Promote_Meet_GiveGift promoteMeetGiveGift, SqlTransaction transaction);

        /// <summary>
        /// 添加满足条件送礼物促销规则.
        /// </summary>
        /// <param name="promoteMeetGiveGift">
        /// Promote_Meet_GiveGift的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void UpdateByRuleID(Promote_Meet_GiveGift promoteMeetGiveGift, SqlTransaction transaction);

        /// <summary>
        /// 删除满足条件送礼物促销规则.
        /// </summary>
        /// <param name="id">
        /// 满足条件送礼物促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Delete(int id, SqlTransaction transaction);

        /// <summary>
        /// 删除满足条件送礼物促销规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <param name="meetTypeID">
        /// 促销类型（0：满就送，1：满件优惠）
        /// </param>
        /// <param name="scopeID">
        /// 范围编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void DeleteByRuleID(int ruleID, int meetTypeID, int scopeID, SqlTransaction transaction);

        /// <summary>
        /// 检查是否存在指定规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <returns>
        /// true:存在，false:不存在.
        /// </returns>
        /// <param name="meetTypeID">
        /// 促销类型（0：满就送，1：满件优惠）
        /// </param>
        /// <param name="scopeID">
        /// 范围编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        bool IsExist(int ruleID, int meetTypeID, int scopeID, SqlTransaction transaction);

        #endregion
    }
}
