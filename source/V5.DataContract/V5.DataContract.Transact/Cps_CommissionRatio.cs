// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cps_CommissionRatio.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps 佣金比例类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    ///     Cps 佣金比例类
    /// </summary>
    public class Cps_CommissionRatio
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置Cps 平台编号．
        /// </summary>
        public int CpsID { get; set; }

        /// <summary>
        ///     获取或设置商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置商品类别名称.
        /// </summary>
        public string ProductCategoryName { get; set; }

        /// <summary>
        ///     获取或设置佣金比例．
        /// </summary>
        public double CommissionRatio { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}