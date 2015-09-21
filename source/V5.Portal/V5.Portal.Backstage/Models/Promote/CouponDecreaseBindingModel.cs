// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseBindingModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券绑定模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    using global::System;

    /// <summary>
    /// 满减券绑定模型类.
    /// </summary>
    public class CouponDecreaseBindingModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置优惠券编号．
        /// </summary>
        public int CouponDecreaseID { get; set; }

        /// <summary>
        ///     获取或设置用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     获取或设置满减券使用的订单.
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        ///     获取或设置优惠券券号．
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     获取或设置优惠券密码．
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     获取或设置赠券原因
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        ///     获取或设置优惠券状态（0：正常，1：已使用，2：已绑定，3：已过期）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置优惠券状态（0：正常，1：已使用，2：已绑定，3：已过期）．
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置满减券使用的时间.
        /// </summary>
        public DateTime? UseTime { get; set; }

        /// <summary>
        ///     获取或设置绑定时间．
        /// </summary>
        public DateTime BindingTime { get; set; }

        #endregion
    }
}