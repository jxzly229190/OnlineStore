// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评价服务类
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
    using V5.Library.Storage.DB;

    /// <summary>
    /// 商品评价服务类
    /// </summary>
    public class ProductCommentService
    {
        #region  Constants and Fields

        /// <summary>
        /// 私有商品评价访问对象
        /// </summary>
        private readonly IProductCommentDA productCommentDA;

        /// <summary>
        /// 商品评价回复访问对象
        /// </summary>
        private readonly IProductCommentReplyDA productCommentReplyDA;

        #endregion

        #region  Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCommentService"/> class. 
        /// 实例化商品评论对象
        /// </summary>
        public ProductCommentService()
        {
            this.productCommentDA = new DAFactoryTransact().CreateProductCommentDA();
            this.productCommentReplyDA = new DAFactoryTransact().CreateProductCommentReplyDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 修改商品评价状态
        /// </summary>
        /// <param name="productComment">
        /// 更新对象
        /// </param>
        public void ModifytCommentStatus(Product_Comment productComment)
        {
            this.productCommentDA.UpdateCommentStatus(productComment);
        }

        /// <summary>
        /// 删除一条商品评价信息
        /// </summary>
        /// <param name="id">
        /// 商品评价ID
        /// </param>
        /// <returns>
        /// 删除的商品评价ID
        /// </returns>
        public int Remove(int id)
        {
            return this.productCommentDA.Delete(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        ///  行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Comment> QueryWithPaging(Paging paging, out int pageCount, out int totalCount)
        {
            var products = HttpRuntime.Cache["ProductComment"] as List<Product_Comment>;
            if (products == null)
            {
                return this.productCommentDA.Paging(paging, out pageCount, out totalCount);
            }

            var productid = int.Parse(paging.Condition.Substring(11).Trim());

            var list = (from product in products where product.ProductID == productid orderby product.CreateTime descending select product).ToList();

            pageCount = list.Count / paging.PageSize;
            totalCount = list.Count;

            list = list.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();

            return list;
        }

        /// <summary>
        /// 添加评论.
        /// </summary>
        /// <param name="productComment">
        /// Product_Comment.
        /// </param>
        /// <returns>
        /// 评论的编号.
        /// </returns>
        public int Add(Product_Comment productComment)
        {
            return this.productCommentDA.Insert(productComment);
        }

        /// <summary>
        /// 根据productID查询Comment
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// 评论列表.
        /// </returns>
        public List<Product_Comment> SelectCommentsFromBrandInfo(string condition)
        {
            return this.productCommentDA.SelectCommentsFromBrandInfo(condition);
        }

        /// <summary>
        /// 查询所有商品评论.
        /// </summary>
        /// <returns>
        /// 评论列表.
        /// </returns>
        public List<Product_Comment> QueryAll()
        {
            var list = this.productCommentDA.SelectAll();
            if (list != null)
            {
                foreach (var comment in list)
                {
                    comment.CommentReplys = this.productCommentReplyDA.SelectByCommentId(comment.ID);
                }
            }

            return list;
        }

        #endregion
    }
}