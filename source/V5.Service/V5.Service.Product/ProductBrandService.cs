// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductBrandService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product brand service.
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
    /// The product brand service.
    /// </summary>
    public class ProductBrandService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IProductBrandDA productBrandDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBrandService"/> class.
        /// </summary>
        public ProductBrandService()
        {
            this.productBrandDA = new DAFactoryProduct().CreateProductBrandDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add product brand.
        /// </summary>
        /// <param name="brand">
        /// The brand.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddProductBrand(Product_Brand brand)
        {
            return this.productBrandDA.Insert(brand);
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
        public List<Product_Brand> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productBrandDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The query product brand by category id.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_Brand> QueryProductBrandByCategoryID(int categoryID, int parentID)
        {
            return this.productBrandDA.SelectProductBrandByCategoryID(categoryID, parentID);
        }

        /// <summary>
        /// The query product brand by parent id.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_Brand> QueryProductBrandByParentID(int parentID)
        {
            return this.productBrandDA.SelectProductBrandByParentID(parentID);
        }

        /// <summary>
        /// The remove product brand by id.
        /// </summary>
        /// <param name="brandID">
        /// The brand id.
        /// </param>
        public void RemoveProductBrandByID(int brandID)
        {
            this.productBrandDA.DeleteByID(brandID);
        }

        /// <summary>
        /// The modify brand category.
        /// </summary>
        /// <param name="brand">
        /// The brand.
        /// </param>
        public void ModifyBrandCategory(Product_Brand brand)
        {
            this.productBrandDA.Update(brand);
        }

        /// <summary>
        /// 获取商品树（大类、品牌、商品）
        /// </summary>
        /// <returns></returns>
        public List<Product_Tree> QueryBrandTree()
        {
            return this.productBrandDA.SelectBrandTree();
        }

        public Product_Brand QueryById(int id)
        {
            return this.productBrandDA.SelectById(id);
        }

        /// <summary>
        /// 查询类别和品牌视图
        /// </summary>
        /// <returns></returns>
        public List<Product_Category_Brand> SelectProductCategoryBrandAll()
        {
            return this.productBrandDA.SelectProductCategoryBrandAll();
        }

        #endregion
    }
}
