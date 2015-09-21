// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Paging.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The paging.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.DB
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// The paging.
    /// </summary>
    public class Paging
    {
        #region Constants and Fields

        /// <summary>
        /// The condition.
        /// </summary>
        private readonly string condition;

        /// <summary>
        /// The order column.
        /// </summary>
        private string orderColumn;

        /// <summary>
        /// The order type.
        /// </summary>
        private int orderType;

        /// <summary>
        /// The distinct.
        /// </summary>
        private readonly bool distinct;

        /// <summary>
        /// The table name.
        /// </summary>
        private string tableName;

        /// <summary>
        /// The column names.
        /// </summary>
        private List<string> columnNames;

        /// <summary>
        /// The primary key.
        /// </summary>
        private string primaryKey;

        /// <summary>
        /// The page index.
        /// </summary>
        private int pageIndex;

        /// <summary>
        /// The page size.
        /// </summary>
        private int pageSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        public Paging(string condition, int pageIndex, int pageSize)
        {
            this.condition = condition;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <param name="columnNames">
        /// The column names.
        /// </param>
        /// <param name="primaryKey">
        /// The primary key.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        public Paging(string tableName, List<string> columnNames, string primaryKey, string condition, int pageIndex, int pageSize)
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.primaryKey = primaryKey;
            this.condition = condition;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <param name="columnNames">
        /// The column names.
        /// </param>
        /// <param name="primaryKey">
        /// The primary key.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="orderColumn">
        /// The order column.
        /// </param>
        /// <param name="orderType">
        /// The order type.
        /// </param>
        public Paging(string tableName, List<string> columnNames, string primaryKey, string condition, int pageIndex, int pageSize, string orderColumn, int orderType)
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.primaryKey = primaryKey;
            this.condition = condition;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.orderColumn = orderColumn;
            this.orderType = orderType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <param name="columnNames">
        /// The column names.
        /// </param>
        /// <param name="primaryKey">
        /// The primary key.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="orderColumn">
        /// The order column.
        /// </param>
        /// <param name="orderType">
        /// The order type.
        /// </param>
        /// <param name="distinct">
        /// The distinct.
        /// </param>
        public Paging(string tableName, List<string> columnNames, string primaryKey, string condition, int pageIndex, int pageSize, string orderColumn, int orderType, bool distinct)
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.primaryKey = primaryKey;
            this.condition = condition;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.orderColumn = orderColumn;
            this.orderType = orderType;
            this.distinct = distinct;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The table name.
        /// </summary>
        public string TableName
        {
            get
            {
                return this.tableName;
            }

            set
            {
                this.tableName = value;
            }
        }

        /// <summary>
        /// The column names.
        /// </summary>
        public List<string> ColumnNames
        {
            get
            {
                return this.columnNames;
            }
            
            set
            {
                this.columnNames = value;
            }
        }

        /// <summary>
        /// The primary key.
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return this.primaryKey;
            }
            
            set
            {
                this.primaryKey = value;
            }
        }

        /// <summary>
        /// The condition.
        /// </summary>
        public string Condition
        {
            get
            {
                return this.condition;
            }
        }

        /// <summary>
        /// The page index.
        /// </summary>
        public int PageIndex
        {
            get
            {
                return this.pageIndex;
            }

            set
            {
                this.pageIndex = value;
            }
        }

        /// <summary>
        /// The page size.
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }

            set
            {
                this.pageSize = value;
            }
        }

        /// <summary>
        /// The order column.
        /// </summary>
        public string OrderColumn
        {
            get
            {
                return this.orderColumn;
            }
            set
            {
                this.orderColumn = value;
            }
        }

        /// <summary>
        /// The order type.
        /// </summary>
        public int OrderType
        {
            get
            {
                return this.orderType;
            }

            set
            {
                this.orderType = value;
            }
        }

        /// <summary>
        /// The distinct.
        /// </summary>
        public bool Distinct
        {
            get
            {
                return this.distinct;
            }
        }

        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 获取分页参数列表
        /// </summary>
        /// <returns>
        /// 分页参数列表
        /// </returns>
        public List<SqlParameter> GetSqlParameters()
        {
            return new List<SqlParameter>
                       {
                           this.CreateSqlParameter(
                               "tableName",
                               SqlDbType.NVarChar,
                               this.TableName,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "columnName",
                               SqlDbType.NVarChar,
                               this.ColumnNames == null ? "*" : string.Join(", ", this.ColumnNames.ToArray()),
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "primaryKey",
                               SqlDbType.NVarChar,
                               string.IsNullOrEmpty(this.PrimaryKey) ? "ID" : this.PrimaryKey,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "condition",
                               SqlDbType.NVarChar,
                               this.Condition,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageIndex",
                               SqlDbType.Int,
                               this.PageIndex == 0 ? 1 : this.PageIndex,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageSize",
                               SqlDbType.Int,
                               this.PageSize == 0 ? 1 : this.PageSize,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "orderColumn",
                               SqlDbType.VarChar,
                               this.OrderColumn,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "orderType",
                               SqlDbType.Int,
                               this.OrderType < 0 || this.OrderType > 1 ? 0 : this.OrderType,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageCount",
                               SqlDbType.Int,
                               null,
                               ParameterDirection.Output),
                           this.CreateSqlParameter(
                               "totalCount",
                               SqlDbType.Int,
                               null,
                               ParameterDirection.Output)
                       };
        }

        /// <summary>
        /// 获取会员分页参数列表
        /// </summary>
        /// <returns>
        /// 会员分页参数列表
        /// </returns>
        public List<SqlParameter> GetSqlParametersForUserPaging()
        {
            return new List<SqlParameter>
                       {
                           this.CreateSqlParameter(
                               "tableName",
                               SqlDbType.NVarChar,
                               string.IsNullOrEmpty(this.TableName) ? string.Empty : this.TableName,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "condition",
                               SqlDbType.NVarChar,
                               this.Condition,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageIndex",
                               SqlDbType.Int,
                               this.PageIndex == 0 ? 1 : this.PageIndex,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageSize",
                               SqlDbType.Int,
                               this.PageSize == 0 ? 1 : this.PageSize,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "orderColumn",
                               SqlDbType.VarChar,
                               this.OrderColumn,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "orderType",
                               SqlDbType.Int,
                               this.OrderType < 0 || this.OrderType > 1 ? 0 : this.OrderType,
                               ParameterDirection.Input),
                           this.CreateSqlParameter(
                               "pageCount",
                               SqlDbType.Int,
                               null,
                               ParameterDirection.Output),
                           this.CreateSqlParameter(
                               "totalCount",
                               SqlDbType.Int,
                               null,
                               ParameterDirection.Output)
                       };
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// 创建数据库参数
        /// </summary>
        /// <param name="parameterName">
        /// 参数名称
        /// </param>
        /// <param name="sqlDbType">
        /// 参数数据类型
        /// </param>
        /// <param name="parameterValue">
        /// 参数值
        /// </param>
        /// <param name="parameterDirection">
        /// 参数类型
        /// </param>
        /// <returns>
        /// 参数对象
        /// </returns>
        private SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, object parameterValue, ParameterDirection parameterDirection)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                return null;
            }

            return new SqlParameter(parameterName, sqlDbType)
            {
                Value = parameterValue,
                Direction = parameterDirection
            };
        }

        #endregion
    }
}