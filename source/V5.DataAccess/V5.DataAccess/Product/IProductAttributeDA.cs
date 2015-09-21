// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductAttributeDA.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   商品属性数据库访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 商品属性数据库访问接口.
    /// </summary>
    public interface IProductAttributeDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加商品属性.
        /// </summary>
        /// <param name="productAttribute">
        /// 商品属性实体
        /// </param>
        /// <returns>
        /// 添加记录的主键值.
        /// </returns>
        int Insert(Product_Attribute productAttribute);

        /// <summary>
        /// The paging.
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
        List<Product_Attribute> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 修改图片信息.
        /// </summary>
        /// <param name="productAttribute">
        /// 图片实体.
        /// </param>
        void Update(Product_Attribute productAttribute);

        /// <summary>
        /// 根据图片ID做删除.
        /// </summary>
        /// <param name="productAttributeID">
        /// 图片ID.
        /// </param>
        void DeleteByID(int productAttributeID);

        /// <summary>
        /// 根据类目查询属性和属性值
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        string QueryByCategoryId(string categoryId);

        #endregion
    }
}