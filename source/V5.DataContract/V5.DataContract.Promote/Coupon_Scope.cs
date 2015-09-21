// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Coupon_Scope.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   电子券使用范围类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    using System;

    /// <summary>
    /// 电子券使用范围类.
    /// </summary>
    public class Coupon_Scope
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置电子券编号.
        /// </summary>
        public int CouponID { get; set; }

        /// <summary>
        ///     获取或设置电子券类型（0：现金券， 1：满减券）.
        /// </summary>
        public int CouponTypeID { get; set; }

        /// <summary>
        ///     获取或设置范围类型（0：全场，1：商品种类，2：商品类型，3：商品品牌，4：具体商品）.
        /// </summary>
        public int ScopeType { get; set; }

        /// <summary>
        ///     获取或设置目标范围类型编号.
        /// </summary>
        public int TargetTypeID { get; set; }

        /// <summary>
        ///     获取或设置目标范围类型名称.
        /// </summary>
        public string TargetTypeName { get; set; }

        /// <summary>
        ///     获取或设置创建时间.
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}
