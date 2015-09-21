// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryPromote.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.Promote;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataAccess.Promote.MeetMoney;

    /// <summary>
    /// 促销模块数据访问工厂类
    /// </summary>
    public class DAFactoryPromote : DataAccess
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryPromote"/> class.
        /// </summary>
        public DAFactoryPromote()
        {
            this.AssemblyPath = this.AssemblyPath + ".Promote";
        }

        /// <summary>
        /// The create user da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICouponCashDA"/>.
        /// </returns>
        public ICouponCashDA CreateCouponCashDA()
        {
            string nameSpace = AssemblyPath + ".CouponCashDA";
            object couponCashDA = Create(AssemblyPath, nameSpace);
            return (ICouponCashDA)couponCashDA;
        }

        /// <summary>
        /// The create voucher preferential da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICouponDecreaseDA"/>.
        /// </returns>
        public ICouponDecreaseDA CreateCouponDecreaseDA()
        {
            string nameSpace = AssemblyPath + ".CouponDecreaseDA";
            object voucherPreferentialDA = Create(AssemblyPath, nameSpace);
            return (ICouponDecreaseDA)voucherPreferentialDA;
        }

        /// <summary>
        /// The create coupon cash binding da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICouponCashBindingDA"/>.
        /// </returns>
        public ICouponCashBindingDA CreateCouponCashBindingDA()
        {
            string nameSpace = AssemblyPath + ".CouponCashBindingDA";
            object couponCashBindingDA = Create(AssemblyPath, nameSpace);
            return (ICouponCashBindingDA)couponCashBindingDA;
        }

        /// <summary>
        /// The create coupon decrease binding da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICouponDecreaseBindingDA"/>.
        /// </returns>
        public ICouponDecreaseBindingDA CreateCouponDecreaseBindingDA()
        {
            string nameSpace = AssemblyPath + ".CouponDecreaseBindingDA";
            object couponDecreaseBindingDA = Create(AssemblyPath, nameSpace);
            return (ICouponDecreaseBindingDA)couponDecreaseBindingDA;
        }

        /// <summary>
        /// The create coupon scope da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICouponScopeDA"/>.
        /// </returns>
        public ICouponScopeDA CreateCouponScopeDA()
        {
            string nameSpace = AssemblyPath + ".CouponScopeDA";
            object couponScopeDA = Create(AssemblyPath, nameSpace);
            return (ICouponScopeDA)couponScopeDA;
        }

        /// <summary>
        /// The create promote much bottled da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMuchBottledDA"/>.
        /// </returns>
        public IPromoteMuchBottledDA CreatePromoteMuchBottledDA()
        {
            string nameSpace = AssemblyPath + ".PromoteMuchBottledDA";
            object promoteMuchBottledDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMuchBottledDA)promoteMuchBottledDA;
        }

        /// <summary>
        /// The create promote much bottled rule da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMuchBottledRuleDA"/>.
        /// </returns>
        public IPromoteMuchBottledRuleDA CreatePromoteMuchBottledRuleDA()
        {
            string nameSpace = AssemblyPath + ".PromoteMuchBottledRuleDA";
            object promoteMuchBottledRuleDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMuchBottledRuleDA)promoteMuchBottledRuleDA;
        }

        /// <summary>
        /// The create promote limited discount da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteLimitedDiscountDA"/>.
        /// </returns>
        public IPromoteLimitedDiscountDA CreatePromoteLimitedDiscountDA()
        {
            string nameSpace = AssemblyPath + ".PromoteLimitedDiscountDA";
            object promoteLimitedDiscountDA = Create(AssemblyPath, nameSpace);
            return (IPromoteLimitedDiscountDA)promoteLimitedDiscountDA;
        }

        /// <summary>
        /// The create promote meet money da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetMoneyDA"/>.
        /// </returns>
        public IPromoteMeetMoneyDA CreatePromoteMeetMoneyDA()
        {
            string nameSpace = AssemblyPath + ".MeetMoney.PromoteMeetMoneyDA";
            object promoteMeetMoneyDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetMoneyDA)promoteMeetMoneyDA;
        }

        /// <summary>
        /// The create promote meet money scope da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetMoneyScopeDA"/>.
        /// </returns>
        public IPromoteMeetMoneyScopeDA CreatePromoteMeetMoneyScopeDA()
        {
            string nameSpace = AssemblyPath + ".MeetMoney.PromoteMeetMoneyScopeDA";
            object promoteMeetMoneyScopeDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetMoneyScopeDA)promoteMeetMoneyScopeDA;
        }

        /// <summary>
        /// The create promote meet money rule da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetMoneyRuleDA"/>.
        /// </returns>
        public IPromoteMeetMoneyRuleDA CreatePromoteMeetMoneyRuleDA()
        {
            string nameSpace = AssemblyPath + ".MeetMoney.PromoteMeetMoneyRuleDA";
            object promoteMeetMoneyRuleDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetMoneyRuleDA)promoteMeetMoneyRuleDA;
        }

        /// <summary>
        /// The create promote meet amount da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetAmountDA"/>.
        /// </returns>
        public IPromoteMeetAmountDA CreatePromoteMeetAmountDA()
        {
            string nameSpace = AssemblyPath + ".MeetAmount.PromoteMeetAmountDA";
            object promoteMeetAmountDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetAmountDA)promoteMeetAmountDA;
        }

        /// <summary>
        /// The create promote meet amount rule da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetAmountRuleDA"/>.
        /// </returns>
        public IPromoteMeetAmountRuleDA CreatePromoteMeetAmountRuleDA()
        {
            string nameSpace = AssemblyPath + ".MeetAmount.PromoteMeetAmountRuleDA";
            object promoteMeetAmountRuleDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetAmountRuleDA)promoteMeetAmountRuleDA;
        }

        /// <summary>
        /// The create promote meet amount scope da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteMeetAmountScopeDA"/>.
        /// </returns>
        public IPromoteMeetAmountScopeDA CreatePromoteMeetAmountScopeDA()
        {
            string nameSpace = AssemblyPath + ".MeetAmount.PromoteMeetAmountScopeDA";
            object promoteMeetAmountScopeDA = Create(AssemblyPath, nameSpace);
            return (IPromoteMeetAmountScopeDA)promoteMeetAmountScopeDA;
        }

        /// <summary>
        /// The create promote meet discount da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteLandingPageDA"/>.
        /// </returns>
        public IPromoteLandingPageDA CreatePromoteLandingPageDA()
        {
            string nameSpace = AssemblyPath + ".PromoteLandingPageDA";
            object promoteLandingPageDA = Create(AssemblyPath, nameSpace);
            return (IPromoteLandingPageDA)promoteLandingPageDA;
        }

        /// <summary>
        /// The create promote vip da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteVipDA"/>.
        /// </returns>
        public IPromoteVipDA CreatePromoteVipDA()
        {
            string nameSpace = AssemblyPath + ".PromoteVipDA";
            object promoteVipDA = Create(AssemblyPath, nameSpace);
            return (IPromoteVipDA)promoteVipDA;
        }

        /// <summary>
        /// The create promote vip scope da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPromoteVipScopeDA"/>.
        /// </returns>
        public IPromoteVipScopeDA CreatePromoteVipScopeDA()
        {
            string nameSpace = AssemblyPath + ".PromoteVipScopeDA";
            object promoteVipScopeDA = Create(AssemblyPath, nameSpace);
            return (IPromoteVipScopeDA)promoteVipScopeDA;
        }
    }
}