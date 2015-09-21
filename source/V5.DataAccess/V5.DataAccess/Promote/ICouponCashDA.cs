// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICouponCashDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 现金券数据访问接口.
    /// </summary>
    public interface ICouponCashDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加现金券信息.
        /// </summary>
        /// <param name="couponCash">
        /// 现金券Coupon_Cash的对象实例.
        /// </param>
        /// <returns>
        /// 现金券的编号.
        /// </returns>
        int InsertCoupon_Cash(Coupon_Cash couponCash);

       /// <summary>
        /// 查询现金券列表
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
        /// 现金券列表
        /// </returns>
        List<Coupon_Cash> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查询所有有效的现金券.
        /// </summary>
        /// <returns>
        /// 现金券列表.
        /// </returns>
        List<Coupon_Cash> SelectAllValidCouponCash();

        /// <summary>
        /// 查询所有有效的现金券.
        /// </summary>
        /// <returns>
        /// 现金券列表.
        /// </returns>
        List<Coupon_Query> CouponPaging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查询指定编号的现金券信息.
        /// </summary>
        /// <param name="ID">
        /// 现金券编号.
        /// </param>
        /// <returns>
        /// Coupon_Cash的对象实例.
        /// </returns>
        Coupon_Cash SelectCouponCashByID(int ID);

        /// <summary>
        /// 增加现金券初始数量
        /// </summary>
        /// <param name="ID">
        /// 现金券编号
        /// </param>
        /// <param name="initialNumber">
        /// 更新的初始数量.
        /// </param>
        void UpdateInitialNumberByID(int ID, int initialNumber);

        /// <summary>
        /// 查询指定的现金券名称
        /// </summary>
        /// <param name="name">
        /// 现金券名称
        /// </param>
        /// <returns>
        /// 0：没有符合条件的结果、1：有符合条件的结果
        /// </returns>
        int IsNameExists(string name);

        #endregion
    }
}