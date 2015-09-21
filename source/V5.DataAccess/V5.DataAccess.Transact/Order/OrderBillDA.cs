// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderBillDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   购物结算.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.DataContract.Transact.ShoppingCart;
    using V5.Library;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 购物结算.
    /// </summary>
    public class OrderBillDA : IOrderBillDA
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
        /// 查询商品的促销结果(用于刷新缓存).
        /// </summary>
        /// <returns>
        /// 列表.
        /// </returns>
        public List<Product_Cache> SelectByAllProduct()
        {
            var ds = this.SqlServer.ExecuteDataAdapter(
                CommandType.StoredProcedure,
                "[sp_Product_SelectByProductCache]",
                null,
                null);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            var list = ds.Tables[0].ToListNew<Product_Cache>();

            if (list != null && list.Count > 0)
            {
                foreach (var p in list)
                {
                    var parameters = new List<SqlParameter>
                                         {
                                             this.SqlServer.CreateSqlParameter(
                                                 "ProductID",
                                                 SqlDbType.Int,
                                                 p.ProductID,
                                                 ParameterDirection.Input)
                                         };
                    var dataReader = this.SqlServer.ExecuteDataReader(
                        CommandType.StoredProcedure,
                        "sp_Product_Promote_SelectAll",
                        parameters,
                        null);
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            p.PromotePrice =
                                Convert.ToDouble(
                                    DBNull.Value == dataReader["PromotePrice"] ? 0 : dataReader["PromotePrice"]);
                            p.PromoteIDs = Utils.ToString(dataReader["PromoteIDs"]);
                            p.PromoteTypes = Utils.ToString(dataReader["PromoteTypes"]);
                            p.PromoteNames = Utils.ToString(dataReader["PromoteNames"]);
                        }
                    }
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }

            return list;
        }

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Product_Cache SelectByProduct(int productID)
        {
            var ds = this.SqlServer.ExecuteDataAdapter(
                CommandType.Text,
                "select * from view_product_cache where productId=" + Utils.ToString(productID),
                null,
                null);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            var list = ds.Tables[0].ToListNew<Product_Cache>();
            if (list != null && list.Count > 0)
            {
                var p = list[0];
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
                    "sp_Product_Promote_SelectAll",
                    parameters,
                    null);
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        p.PromotePrice =
                            Convert.ToDouble(
                                DBNull.Value == dataReader["PromotePrice"] ? 0 : dataReader["PromotePrice"]);
                        p.PromoteIDs = Utils.ToString(dataReader["PromoteIDs"]);
                        p.PromoteTypes = Utils.ToString(dataReader["PromoteTypes"]);
                        p.PromoteNames = Utils.ToString(dataReader["PromoteNames"]);
                    }
                }
                dataReader.Close();
                dataReader.Dispose();
                return p;
            }

            return null;
        }

        /// <summary>
        /// 查询商品的促销结果.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="quantity">
        /// The quantity.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Cart_Product SelectByProduct(int productID, int quantity, int userID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Quantity",
                                         SqlDbType.Int,
                                         quantity,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         userID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Product_Promote_Select",
                parameters,
                null);
            var list = dataReader.ToList<Cart_Product>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 验证商品是否参加促销.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="type">
        /// 1：单品促销（限时抢购，团购），2：多品促销（满额优惠、满件优惠）.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifyPromote(int productID, int type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 促销结果（限时抢购、团购）.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="Cart_Product"/>.
        /// </returns>
        public Cart_Product Select(int productID, int userID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         userID,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Product_Promote_Select",
                parameters,
                null);
            var list = dataReader.ToList<Cart_Product>();
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 查询购物车商品的满减金额.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="totalPrice">
        /// The total Price.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        public double SelectMeetMoneyCutMoney(int promoteID, double totalPrice)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteID",
                                         SqlDbType.Int,
                                         promoteID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteType",
                                         SqlDbType.VarChar,
                                         "MeetMoney",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalPrice",
                                         SqlDbType.Float,
                                         totalPrice,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalQuantity",
                                         SqlDbType.Int,
                                         0,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteScalar(
                CommandType.StoredProcedure,
                "sp_Product_Promote_SelectCutMoney",
                parameters,
                null);
            return (double)dataReader;
        }

        /// <summary>
        /// 查询购物车商品的满件折扣金额.
        /// </summary>
        /// <param name="promoteID">
        /// The promote id.
        /// </param>
        /// <param name="totalQuantity">
        /// The total Quantity.
        /// </param>
        /// <returns>
        /// The <see cref="Gift_Product"/>.
        /// </returns>
        public double SelectMeetAmountCutMoney(int promoteID, int totalQuantity)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteID",
                                         SqlDbType.Int,
                                         promoteID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "PromoteType",
                                         SqlDbType.VarChar,
                                         "MeetAmount",
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalPrice",
                                         SqlDbType.Float,
                                         0,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "TotalQuantity",
                                         SqlDbType.Int,
                                         totalQuantity,
                                         ParameterDirection.Input)
                                 };
            var dataReader = this.SqlServer.ExecuteScalar(
                CommandType.StoredProcedure,
                "sp_Product_Promote_SelectCutMoney",
                parameters,
                null);
            return (double)dataReader;
        }

        #endregion
    }
}
