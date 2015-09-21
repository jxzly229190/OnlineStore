// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigInvoiceContentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//  发票内容数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Configuration;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 发票内容数据访问类
    /// </summary>
    public class ConfigInvoiceContentDA : IConfigInvoiceContentDA
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
        /// 查询所有发票内容列表
        /// </summary>
        /// <returns>
        /// 查询的结果
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Invoice_Content> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Content_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Invoice_Content>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigInvoiceContentDA - SelectAll", ex);
            }

            return null;
        }

        /// <summary>
        /// 新增一条发票内容
        /// </summary>
        /// <param name="invoiceContent">
        /// 要新增的发票内容
        /// </param>
        /// <returns>
        /// 新增的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Invoice_Content invoiceContent)
        {
            var parameters = new List<SqlParameter>()
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         invoiceContent.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Description",
                                         SqlDbType.NVarChar,
                                         invoiceContent.Description,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         DateTime.Now,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };
            try
            {

                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Content_Insert",
                    parameters,
                    null);
                return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigInvoiceContentDA - Insert", exception);
            }
        }

        /// <summary>
        /// 根据ID删除一条发票内容
        /// </summary>
        /// <param name="id">
        /// 要删除的Id
        /// </param>
        /// <returns>
        /// 已删除的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Content_DeleteRow",
                    new List<SqlParameter>()
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
                throw new Exception("Exception - ConfigInvoiceContentDA - Delete", exception);
            }
        }

        /// <summary>
        /// 更新发票内容
        /// </summary>
        /// <param name="invoiceContent">
        /// 要更新的发票内容
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Invoice_Content invoiceContent)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       invoiceContent.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Description",
                                       SqlDbType.NVarChar,
                                       invoiceContent.Description,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ID",
                                       SqlDbType.Int,
                                       invoiceContent.ID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Content_Update",
                    paraList,
                    null);

            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigInvoiceContentDA - Update", exception);
            }
        }

        #endregion
    }
}