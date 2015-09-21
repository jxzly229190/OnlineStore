// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductBrandDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product brand da.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using V5.DataContract.Transact;

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The product brand da.
    /// </summary>
    public class ProductBrandDA : IProductBrandDA
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
        /// <param name="brand">
        /// The brand.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Product_Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException("brand");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         brand.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         brand.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandName",
                                         SqlDbType.NVarChar,
                                         brand.BrandName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandNameSpell",
                                         SqlDbType.NVarChar,
                                         brand.BrandNameSpell ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandNameEnglish",
                                         SqlDbType.NVarChar,
                                         brand.BrandNameEnglish ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         brand.SEOKeywords ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         brand.SEODescription ?? string.Empty,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         brand.SEOTitle,
                                         ParameterDirection.Input
                                         ),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDisplay",
                                         SqlDbType.Bit,
                                         brand.IsDisplay,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Layer",
                                         SqlDbType.Int,
                                         brand.Layer,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Brand_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// The select product brand by category id.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Product_Brand> SelectProductBrandByCategoryID(int categoryID, int parentID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "categoryID",
                                         SqlDbType.Int,
                                         categoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "parentID",
                                         SqlDbType.Int,
                                         parentID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Brand_SelectByCategoryID", parameters, null);
                var list = dataReader.ToList<Product_Brand>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductBrandDA - SelectProductBrandByCategoryID", exception);
            }

            return null;
        }

        /// <summary>
        /// The select product brand by parent id.
        /// </summary>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Product_Brand> SelectProductBrandByParentID(int parentID)
        {

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "parentID",
                                         SqlDbType.Int,
                                         parentID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Brand_SelectByParentID", parameters, null);
                var list = dataReader.ToList<Product_Brand>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductBrandDA - SelectProductBrandByParentID", exception);
            }

            return null;
        }

        /// <summary>
        /// 商品品牌分页方法
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
        /// 商品品牌列表
        /// </returns>
        public List<Product_Brand> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Product_Brand>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="brand">
        /// The brand.
        /// </param>
        public void Update(Product_Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException("brand");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         brand.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         brand.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         brand.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandName",
                                         SqlDbType.NVarChar,
                                         brand.BrandName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandNameSpell",
                                         SqlDbType.NVarChar,
                                         brand.BrandNameSpell,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "BrandNameEnglish",
                                         SqlDbType.NVarChar,
                                         brand.BrandNameEnglish,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         brand.SEOKeywords,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDisplay",
                                         SqlDbType.Bit,
                                         brand.IsDisplay,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         brand.Sorting,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         brand.SEOTitle,
                                         ParameterDirection.Input
                                         ),
                                         this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         brand.SEODescription,
                                         ParameterDirection.Input
                                         )
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Brand_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductBrandDA - Update", exception);
            }
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="brandID">
        /// The brand id.
        /// </param>
        public void DeleteByID(int brandID)
        {
            if (brandID <= 0)
            {
                throw new ArgumentNullException("brandID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         brandID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Brand_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductBrandDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 商品大类品牌树（大类、品牌（一级品牌））
        /// </summary>
        /// <returns></returns>
        public List<Product_Tree> SelectBrandTree()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Brand_Tree", null, null);
            if (!dataReader.HasRows)
            {
                return null;
            }

            return dataReader.ToList<Product_Tree>();
        }

        /// <summary>
        /// 根据ID查询商品牌信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product_Brand SelectById(int id)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Brand_ById", parameters, null);
            var list = dataReader.ToList<Product_Brand>();
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 查询品牌，分类视图
        /// </summary>
        /// <returns></returns>
        public List<Product_Category_Brand> SelectProductCategoryBrandAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure,
                "[sp_Product_Category_Brand_SelectAll]", null, null);
            var list = dataReader.ToList<Product_Category_Brand>();
            if (list != null)
            {
                return list;
            }
            return null;
        }

        #endregion
    }
}