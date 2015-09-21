// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the Product type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Product
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    using V5.DataAccess;
    using V5.DataAccess.Product;
    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The product.
    /// </summary>
    public class ProductService
    {
        #region Constants and Fields

        /// <summary>
        /// The product da.
        /// </summary>
        private readonly IProductDA productDA;

        /// <summary>
        /// The product attribute value set da.
        /// </summary>
        private readonly IProductAttributeValueSetDA productAttributeValueSetDA;

        /// <summary>
        /// The product picture da.
        /// </summary>
        private readonly IProductPictureDA productPictureDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService()
        {
            this.productDA = new DAFactoryProduct().CreateProductDA();
            this.productAttributeValueSetDA = new DAFactoryProduct().CreateProductAttributeValueSetDA();
            this.productPictureDA = new DAFactoryProduct().CreateProductPictureDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 判断商品是否存在相同的名称或条形码.
        /// </summary>
        /// <param name="id">
        /// 商品编号.
        /// </param>
        /// <param name="name">
        /// 商品名称.
        /// </param>
        /// <param name="barcode">
        /// 商品条形码.
        /// </param>
        /// <returns>
        /// 重复数据条数.
        /// </returns>
        public int VerifyProduct(int id, string name, string barcode)
        {
            return this.productDA.VerifyProduct(id, name, barcode);
        }

        /// <summary>
        /// 根据类别和条件查询商品信息
        /// </summary>
        /// <param name="productType">
        /// 商品类别
        /// </param>
        /// <param name="count">
        /// 需要的数量
        /// </param>
        /// <param name="whereString">
        /// 查询条件
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<ProductSearchResult> Query(ProductType productType, int count, string whereString)
        {
            return this.productDA.Select(productType, count, whereString);
        }

        /// <summary>
        /// 根据商品编码查询商品详细信息
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public ProductSearchResult QueryByID(int productID)
        {
            return this.productDA.SelectByID(productID);
        }

        /// <summary>
        /// 根据商品编码查询商品详细信息.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="all">
        /// true 所有商品，false:商品详情页（仅上架商品且类别不为其他）.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public ProductSearchResult QueryByID(int productID, bool all)
        {
            return this.productDA.SelectByID(productID, all);
        }

        /// <summary>
        /// 查询指定的商品.
        /// </summary>
        /// <param name="productIDs">
        /// 商品编号（例如：1，2，3）.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public List<ProductSearchResult> QueryResultByID(string productIDs)
        {
            return this.productDA.SelectResultByID(productIDs);
        }

        /// <summary>
        /// 商品查询分页方法
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 商品查询结果列表
        /// </returns>
        public List<ProductSearchResult> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productDA.Paging(paging, out pageCount, out totalCount);
        }
        
        /// <summary>
        /// 商品查询分页方法
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 商品查询结果列表
        /// </returns>
        public List<int> QueryProductID(Paging paging, out int pageCount, out int totalCount)
        {
            return this.productDA.SelectProductID(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The query by id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="Product"/>.
        /// </returns>
        public Product QueryByID(string productID)
        {
            return this.productDA.SelectByID(productID);
        }

        /// <summary>
        /// The update product status.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        public void ModifyProductStatus(string productID, int status)
        {
            this.productDA.UpdateProductStatus(productID, status);
        }

        /// <summary>
        /// The remove by id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        public void RemoveByID(string productID)
        {
            this.productDA.DeleteByID(productID);
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="productAttributeValueSets">
        /// The product attribute value sets.
        /// </param>
        /// <param name="pictureIDs">
        /// The picture i ds.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Add(Product product, List<Product_AttributeValueSet> productAttributeValueSets, List<string> pictureIDs, string masterPictureID)
        {
            var productID = 0;
            try
            {
                productID = this.productDA.Insert(product);
                //if (productAttributeValueSets != null && productAttributeValueSets.Count > 0)
                //{
                //    foreach (var productAttributeValueSet in productAttributeValueSets)
                //    {
                //        productAttributeValueSet.ProductID = productID;
                //        this.productAttributeValueSetDA.Insert(productAttributeValueSet, transaction);
                //    }
                //}

                foreach (var pictureID in pictureIDs)
                {
                    var productPicture = new Product_Picture
                                             {
                                                 ProductID = productID,
                                                 PictureID = Convert.ToInt32(pictureID),
                                                 IsMaster = pictureID == masterPictureID ? true : false
                                             };

                    this.productPictureDA.Insert(productPicture);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return productID;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="productAttributeValueSets">
        /// The product attribute value sets.
        /// </param>
        /// <param name="pictureIDs">
        /// The picture i ds.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Modify(Product product, List<Product_AttributeValueSet> productAttributeValueSets, List<string> pictureIDs, string masterPictureID)
        {
            var productID = product.ID;
            try
            {
                this.productDA.Update(product);
                //if (productAttributeValueSets != null && productAttributeValueSets.Count > 0)
                //{
                //    foreach (var productAttributeValueSet in productAttributeValueSets)
                //    {
                //        if (productAttributeValueSet.AttributeID == -1 && productAttributeValueSet.AttributeValueID == -1 && productAttributeValueSet.ID > 0)
                //        {
                //            this.productAttributeValueSetDA.Delete(productAttributeValueSet.ID, transaction);
                //        }
                //        else
                //        {
                //            productAttributeValueSet.ProductID = productID;
                //            this.productAttributeValueSetDA.Insert(productAttributeValueSet, transaction);
                //        }
                //    }
                //}

                if (pictureIDs != null && pictureIDs.Count > 0)
                {
                    this.productPictureDA.Delete(productID);
                    foreach (var pictureID in pictureIDs)
                    {
                        var productPicture = new Product_Picture
                        {
                            ProductID = productID,
                            PictureID = Convert.ToInt32(pictureID),
                            IsMaster = pictureID == masterPictureID ? true : false
                        };
                        this.productPictureDA.Insert(productPicture);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return productID;
        }

        /// <summary>
        /// The build product query condition.
        /// </summary>
        /// <param name="parentCategoryID">
        /// The parent category id.
        /// </param>
        /// <param name="productCategoryID">
        /// The product category id.
        /// </param>
        /// <param name="parentBrandID">
        /// The parent brand id.
        /// </param>
        /// <param name="productBrandID">
        /// The product brand id.
        /// </param>
        /// <param name="productName">
        /// The product name.
        /// </param>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <param name="minPrice">
        /// The min price.
        /// </param>
        /// <param name="maxPrice">
        /// The max price.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BuildProductQueryCondition(string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID, string productName, string barcode, string minPrice, string maxPrice)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[Status] = 2");

            if (!string.IsNullOrEmpty(productName))
            {
                stringBuilder.Append(string.Format(" And [Name] like '%{0}%' ", productName));
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stringBuilder.Append(string.Format(" And [Barcode] like '%{0}%' ", barcode));
            }

            if (!string.IsNullOrEmpty(parentCategoryID) && parentCategoryID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ParentCategoryID] = {0} ", parentCategoryID));
            }

            if (!string.IsNullOrEmpty(productCategoryID) && productCategoryID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ProductCategoryID] = {0} ", productCategoryID));
            }

            if (!string.IsNullOrEmpty(parentBrandID) && parentBrandID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ParentBrandID] = {0} ", parentBrandID));
            }

            if (!string.IsNullOrEmpty(productBrandID) && productBrandID != "-1")
            {
                stringBuilder.Append(string.Format(" And [ProductBrandID] = '{0}' ", productBrandID));
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                stringBuilder.Append(string.Format(" And [GoujiuPrice] >= '{0}' ", minPrice));
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                stringBuilder.Append(string.Format(" And [GoujiuPrice] <= '{0}' ", maxPrice));
            }

            return stringBuilder.ToString();
        }

        public List<ProductSearchSuggest> SearchSuggest(string search)
        {
            return this.productDA.SearchSuggest(search);
        }

        /// <summary>
        /// 获取商品树（大类、品牌、商品）
        /// </summary>
        /// <returns></returns>
        public List<Product_Tree> QueryAllTree()
        {
            return this.productDA.SelectAllTree();
        }

        public List<ProductSearchSuggestTip> SearchSuggestTip(string keyword, string condition)
        {
            return this.productDA.SearchSuggestTip(keyword, condition);
        }

        /// <summary>
        /// 促销选择商品时获取全部在售商品.
        /// </summary>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<Product_Tree> QueryAllProduct()
        {
            return this.productDA.SelectAllProduct();
        }
        
        /// <summary>
        /// 促销选择商品时获取全部在售商品.
        /// </summary>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<int> QueryAllProductID(int status)
        {
            return this.productDA.SelectAllProductID(status);
        }

        /// <summary>
        /// 猜你喜欢.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Product> QueryGuessLike()
        {
            return this.productDA.SelectGuessLike();
        }

        /// <summary>
        /// 查询所有已参加促销活动的商品.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Product> QueryPromoteProducts(string productStr)
        {
            return this.productDA.SelectPromoteProducts(productStr);
        }

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public ProductSearch QueryProductSearchByID(string productID)
        {
            return this.productDA.SelectProductSearchByID(productID);
        }

        /// <summary>
        /// 查询商品的最大编号.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int QueryProductCount()
        {
            return this.productDA.SelectProductCount();
        }

        /// <summary>
        /// 根据多个ID接接条件查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<Product> QueryProductFromInfo(string condition)
        {
            return this.productDA.SelectProductById(condition);
        }

        /// <summary>
        /// 更新产品搜索
        /// </summary>
        public void UpdateProductSearch()
        {
            this.productDA.UpdateProductSearch();
        }

        /// <summary>
        /// 更新产品搜索
        /// </summary>
        public List<ProductSearch> QueryAllProductSearch()
        {
            return this.productDA.SelectAllProductSearch();
        }

        #endregion
    }
}
