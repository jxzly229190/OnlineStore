// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductPictureDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductPictureDA type.
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
    /// The product picture da.
    /// </summary>
    public class ProductPictureDA : IProductPictureDA
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
        /// <param name="productPicture">
        /// The product picture.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Insert(Product_Picture productPicture, SqlTransaction transaction)
        {
            if (productPicture == null)
            {
                throw new ArgumentNullException("productPicture");
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
                                         productPicture.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PictureID",
                                         SqlDbType.Int,
                                         productPicture.PictureID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMaster",
                                         SqlDbType.Bit,
                                         productPicture.IsMaster,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Picture_Insert", parameters, transaction);
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Picture_DeleteByProductID", parameters, transaction);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="productPicture">
        /// The product picture.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Insert(Product_Picture productPicture)
        {
            if (productPicture == null)
            {
                throw new ArgumentNullException("productPicture");
            }
            
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productPicture.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PictureID",
                                         SqlDbType.Int,
                                         productPicture.PictureID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsMaster",
                                         SqlDbType.Bit,
                                         productPicture.IsMaster,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Picture_Insert", parameters, null);
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
        public void Delete(int productID)
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

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Picture_DeleteByProductID", parameters, null);
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
        public List<Product_Picture> SelectByProductID(int productID)
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

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Picture_SelectByProductID", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<Product_Picture>();
        }

        #endregion
    }
}
