// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderBillServices.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   购物结算服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using V5.DataAccess;
    using V5.DataAccess.Product;
    using V5.DataAccess.Promote;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataAccess.Promote.MeetMoney;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Product;
    using V5.DataContract.Transact.ShoppingCart;

    /// <summary>
    /// 购物结算服务类.
    /// </summary>
    public class OrderBillServices
    {
        #region Constants and Fields

        /// <summary>
        /// 购物结算数据访问类.
        /// </summary>
        private readonly IOrderBillDA orderBillDA;

        /// <summary>
        /// 满额优惠促销规则数据访问类.
        /// </summary>
        private readonly IPromoteMeetMoneyRuleDA promoteMeetMoneyRuleDA;

        /// <summary>
        /// 满件优惠促销规则数据访问类.
        /// </summary>
        private readonly IPromoteMeetAmountRuleDA promoteMeetAmountRuleDA;

        /// <summary>
        /// 商品数据访问类.
        /// </summary>
        private readonly IProductDA productDA;

        /// <summary>
        /// 现金券数据访问类.
        /// </summary>
        private readonly ICouponCashDA couponCashDA;

        /// <summary>
        /// 满减券数据访问类.
        /// </summary>
        private readonly ICouponDecreaseDA couponDecreaseDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBillServices"/> class.
        /// </summary>
        public OrderBillServices()
        {
            this.orderBillDA = new DAFactoryTransact().CreateOrderBillDA();
            this.promoteMeetAmountRuleDA = new DAFactoryPromote().CreatePromoteMeetAmountRuleDA();
            this.promoteMeetMoneyRuleDA = new DAFactoryPromote().CreatePromoteMeetMoneyRuleDA();
            this.productDA = new DAFactoryProduct().CreateProductDA();
            this.couponCashDA = new DAFactoryPromote().CreateCouponCashDA();
            this.couponDecreaseDA = new DAFactoryPromote().CreateCouponDecreaseDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 根据商品编号和数量生成结算详情.
        /// </summary>
        /// <param name="products">
        /// The product.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// The <see cref="Order_Bill"/>.
        /// </returns>
        public Order_Bill QueryOrderBill(Dictionary<int, int> products, int userID)
        {
            // todo: 有待整理
            var orderBill = new Order_Bill();
            var productAllList = new List<Cart_Product>(); // 购物车中所有商品
            var productList = new List<Cart_Product>(); // 非组合促销商品
            var suitPromoteList = new List<Suit_Promote>(); // 购物车中所有组合促销

            // 处理购物车商品并返回商品总价（不含运费）
            foreach (var product in products)
            {
                bool isSuitPromoteProduct = false;

                // 购物车中购买的商品
                var cartProduct = this.orderBillDA.SelectByProduct(product.Key, product.Value, userID);
                productAllList.Add(cartProduct);

                // 处理购物车商品的促销信息
                var productPromoteList = new List<Product_Promote>(); // 购物车单个商品参与的所有促销活动列表
                if (!string.IsNullOrEmpty(cartProduct.PromoteIDs))
                {
                    var promoteIDArray = cartProduct.PromoteIDs.Split(',');
                    var promoteTypeArray = cartProduct.PromoteTypes.Split(',');
                    var promoteNameArray = cartProduct.PromoteNames.Split(',');

                    // 判断促销类型
                    for (int i = 0; i < promoteIDArray.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(promoteTypeArray[i]) && int.Parse(promoteTypeArray[i]) < 10)
                        {
                            var productPromote = new Product_Promote
                            {
                                PromoteID = int.Parse(promoteIDArray[i]),
                                PromoteType = int.Parse(promoteTypeArray[i]),
                                PromoteName = promoteNameArray[i]
                            };
                            productPromoteList.Add(productPromote); // 购物车单个商品参与的所有促销活动列表
                        }

                        // 判断是否参加组合促销（满额、满件）  
                        if (!string.IsNullOrEmpty(promoteTypeArray[i]) && int.Parse(promoteTypeArray[i]) > 10)
                        {
                            isSuitPromoteProduct = true;

                            var suitPromote =
                                suitPromoteList.Find(
                                    f =>
                                    f.PromoteID == int.Parse(promoteIDArray[i])
                                    && f.PromoteType == int.Parse(promoteTypeArray[i]));

                            if (suitPromote != null)
                            {
                                suitPromote.Products.Add(cartProduct);
                            }
                            else
                            {
                                suitPromote = new Suit_Promote
                                {
                                    PromoteID = int.Parse(promoteIDArray[i]),
                                    PromoteType = int.Parse(promoteTypeArray[i]),
                                    PromoteName = promoteNameArray[i],
                                    Products = new List<Cart_Product> { cartProduct }
                                };

                                suitPromoteList.Add(suitPromote);
                            }
                        }
                    }

                    cartProduct.Promotes = productPromoteList;
                }

                if (!isSuitPromoteProduct)
                {
                    productList.Add(cartProduct);
                }
            }

            foreach (var promoteInfo in suitPromoteList)
            {
                // 根据促销活动编号获取促销信息
                if (promoteInfo.PromoteType == 11)
                {
                    // 根据满足金额排序
                    var meetMoneyRuleList = this.promoteMeetMoneyRuleDA.SelectByMeetMoneyID(promoteInfo.PromoteID);
                    meetMoneyRuleList = meetMoneyRuleList.OrderByDescending(rule => rule.MeetMoney).ToList();
                    promoteInfo.TotalPrice = promoteInfo.Products.Sum(q => (q.PromotePrice * q.Quantity)); // 计算活动商品的购买总金额
                    bool promoteResult = false; // 促销结果
                    promoteInfo.GiftProducts = new List<Gift_Product>();
                    promoteInfo.GiftCoupons = new List<Gift_Coupon>();

                    for (var i = 0; i < meetMoneyRuleList.Count; i++)
                    {
                        if (meetMoneyRuleList[i].MeetMoney <= promoteInfo.TotalPrice && !promoteResult)
                        {
                            promoteResult = true;
                            var count = 1;
                            if (meetMoneyRuleList[i].IsNoCeiling)
                            {
                                count = (int)(promoteInfo.TotalPrice / meetMoneyRuleList[i].MeetMoney); // 上不封顶（叠加促销）
                            }

                            for (int j = 0; j < count; j++)
                            {
                                if (meetMoneyRuleList[i].IsDecreaseCash)
                                {
                                    promoteInfo.PromoteDiscount = meetMoneyRuleList[i].DecreaseCash;
                                }

                                if (meetMoneyRuleList[i].IsGiveGift)
                                {
                                    var giftPro =
                                        promoteInfo.GiftProducts.Find(
                                            p => p.ProductID == meetMoneyRuleList[i].ProductID);

                                    if (giftPro != null)
                                    {
                                        giftPro.Quantity += 1;
                                    }
                                    else
                                    {
                                        var product = this.productDA.SelectByID(meetMoneyRuleList[i].ProductID);
                                        giftPro = new Gift_Product
                                        {
                                            ProductID = product.ID,
                                            ProductName = product.Name,
                                            Quantity = 1, // todo: 买一送二
                                            PromotID = meetMoneyRuleList[i].PromoteMeetMoneyID,
                                            PromotType = 1,
                                            Path = product.Path
                                        };
                                        promoteInfo.GiftProducts.Add(giftPro);
                                    }
                                }

                                if (meetMoneyRuleList[i].IsGiveCoupon)
                                {
                                    var giftCoupon = new Gift_Coupon();
                                    if (meetMoneyRuleList[i].CouponType == 0)
                                    {
                                        var couponCash =
                                            this.couponCashDA.SelectCouponCashByID(meetMoneyRuleList[i].CouponID); // 赠券
                                        giftCoupon.CouponID = couponCash.ID;
                                        giftCoupon.CouponName = couponCash.Name;
                                        giftCoupon.CouponType = 0;
                                        giftCoupon.PromotID = meetMoneyRuleList[i].PromoteMeetMoneyID;
                                        giftCoupon.PromotType = 1;
                                    }
                                    else if (meetMoneyRuleList[i].CouponType == 1)
                                    {
                                        var couponDecrease =
                                            this.couponDecreaseDA.SelectCouponDecreaseByID(
                                                meetMoneyRuleList[i].CouponID); // 赠券
                                        giftCoupon.CouponID = couponDecrease.ID;
                                        giftCoupon.CouponName = couponDecrease.Name;
                                        giftCoupon.CouponType = 0;
                                        giftCoupon.PromotID = meetMoneyRuleList[i].PromoteMeetMoneyID;
                                        giftCoupon.PromotType = 1;
                                    }

                                    promoteInfo.GiftCoupons.Add(giftCoupon);
                                }

                                if (meetMoneyRuleList[i].IsGiveIntegral)
                                {
                                    promoteInfo.GiftIntegral += meetMoneyRuleList[i].Integral; // 赠积分
                                }

                                if (meetMoneyRuleList[i].IsNoPostage)
                                {
                                    promoteInfo.IsNoPostage = promoteInfo.IsNoPostage
                                                              || meetMoneyRuleList[i].IsNoPostage; // 免邮
                                }
                            }

                            promoteInfo.PromoteInfo = string.Format(
                                "活动商品已购满{0}元，已优惠{1}元",
                                meetMoneyRuleList[i].MeetMoney,
                                promoteInfo.PromoteDiscount);
                        }

                        if (string.IsNullOrEmpty(promoteInfo.PromoteInfo))
                        {
                            promoteInfo.PromoteInfo = string.Format(
                                "活动商品购满{0}元，再购{1}元即可享受优惠",
                                promoteInfo.TotalPrice,
                                meetMoneyRuleList[meetMoneyRuleList.Count - 1].MeetMoney - promoteInfo.TotalPrice);
                        }
                    }
                }
                else if (promoteInfo.PromoteType == 12)
                {
                    // 根据满足数量排序
                    var promoteRules = this.promoteMeetAmountRuleDA.SelectByMeetAmountID(promoteInfo.PromoteID);
                    promoteRules = promoteRules.OrderByDescending(rule => rule.MeetAmount).ToList();

                    promoteInfo.TotalQuantity = promoteInfo.Products.Sum(q => q.Quantity); // 计算活动商品的购买数量
                    promoteInfo.TotalPrice = promoteInfo.Products.Sum(q => (q.Quantity * q.PromotePrice)); // 计算活动商品的购买金额
                    bool promoteResult = false; // 促销结果
                    for (int i = 0; i < promoteRules.Count; i++)
                    {
                        if (promoteRules[i].MeetAmount <= promoteInfo.TotalQuantity && !promoteResult)
                        {
                            promoteResult = true;
                            if (promoteRules[i].IsDiscount)
                            {
                                promoteInfo.PromoteDiscount = promoteInfo.TotalPrice * (10 - promoteRules[i].Discount)
                                                              * 0.1; // 满减金额

                                promoteInfo.PromoteInfo = string.Format(
                                    "活动商品已购满{0}件，已优惠{1}元",
                                    promoteRules[i].MeetAmount,
                                    promoteInfo.PromoteDiscount);
                            }

                            if (promoteRules[i].IsGiveGift)
                            {
                                var product = this.productDA.SelectByID(promoteRules[i].ProductID);
                                var giftProduct = new Gift_Product
                                {
                                    ProductID = product.ID,
                                    ProductName = product.Name,
                                    Quantity = 1,
                                    PromotID = promoteRules[i].PromoteMeetAmountID,
                                    PromotType = 12,
                                    Path = product.Path
                                };
                                promoteInfo.GiftProducts = new List<Gift_Product> { giftProduct };
                                promoteInfo.PromoteInfo = string.Format("活动商品已购满{0}件，可领取赠品", promoteRules[i].MeetAmount);
                            }

                            if (promoteRules[i].IsNoPostage)
                            {
                                promoteInfo.IsNoPostage = promoteInfo.IsNoPostage || promoteRules[i].IsNoPostage; // 免邮
                            }
                        }

                        if (string.IsNullOrEmpty(promoteInfo.PromoteInfo))
                        {
                            promoteInfo.PromoteInfo = string.Format(
                                "活动商品购满{0}件，再购{1}件即可享受优惠",
                                promoteInfo.TotalQuantity,
                                promoteRules[promoteRules.Count - 1].MeetAmount - promoteInfo.TotalQuantity);
                        }
                    }
                }
            }

            orderBill.Products = productList;
            orderBill.SuitPromoteInfos = suitPromoteList;
            orderBill.TotalPrice = suitPromoteList.Sum(p => p.TotalPrice) + productList.Sum(p => p.Subtotal);
            orderBill.TotalDiscount = suitPromoteList.Sum(p => p.PromoteDiscount);
            return orderBill;
        }

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="quantity">
        /// 商品数量.
        /// </param>
        /// <param name="userID">
        /// 会员编号.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Cart_Product QueryCartProduct(int productID, int quantity, int userID)
        {
            return this.orderBillDA.SelectByProduct(productID, quantity, userID);
        }

        /// <summary>
        /// 从数据库查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Product_Cache QueryCartProductFromDB(int productID)
        {
            return this.orderBillDA.SelectByProduct(productID);
        }

        /// <summary>
        /// 从数据库查询商品的促销结果（刷缓存）.
        /// </summary>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public List<Product_Cache> QueryCartProductFromDB()
        {
            return this.orderBillDA.SelectByAllProduct();
        }

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Product_Cache QueryCartProduct(int productID)
        {
            var products = HttpRuntime.Cache["Product_Cache"] as List<Product_Cache>;
            if (products != null)
            {
                var cartProdcut = products.Find(p => p.ProductID == productID);
                if (cartProdcut != null)
                {
                    return cartProdcut;
                }
            }

            return this.QueryCartProductFromDB(productID);
        }

        /// <summary>
        /// 查询商品的.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public List<Product_Cache> QueryCartProduct(int[] productIds)
        {
            var products = HttpRuntime.Cache["Product_Cache"] as List<Product_Cache>;
            var cartProducts = new List<Product_Cache>();
            if (products != null)
            {
                foreach (var id in productIds)
                {
                    var cartProduct = products.Find(p => p.ProductID == id);
                    if (cartProduct != null)
                    {
                        cartProducts.Add(cartProduct);
                    }
                    else
                    {
                        cartProduct = this.QueryCartProductFromDB(id);
                        if (cartProduct != null)
                        {
                            cartProducts.Add(cartProduct);
                        }
                    }
                }

                return cartProducts;
            }

            foreach (var id in productIds)
            {
                var cartProduct = this.QueryCartProductFromDB(id);
                cartProducts.Add(cartProduct);
            }

            return cartProducts;
        }

        #endregion

        #region

        #endregion
    }
}
