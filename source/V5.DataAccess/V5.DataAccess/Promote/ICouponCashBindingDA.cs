// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICouponCashBindingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券绑定数据访问接口类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 现金券绑定数据访问接口类.
    /// </summary>
    public interface ICouponCashBindingDA
    {
        #region Public Methods and Operators

	    /// <summary>
	    /// 添加现金券绑定.
	    /// </summary>
	    /// <param name="couponCashBinding">
	    /// Coupon_Cash_Binding的对象实例.
	    /// </param>
	    /// <param name="transaction">数据库事务</param>
	    /// <returns>
	    /// 现金券绑定的编号.
	    /// </returns>
	    int Insert(Coupon_Cash_Binding couponCashBinding, SqlTransaction transaction = null);

        /// <summary>
        /// 添加现金券绑定.
        /// </summary>
        /// <param name="couponCashBinding">
        /// Coupon_Cash_Binding的对象实例.
        /// </param>
        void Update(Coupon_Cash_Binding couponCashBinding);

       /// <summary>
        /// 查询现金券绑定列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 现金券绑定列表
        /// </returns>
        List<Coupon_Cash_Binding> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 根据指定会员编号查询电子券列表.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="status">
        /// 状态（1：可用，2：不可用）
        /// </param>
        /// <returns>
        /// 电子券列表.
        /// </returns>
        List<Coupon_Cash_Binding> SelectByUserID(int userID, int status);

        /// <summary>
        /// 可以使用指定电子券的商品.
        /// </summary>
        /// <param name="products">
        /// 可以使用电子券的商品(排除).
        /// </param>
        /// <param name="couponID">
        /// The coupon ID.
        /// </param>
        /// <returns>
        /// 可以使用指定电子券的商品列表.
        /// </returns>
        List<int> SelectProducts(string products, int couponID);

        #endregion
    }
}