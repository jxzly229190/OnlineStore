// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Comment_Reply.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评论回复类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    /// 商品评论回复类
    /// </summary>
    [Serializable]
    public class Product_Comment_Reply
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置评论编号．
        /// </summary>
        public int CommentID { get; set; }

        /// <summary>
        /// 获取或设置回复用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置回复给用户编号．
        /// </summary>
        public int ToUserID { get; set; }

        /// <summary>
        /// 获取或设置回复给用户名称
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 获取或设置回复用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置父回复编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 获取或设置评论回复内容．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}