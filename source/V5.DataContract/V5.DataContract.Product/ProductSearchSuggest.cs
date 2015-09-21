// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    /// <summary>
    ///     商品类
    /// </summary>
    public class ProductSearchSuggest
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
