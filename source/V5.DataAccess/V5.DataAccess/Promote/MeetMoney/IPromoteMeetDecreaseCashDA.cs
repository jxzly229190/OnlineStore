// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetDecreaseCashDA.cs" company="www.gjw.com ">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件减现金促销规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满足条件减现金促销规则数据访问接口.
    /// </summary>
    public interface IPromoteMeetDecreaseCashDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满足条件减现金促销规则.
        /// </summary>
        /// <param name="promoteMeetDecreaseCash">
        /// Promote_Meet_DecreaseCash的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满足条件减现金促销规则编号.
        /// </returns>
        int Insert(Promote_Meet_DecreaseCash promoteMeetDecreaseCash, SqlTransaction transaction);

        /// <summary>
        /// 修改满足条件减现金促销规则.
        /// </summary>
        /// <param name="promoteMeetDecreaseCash">
        /// Promote_Meet_DecreaseCash的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void UpdateByRuleID(Promote_Meet_DecreaseCash promoteMeetDecreaseCash, SqlTransaction transaction);

        /// <summary>
        /// 删除满足条件减现金促销规则.
        /// </summary>
        /// <param name="id">
        /// 满足条件减现金促销规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Delete(int id, SqlTransaction transaction);

        /// <summary>
        /// 删除满足条件减现金促销规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void DeleteByRuleID(int ruleID, SqlTransaction transaction);

        /// <summary>
        /// 检查是否存在指定规则.
        /// </summary>
        /// <param name="ruleID">
        /// 满就送活动规则编号.
        /// </param>
        /// <returns>
        /// true:存在，false:不存在.
        /// </returns>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        bool IsExist(int ruleID, SqlTransaction transaction);

        #endregion
    }
}
