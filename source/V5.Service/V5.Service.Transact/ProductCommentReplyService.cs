// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentReplyService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品回复评论服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using V5.DataAccess;
    using V5.DataAccess.Transact;
    using V5.DataContract.Transact;

    /// <summary>
    /// 商品回复评论服务类
    /// </summary>
    public class ProductCommentReplyService
    {
        #region  Constants and Fields

        /// <summary>
        /// 私有商品评价访问对象
        /// </summary>
        private readonly IProductCommentReplyDA da;

        #endregion

        #region  Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCommentReplyService"/> class.
        /// </summary>
        public ProductCommentReplyService()
        {
            this.da = new DAFactoryTransact().CreateProductCommentReplyDA();
        }

        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 根据评论Id查询回复评论的列表
        /// </summary>
        /// <param name="cID">
        /// 评论Id
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Comment_Reply> QueryByCommentID(int cID)
        {
            var replys = HttpRuntime.Cache["CommentReply"] as List<Product_Comment_Reply>;
            return replys != null ? (from reply in replys where reply.CommentID == cID select reply).ToList() : this.da.SelectByCommentId(cID);
        }

        /// <summary>
        /// 根据Id删除回复评论
        /// </summary>
        /// <param name="id">
        /// 回复评论Id
        /// </param>
        /// <returns>
        /// 删除的回复评论Id
        /// </returns>
        public int Remove(int id)
        {
            return this.da.Delete(id);
        }

        /// <summary>
        /// 添加评论的回复.
        /// </summary>
        /// <param name="commentReply">
        /// Product_Comment_Reply.
        /// </param>
        /// <returns>
        /// 评论回复的编号.
        /// </returns>
        public int Add(Product_Comment_Reply commentReply)
        {
            return this.da.Insert(commentReply);
        }

        /// <summary>
        /// 查询商品评论回复.
        /// </summary>
        /// <returns>
        /// 结果列表.
        /// </returns>
        public List<Product_Comment_Reply> QueryAll()
        {
            return this.da.SelectAll();
        }

        #endregion
    }
}