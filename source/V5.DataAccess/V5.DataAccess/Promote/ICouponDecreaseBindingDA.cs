// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICouponDecreaseBindingDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券绑定数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满减券绑定数据访问接口.
    /// </summary>
    public interface ICouponDecreaseBindingDA
    {
        #region Public Methods and Operators

	    /// <summary>
	    /// 添加满减券绑定.
	    /// </summary>
	    /// <param name="couponDecreaseBinding">
	    /// Coupon_Cash_Binding的对象实例.
	    /// </param>
	    /// <param name="transaction">数据库事务，默认为Null</param>
	    /// <returns>
	    /// 满减券绑定编号.
	    /// </returns>
	    int Insert(Coupon_Decrease_Binding couponDecreaseBinding, SqlTransaction transaction = null);

        /// <summary>
        /// 查询满减券绑定列表
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
        /// 满减券绑定列表
        /// </returns>
        List<Coupon_Decrease_Binding> Paging(Paging paging, out int pageCount, out int totalCount);

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
        List<Coupon_Decrease_Binding> SelectByUserID(int userID, int status);
        
        #endregion
    }
}