﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetGiveIntegralModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件送积分促销规则模型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote.MeetMoney
{
    /// <summary>
    /// 满足条件送积分促销规则模型
    /// </summary>
    public class PromoteMeetGiveIntegralModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满足条件类型编号（0：满足金额，1：满足数量）．
        /// </summary>
        public int MeetTypeID { get; set; }

        /// <summary>
        /// 获取或设置满足条件活动规则编号．
        /// </summary>
        public int MeetRuleID { get; set; }

        /// <summary>
        /// 获取或设置赠送积分．
        /// </summary>
        public int Integral { get; set; }

        #endregion
    }
}