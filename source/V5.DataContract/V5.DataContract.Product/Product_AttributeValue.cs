// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_AttributeValue.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品属性值定义类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     商品属性值定义类
    /// </summary>
    public class Product_AttributeValue
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品属性编号．
        /// </summary>
        public int AttributeID { get; set; }

        /// <summary>
        ///     获取或设置商品属性值．
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     获取或设置默认值
        /// </summary>
        public int IsDefault { get; set; }

        #endregion
    }
}