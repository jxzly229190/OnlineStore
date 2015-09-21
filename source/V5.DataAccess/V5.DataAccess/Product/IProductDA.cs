// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The ProductDA interface.
    /// </summary>
    public interface IProductDA
    {
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
        int VerifyProduct(int id, string name, string barcode);
        
        List<ProductSearchResult> Select(ProductType productType, int count, string whereString);

        /// <summary>
        /// 根据商品编码查询商品详细信息.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        ProductSearchResult SelectByID(int productID);

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
        ProductSearchResult SelectByID(int productID, bool all);

        /// <summary>
        /// 查询指定的商品.
        /// </summary>
        /// <param name="productIDs">
        /// 商品编号（例如：1，2，3）.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        List<ProductSearchResult> SelectResultByID(string productIDs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProductSearchResult> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<int> SelectProductID(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product SelectByID(string productID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="status"></param>
        void UpdateProductStatus(string productID, int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        void DeleteByID(string productID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int Insert(Product product, out SqlTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        void Update(Product product, out SqlTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int Insert(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        void Update(Product product);

        /// <summary>
        /// 更新商品销售量
        /// </summary>
        /// <param name="productID">
        /// 商品编码
        /// </param>
        /// <param name="saleAmount">
        /// 需更新的销量
        /// </param>
        /// <param name="transaction">
        /// 数据库事务
        /// </param>
        void UpdateProductSaleAmount(int productID, int saleAmount, SqlTransaction transaction);

        /// <summary>
        /// 搜索建议
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<ProductSearchSuggest> SearchSuggest(string keyword);

        /// <summary>
        /// 获取商品树（大类、品牌、商品）
        /// </summary>
        /// <returns></returns>
        List<Product_Tree> SelectAllTree();

        /// <summary>
        /// 获取商品分组统计
        /// </summary>
        /// <returns></returns>
        List<ProductSearchSuggestTip> SearchSuggestTip(string keyword, string condition);

        /// <summary>
        /// 促销选择商品时获取全部在售商品.
        /// </summary>
        /// <returns>
        /// 商品列表.
        /// </returns>
        List<Product_Tree> SelectAllProduct();
        
        /// <summary>
        /// 查询所有商品ID
        /// </summary>
        /// <returns></returns>
        List<int> SelectAllProductID(int status);

        /// <summary>
        /// 猜你喜欢.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Product> SelectGuessLike();

        /// <summary>
        /// 更新商品促销结束时间.
        /// </summary>
        /// <param name="productStr">
        /// 商品集合字符串.
        /// </param>
        /// <param name="promoteEndTime">
        /// 商品促销结束时间
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        void UpdateProductPromoteEndTime(string productStr, DateTime? promoteEndTime, SqlTransaction transaction);

        /// <summary>
        /// 查询所有已参加促销活动的商品.
        /// </summary>
        /// <param name="productStr">
        /// 商品集合字符串.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<Product> SelectPromoteProducts(string productStr);

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        ProductSearch SelectProductSearchByID(string productID);
        
        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductSearch> SelectAllProductSearch();

        /// <summary>
        /// 查询商品的最大编号.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SelectProductCount();

        /// <summary>
        /// 根据ID查询多个商品
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<Product> SelectProductById(string condition);

        /// <summary>
        /// 更新产品搜索
        /// </summary>
        void UpdateProductSearch();
    }
}