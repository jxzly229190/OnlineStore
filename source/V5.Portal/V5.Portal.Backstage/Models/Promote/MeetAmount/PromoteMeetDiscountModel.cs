// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetDiscountModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件打折促销规则类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote.MeetAmount
{
    /// <summary>
    /// 满足条件打折促销规则类
    /// </summary>
    public class PromoteMeetDiscountModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满足条件类别编号．
        /// </summary>
        public int MeetTypeID { get; set; }

        /// <summary>
        /// 获取或设置满足条件促销活动规则编号．
        /// </summary>
        public int MeetRuleID { get; set; }

        /// <summary>
        /// 获取或设置满足条件促销活动范围规则编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）．
        /// </summary>
        public int ScopeTypeID { get; set; }

        /// <summary>
        /// 获取或设置折扣（例如：打 3 折就等于 0.3）．
        /// </summary>
        public double Discount { get; set; }

        #endregion
    }
}