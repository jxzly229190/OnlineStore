// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoryDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.  
// </copyright>
// <summary>
//   商品类别数据库访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 商品类别数据库访问类.
    /// </summary>
    public class ProductCategoryDA : IProductCategoryDA
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
        /// 添加类别
        /// </summary>
        /// <param name="category">
        /// 商品类别实体
        /// </param>
        /// <returns>
        /// 新增记录主键值
        /// </returns>
        public int Insert(Product_Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "@ParentID",
                                         SqlDbType.Int,
                                         category.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@CategoryName",
                                         SqlDbType.NVarChar,
                                         category.CategoryName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@CategoryNameSpell",
                                         SqlDbType.NVarChar,
                                         category.CategoryNameSpell ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@CategoryNameEnglish",
                                         SqlDbType.NVarChar,
                                         category.CategoryNameEnglish ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@SEOKeywords",
                                         SqlDbType.NVarChar,
                                         category.SEOKeywords ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@SEODescription",
                                         SqlDbType.NVarChar,
                                         category.SEODescription ?? string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@IsGjw",
                                         SqlDbType.Bit,
                                         category.IsGjw,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@IsDisplay",
                                         SqlDbType.Bit,
                                         category.IsDisplay,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@Layer",
                                         SqlDbType.Int,
                                         category.Layer,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@CreateTime",
                                         SqlDbType.DateTime,
                                         category.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "@ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output),
                                         this.SqlServer.CreateSqlParameter(
                                         "@SEOTitle",
                                          SqlDbType.NVarChar,
                                          category.SEOTitle,
                                          ParameterDirection.Input
                                         )
                                 };
            var SqlString = new StringBuilder();
            SqlString.Append("Insert Into Product_Category([ParentID],[CategoryName],[CategoryNameSpell],[CategoryNameEnglish],[SEOKeywords],[SEODescription],[IsGjw],[IsDisplay],[Layer],[Sorting],[CreateTime],[SEOTitle]) ");
            SqlString.Append(
                "Values (@ParentID,@CategoryName,@CategoryNameSpell,@CategoryNameEnglish,@SEOKeywords,@SEODescription,@IsGjw,@IsDisplay,@Layer,null,@CreateTime,@SEOTitle)  ");
            SqlString.Append("set @ReferenceID = @@IDENTITY");
            this.SqlServer.ExecuteNonQuery(CommandType.Text, SqlString.ToString(), parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "@ReferenceID").Value;
        }

        /// <summary>
        /// 商品类别分页方法
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
        /// 商品类别列表
        /// </returns>
        public List<Product_Category> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Product_Category>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 根据父类别ID获取类别列表.
        /// </summary>
        /// <param name="parentID">
        /// 父ID.
        /// </param>
        /// <returns>
        /// 商品类别列表.
        /// </returns>
        public List<Product_Category> SelectCategoryByParentID(int parentID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "@parentID",
                                         SqlDbType.Int,
                                         parentID,
                                         ParameterDirection.Input)
                                 };
            string SqlString =
                "Select [ID],[ParentID],[CategoryName],[CategoryNameSpell],[CategoryNameEnglish],[SEOKeywords],[SEODescription],[IsGjw],[IsDisplay],[Layer],[Sorting],[CreateTime],[SEOTitle] From Product_Category Where IsDelete = 0 And [ParentID] = @ParentID";
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.Text, SqlString, parameters, null);
                var list = dataReader.ToList<Product_Category>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductCategoryDA - SelectCategoryByParentID", exception);
            }

            return null;
        }

        /// <summary>
        /// 修改类别信息.
        /// </summary>
        /// <param name="category">
        /// 类别实体.
        /// </param>
        public void Update(Product_Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         category.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentID",
                                         SqlDbType.Int,
                                         category.ParentID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CategoryName",
                                         SqlDbType.NVarChar,
                                         category.CategoryName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CategoryNameSpell",
                                         SqlDbType.NVarChar,
                                         category.CategoryNameSpell??string.Empty,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CategoryNameEnglish",
                                         SqlDbType.NVarChar,
                                         category.CategoryNameEnglish,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEOKeywords",
                                         SqlDbType.NVarChar,
                                         category.SEOKeywords,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "SEODescription",
                                         SqlDbType.NVarChar,
                                         category.SEODescription,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDisplay",
                                         SqlDbType.Bit,
                                         category.IsDisplay,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         category.Sorting,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "SEOTitle",
                                         SqlDbType.NVarChar,
                                         category.SEOTitle??string.Empty,
                                         ParameterDirection.Input
                                         )
                                 };
            string sqlString =
                "Update Product_Category Set [ParentID] = @ParentID,[CategoryName] = @CategoryName,[CategoryNameSpell] = @CategoryNameSpell,[CategoryNameEnglish] = @CategoryNameEnglish,[SEOKeywords] = @SEOKeywords,[SEODescription] = @SEODescription,[IsDisplay] = @IsDisplay,[Sorting] = @Sorting,[SEOTitle]=@SEOTitle Where [ID] = @ID";
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.Text, sqlString, parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductCategpryDA - Update", exception);
            }
        }

        /// <summary>
        /// 删除类别.
        /// </summary>
        /// <param name="categoryID">
        /// 商品类别ID.
        /// </param>
        public void Delete(int categoryID)
        {
            if (categoryID <= 0)
            {
                throw new ArgumentNullException("categoryID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         categoryID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Category_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - BackstageDepartmentDA - Delete", exception);
            }
        }

        #endregion
    }
}