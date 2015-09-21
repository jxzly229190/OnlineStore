// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountScopeService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠活动范围数据访问服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataContract.Promote.MeetAmount;

    /// <summary>
    /// 满件优惠活动范围数据访问服务类.
    /// </summary>
    public class PromoteMeetAmountScopeService
    {
        #region Constants and Fields

        /// <summary>
        /// The promote meet money scope.
        /// </summary>
        private readonly IPromoteMeetAmountScopeDA promoteMeetAmountScope;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetAmountScopeService"/> class.
        /// </summary>
        public PromoteMeetAmountScopeService()
        {
            this.promoteMeetAmountScope = new DAFactoryPromote().CreatePromoteMeetAmountScopeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询所有商品参加个促销.
        /// </summary>
        /// <returns>
        /// The <see cref="Promote_MeetAmount_Scope"/>.
        /// </returns>
        public List<Promote_MeetAmount_Scope> QueryAll()
        {
            return this.promoteMeetAmountScope.SelectAll();
        }

        /// <summary>
        /// 查询指定活动商品参加的促销.
        /// </summary>
        /// <param name="meetAmountID">
        /// 促销活动编号.
        /// </param>
        /// <returns>
        /// 参加促销的活动商品.
        /// </returns>
        public Promote_MeetAmount_Scope QueryByMeetAmountID(int meetAmountID)
        {
            return this.promoteMeetAmountScope.SelectByMeetAmountID(meetAmountID);
        }

        #endregion
    }
}
