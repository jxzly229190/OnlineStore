// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetAmount_Rule.cs" company="www.gjw.com">
//   (C) 2014 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠规则类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetAmount
{
    /// <summary>
    /// 满件优惠规则类.
    /// </summary>
    public class Promote_MeetAmount_Rule
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
        /// 获取或设置打折优惠折扣．
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

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}