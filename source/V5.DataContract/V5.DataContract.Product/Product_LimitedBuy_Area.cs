// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_LimitedBuy_Area.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品限制购买区域类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品限制购买区域类
    /// </summary>
    public class Product_LimitedBuy_Area
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
        ///     获取或设置地区编号（省会编号 or 城市编号 or 区县编号）．
        /// </summary>
        public string AreaID { get; set; }

        /// <summary>
        ///     获取或设置区域类别（0：省会，1：城市，2：区县）．
        /// </summary>
        public int AreaType { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public int IsDelete { get; set; }

        #endregion
    }
}