// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The product da.
    /// </summary>
    public class ProductDA : IProductDA
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
        /// 判断商品是否存在相同的名称或条形码.
        /// </summary>
        /// <param name="id">
        /// 商品编号.
        /// </param>
        /// <param name="name">
        /// 商品名称.
        /// </param>
        /// <param name="barcode">
        /// 商品条形码.
        /// </param>
        /// <returns>
        /// 重复数据条数.
        /// </returns>
        public int VerifyProduct(int id, string name, string barcode)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.VarChar,
                                         name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Barcode",
                                         SqlDbType.VarChar,
                                         barcode,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteScalar(CommandType.StoredProcedure, "sp_Product_Verify_Name_Barcode", parameters, null);

            return (int)dataReader;
        }

        /// <summary>
        /// The select by id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public ProductSearchResult SelectByID(int productID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectByProductID", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = dataReader.ToList<ProductSearchResult>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 根据商品编码查询商品详细信息.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="all">
        /// true 所有商品，false:商品详情页（仅上架商品且类别不为其他）.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public ProductSearchResult SelectByID(int productID, bool all)
        {
            if (all)
            {
                return this.SelectByID(productID);
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Product_SelectByOnsaleProductID",
                parameters,
                null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = dataReader.ToList<ProductSearchResult>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 查询指定的商品.
        /// </summary>
        /// <param name="productIDs">
        /// 商品编号（例如：1，2，3）.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearchResult"/>.
        /// </returns>
        public List<ProductSearchResult> SelectResultByID(string productIDs)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductIDs",
                                         SqlDbType.Text,
                                         productIDs,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectByProductStr", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<ProductSearchResult>();
        }

        /// <summary>
        /// 商品查询分页方法
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 商品查询结果列表
        /// </returns>
        public List<ProductSearchResult> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<ProductSearchResult>(paging, out pageCount, out totalCount, null);
        }
        
        /// <summary>
        /// 查询产品ID
        /// </summary>
        /// <param name="paging">
        /// 分页对像.
        /// </param>
        /// <param name="pageCount">
        /// The page Count.
        /// </param>
        /// <param name="totalCount">
        /// The total Count.
        /// </param>
        /// <returns>
        /// 结果列表.
        /// </returns>
        public List<int> SelectProductID(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// The select by id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="Product"/>.
        /// </returns>
        public Product SelectByID(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                throw new ArgumentNullException("productID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.NVarChar,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectRow", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = dataReader.ToList<Product>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 修改商品状态.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="status">
        /// 商品状态（1：未上架，2：已上架，3：已下架，4：已删除）.
        /// </param>
        public void UpdateProductStatus(string productID, int status)
        {
            if (string.IsNullOrEmpty(productID))
            {
                throw new ArgumentNullException("productID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.NVarChar,
                                         productID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         status,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_UpdateStates", parameters, null);
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        public void DeleteByID(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                throw new ArgumentNullException("productID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.NVarChar,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_DeleteRow", parameters, null);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Product product, out SqlTransaction transaction)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentCategoryID",
                                         SqlDbType.Int,
                                         product.ParentCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         product.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         product.ParentBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         product.ProductBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Barcode",
                                         SqlDbType.NVarChar,
                                         product.Barcode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         product.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         product.SEOTitle,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         product.SEOKeywords,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         product.SEODescription,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Advertisement",
                                         SqlDbType.NVarChar,
                                         product.Advertisement,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MarketPrice",
                                         SqlDbType.Float,
                                         product.MarketPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "GoujiuPrice",
                                         SqlDbType.Float,
                                         product.GoujiuPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Introduce",
                                         SqlDbType.NText,
                                         product.Introduce,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         product.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "InventoryNumber",
                                         SqlDbType.Int,
                                         product.InventoryNumber,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CommentNumber",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfReality",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfVirtual",
                                         SqlDbType.Int,
                                         product.SoldOfVirtual,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PageView",
                                         SqlDbType.Int,
                                         product.PageView,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         product.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         product.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Attributes",
                                         SqlDbType.VarChar,
                                         product.Attributes,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Update(Product product, out SqlTransaction transaction)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            this.SqlServer.BeginTransaction(IsolationLevel.ReadCommitted);
            transaction = this.SqlServer.Transaction;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         product.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Barcode",
                                         SqlDbType.NVarChar,
                                         product.Barcode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         product.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEOTitle),
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEOKeywords),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEODescription),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Advertisement",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.Advertisement),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MarketPrice",
                                         SqlDbType.Float,
                                         product.MarketPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "GoujiuPrice",
                                         SqlDbType.Float,
                                         product.GoujiuPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Introduce",
                                         SqlDbType.NVarChar,
                                         product.Introduce,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         product.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfVirtual",
                                         SqlDbType.Int,
                                         Utils.ToString(product.SoldOfVirtual, "0"),
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "InventoryNumber",
                                         SqlDbType.Int,
                                         product.InventoryNumber,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         product.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Attributes",
                                         SqlDbType.VarChar,
                                         product.Attributes,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Update", parameters, null);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentCategoryID",
                                         SqlDbType.Int,
                                         product.ParentCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         product.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         product.ParentBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         product.ProductBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Barcode",
                                         SqlDbType.NVarChar,
                                         product.Barcode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         product.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         product.SEOTitle,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         product.SEOKeywords,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         product.SEODescription,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Advertisement",
                                         SqlDbType.NVarChar,
                                         product.Advertisement,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MarketPrice",
                                         SqlDbType.Float,
                                         product.MarketPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "GoujiuPrice",
                                         SqlDbType.Float,
                                         product.GoujiuPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Introduce",
                                         SqlDbType.NText,
                                         product.Introduce,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         product.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "InventoryNumber",
                                         SqlDbType.Int,
                                         product.InventoryNumber,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CommentNumber",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfReality",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfVirtual",
                                         SqlDbType.Int,
                                         product.SoldOfVirtual,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PageView",
                                         SqlDbType.Int,
                                         product.PageView,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         product.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         product.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Attributes",
                                         SqlDbType.VarChar,
                                         product.Attributes,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         product.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Barcode",
                                         SqlDbType.NVarChar,
                                         product.Barcode,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         product.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEOTitle),
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEOKeywords),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.SEODescription),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Advertisement",
                                         SqlDbType.NVarChar,
                                         Utils.ToString(product.Advertisement),
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "MarketPrice",
                                         SqlDbType.Float,
                                         product.MarketPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "GoujiuPrice",
                                         SqlDbType.Float,
                                         product.GoujiuPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Introduce",
                                         SqlDbType.NVarChar,
                                         product.Introduce,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Integral",
                                         SqlDbType.Int,
                                         product.Integral,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SoldOfVirtual",
                                         SqlDbType.Int,
                                         Utils.ToString(product.SoldOfVirtual, "0"),
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "InventoryNumber",
                                         SqlDbType.Int,
                                         product.InventoryNumber,
                                         ParameterDirection.Input),
                                     this.sqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         product.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Attributes",
                                         SqlDbType.VarChar,
                                         product.Attributes,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Update", parameters, null);
        }

        /// <summary>
        /// 更新商品销售量
        /// </summary>
        /// <param name="productID">
        /// 商品编码
        /// </param>
        /// <param name="saleAmount">
        /// 需更新的销量
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public void UpdateProductSaleAmount(int productID, int saleAmount, SqlTransaction transaction)
        {
            /**
             Create Procedure sp_Product_Update_SaleAmount
	            @ID int,
	            @SaleAmount int
            As
             */
            var paras = new List<SqlParameter>
                            {
                                this.SqlServer.CreateSqlParameter(
                                    "ID",
                                    SqlDbType.Int,
                                    productID,
                                    ParameterDirection.Input),
                                this.SqlServer.CreateSqlParameter(
                                    "SaleAmount",
                                    SqlDbType.Int,
                                    saleAmount,
                                    ParameterDirection.Input)
                            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Update_SaleAmount", paras, transaction);
        }

        /// <summary>
        /// The select.
        /// </summary>
        /// <param name="productType">
        /// The product type.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="whereString">
        /// The where string.
        /// </param>
        /// <returns>
        /// ProductSearchResult列表.
        /// </returns>
        public List<ProductSearchResult> Select(ProductType productType, int count, string whereString)
        {
            // Select * from view_product Where wherestring and 
            string orderColumn = "CreateTime";
            if (productType == ProductType.Rand)
            {
                orderColumn = "newid()";
            }

            var paging = new Paging(whereString, 1, count)
            {
                TableName = "view_Product_Paging",
                PrimaryKey = "ID",
                OrderType = 1,
                OrderColumn = orderColumn
            };
            int pageCount, totalCount;

            return this.SqlServer.Paging<ProductSearchResult>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 搜索建议
        /// </summary>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <returns>
        /// 结果列表.
        /// </returns>
        public List<ProductSearchSuggest> SearchSuggest(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentNullException("search");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "SearchText",
                                         SqlDbType.VarChar,
                                         search,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Search_Suggest", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<ProductSearchSuggest>();
        }

        /// <summary>
        /// 更新搜索
        /// </summary>
        public void UpdateProductSearch()
        {
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Search_Update", null, null);            
        }

        /// <summary>
        /// 商品树（大类、品牌、）
        /// </summary>
        /// <returns>
        /// 结果列表.
        /// </returns>
        public List<Product_Tree> SelectAllTree()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectAll_Tree", null, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<Product_Tree>();
        }

        /// <summary>
        /// 商品搜索统计
        /// </summary>
        /// <param name="keyword">
        /// The keyword.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// The .
        /// </returns>
        public List<ProductSearchSuggestTip> SearchSuggestTip(string keyword, string condition)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "keyword",
                                         SqlDbType.VarChar,
                                         keyword,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "condition",
                                         SqlDbType.VarChar,
                                         condition,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Search_SuggestTip", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<ProductSearchSuggestTip>();
        }

        /// <summary>
        /// 促销选择商品时获取全部在售商品.
        /// </summary>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<Product_Tree> SelectAllProduct()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectAllProduct", null, null);
            return !dataReader.HasRows ? null : dataReader.ToList<Product_Tree>();
        }
        
        /// <summary>
        /// 查询所有商品ID
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// 所有商品编号列表.
        /// </returns>
        public List<int> SelectAllProductID(int status)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         status,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Product_SelectAllProductID",
                parameters,
                null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = new List<int>();
            while (dataReader.Read())
            {
                list.Add(Convert.ToInt32(dataReader[0]));
            }

            dataReader.Close();
            dataReader.Dispose();

            return list.Count > 0 ? list : null;
        }

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="productID">
        /// The product ID.
        /// </param>
        /// <returns>
        /// The <see cref="ProductSearch"/>.
        /// </returns>
        public ProductSearch SelectProductSearchByID(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                throw new ArgumentNullException("productID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Search_SelectByID", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            var list = dataReader.ToList<ProductSearch>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 搜索所有产品
        /// </summary>
        /// <returns></returns>
        public List<ProductSearch> SelectAllProductSearch()
        {
            DataSet ds = this.SqlServer.ExecuteDataAdapter(CommandType.StoredProcedure, "sp_Product_Search_SelectAll", null, null);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            return ds.Tables[0].ToList<ProductSearch>();
        }

        /// <summary>
        /// 猜你喜欢.
        /// </summary>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<Product> SelectGuessLike()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectGuessLike", null, null);
            return !dataReader.HasRows ? null : dataReader.ToList<Product>();
        }

        /// <summary>
        /// 更新商品促销结束时间.
        /// </summary>
        /// <param name="productStr">
        /// 商品集合字符串.
        /// </param>
        /// <param name="promoteEndTime">
        /// 商品促销结束时间
        /// </param>
        /// <param name="transaction">数据事务</param>
        public void UpdateProductPromoteEndTime(string productStr, DateTime? promoteEndTime, SqlTransaction transaction)
        {
            if (string.IsNullOrEmpty(productStr))
            {
                throw new ArgumentNullException("productStr");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductStr",
                                         SqlDbType.NVarChar,
                                         productStr,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteEndTime",
                                         SqlDbType.NVarChar,
                                         promoteEndTime,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Product_UpdateProductPromoteEndTime",
                parameters,
                transaction);
        }

        /// <summary>
        /// 查询所有已参加促销活动的商品.
        /// </summary>
        /// <param name="productStr">
        /// 商品集合字符串.
        /// </param>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<Product> SelectPromoteProducts(string productStr)
        {
            if (string.IsNullOrEmpty(productStr))
            {
                throw new ArgumentNullException("productStr");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductStr",
                                         SqlDbType.NVarChar,
                                         productStr,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_SelectPromoteProduct", parameters, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<Product>();
        }

        /// <summary>
        /// 查询商品的最大编号.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SelectProductCount()
        {
            var dataReader = this.SqlServer.ExecuteScalar(
                CommandType.StoredProcedure,
                "sp_Product_SelectByProductCount",
                null,
                null);

            return (int)dataReader;
        }

        /// <summary>
        /// 根据BrandInfoMation里的多个ID量询商品
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// 商品编号.
        /// </returns>
        public List<Product> SelectProductFromInfo(string condition)
        {
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
                "sp_Product_ByProductIDString",
                parameters,
                null);
            var list = DataReader.ToList<Product>();
            return list;
        }

        /// <summary>
        /// The select product by id.
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// 商品列表.
        /// </returns>
        public List<Product> SelectProductById(string condition)
        {
            // [sp_Product_Brand_ById]
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         condition,
                                         ParameterDirection.Input)
                                 };
            var DataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "[sp_Product_Brand_ById]",
                parameters,
                null);
            var list = DataReader.ToList<Product>();
            return list;
        }

        #endregion
    }
}