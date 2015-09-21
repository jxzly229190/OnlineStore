// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_Vip_Scope.cs" company="www.gjw.com">
//   (C) 2014 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销商品范围
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    /// <summary>
    /// 会员促销商品范围
    /// </summary>
    public class Promote_Vip_Scope
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