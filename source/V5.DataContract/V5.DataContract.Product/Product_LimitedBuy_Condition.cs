// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_LimitedBuy_Condition.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品限购条件类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品限购条件类
    /// </summary>
    public class Product_LimitedBuy_Condition
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置用户等级编号．
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        ///     获取或设置限制天数．
        /// </summary>
        public int LimitedDays { get; set; }

        /// <summary>
        ///     获取或设置限制数量．
        /// </summary>
        public int LimitedQuantity { get; set; }

        /// <summary>
        ///     获取或设置状态（0：未启用，1：启用中，2：已停用）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}