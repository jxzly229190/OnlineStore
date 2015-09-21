// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCostDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   快递运费 数据访问类
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

    public class ConfigDeliveryCostDA : IConfigDeliveryCostDA
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

        #region Public Method and Operator
        /// <summary>
        /// 查询所有配送费用列表
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<DataContract.Configuration.Config_Delivery_Cost> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Cost_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Delivery_Cost>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// 新增一条运费
        /// </summary>
        /// <param name="deliveryCost">
        /// 运费对象
        /// </param>
        /// <returns>
        /// 新增对象的ID
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Delivery_Cost deliveryCost)
        {
            var parameters = new List<SqlParameter>()
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "DeliveryCorporationID",
                                         SqlDbType.Int,
                                         deliveryCost.DeliveryCorporationID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CityID",
                                         SqlDbType.Int,
                                         deliveryCost.CityID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Duration",
                                         SqlDbType.Int,
                                         deliveryCost.Duration,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Cost",
                                         SqlDbType.Float,
                                         deliveryCost.Cost,
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
                    "sp_Config_Delivery_Cost_Insert",
                    parameters,
                    null);
                return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryCostDA - Insert", exception);
            }
        }

        /// <summary>
        /// 删除一条运费
        /// </summary>
        /// <param name="Id">
        /// 删除对象的ID
        /// </param>
        /// <returns>
        /// 删除的ID
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int Id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Cost_DeleteRow",
                    new List<SqlParameter>()
                        {
                            this.SqlServer.CreateSqlParameter(
                                "ID",
                                SqlDbType.Int,
                                Id,
                                ParameterDirection.Input)
                        },
                    null);
                return Id;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryCostDA - Delete", exception);
            }
        }

        /// <summary>
        /// 更新一条运费对象
        /// </summary>
        /// <param name="deliveryCost">
        /// 运费对象
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Delivery_Cost deliveryCost)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         deliveryCost.ID,
                                         ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                         "DeliveryCorporationID",
                                         SqlDbType.Int,
                                         deliveryCost.DeliveryCorporationID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CityID",
                                         SqlDbType.Int,
                                         deliveryCost.CityID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Duration",
                                         SqlDbType.Int,
                                         deliveryCost.Duration,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Cost",
                                         SqlDbType.Float,
                                         deliveryCost.Cost,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         DateTime.Now,
                                         ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Cost_Update",
                    paraList,
                    null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryCostDA - Update", exception);
            }
        }
       
        /// <summary>
        /// 查询运费信息
        /// </summary>
        /// <param name="paging">
        /// 页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Delivery_Cost> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            try
            {
                paging.TableName = "Config_Delivery_Cost";
                return this.SqlServer.Paging<Config_Delivery_Cost>(paging, out pageCount, out totalCount, null);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 根据配送公司Id查询配送费用列表
        /// </summary>
        /// <param name="corporationId">
        /// 配送公司Id
        /// </param>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Delivery_Cost> SelectByCorporationId(int corporationId)
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Cost_SelectByCorId",
                    new List<SqlParameter>()
                        {
                            this.SqlServer.CreateSqlParameter(
                                "CorporationID",
                                SqlDbType.Int,
                                corporationId,
                                ParameterDirection.Input)
                        },
                    null);
                var list = dataReader.ToList<Config_Delivery_Cost>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigDeliveryCost - SelectByCorporationId", ex);
            }

            return null;
        } 


        #endregion
    }
}