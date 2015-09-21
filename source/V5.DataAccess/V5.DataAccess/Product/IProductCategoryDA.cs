// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductCategoryDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.  
// </copyright>
// <summary>
//   商品类别数据库访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 商品类别数据库访问接口.
    /// </summary>
    public interface IProductCategoryDA
    {
        /// <summary>
        /// 添加商品类别
        /// </summary>
        /// <param name="category">
        /// 商品类别实体
        /// </param>
        /// <returns>
        /// 新增数据主键值
        /// </returns>
        int Insert(Product_Category category);

        List<Product_Category> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 根据父类别ID获取类别列表.
        /// </summary>
        /// <param name="parentID">
        /// 父ID.
        /// </param>
        /// <returns>
        /// 商品类别列表.
        /// </returns>
        List<Product_Category> SelectCategoryByParentID(int parentID);
        
        /// <summary>
        /// 修改类别信息.
        /// </summary>
        /// <param name="category">
        /// 类别实体.
        /// </param>
        void Update(Product_Category category);

        /// <summary>
        /// 删除类别.
        /// </summary>
        /// <param name="categoryID">
        /// 商品类别ID.
        /// </param>
        void Delete(int categoryID);
    }
}