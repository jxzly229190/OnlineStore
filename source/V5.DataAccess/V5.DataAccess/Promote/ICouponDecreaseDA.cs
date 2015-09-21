// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICouponDecreaseDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满减券数据访问接口.
    /// </summary>
    public interface ICouponDecreaseDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满减券信息.
        /// </summary>
        /// <param name="couponDecrease">
        /// 现金券Voucher_Cash的对象实例.
        /// </param>
        /// <returns>
        /// 满减券的编号.
        /// </returns>
        int InsertCouponDecrease(Coupon_Decrease couponDecrease);

        /// <summary>
        /// 查询满减券列表
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
        /// 满减券列表
        /// </returns>
        List<Coupon_Decrease> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查询全部有效的满减券.
        /// </summary>
        /// <returns>
        /// 满减券列表.
        /// </returns>
        List<Coupon_Decrease> SelectAllValidCouponDecrease();

        /// <summary>
        /// 查询指定编号的满减券信息.
        /// </summary>
        /// <param name="ID">
        /// 编号.
        /// </param>
        /// <returns>
        /// Coupon_Decrease的对象实例.
        /// </returns>
        Coupon_Decrease SelectCouponDecreaseByID(int ID);

        /// <summary>
        /// 增加满减券初始数量
        /// </summary>
        /// <param name="ID">
        /// 满减券编号
        /// </param>
        /// <param name="initialNumber">
        /// 更新的初始数量.
        /// </param>
        void UpdateInitialNumberByID(int ID, int initialNumber);

        /// <summary>
        /// 查询指定的满减券名称
        /// </summary>
        /// <param name="name">
        /// 满减券名称
        /// </param>
        /// <returns>
        /// 0：没有符合条件的结果、1：有符合条件的结果
        /// </returns>
        int IsNameExists(string name);


        #endregion
    }
}