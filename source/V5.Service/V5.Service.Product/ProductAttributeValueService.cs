// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeValueService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductAttributeValueService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Product
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Product;
    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The product attribute value service.
    /// </summary>
    public class ProductAttributeValueService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IProductAttributeValueDA productAttributeValueDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAttributeValueService"/> class.
        /// </summary>
        public ProductAttributeValueService()
        {
            this.productAttributeValueDA = new DAFactoryProduct().CreateProductAttributeValueDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add product attribute value.
        /// </summary>
        /// <param name="productAttributeValue">
        /// The product attribute value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddProductAttributeValue(Product_AttributeValue productAttributeValue)
        {
            return this.productAttributeValueDA.Insert(productAttributeValue);
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="paging">
        /// The paging.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        /// <param name="totalCount">
        /// The total count.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_AttributeValue> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productAttributeValueDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The remove product attribute value by id.
        /// </summary>
        /// <param name="productAttributeValueID">
        /// The product attribute value id.
        /// </param>
        public void RemoveProductAttributeValueByID(int productAttributeValueID)
        {
            this.productAttributeValueDA.DeleteByID(productAttributeValueID);
        }

        /// <summary>
        /// The modify product attribute value.
        /// </summary>
        /// <param name="productAttributeValue">
        /// The product attribute value.
        /// </param>
        public void ModifyProductAttributeValue(Product_AttributeValue productAttributeValue)
        {
            this.productAttributeValueDA.Update(productAttributeValue);
        }

        #endregion
    }
}
