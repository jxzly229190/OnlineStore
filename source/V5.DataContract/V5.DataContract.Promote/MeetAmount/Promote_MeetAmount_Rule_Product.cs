// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetAmount_Rule_Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠指定商品范围活动规则类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetAmount
{
    using V5.DataContract.Promote.MeetMoney;
    using V5.DataContract.Promote.PromoteMeetAmount;

    /// <summary>
    /// 满件优惠指定商品范围活动规则类
    /// </summary>
    public class Promote_MeetAmount_Rule_Product
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
        /// 获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

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
        /// 获取或设置礼物的编号．
        /// </summary>
        public int GiftID { get; set; }

        /// <summary>
        /// 获取或设置礼物的名称．
        /// </summary>
        public string GiftName { get; set; }

        /// <summary>
        /// 获取或设置满足条件送礼物促销规则类
        /// </summary>
        public Promote_Meet_GiveGift PromoteMeetGiveGift { get; set; }

        /// <summary>
        /// 获取或设置满足条件打折促销规则类
        /// </summary>
        public Promote_Meet_Discount PromoteMeetDiscount { get; set; }

        #endregion
    }
}
