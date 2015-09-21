namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    public class ProductAttributeValueDA : IProductAttributeValueDA
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

        public int Insert(Product_AttributeValue productAttributeValue)
        {
            if (productAttributeValue == null)
            {
                throw new ArgumentNullException("productAttributeValue");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeID",
                                         SqlDbType.NVarChar,
                                         productAttributeValue.AttributeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeValue",
                                         SqlDbType.NVarChar,
                                         productAttributeValue.AttributeValue,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         productAttributeValue.Sorting,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDefault",
                                         SqlDbType.Int,
                                         productAttributeValue.IsDefault,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_AttributeValue_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        public List<Product_AttributeValue> Paging(int pageIndex, int pageSize, List<string> columns, string condition, string orderBy, out int totalCount)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "tableName",
                                         SqlDbType.NVarChar,
                                         "Product_AttributeValue",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageIndex",
                                         SqlDbType.Int,
                                         pageIndex,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageSize",
                                         SqlDbType.Int,
                                         pageSize,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "pageColumn",
                                         SqlDbType.VarChar,
                                         "ID",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "columns",
                                         SqlDbType.VarChar,
                                         null,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "orderBy",
                                         SqlDbType.VarChar,
                                         "ID ASC",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "condition",
                                         SqlDbType.NVarChar,
                                         condition,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "totalCount",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Paging_old", parameters, null);
                var list = dataReader.ToList<Product_AttributeValue>();
                if (parameters[7].Value != null)
                {
                    totalCount = (int)parameters[7].Value;
                }
                else
                {
                    totalCount = 0;
                }
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeValueDA - Paging", exception);
            }

            return null;
        }

        public List<Product_AttributeValue> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Product_AttributeValue>(paging, out pageCount, out totalCount, null);
        }

        public void Update(Product_AttributeValue productAttributeValue)
        {
            if (productAttributeValue == null)
            {
                throw new ArgumentNullException("productAttributeValue");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         productAttributeValue.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "AttributeValue",
                                         SqlDbType.NVarChar,
                                         productAttributeValue.AttributeValue,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Sorting",
                                         SqlDbType.Int,
                                         productAttributeValue.Sorting,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "IsDefault",
                                         SqlDbType.Int,
                                         productAttributeValue.IsDefault,
                                         ParameterDirection.Input
                                         ),
                                         this.SqlServer.CreateSqlParameter(
                                         "AttributeID",
                                         SqlDbType.Int,
                                         productAttributeValue.AttributeID,
                                         ParameterDirection.Input
                                         )
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_AttributeValue_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeValueDA - Update", exception);
            }
        }

        public void DeleteByID(int productAttributeValueID)
        {
            if (productAttributeValueID <= 0)
            {
                throw new ArgumentNullException("productAttributeValueID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         productAttributeValueID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Product_AttributeValue_DeleteRow", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ProductAttributeValueDA - DeleteByID", exception);
            }
        }

        #endregion
    }
}