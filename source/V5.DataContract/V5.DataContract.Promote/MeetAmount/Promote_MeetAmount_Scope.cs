// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetAmount_Scope.cs" company="www.gjw.com">
//   (C) 2014 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠促销商品类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetAmount
{
    /// <summary>
    /// 满件优惠促销商品类.
    /// </summary>
    public class Promote_MeetAmount_Scope
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置满就送活动编号．
        /// </summary>
        public int MeetAmountID { get; set; }

        /// <summary>
        /// 获取或设置参与活动的商品．
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}