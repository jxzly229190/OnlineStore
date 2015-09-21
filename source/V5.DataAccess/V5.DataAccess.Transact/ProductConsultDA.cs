// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductConsultDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询数据访问类
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
    /// 商品咨询数据访问类
    /// </summary>
    public class ProductConsultDA : IProductConsultDA
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
        /// 回复一条咨询
        /// </summary>
        /// <param name="reply">
        /// 回复咨询的对象
        /// </param>
        public void ReplyConsult(Product_Consult reply)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ConsultID",
                                    SqlDbType.Int,
                                    reply.ID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    reply.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Content",
                                    SqlDbType.NVarChar,
                                    reply.Content,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CreateTime",
                                    SqlDbType.VarChar,
                                    DateTime.Now,
                                    ParameterDirection.Input)
                            };
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Consult_Reply_Insert",
                    paras,
                    null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductConsult - ReplyConsult", exception);
            }
        }

        /// <summary>
        /// 更新一条回复咨询
        /// </summary>
        /// <param name="reply">
        /// 回复咨询的对象
        /// </param>
        public void UpdateConsultReply(Product_Consult reply)
        {
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    reply.ID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "EmployeeID",
                                    SqlDbType.Int,
                                    reply.EmployeeID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "Content",
                                    SqlDbType.NVarChar,
                                    reply.Content,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "CreateTime",
                                    SqlDbType.VarChar,
                                    reply.CreateTime,
                                    ParameterDirection.Input)
                            };
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Consult_Reply_Update",
                    paras,
                    null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductConsult - ReplyConsult", exception);
            }
        }

        /// <summary>
        /// 根据Id删除一条咨询
        /// </summary>
        /// <param name="id">
        /// 需删除的对象编码
        /// </param>
        /// <returns>
        ///  删除的对象编码 <see cref="int"/>.
        /// </returns>
        public int DeleteConsult(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Consult_DeleteRow",
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
                throw new Exception("Exception - ProductConsult - RemoveConsult", exception);
            }
        }

        /// <summary>
        /// 删除一条回复咨询
        /// </summary>
        /// <param name="id">
        /// 需删除的对象编码
        /// </param>
        /// <returns>
        /// 删除的对象编码 <see cref="int"/>.
        /// </returns>
        public int DeleteConsultReply(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Product_Consult_Reply_DeleteRow",
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
                throw new Exception("Exception - ProductConsult - DeleteConsultReply", exception);
            }
        }

        /// <summary>
        /// 分页查询咨询回复对象
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
        /// 查询结果.
        /// </returns>
        public List<Product_Consult> PagingReplies(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                paging.TableName = "View_Product_Consult_Reply";
                paging.OrderColumn = "CreateTime";
                paging.OrderType = 1;
                return this.SqlServer.Paging<Product_Consult>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 分页查询咨询对象
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
        /// 查询结果 .
        /// </returns>
        public List<Product_Consult> QueryConsult(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                paging.TableName = "[view_Product_Consults_All]";
                return this.SqlServer.Paging<Product_Consult>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 添加商品咨询.
        /// </summary>
        /// <param name="productConsult">
        /// Product_Consult.
        /// </param>
        /// <returns>
        /// 咨询的编号.
        /// </returns>
        public int Insert(Product_Consult productConsult)
        {
            if (productConsult == null)
            {
                throw new ArgumentNullException("productConsult");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productConsult.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         productConsult.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Content",
                                         SqlDbType.VarChar,
                                         productConsult.Content,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Consult_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 查询所有商品咨询.
        /// </summary>
        /// <returns>
        /// 商品咨询列表.
        /// </returns>
        public List<Product_Consult> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Consult_SelectAll", null, null);
            return dataReader.ToList<Product_Consult>();
        }

        #endregion
    }
}