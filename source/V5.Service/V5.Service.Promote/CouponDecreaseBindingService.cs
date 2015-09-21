// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseBindingService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满减券数据服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Security.Cryptography;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    ///     满减券数据服务类.
    /// </summary>
    public class CouponDecreaseBindingService
    {
        #region Constants and Fields

        /// <summary>
        /// 满减券数据库访问对象.
        /// </summary>
        private readonly ICouponDecreaseBindingDA couponDecreaseBindingDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponDecreaseBindingService"/> class.
        /// </summary>
        public CouponDecreaseBindingService()
        {
            this.couponDecreaseBindingDA = new DAFactoryPromote().CreateCouponDecreaseBindingDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="orderID">
        /// The order ID.
        /// </param>
        /// <param name="couponID">
        /// The coupon id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="cause">
        /// The cause.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Add(int orderID, int couponID, int userID, string cause, int status, SqlTransaction transaction)
        {
            var coupon = new Coupon_Decrease_Binding
                             {
                                 Cause = cause,
                                 CouponDecreaseID = couponID,
                                 OrderID = orderID,
                                 UserID = userID,
                                 Status = status,
                                 Number = "L" + this.CreateRandomCode(8),
                                 Password = this.CreateRandomCode(6),
                                 BindingTime = DateTime.Now,
                                 UseTime = null
                             };
            return this.Add(coupon, transaction);
        }

        /// <summary>
		/// 生成指定长度随机码
		/// </summary>
		/// <param name="codeCount">随机码长度</param>
		/// <returns>生成的随机码</returns>
		public string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = string.Empty;
            var bytes = new byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            var rand = new Random(BitConverter.ToInt32(bytes, 0));
            for (int i = 0; i < codeCount; i++)
            {
                int t = rand.Next(36);
                randomCode += allCharArray[t];
            }

            return randomCode;
        }

	    /// <summary>
	    /// 添加满减券
	    /// </summary>
	    /// <param name="couponDecreaseBinding">
	    /// Coupon_Decrease_Binding的对象实例
	    /// </param>
	    /// <param name="transaction">数据库事务，默认为Null</param>
	    /// <returns>
	    /// 满减券绑定的编号
	    /// </returns>
	    public int Add(Coupon_Decrease_Binding couponDecreaseBinding, SqlTransaction transaction = null)
	    {
	        return this.couponDecreaseBindingDA.Insert(couponDecreaseBinding, transaction);
	    }

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
        public List<Coupon_Decrease_Binding> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.couponDecreaseBindingDA.Paging(paging, out pageCount, out totalCount);
        }

        #endregion
    }
}
