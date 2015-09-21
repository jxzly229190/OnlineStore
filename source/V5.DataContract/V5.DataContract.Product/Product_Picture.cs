// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Picture.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品图片类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using V5.Library;

    /// <summary>
    ///     商品图片类
    /// </summary>
    public class Product_Picture
    {
        #region Public Properties

        private string _path;

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置图片编号．
        /// </summary>
        public int PictureID { get; set; }
        
        /// <summary>
        ///     获取或设置图片地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置是否为主图．
        /// </summary>
        public bool IsMaster { get; set; }

        #endregion
    }
}