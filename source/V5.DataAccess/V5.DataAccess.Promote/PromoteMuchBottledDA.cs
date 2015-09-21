// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMuchBottledDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销活动数据访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 多瓶装促销活动数据访问类.
    /// </summary>
    public class PromoteMuchBottledDA : IPromoteMuchBottledDA
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
        /// 添加多瓶装促销活动.
        /// </summary>
        /// <param name="promoteMuchBottled">
        /// Promote_MuchBottled的对象实例.
        /// </param>
        /// <returns>
        /// 多瓶装编号.
        /// </returns>
        public int Insert(Promote_MuchBottled promoteMuchBottled)
        {
            if (promoteMuchBottled == null)
            {
                throw new ArgumentNullException("promoteMuchBottled");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMuchBottled.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         promoteMuchBottled.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ProductID",
                                         SqlDbType.Int,
                                         promoteMuchBottled.ProductID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsOnlinePayment",
                                         SqlDbType.Bit,
                                         promoteMuchBottled.IsOnlinePayment,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "StartTime",
                                         SqlDbType.DateTime,
                                         promoteMuchBottled.StartTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMuchBottled.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDisplayTime",
                                         SqlDbType.Bit,
                                         promoteMuchBottled.IsDisplayTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         promoteMuchBottled.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_Insert",
                parameters,
                null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }

        /// <summary>
        /// 更新多瓶装促销.
        /// </summary>
        /// <param name="promoteMuchBottled">
        /// Promote_MuchBottled的对象实例.
        /// </param>
        public void Update(Promote_MuchBottled promoteMuchBottled)
        {
            if (promoteMuchBottled == null)
            {
                throw new ArgumentNullException("promoteMuchBottled");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         promoteMuchBottled.ID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EmployeeID",
                                         SqlDbType.Int,
                                         promoteMuchBottled.EmployeeID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsOnlinePayment",
                                         SqlDbType.Bit,
                                         promoteMuchBottled.IsOnlinePayment,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "EndTime",
                                         SqlDbType.DateTime,
                                         promoteMuchBottled.EndTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "IsDisplayTime",
                                         SqlDbType.Bit,
                                         promoteMuchBottled.IsDisplayTime,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_Update",
                parameters,
                null);
        }

        /// <summary>
        /// 查询多瓶装促销列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 多瓶装列表
        /// </returns>
        public List<Promote_MuchBottled> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Promote_MuchBottled>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 查看指定商品是否已参加多瓶装促销.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// true :已参加，false：未参加.
        /// </returns>
        public bool SelectByProductID(int productID)
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

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_SelectByProductID",
                parameters,
                null);
            return dataReader.HasRows;
        }

        /// <summary>
        /// 查询指定编号的多瓶装促销.
        /// </summary>
        /// <param name="id">
        /// 多瓶装促销编号.
        /// </param>
        /// <returns>
        /// Promote_MuchBottled的对象实例.
        /// </returns>
        public Promote_MuchBottled SelectByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         id,
                                         ParameterDirection.Input)
                                 };

            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_Promote_MuchBottled_SelectRow",
                parameters,
                null);
            var list = dataReader.ToList<Promote_MuchBottled>();
            return list.Count > 0 ? list[0] : null;
        }

        #endregion
    }
}
