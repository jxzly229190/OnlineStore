// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponScopeService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   电子券适用范围服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Promote;

    /// <summary>
    /// 电子券适用范围服务类.
    /// </summary>
    public class CouponScopeService
    {
        #region Constants and Fields

        /// <summary>
        /// 电子券使用范围数据访问对象.
        /// </summary>
        private readonly ICouponScopeDA couponScopeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponScopeService"/> class.
        /// </summary>
        public CouponScopeService()
        {
            this.couponScopeDA = new DAFactoryPromote().CreateCouponScopeDA();
        }

        #endregion

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
        public int Add(Coupon_Scope couponScope)
        {
            return this.couponScopeDA.Inseret(couponScope);
        }

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
        public List<Coupon_Scope> QueryByCouponID(int couponID, int couponType)
        {
            return this.couponScopeDA.SelectByCouponID(couponID, couponType);
        }

        #endregion
    }
}
