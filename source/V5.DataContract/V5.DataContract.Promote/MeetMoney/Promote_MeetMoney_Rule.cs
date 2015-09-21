// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetMoney_Rule.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销活动规则类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetMoney
{
    /// <summary>
    /// 满就送促销活动规则类
    /// </summary>
    public class Promote_MeetMoney_Rule
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满就送活动编号．
        /// </summary>
        public int PromoteMeetMoneyID { get; set; }

        /// <summary>
        /// 获取或设置满足金额．
        /// </summary>
        public double MeetMoney { get; set; }

        /// <summary>
        /// 获取或设置是否上不封顶（叠加）．
        /// </summary>
        public bool IsNoCeiling { get; set; }

        /// <summary>
        /// 获取或设置是否减现金．
        /// </summary>
        public bool IsDecreaseCash { get; set; }

        /// <summary>
        /// 获取或设置减少现金金额．
        /// </summary>
        public double DecreaseCash { get; set; }

        /// <summary>
        /// 获取或设置是否送礼物．
        /// </summary>
        public bool IsGiveGift { get; set; }

        /// <summary>
        /// 获取或设置礼物的编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置礼物的名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置是否送积分．
        /// </summary>
        public bool IsGiveIntegral { get; set; }

        /// <summary>
        /// 获取或设置赠送积分．
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 获取或设置是否免邮．
        /// </summary>
        public bool IsNoPostage { get; set; }

        /// <summary>
        /// 获取或设置是否送优惠券．
        /// </summary>
        public bool IsGiveCoupon { get; set; }

        /// <summary>
        /// 获取或设置优惠券类型编号（0：现金券，1：满减券）．
        /// </summary>
        public int CouponType { get; set; }

        /// <summary>
        /// 获取或设置优惠券编号．
        /// </summary>
        public int CouponID { get; set; }

        /// <summary>
        /// 获取或设置现金券名称．
        /// </summary>
        public string CashName { get; set; }

        /// <summary>
        /// 获取或设置满减券名称．
        /// </summary>
        public string DecreaseName { get; set; }
        
        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}
