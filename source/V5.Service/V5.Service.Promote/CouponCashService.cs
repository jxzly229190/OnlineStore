// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponCashService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券服务类.
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
    /// 现金券服务类.
    /// </summary>
    public class CouponCashService
    {
        #region Constants and Fields

        /// <summary>
        /// 现金券数据库访问对象.
        /// </summary>
        private readonly ICouponCashDA couponCashDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponCashService"/> class.
        /// </summary>
        public CouponCashService()
        {
            this.couponCashDA = new DAFactoryPromote().CreateCouponCashDA();
        }

        #endregion

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
        public int Add(Coupon_Cash couponCash)
        {
            return this.couponCashDA.InsertCoupon_Cash(couponCash);
        }

        /// <summary>
        /// 查询所有有效的现金券.
        /// </summary>
        /// <returns>
        /// 现金券列表.
        /// </returns>
        public List<Coupon_Cash> QueryAllValid()
        {
            return this.couponCashDA.SelectAllValidCouponCash();
        }

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
        public List<Coupon_Cash> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.couponCashDA.Paging(paging, out pageCount, out totalCount);
        }

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
        public List<Coupon_Query> CouponPaging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.couponCashDA.CouponPaging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询指定编号的现金券信息.
        /// </summary>
        /// <param name="ID">
        /// 现金券编号.
        /// </param>
        /// <returns>
        /// Coupon_Cash的对象实例.
        /// </returns>
        public Coupon_Cash QueryCouponCashByID(int ID)
        {
            return this.couponCashDA.SelectCouponCashByID(ID);
        }

        /// <summary>
        /// 增加现金券初始数量
        /// </summary>
        /// <param name="ID">
        /// 现金券编号
        /// </param>
        /// <param name="initialNumber">
        /// 更新的初始数量.
        /// </param>
        public void Modify(int ID, int initialNumber)
        {
            this.couponCashDA.UpdateInitialNumberByID(ID, initialNumber);
        }

        /// <summary>
        /// 查询指定的现金券名称
        /// </summary>
        /// <param name="name">
        /// 现金券名称
        /// </param>
        /// <returns>
        /// 0：没有符合条件的结果、1：有符合条件的结果
        /// </returns>
        public int IsNameExists(string name)
        {
            return this.couponCashDA.IsNameExists(name);
        }

        #endregion
    }
}
