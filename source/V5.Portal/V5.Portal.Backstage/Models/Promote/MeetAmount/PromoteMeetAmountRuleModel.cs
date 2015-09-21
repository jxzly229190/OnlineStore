﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountRuleModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件促销规则模型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote.MeetAmount
{
    /// <summary>
    /// 满足条件促销规则模型
    /// </summary>
    public class PromoteMeetAmountRuleModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满件优惠促销活动编号．
        /// </summary>
        public int PromoteMeetAmountID { get; set; }

        /// <summary>
        /// 获取或设置满足件数．
        /// </summary>
        public int MeetAmount { get; set; }

        /// <summary>
        /// 获取或设置是否打折．
        /// </summary>
        public bool IsDiscount { get; set; }

        /// <summary>
        /// 获取或设置折扣（例如：打 3 折就等于 0.3）．
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// 获取或设置是否包邮．
        /// </summary>
        public bool IsNoPostage { get; set; }

        /// <summary>
        /// 获取或设置是否送礼物．
        /// </summary>
        public bool IsGiveGift { get; set; }

        /// <summary>
        /// 获取或设置礼物的商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置礼物的名称．
        /// </summary>
        public string ProductName { get; set; }

        #endregion
    }
}