// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeValueSetService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product attribute value set service.
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
    /// The product attribute value set service.
    /// </summary>
    public class ProductAttributeValueSetService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IProductAttributeValueSetDA productAttributeValueSetDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAttributeValueSetService"/> class.
        /// </summary>
        public ProductAttributeValueSetService()
        {
            this.productAttributeValueSetDA = new DAFactoryProduct().CreateProductAttributeValueSetDA();
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
        public List<Product_AttributeValueSet> QueryByProductID(string productID)
        {
            return this.productAttributeValueSetDA.SelectByProductID(Convert.ToInt32(productID));
        }

        #endregion
    }
}
