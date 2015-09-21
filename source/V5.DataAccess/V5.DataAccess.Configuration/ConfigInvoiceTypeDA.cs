// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigInvoiceTypeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票类别数据访问类
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
    /// 发票类别数据操作类
    /// </summary>
    public class ConfigInvoiceTypeDA : IConfigInvoiceTypeDA
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
        /// 查询所有发票类别
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Invoice_Type> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Type_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Invoice_Type>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigInvoiceTypeDA - SelectAll", ex);
            }

            return null;
        }

        /// <summary>
        /// 新增一条发票类别
        /// </summary>
        /// <param name="invoiceType">
        /// 新增的类别
        /// </param>
        /// <returns>
        /// 增加的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Invoice_Type invoiceType)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       invoiceType.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Description",
                                       SqlDbType.NVarChar,
                                       invoiceType.Description,
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
                    "sp_Config_Invoice_Type_Insert",
                    paraList,
                    null);
                return (int)paraList.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigInvoiceTypeDA - Insert", exception);
            }

        }

        /// <summary>
        /// 根据ID删除发票类别
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
                    "sp_Config_Invoice_Type_DeleteRow",
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
                throw new Exception("Exception - ConfigInvoiceTypeDA - Delete", exception);
            }
        }

        /// <summary>
        /// 更新发票类别
        /// </summary>
        /// <param name="invoiceType">
        /// 要更新的发票类别
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Invoice_Type invoiceType)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       invoiceType.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Description",
                                       SqlDbType.NVarChar,
                                       invoiceType.Description,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ID",
                                       SqlDbType.Int,
                                       invoiceType.ID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Invoice_Type_Update",
                    paraList,
                    null);

            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigInvoiceTypeDA - Update", exception);
            }
        }

        #endregion
    }
}