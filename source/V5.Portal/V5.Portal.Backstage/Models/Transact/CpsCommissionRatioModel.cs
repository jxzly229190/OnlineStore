// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsCommissionRatioModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   CPS平台佣金模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// CPS平台佣金模型类.
    /// </summary>
    public class CpsCommissionRatioModel
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
        ///     获取或设置Cps 平台名称．
        /// </summary>
        public int CpsName { get; set; }

        /// <summary>
        ///     获取或设置商品类别编号．
        /// </summary>
        [RegularExpression(@"^[1-9]{1}[\d]*$", ErrorMessage = "请选择商品类别")]
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置商品类别名称．
        /// </summary>
        public string ProductCategoryName { get; set; }

        /// <summary>
        ///     获取或设置佣金比例．
        /// </summary>
        [RegularExpression(@"^(0\.[0-9]+)$", ErrorMessage = "佣金比例范围 0~1！")]
        public double CommissionRatio { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}