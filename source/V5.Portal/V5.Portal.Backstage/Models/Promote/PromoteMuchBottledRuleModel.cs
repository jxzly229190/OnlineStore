// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMuchBottledRuleModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销活动规则模型.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    /// <summary>
    /// 多瓶装促销活动规则模型.
    /// </summary>
    public class PromoteMuchBottledRuleModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置规则名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置活动编号.
        /// </summary>
        public int MuchBottledID { get; set; }

        /// <summary>
        /// 获取或设置数量.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 获取或设置活动单价.
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 获取或设置总优惠金额.
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// 获取或设置总价.
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 获取或设置缩略图路径.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 获取或设置是否默认.
        /// </summary>
        public bool IsDefault { get; set; }

        #endregion
    }
}