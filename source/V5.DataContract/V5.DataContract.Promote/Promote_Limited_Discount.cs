// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_Limited_Discount.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   限时打折促销活动类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    using System;

    /// <summary>
    /// 限时打折促销活动类
    /// </summary>
    public class Promote_Limited_Discount
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置商品名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置商品价格．
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        /// 获取或设置活动名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置折扣．
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// 获取或设置单品折后价．
        /// </summary>
        public double DiscountPrice { get; set; }

        /// <summary>
        /// 获取或设置活动商品总数量．
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 获取或设置每人限制购买数量．
        /// </summary>
        public int LimitedBuyQuantity { get; set; }

        /// <summary>
        /// 获取或设置是否仅支持在线支付．
        /// </summary>
        public bool IsOnlinePayment { get; set; }

        /// <summary>
        /// 获取或设置是否需要手机验证（true:手机验证后才可参与）．
        /// </summary>
        public bool IsMobileValidate { get; set; }

        /// <summary>
        /// 获取或设置是否可使用优惠券（true:可以使用）．
        /// </summary>
        public bool IsUseCoupon { get; set; }

        /// <summary>
        /// 获取或设置是否是新会员活动（true:只能新会员参与）．
        /// </summary>
        public bool IsNewUser { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 获取或设置开始时间．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置结束时间．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置活动状态．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置活动剩余数量．
        /// </summary>
        public int RemainQuantity { get; set; }

        #endregion
    }
}
