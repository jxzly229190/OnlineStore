// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductCommentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评价数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact;

    /// <summary>
    /// 商品评价数据访问接口
    /// </summary>
    public interface IProductCommentDA
    {
        /// <summary>
        /// 更新商品评价状态
        /// </summary>
        /// <param name="model">
        /// 需更新的对象
        /// </param>
        void UpdateCommentStatus(Product_Comment model);

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="id">
        /// 商品评价编号
        /// </param>
        /// <returns>
        /// 已删除的评价编号
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Product_Comment> Paging(Library.Storage.DB.Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 添加评论.
        /// </summary>
        /// <param name="productComment">
        /// Product_Comment.
        /// </param>
        /// <returns>
        /// 评论的编号.
        /// </returns>
        int Insert(Product_Comment productComment);

        /// <summary>
        /// 查询商品的comments
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// Product_Comment.
        /// </returns>
        List<Product_Comment> SelectCommentsFromBrandInfo(string condition);

        /// <summary>
        /// 查询所有商品评论.
        /// </summary>
        /// <returns>
        /// 评论列表.
        /// </returns>
        List<Product_Comment> SelectAll();
    }
}