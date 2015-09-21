// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductCommentReplyDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评价回复数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact;

    /// <summary>
    /// 商品评价回复数据访问接口
    /// </summary>
    public interface IProductCommentReplyDA
    {
        /// <summary>
        /// 根据商品评价编码查询回复评价信息
        /// </summary>
        /// <param name="productCommentId">商品评价编码</param>
        /// <returns>查询结果</returns>
        List<Product_Comment_Reply> SelectByCommentId(int productCommentId);

        /// <summary>
        /// 删除一条回复
        /// </summary>
        /// <param name="id">
        /// 评价编码
        /// </param>
        /// <returns>
        /// 删除的实体编码
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 添加评论的回复.
        /// </summary>
        /// <param name="commentReply">
        /// Product_Comment_Reply.
        /// </param>
        /// <returns>
        /// 评论回复的编号.
        /// </returns>
        int Insert(Product_Comment_Reply commentReply);

        /// <summary>
        /// 查询商品评论回复.
        /// </summary>
        /// <returns>
        /// 结果列表.
        /// </returns>
        List<Product_Comment_Reply> SelectAll();
    }
}