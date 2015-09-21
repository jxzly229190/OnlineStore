// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteVipScopeModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销商品范围
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    /// <summary>
    /// 会员促销商品范围
    /// </summary>
    public class PromoteVipScopeModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置活动编号．
        /// </summary>
        public int PromoteVipID { get; set; }

        /// <summary>
        /// 获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}