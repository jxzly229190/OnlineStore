// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentReplyDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评价回复数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 商品评价回复数据访问接口
    /// </summary>
    public class ProductCommentReplyDA : IProductCommentReplyDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 根据评论编码查询回复评论信息
        /// </summary>
        /// <param name="productCommentId">
        /// 商品评论编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Comment_Reply> SelectByCommentId(int productCommentId)
        {
            try
            {
                var reader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Product_Comment_Reply_SelectAll",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "ID",
                                SqlDbType.Int,
                                productCommentId,
                                ParameterDirection.Input)
                        },
                    null);

                return reader.ToList<Product_Comment_Reply>();
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductCommentReply - UpdateCommentStatus", exception);
            }
        }

        /// <summary>
        /// 根据编码删除评论
        /// </summary>
        /// <param name="id">
        /// 评论编码
        /// </param>
        /// <returns>
        /// 删除的评论编码
        /// </returns>
        public int Delete(int id)
        {
            try
            {
                SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Comment_Reply_DeleteRow",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "ID",
                                SqlDbType.Int,
                                id,
                                ParameterDirection.Input)
                        },
                    null);

                return id;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductCommentReply - Delete", exception);
            }
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
        public int Insert(Product_Comment_Reply commentReply)
        {
            if (commentReply == null)
            {
                throw new ArgumentNullException("commentReply");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "CommentID",
                                         SqlDbType.Int,
                                         commentReply.CommentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         commentReply.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         commentReply.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Content",
                                         SqlDbType.VarChar,
                                         commentReply.Content,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Comment_Reply_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询商品评论回复.
        /// </summary>
        /// <returns>
        /// 结果列表.
        /// </returns>
        public List<Product_Comment_Reply> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Comment_Reply_AllSelect", null, null);
            return dataReader.ToList<Product_Comment_Reply>();
        }

        #endregion
    }
}