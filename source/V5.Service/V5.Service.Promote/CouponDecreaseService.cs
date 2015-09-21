// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满减券服务类.
    /// </summary>
    public class CouponDecreaseService
    {
        #region Constants and Fields

        /// <summary>
        /// 满减券数据库访问对象.
        /// </summary>
        private readonly ICouponDecreaseDA couponDecreaseDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponDecreaseService"/> class.
        /// </summary>
        public CouponDecreaseService()
        {
            this.couponDecreaseDA = new DAFactoryPromote().CreateCouponDecreaseDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加满减券.
        /// </summary>
        /// <param name="couponDecrease">
        /// Coupon_Decrease的对象实例.
        /// </param>
        /// <returns>
        /// 满减券的编号.
        /// </returns>
        public int Add(Coupon_Decrease couponDecrease)
        {
            return this.couponDecreaseDA.InsertCouponDecrease(couponDecrease);
        }

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
        public List<Coupon_Decrease> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.couponDecreaseDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询全部有效的满减券.
        /// </summary>
        /// <returns>
        /// 满减券列表.
        /// </returns>
        public List<Coupon_Decrease> QueryAllValidCouponDecreases()
        {
            return this.couponDecreaseDA.SelectAllValidCouponDecrease();
        }

        /// <summary>
        /// 查询指定编号的满减券信息.
        /// </summary>
        /// <param name="ID">
        /// 满减券编号.
        /// </param>
        /// <returns>
        /// Coupon_Decrease的对象实例.
        /// </returns>
        public Coupon_Decrease QueryCouponDecreaseByID(int ID)
        {
            return this.couponDecreaseDA.SelectCouponDecreaseByID(ID);
        }

        /// <summary>
        /// 增加满减券初始数量
        /// </summary>
        /// <param name="ID">
        /// 满减券编号
        /// </param>
        /// <param name="initialNumber">
        /// 更新的初始数量.
        /// </param>
        public void ModifyInitialNumber(int ID, int initialNumber)
        {
            this.couponDecreaseDA.UpdateInitialNumberByID(ID, initialNumber);
        }

        /// <summary>
        /// 查询指定的满减券名称
        /// </summary>
        /// <param name="name">
        /// 满减券名称
        /// </param>
        /// <returns>
        /// 0：没有符合条件的结果、1：有符合条件的结果
        /// </returns>
        public int IsNameExists(string name)
        {
            return this.couponDecreaseDA.IsNameExists(name);
        }

        #endregion
    }
}
