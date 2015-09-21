// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_Meet_GiveGift.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满足条件送礼物促销规则类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetMoney
{
    /// <summary>
    /// 满足条件送礼物促销规则类
    /// </summary>
    public class Promote_Meet_GiveGift
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
        /// 获取或设置满足条件促销活动范围规则编号（1：全场，2：商品类别，3：商品品牌，4：指定商品）．
        /// </summary>
        public int ScopeTypeID { get; set; }

        /// <summary>
        /// 获取或设置礼物编号．
        /// </summary>
        public int ProductID { get; set; }

        #endregion
    }
}
