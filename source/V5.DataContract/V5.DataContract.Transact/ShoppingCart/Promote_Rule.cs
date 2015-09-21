namespace V5.DataContract.Transact.ShoppingCart
{
    public class Promote_Rule
    {
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
    }
}
