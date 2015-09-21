// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评价数据访问类
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
    /// 商品评价数据访问类
    /// </summary>
    public class ProductCommentDA : IProductCommentDA
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

        #region Public Methods And Operators

        /// <summary>
        /// 更新商品评价状态
        /// </summary>
        /// <param name="model">
        /// 需更新的商品评价对象
        /// </param>
        public void UpdateCommentStatus(Product_Comment model)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    model.ID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Status",
                                    SqlDbType.Int,
                                    model.Status,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "AuditDescription",
                                    SqlDbType.NVarChar,
                                    model.AuditDescription,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    model.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "AuditTime",
                                    SqlDbType.DateTime,
                                    DateTime.Now,
                                    ParameterDirection.Input)
                            };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Comment_UpdateStatus",
                    paras,
                    null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductComment - UpdateCommentStatus", exception);
            }
        }

        /// <summary>
        /// 删除商品评论
        /// </summary>
        /// <param name="id">
        /// 商品评论编号
        /// </param>
        /// <returns>
        /// 删除的商品评论编号
        /// </returns>
        public int Delete(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Comment_DeleteRow",
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
                throw new Exception("Exception - ProductComment - Delete", exception);
            }
        }

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
        public List<Product_Comment> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paging.TableName))
                {
                    paging.TableName = "View_Product_Comment";
                }
                paging.OrderColumn = "CreateTime";
                paging.OrderType = 1;

                return this.SqlServer.Paging<Product_Comment>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
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
        public int Insert(Product_Comment productComment)
        {
            if (productComment == null)
            {
                throw new ArgumentNullException("productComment");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productComment.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "OrderID",
                                         SqlDbType.Int,
                                         productComment.OrderID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         productComment.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Score",
                                         SqlDbType.Int,
                                         productComment.Score,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         productComment.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Content",
                                         SqlDbType.VarChar,
                                         productComment.Content,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Comment_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }
        
        /// <summary>
        /// 查询ProductComments
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// 评论列表.
        /// </returns>
        public List<Product_Comment> SelectCommentsFromBrandInfo(string condition)
        {
            // sp_ProductComments_FromBtandInfo
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "condition",
                                         SqlDbType.NVarChar,
                                         condition,
                                         ParameterDirection.Input)
                                 };
            var DataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Product_Comments_SelectByCategory",
                parameters,
                null);
            return DataReader.ToList<Product_Comment>();
        }

        /// <summary>
        /// 查询所有商品评论.
        /// </summary>
        /// <returns>
        /// 评论列表.
        /// </returns>
        public List<Product_Comment> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Comment_SelectAll", null, null);
            return dataReader.ToList<Product_Comment>();
        }

        #endregion
    }
}