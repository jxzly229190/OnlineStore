// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCorporationDAL.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   配送方式 数据库访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Configuration;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 配送方式 数据库访问类
    /// </summary>
    public class ConfigDeliveryMethodDA:IConfigDeliveryMethodDA
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
        /// 查询所有配送方式
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Delivery_Method> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Method_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Delivery_Method>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigDeliveryMethodDA - SelectAll", ex);
            }

            return null;
        }

        /// <summary>
        /// 新增配送方式
        /// </summary>
        /// <param name="deliveryMethod">
        /// 要新增的配送方式
        /// </param>
        /// <returns>
        /// 新增的配送方式Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Delivery_Method deliveryMethod)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       deliveryMethod.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Description",
                                       SqlDbType.NVarChar,
                                       deliveryMethod.Description,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ReferenceID",
                                       SqlDbType.Int,
                                       null,
                                       ParameterDirection.Output)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                                   CommandType.StoredProcedure,
                                   "sp_Config_Delivery_Method_Insert",
                                   paraList,
                                   null);
                return (int)paraList.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryMethodDA - Insert", exception);
            }
        }

        /// <summary>
        /// 删除配送方式
        /// </summary>
        /// <param name="id">
        /// 要删除的Id
        /// </param>
        /// <returns>
        /// 已删除的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Method_DeleteRow",
                    new List<SqlParameter>()
                        {
                            this.SqlServer.CreateSqlParameter(
                                "ID",
                                SqlDbType.Int,
                                id,
                                ParameterDirection.Input)
                        },
                    null);
                return id;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryMethodDA - Delete", exception);
            }
        }

        /// <summary>
        /// 更新配送方式
        /// </summary>
        /// <param name="deliveryMethod">
        /// 更新的配送方式
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Delivery_Method deliveryMethod)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       deliveryMethod.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Description",
                                       SqlDbType.NVarChar,
                                       deliveryMethod.Description,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                       this.SqlServer.CreateSqlParameter(
                                       "ID",
                                       SqlDbType.Int,
                                       deliveryMethod.ID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                                   CommandType.StoredProcedure,
                                   "sp_Config_Delivery_Method_Update",
                                   paraList,
                                   null);

            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryMethodDA - Update", exception);
            }
        } 
        #endregion
    }
}