// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品属性数据库访问类.
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
    /// 商品属性数据库访问类.
    /// </summary>
    public class ProductAttributeDA : IProductAttributeDA
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
        /// 添加商品属性.
        /// </summary>
        /// <param name="productAttribute">
        /// 商品属性实体
        /// </param>
        /// <returns>
        /// 添加记录的主键值.
        /// </returns>
        public int Insert(Product_Attribute productAttribute)
        {
            if (productAttribute == null)
            {
                throw new ArgumentNullException("productAttribute");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.NVarChar,
                                         productAttribute.ProductCategoryID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeName",
                                         SqlDbType.NVarChar,
                                         productAttribute.AttributeName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DataType",
                                         SqlDbType.NVarChar,
                                         productAttribute.DataType,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DataLength",
                                         SqlDbType.Int,
                                         productAttribute.DataLength,
                                         ParameterDirection.Input),
                                         this.sqlServer.CreateSqlParameter(
                                         "InputType",
                                         SqlDbType.NVarChar,
                                         productAttribute.InputType,
                                         ParameterDirection.Input
                                         ),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Attribute_Insert", parameters, null);
                return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeDA - Insert", exception);
            }
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
        public List<Product_Attribute> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Product_Attribute>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 修改图片信息.
        /// </summary>
        /// <param name="productAttribute">
        /// 图片实体.
        /// </param>
        public void Update(Product_Attribute productAttribute)
        {
            if (productAttribute == null)
            {
                throw new ArgumentNullException("productAttribute");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         productAttribute.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeName",
                                         SqlDbType.NVarChar,
                                         productAttribute.AttributeName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         productAttribute.Sorting,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "DataLength",
                                         SqlDbType.Int,
                                         productAttribute.DataLength,
                                         ParameterDirection.Input),
                                         this.sqlServer.CreateSqlParameter(
                                         "InputType",
                                         SqlDbType.NVarChar,
                                         productAttribute.InputType,
                                         ParameterDirection.Input
                                         ),
                                         this.SqlServer.CreateSqlParameter(
                                         "DataType",
                                         SqlDbType.NVarChar,
                                         productAttribute.DataType,
                                         ParameterDirection.Input
                                         )
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Attribute_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeDA - Update", exception);
            }
        }

        /// <summary>
        /// 根据图片ID做删除.
        /// </summary>
        /// <param name="productAttributeID">
        /// 图片ID.
        /// </param>
        public void DeleteByID(int productAttributeID)
        {
            if (productAttributeID <= 0)
            {
                throw new ArgumentNullException("productAttributeID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         productAttributeID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_Attribute_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeDA - DeleteByID", exception);
            }
        }

        /// <summary>
        /// 根据类目查询属性和属性值
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string QueryByCategoryId(string categoryId)
        {
            if (categoryId == null)
            {
                throw new ArgumentNullException("productAttribute");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.NVarChar,
                                         categoryId,
                                         ParameterDirection.Input)
                                 };

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Product_Attribute_SelectByCategoryID", parameters, null);
                if (!dataReader.HasRows)
                {
                    return "";
                }

                List<string> list = dataReader.ToList();
                return "[" + string.Join(", ", list.ToArray()) + "]";
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeDA - QueryByCategoryId", exception);
            }
        }

        #endregion
    }
}