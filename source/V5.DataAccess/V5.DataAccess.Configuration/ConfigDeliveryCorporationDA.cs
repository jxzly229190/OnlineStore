// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCorporationDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货公司访问类
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
    /// 送货公司数据访问类
    /// </summary>
    public class ConfigDeliveryCorporationDA:IConfigDeliveryCorporationDA
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
        /// 查询所有合作快递公司列表
        /// </summary>
        /// <returns>
        /// The  <see cref="List"/>.
        /// </returns>
        public List<Config_Delivery_Corporation> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Corporation_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Delivery_Corporation>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigDeliveryCorporation - SelectAll", ex);
            }

            return null;
        }

        /// <summary>
        /// 新增一条送货公司记录
        /// </summary>
        /// <param name="corporation">
        /// 送货公司记录
        /// </param>
        /// <returns>
        /// 新记录的ID
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Delivery_Corporation corporation)
        {
	        var parameters = new List<SqlParameter>()
		                         {
			                         this.SqlServer.CreateSqlParameter(
				                         "Name",
				                         SqlDbType.NVarChar,
				                         corporation.Name,
				                         ParameterDirection.Input),
			                         this.SqlServer.CreateSqlParameter(
				                         "URL",
				                         SqlDbType.VarChar,
				                         corporation.URL,
				                         ParameterDirection.Input),
			                         this.SqlServer.CreateSqlParameter(
				                         "Description",
				                         SqlDbType.NVarChar,
				                         corporation.Description,
				                         ParameterDirection.Input),
			                         this.SqlServer.CreateSqlParameter(
				                         "Tel",
				                         SqlDbType.NVarChar,
				                         corporation.Tel,
				                         ParameterDirection.Input),
			                         this.SqlServer.CreateSqlParameter(
				                         "Number",
				                         SqlDbType.NVarChar,
				                         corporation.Number,
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
                    "sp_Config_Delivery_Corporation_Insert",
                    parameters,
                    null);
                return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryCorporationDA - Insert", exception);
            }
        }

        /// <summary>
        /// 删除一条送货公司记录
        /// </summary>
        /// <param name="id">
        /// 公司编码
        /// </param>
        /// <returns>
        /// 已经删除的ID
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Corporation_DeleteRow",
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
                throw new Exception("Exception - ConfigDeliveryCorporationDA - Delete", exception);
            }
        }

        /// <summary>
        /// 更新送货公司记录
        /// </summary>
        /// <param name="corporation">
        /// 要更新的记录
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Delivery_Corporation corporation)
        {
	        var paraList = new List<SqlParameter>()
		                       {
			                       this.SqlServer.CreateSqlParameter(
				                       "ID",
				                       SqlDbType.Int,
				                       corporation.ID,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "Name",
				                       SqlDbType.NVarChar,
				                       corporation.Name,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "URL",
				                       SqlDbType.VarChar,
				                       corporation.URL,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "Tel",
				                       SqlDbType.NVarChar,
				                       corporation.Tel,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "Number",
				                       SqlDbType.NVarChar,
				                       corporation.Number,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "Description",
				                       SqlDbType.NVarChar,
				                       corporation.Description,
				                       ParameterDirection.Input),
			                       this.SqlServer.CreateSqlParameter(
				                       "CreateTime",
				                       SqlDbType.DateTime,
				                       DateTime.Now,
				                       ParameterDirection.Input),
		                       };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Delivery_Corporation_Update",
                    paraList,
                    null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigDeliveryCorporationDA - Delete", exception);
            }
        } 
        #endregion
    }
}