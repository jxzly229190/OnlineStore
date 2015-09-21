// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoryService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品类别业务类.
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
    /// 商品类别业务类.
    /// </summary>
    public class ProductCategoryService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IProductCategoryDA productCategoryDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryService"/> class. 
        /// </summary>
        public ProductCategoryService()
        {
            this.productCategoryDA = new DAFactoryProduct().CreateProductCategoryDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add product category.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddProductCategory(Product_Category product)
        {
            return this.productCategoryDA.Insert(product);
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
        public List<Product_Category> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productCategoryDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The query category by parent id.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_Category> QueryCategoryByParentID(int parentID)
        {
            return this.productCategoryDA.SelectCategoryByParentID(parentID);
        }

        /// <summary>
        /// The remove product category by id.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        public void RemoveProductCategoryByID(int categoryID)
        {
            this.productCategoryDA.Delete(categoryID);
        }

        /// <summary>
        /// The modify product category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public void ModifyProductCategory(Product_Category category)
        {
            this.productCategoryDA.Update(category);
        }

        #endregion
    }
}
