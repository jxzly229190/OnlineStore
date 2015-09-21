// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductSearchSuggestTip.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品分组统计类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    /// <summary>
    ///     商品分组统计类
    /// </summary>
    public class ProductSearchSuggestTip
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置类型．
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     获取或设置名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置数量．
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        ///     获取或设置搜索关键字
        /// </summary>
        public string Search { get; set; }

        #endregion
    }
}
