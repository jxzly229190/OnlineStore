// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigPaymentOrganizationDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付机构 数据访问类
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
    /// 支付机构 数据访问类
    /// </summary>
    public class ConfigPaymentOrganizationDA:IConfigPaymentOrganizationDA
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
        /// 查询所有支付机构列表
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Payment_Organization> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Organization_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Payment_Organization>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigPaymentOrganization - SelectAll", ex);
            }
            return null;
        }

        /// <summary>
        /// 根据Id查询支付机构（暂不支持，没有实现）
        /// </summary>
        /// <param name="Id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public List<Config_Payment_Organization> SelectById(int Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增支付机构
        /// </summary>
        /// <param name="paymentOrganization">
        /// 要新增的支付机构
        /// </param>
        /// <returns>
        /// 新增的支付机构Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Insert(Config_Payment_Organization paymentOrganization)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       paymentOrganization.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "PaymentTypeID",
                                       SqlDbType.Int,
                                       paymentOrganization.PaymentTypeID,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "URL",
                                       SqlDbType.VarChar,
                                       paymentOrganization.URL,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ImageURL",
                                       SqlDbType.VarChar,
                                       paymentOrganization.ImageURL,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Sorting",
                                       SqlDbType.Int,
                                       paymentOrganization.Sorting,
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
                    "sp_Config_Payment_Organization_Insert",
                    paraList,
                    null);

                return (int)paraList.Find(p => p.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {

                throw new Exception("Exception - ConfigPaymentOrganization - Insert", exception);
            }
        }

        /// <summary>
        /// 更新支付机构
        /// </summary>
        /// <param name="paymentOrganization">
        /// 要更新的支付机构
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Payment_Organization paymentOrganization)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.NVarChar,
                                       paymentOrganization.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "PaymentTypeID",
                                       SqlDbType.Int,
                                       paymentOrganization.PaymentTypeID,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "URL",
                                       SqlDbType.VarChar,
                                       paymentOrganization.URL,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ImageURL",
                                       SqlDbType.VarChar,
                                       paymentOrganization.ImageURL,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "Sorting",
                                       SqlDbType.Int,
                                       paymentOrganization.Sorting,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ID",
                                       SqlDbType.Int,
                                       paymentOrganization.ID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Organization_Update",
                    paraList,
                    null);

            }
            catch (Exception exception)
            {

                throw new Exception("Exception - ConfigPaymentOrganization - Update", exception);
            }
        }

        /// <summary>
        /// 删除支付机构记录
        /// </summary>
        /// <param name="Id">
        /// 要删除的支付机构Id
        /// </param>
        /// <returns>
        /// 已删除的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int Id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "[sp_Config_Payment_Organization_DeleteRow]",
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
                throw new Exception("Exception - ConfigPaymentOrganization - Delete", exception);
            }
        }

        /// <summary>
        /// 更加支付类别Id查询支付机构列表
        /// </summary>
        /// <param name="paymentTypeId">
        /// 支付类别Id
        /// </param>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Config_Payment_Organization> SelectByPaymentTypeId(int paymentTypeId)
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Organization_SelectByTypeID",
                    new List<SqlParameter>()
                        {
                            this.SqlServer.CreateSqlParameter(
                                "PaymentTypeID",
                                SqlDbType.Int,
                                paymentTypeId,
                                ParameterDirection.Input)
                        },
                    null);

                var list = dataReader.ToList<Config_Payment_Organization>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigPaymentOrganization - SelectAll", ex);
            }
            return null;
        }

        #endregion
    }
}