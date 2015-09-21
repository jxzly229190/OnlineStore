// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderBillDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   购物结算.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.DataContract.Transact.ShoppingCart;

    /// <summary>
    /// 购物结算.
    /// </summary>
    public interface IOrderBillDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 查询商品的促销结果(用于刷新缓存).
        /// </summary>
        /// <param name="productIDs">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        List<Product_Cache> SelectByAllProduct();

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        Product_Cache SelectByProduct(int productID);

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="quantity">
        /// The quantity.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        Cart_Product SelectByProduct(int productID, int quantity, int userID);

        /// <summary>
        /// 验证商品是否参加促销.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="type">
        /// 1：单品促销（限时抢购，团购），2：多品促销（满额优惠、满件优惠）.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool VerifyPromote(int productID, int type);

        /// <summary>
        /// 单品促销结果（限时抢购、团购）.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        Cart_Product Select(int productID, int userID);

        /*
        /// <summary>
        /// 查询购物车商品的赠品商品.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="promoteType">
        /// The promote Type.
        /// </param>
        /// <param name="totalPrice">
        /// The total Price.
        /// </param>
        /// <param name="totalQuantity">
        /// The total Quantity.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        Gift_Product SelectProductByPromoteID(int promoteID, string promoteType, double totalPrice, int totalQuantity);

        /// <summary>
        /// 查询购物车商品的赠品电子券.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="promoteType">
        /// The promote Type.
        /// </param>
        /// <param name="totalPrice">
        /// The total Price.
        /// </param>
        /// <param name="totalQuantity">
        /// The total Quantity.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        Gift_Coupon SelectCouponByPromoteID(int promoteID, string promoteType, double totalPrice, int totalQuantity);
        */

        /// <summary>
        /// 查询购物车商品的满减金额.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="totalPrice">
        /// The total Price.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        double SelectMeetMoneyCutMoney(int promoteID, double totalPrice);

        /// <summary>
        /// 查询购物车商品的满件折扣金额.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="totalQuantity">
        /// The total Quantity.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        double SelectMeetAmountCutMoney(int promoteID, int totalQuantity);

        #endregion 
    }
}
