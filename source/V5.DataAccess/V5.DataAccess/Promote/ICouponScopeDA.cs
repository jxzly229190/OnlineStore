// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICouponScopeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   电子券使用范围数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;

    /// <summary>
    /// 电子券使用范围数据访问接口.
    /// </summary>
    public interface ICouponScopeDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加电子券使用范围.
        /// </summary>
        /// <param name="couponScope">
        /// Coupon_Scope的对象实例.
        /// </param>
        /// <returns>
        ///  电子券使用范围编号.
        /// </returns>
        int Inseret(Coupon_Scope couponScope);

        /// <summary>
        /// 查询指定编号的电子券使用范围列表.
        /// </summary>
        /// <param name="couponID">
        /// 电子券编号.
        /// </param>
        /// <param name="couponType">
        /// 电子券类型（0：现金券，1：满减券）.
        /// </param>
        /// <returns>
        /// Coupon_Scope的对象实例的列表.
        /// </returns>
        List<Coupon_Scope> SelectByCouponID(int couponID, int couponType);

        #endregion
    }
}
