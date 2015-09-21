// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelGroupBuyDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ChannelGroupBuyDA type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Channel
{
    using global::System;

    using global::System.Collections.Generic;

    using global::System.Data;

    using global::System.Data.SqlClient;
    using global::System.Linq;

    using V5.DataContract.Channel;
    using V5.DataContract.Product;
    using V5.Library.Storage.DB;
    using V5.DataAccess.Channel;

    /// <summary>
    /// 团购数据库访问类
    /// </summary>
    public class ChannelGroupBuyDA : IChannelGroupBuyDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer _sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this._sqlServer ?? (this._sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加团购活动
        /// </summary>
        /// <param name="groupBy">
        /// 团购活动对象
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// 返回数据
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 数据库操作异常
        /// </exception>
        public int Insert(Channel_GroupBuy groupBy)
        {
            if (groupBy == null)
            {
                throw new ArgumentNullException("groupBy");
            }

            var parameters = new List<SqlParameter>
			                     {
				                     this._sqlServer.CreateSqlParameter(
					                     "ProductID",
					                     SqlDbType.Int,
					                     groupBy.ProductID,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "UserLevelID",
					                     SqlDbType.Int,
					                     groupBy.UserLevelID,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Name",
					                     SqlDbType.NVarChar,
					                     groupBy.Name,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "GBPrice",
					                     SqlDbType.Decimal,
					                     groupBy.GBPrice,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "TotalNumber",
					                     SqlDbType.Int,
					                     groupBy.TotalNumber,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Introduce",
					                     SqlDbType.NText,
					                     groupBy.Introduce,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "ShowLevel",
					                     SqlDbType.Int,
					                     groupBy.ShowLevel,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "StartTime",
					                     SqlDbType.DateTime,
					                     groupBy.StartTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "EndTime",
					                     SqlDbType.DateTime,
					                     groupBy.EndTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "IsShowTime",
					                     SqlDbType.Bit,
					                     groupBy.IsShowTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "IsOnlinePayment",
					                     SqlDbType.Bit,
					                     groupBy.IsOnlinePayment,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "SoldOfReality",
					                     SqlDbType.Int,
					                     groupBy.SoldOfReality,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "SoldOfVirtual",
					                     SqlDbType.Int,
					                     groupBy.SoldOfVirtual,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "PageView",
					                     SqlDbType.Int,
					                     groupBy.PageView,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Status",
					                     SqlDbType.Int,
					                     groupBy.Status,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "CreateTime",
					                     SqlDbType.DateTime,
					                     groupBy.CreateTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "ReferenceID",
					                     SqlDbType.Int,
					                     null,
					                     ParameterDirection.Output)
			                     };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Channel_GroupBuy_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 团购商品分页
        /// </summary>
        /// <param name="paging">
        ///     分页数据对象
        /// </param>
        /// <param name="pageCount">
        ///     总页数
        /// </param>
        /// <param name="totalCount">
        ///     总记录数
        /// </param>
        /// <returns>
        /// <see>
        /// <cref>List</cref>
        /// </see>
        /// </returns>
        public List<View_GroupBuy_Product> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<View_GroupBuy_Product>(paging, out pageCount, out totalCount, null);
        }

        public List<Product> PagingProduct(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Product>(paging, out pageCount, out totalCount, null);
        }

        #endregion

        /// <summary>
        /// 根据商品ProductId查询团购商品是否存在
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns></returns>
        public List<Channel_GroupBuy> SelectGroupBuyByProductId(int productId)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productId,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this._sqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Channel_GroupBuy_SelectProductId", parameters, null);
            var list = dataReader.ToList<Channel_GroupBuy>();

            if (list.Count > 0)
            {
                return list;
            }

            return list;
        }


        public int Update(Channel_GroupBuy groupBy)
        {
            if (groupBy == null)
            {
                throw new ArgumentNullException("groupBy");
            }

            var parameters = new List<SqlParameter>
			                     {
				                     this._sqlServer.CreateSqlParameter(
					                     "ProductID",
					                     SqlDbType.Int,
					                     groupBy.ProductID,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "UserLevelID",
					                     SqlDbType.Int,
					                     groupBy.UserLevelID,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Name",
					                     SqlDbType.NVarChar,
					                     groupBy.Name,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "GBPrice",
					                     SqlDbType.Decimal,
					                     groupBy.GBPrice,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "TotalNumber",
					                     SqlDbType.Int,
					                     groupBy.TotalNumber,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Introduce",
					                     SqlDbType.NText,
					                     groupBy.Introduce,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "ShowLevel",
					                     SqlDbType.Int,
					                     groupBy.ShowLevel,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "StartTime",
					                     SqlDbType.DateTime,
					                     groupBy.StartTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "EndTime",
					                     SqlDbType.DateTime,
					                     groupBy.EndTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "IsShowTime",
					                     SqlDbType.Bit,
					                     groupBy.IsShowTime,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "IsOnlinePayment",
					                     SqlDbType.Bit,
					                     groupBy.IsOnlinePayment,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "SoldOfReality",
					                     SqlDbType.Int,
					                     groupBy.SoldOfReality,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "SoldOfVirtual",
					                     SqlDbType.Int,
					                     groupBy.SoldOfVirtual,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "PageView",
					                     SqlDbType.Int,
					                     groupBy.PageView,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "Status",
					                     SqlDbType.Int,
					                     groupBy.Status,
					                     ParameterDirection.Input),
				                     this.SqlServer.CreateSqlParameter(
					                     "CreateTime",
					                     SqlDbType.DateTime,
					                     groupBy.CreateTime,
					                     ParameterDirection.Input),
				                  
			                     };

            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Channel_GroupBuy_Update", parameters, null);
        }


        public int UpdateStatus(int productId, int status)
        {
            var parameters = new List<SqlParameter>
				                 {
					                 this.SqlServer.CreateSqlParameter(
						                 "ProductID",
						                 SqlDbType.Int,
						                 productId,
						                 ParameterDirection.Input),
					                 this.SqlServer.CreateSqlParameter(
						                 "Status",
						                 SqlDbType.Int,
						                 status,
						                 ParameterDirection.Input)
				                 };
            return this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Channel_GroupBuy_Status_Update",
                parameters,
                null);
        }


        public int UpdateImg(int productId, string imgUrl)
        {
            var parameters = new List<SqlParameter>
				                 {
					                 this.SqlServer.CreateSqlParameter(
						                 "ProductID",
						                 SqlDbType.Int,
						                 productId,
						                 ParameterDirection.Input),
					                 this.SqlServer.CreateSqlParameter(
						                 "ImageUrl",
						                 SqlDbType.NVarChar,
						                 imgUrl,
						                 ParameterDirection.Input)
				                 };
            return this._sqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Channel_GroupBuy_Img_Update",
                parameters,
                null);
        }


        public Product SelectProductById(int id)
        {
            var parameters = new List<SqlParameter>()
				                 {
					                 this._sqlServer.CreateSqlParameter(
						                 "ProductID",
						                 SqlDbType.Int,
						                 id,
						                 ParameterDirection.Input)
				                 };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Picture_SelectByProductID", parameters, null);
            var list = dataReader.ToList<Product>();
            var product = new Product();
            if (list != null)
            {
                product = list.FirstOrDefault();
            }

            return product;
        }


        public int DeleteGrouBuyProductId(int productId)
        {
            var parameters = new List<SqlParameter>()
				                 {
					                 this.SqlServer.CreateSqlParameter(
						                 "ProductID",
						                 SqlDbType.Int,
						                 productId,
						                 ParameterDirection.Input)
				                 };
            var dataId = this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Channel_GroupBuy_DeleteRow",
                parameters,
                null);
            return dataId;
        }
    }
}
