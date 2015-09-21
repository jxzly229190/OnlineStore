// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentModel.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   商品评论类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 商品评论类
    /// </summary>
    public class ProductCommentModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置评论商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置评论用户名称．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置评论分数．
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 获取或设置评论内容．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置评论的回复列表．
        /// </summary>
        public List<ProductCommentReplyModel> CommentReplys { get; set; }

        #endregion
    }
}