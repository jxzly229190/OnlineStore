// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductModifyModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product modify model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    using global::System.Collections.Generic;

    /// <summary>
    /// The product modify model.
    /// </summary>
    public class ProductModifyModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductModifyModel"/> class.
        /// </summary>
        public ProductModifyModel()
        {
            this.Product = new ProductModel();
            this.ProductAttributeValueSetModels = new List<ProductAttributeValueSetModel>();
            this.ProductPictures = new List<ProductPictureModel>();
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public ProductModel Product { get; set; }

        /// <summary>
        /// Gets or sets the product attribute value set models.
        /// </summary>
        public List<ProductAttributeValueSetModel> ProductAttributeValueSetModels { get; set; }

        /// <summary>
        /// Gets or sets the product pictures.
        /// </summary>
        public List<ProductPictureModel> ProductPictures { get; set; }

        #endregion
    }
}