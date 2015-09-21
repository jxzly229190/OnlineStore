// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductPictureService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product picture service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Product
{
    using System;
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Product;
    using V5.DataContract.Product;

    /// <summary>
    /// The product picture service.
    /// </summary>
    public class ProductPictureService
    {
        #region Constants and Fields

        /// <summary>
        /// The product picture da.
        /// </summary>
        private readonly IProductPictureDA productPictureDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPictureService"/> class.
        /// </summary>
        public ProductPictureService()
        {
            this.productPictureDA = new DAFactoryProduct().CreateProductPictureDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The query by product id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_Picture> QueryByProductID(string productID)
        {
            return this.productPictureDA.SelectByProductID(Convert.ToInt32(productID));
        }

        #endregion
    }
}
