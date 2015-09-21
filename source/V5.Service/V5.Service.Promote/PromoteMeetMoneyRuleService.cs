// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyRuleService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;
    
    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetMoney;
    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满就送促销服务类.
    /// </summary>
    public class PromoteMeetMoneyRuleService
    {
        #region Constants and Fields

        /// <summary>
        /// 满就送促销数据访问对象.
        /// </summary>
        private readonly IPromoteMeetMoneyRuleDA promoteMeetMoneyRuleDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetMoneyRuleService"/> class.
        /// </summary>
        public PromoteMeetMoneyRuleService()
        {
            this.promoteMeetMoneyRuleDA = new DAFactoryPromote().CreatePromoteMeetMoneyRuleDA();
        }

        #endregion

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
        public List<Promote_MeetMoney_Rule> QueryByMeetMoneyID(int meetMoneyID)
        {
            return this.promoteMeetMoneyRuleDA.SelectByMeetMoneyID(meetMoneyID);
        }

        #endregion
    }
}
