// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Tree.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品树（大类、品牌、商品）
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    /// <summary>
    ///     商品树（大类、品牌、商品）
    /// </summary>
    public class Product_Tree
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置编号．
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        ///     获取或设置父级编号
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        ///     获取或设置名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置价格．
        /// </summary>
        public string GoujiuPrice { get; set; }

        /// <summary>
        ///     获取或设置库存数量．
        /// </summary>
        public string InventoryNumber { get; set; }

        #endregion
    }
}
