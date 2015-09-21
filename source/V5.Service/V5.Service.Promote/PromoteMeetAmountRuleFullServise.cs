// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleFullServise.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠全网范围活动规则数据访问服务.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满件优惠全网范围活动规则数据访问服务.
    /// </summary>
    public class PromoteMeetAmountRuleFullServise
    {
       #region Constants and Fields

        /// <summary>
        /// 满件优惠活动的规则的数据访问服务.
        /// </summary>
        private readonly IPromoteMeetAmountRuleFullDA promoteMeetAmountRuleFullDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetAmountRuleFullServise"/> class.
        /// </summary>
        public PromoteMeetAmountRuleFullServise()
        {
            this.promoteMeetAmountRuleFullDA = new DAFactoryPromote().CreatePromoteMeetAmountRuleFullDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询指定满件优惠活动的规则列表.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件优惠活动的编号.
        /// </param>
        /// <returns>
        /// 满件优惠活动的规则列表.
        /// </returns>
        public List<Promote_MeetAmount_Rule_Full> QueryByMeetAmountID(int meetAmountID)
        {
            return this.promoteMeetAmountRuleFullDA.SelectByMeetAmountID(meetAmountID);
        }

        #endregion
    }
}
