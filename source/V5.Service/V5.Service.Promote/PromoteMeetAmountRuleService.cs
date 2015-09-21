// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件促销促销服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满件促销促销服务类.
    /// </summary>
    public class PromoteMeetAmountRuleService
    {
        #region Constants and Fields

        /// <summary>
        /// 满件促销促销数据访问对象.
        /// </summary>
        private readonly IPromoteMeetAmountRuleDA promoteMeetAmountRuleDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetAmountRuleService"/> class.
        /// </summary>
        public PromoteMeetAmountRuleService()
        {
            this.promoteMeetAmountRuleDA = new DAFactoryPromote().CreatePromoteMeetAmountRuleDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询指定满件促销的促销规则.
        /// </summary>
        /// <param name="meetAmountID">
        /// 满件促销促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount_Rule对象实例的列表.
        /// </returns>
        public List<Promote_MeetAmount_Rule> QueryByMeetAmountID(int meetAmountID)
        {
            return this.promoteMeetAmountRuleDA.SelectByMeetAmountID(meetAmountID);
        }

        #endregion
    }
}
