// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponCashBindingService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   现金券绑定服务类.
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
    /// 现金券绑定服务类.
    /// </summary>
    public class CouponCashBindingService
    {
        #region Constants and Fields

        /// <summary>
        /// 现金券数据库访问对象.
        /// </summary>
        private readonly ICouponCashBindingDA couponCashBindingDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponCashBindingService"/> class.
        /// </summary>
        public CouponCashBindingService()
        {
            this.couponCashBindingDA = new DAFactoryPromote().CreateCouponCashBindingDA();
        }

        #endregion

        #region Public Methods and Operators

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
		/// 添加现金券绑定
		/// </summary>
		/// <param name="couponID">
		/// 券ID
		/// </param>
		/// <param name="userID">
		/// 会员ID
		/// </param>
		/// <param name="cause">
		/// 描述
		/// </param>
		/// <param name="status">
		/// The status.
		/// </param>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		/// <returns>
		/// 绑定的编码
		/// </returns>
		public int Add(int orderId, int couponID, int userID, string cause, int status, SqlTransaction transaction)
		{
			var coupon = new Coupon_Cash_Binding
			{
				Cause = cause,
				CouponCashID = couponID,
				OrderID = orderId,
				Status = status,
				UserID = userID,
				Number = "M" + this.CreateRandomCode(8),
				Password = this.CreateRandomCode(6),
				BindingTime = DateTime.Now,
				UseTime = null
			};
			return this.Add(coupon, transaction);
		}

	    /// <summary>
	    /// 添加现金券绑定
	    /// </summary>
	    /// <param name="couponCashBinding">
	    /// Coupon_Cash_Binding的对象实例
	    /// </param>
	    /// <param name="transaction">数据库事务，默认值为Null</param>
	    /// <returns>
	    /// 现金券绑定编号
	    /// </returns>
	    public int Add(Coupon_Cash_Binding couponCashBinding, SqlTransaction transaction = null)
	    {
			return this.couponCashBindingDA.Insert(couponCashBinding, transaction);
	    }

	    /// <summary>
        /// 添加现金券绑定
        /// </summary>
        /// <param name="couponCashBinding">
        /// Coupon_Cash_Binding的对象实例
        /// </param>
        public void Modify(Coupon_Cash_Binding couponCashBinding)
        {
            this.couponCashBindingDA.Update(couponCashBinding);
        }
        
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
        public List<Coupon_Cash_Binding> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.couponCashBindingDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 订单结算时查询会员可使用的电子券.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="products">
        /// The products.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Coupon_Cash_Binding> QueryByUserID(int userID, int status, Dictionary<int,int> products)
        {
            var couponCash = this.couponCashBindingDA.SelectByUserID(userID, status);
            /*
            var orderBill = new OrderBillServices().QueryOrderBill(products, userID);

            var sb = new StringBuilder();
            foreach (var product in products)
            {
                sb.Append(product.Key + ",");
            }

            foreach (var item in couponCash)
            {
                // 购物车中可以使用当前电子券的商品
                var productlist = this.couponCashBindingDA.SelectProducts(sb.ToString(), item.ID);

                double couponProductPrice = 0.0;// 购物车中可以使用当前电子券的商品
                foreach (var i in productlist)
                {
                   //
                    //couponProductPrice + = (double)orderBill.Products.Find(p => p.ProductID == i).Subtotal;
                }
                //item.CanUse=
            }

             */
            return couponCash;
        }

        #endregion
    }
}
