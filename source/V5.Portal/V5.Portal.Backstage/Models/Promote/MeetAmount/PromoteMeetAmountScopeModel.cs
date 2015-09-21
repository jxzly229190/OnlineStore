// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountScopeModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠活动商品.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote.MeetAmount
{
    /// <summary>
    /// 满件优惠活动商品.
    /// </summary>
    public class PromoteMeetAmountScopeModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满就送活动编号．
        /// </summary>
        public int MeetAmountID { get; set; }

        /// <summary>
        /// 获取或设置参与活动的商品．
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}