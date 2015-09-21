// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductPictureModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product picture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    /// <summary>
    /// The product picture.
    /// </summary>
    public class ProductPictureModel
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
        ///     获取或设置图片编号．
        /// </summary>
        public int PictureID { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail path.
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        ///     获取或设置是否为主图．
        /// </summary>
        public bool IsMaster { get; set; }

        #endregion 
    }
}