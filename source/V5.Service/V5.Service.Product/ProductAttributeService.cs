// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeService.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   商品属性业务类.
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
    /// 商品属性业务类.
    /// </summary>
    public class ProductAttributeService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IProductAttributeDA productAttributeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAttributeService"/> class.
        /// </summary>
        public ProductAttributeService()
        {
            this.productAttributeDA = new DAFactoryProduct().CreateProductAttributeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add product attribute.
        /// </summary>
        /// <param name="productAttribute">
        /// The product attribute.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddProductAttribute(Product_Attribute productAttribute)
        {
            return this.productAttributeDA.Insert(productAttribute);
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
        public List<Product_Attribute> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productAttributeDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The remove product attribute by id.
        /// </summary>
        /// <param name="productAttributeID">
        /// The product attribute id.
        /// </param>
        public void RemoveProductAttributeByID(int productAttributeID)
        {
            this.productAttributeDA.DeleteByID(productAttributeID);
        }

        /// <summary>
        /// The modify product attribute.
        /// </summary>
        /// <param name="productAttribute">
        /// The product attribute.
        /// </param>
        public void ModifyProductAttribute(Product_Attribute productAttribute)
        {
            this.productAttributeDA.Update(productAttribute);
        }

        /// <summary>
        /// 根据类目查询属性和属性值
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string QueryByCategoryId(string categoryId)
        {
            return this.productAttributeDA.QueryByCategoryId(categoryId);
        }

        #endregion
    }
}
