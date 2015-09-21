// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeValueSetDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product attribute value set da.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The product attribute value set da.
    /// </summary>
    public class ProductAttributeValueSetDA : IProductAttributeValueSetDA
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
        /// The insert.
        /// </summary>
        /// <param name="productAttributeValueSet">
        /// The product attribute value set.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Insert(Product_AttributeValueSet productAttributeValueSet, SqlTransaction transaction)
        {
            if (productAttributeValueSet == null)
            {
                throw new ArgumentNullException("productAttributeValueSet");
            }

            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productAttributeValueSet.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeID",
                                         SqlDbType.Int,
                                         productAttributeValueSet.AttributeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeValueID",
                                         SqlDbType.Int,
                                         productAttributeValueSet.AttributeValueID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_AttributeValueSet_Insert", parameters, transaction);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Delete(int productID, SqlTransaction transaction)
        {
            if (productID <= 0)
            {
                throw new ArgumentNullException("productID");
            }

            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_AttributeValueSet_DeleteByProductID", parameters, transaction);
        }

        /// <summary>
        /// The select by product id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        public List<Product_AttributeValueSet> SelectByProductID(int productID)
        {
            if (productID <= 0)
            {
                throw new ArgumentNullException("productID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_AttributeValueSet_SelectByProductID", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<Product_AttributeValueSet>();
        }

        #endregion
    }
}