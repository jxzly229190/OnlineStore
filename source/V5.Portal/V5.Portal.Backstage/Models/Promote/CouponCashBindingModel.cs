﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponCashBindingModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券绑定模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    using global::System;

    /// <summary>
    /// 现金券绑定模型类.
    /// </summary>
    public class CouponCashBindingModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置现金券编号．
        /// </summary>
        public int CouponCashID { get; set; }

        /// <summary>
        ///     获取或设置用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     获取或设置现金券使用的订单编号.
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        ///     获取或设置现金券券号．
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     获取或设置现金券密码．
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     获取或设置赠券原因
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        ///     获取或设置现金券状态（0：正常，1：已使用，2：已过期）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///      获取或设置现金券状态（0：正常，1：已使用，2：已过期）．
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置现金券使用的时间.
        /// </summary>
        public DateTime? UseTime { get; set; }

        /// <summary>
        ///     获取或设置绑定时间．
        /// </summary>
        public DateTime BindingTime { get; set; }

        /// <summary>
        ///     获取或设置赠送数量
        /// </summary>
        public int GiveNumber { get; set; }

        #endregion
    }
}