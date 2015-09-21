// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_AttributeValueSet.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品属性属性值集合类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    /// <summary>
    ///     商品属性属性值集合类
    /// </summary>
    public class Product_AttributeValueSet
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
        ///     获取或设置属性编号．
        /// </summary>
        public int AttributeID { get; set; }

        /// <summary>
        ///     获取或设置属性值编号．
        /// </summary>
        public int AttributeValueID { get; set; }

        #endregion
    }
}