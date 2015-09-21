// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyScopeService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满额优惠活动范围数据访问服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetMoney;
    using V5.DataContract.Promote.MeetMoney;

    /// <summary>
    /// 满额优惠活动范围数据访问服务类.
    /// </summary>
    public class PromoteMeetMoneyScopeService
    {
        #region Constants and Fields

        /// <summary>
        /// The promote meet money scope.
        /// </summary>
        private readonly IPromoteMeetMoneyScopeDA promoteMeetMoneyScope;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetMoneyScopeService"/> class.
        /// </summary>
        public PromoteMeetMoneyScopeService()
        {
            this.promoteMeetMoneyScope = new DAFactoryPromote().CreatePromoteMeetMoneyScopeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询所有商品参加个促销.
        /// </summary>
        /// <returns>
        /// The <see cref="Promote_MeetMoney_Scope"/>.
        /// </returns>
        public List<Promote_MeetMoney_Scope> QueryAll()
        {
            return this.promoteMeetMoneyScope.SelectAll();
        }

        /// <summary>
        /// 查询指定活动商品参加的促销.
        /// </summary>
        /// <param name="meetMoneyID">
        /// The meet Money ID.
        /// </param>
        /// <returns>
        /// 参加促销的活动商品.
        /// </returns>
        public Promote_MeetMoney_Scope QueryByMeetMoneyID(int meetMoneyID)
        {
            return this.promoteMeetMoneyScope.SelectByMeetMoneyID(meetMoneyID);
        }

        #endregion
    }
}
