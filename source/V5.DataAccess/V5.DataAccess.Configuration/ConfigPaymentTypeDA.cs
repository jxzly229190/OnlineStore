// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigPaymentTypeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付类别数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Configuration;
    using global::System.Data;

    using V5.Library.Storage.DB;

    /// <summary>
    /// 支付类别数据访问类
    /// </summary>
    public class ConfigPaymentTypeDA : IConfigPaymentTypeDA
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
        /// 查询所有支付类别
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Payment_Type> SelectAll()
        {
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Type_SelectAll",
                    null,
                    null);
                var list = dataReader.ToList<Config_Payment_Type>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception - ConfigPaymentType - SelectAll", ex);
            }

            return null;
        }

        /// <summary>
        /// 根据Id查询支付类别（暂不支持）
        /// </summary>
        /// <param name="Id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Config_Payment_Type> SelectById(int Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增一条支付类别
        /// </summary>
        /// <param name="paymentMethod">
        /// 支付类别
        /// </param>
        /// <returns>
        /// 新增的Id
        /// </returns>
        public int Insert(Config_Payment_Type paymentMethod)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.VarChar,
                                       paymentMethod.Name,
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
                                       ParameterDirection.Output),
                                   this.SqlServer.CreateSqlParameter(
                                       "PaymentMethodID",
                                       SqlDbType.Int,
                                       paymentMethod.PaymentMethodID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Type_Insert",
                    paraList,
                    null);

                return (int)paraList.Find(p => p.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigPaymentType - Insert", exception);
            }
        }

        /// <summary>
        /// 更新一条支付类别
        /// </summary>
        /// <param name="paymentMethod">
        /// 支付类别
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Update(Config_Payment_Type paymentMethod)
        {
            var paraList = new List<SqlParameter>()
                               {
                                   this.SqlServer.CreateSqlParameter(
                                       "Name",
                                       SqlDbType.VarChar,
                                       paymentMethod.Name,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "CreateTime",
                                       SqlDbType.DateTime,
                                       DateTime.Now,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "ID",
                                       SqlDbType.Int,
                                       paymentMethod.ID,
                                       ParameterDirection.Input),
                                   this.SqlServer.CreateSqlParameter(
                                       "PaymentMethodID",
                                       SqlDbType.Int,
                                       paymentMethod.PaymentMethodID,
                                       ParameterDirection.Input)
                               };

            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "sp_Config_Payment_Type_Update",
                    paraList,
                    null);

            }
            catch (Exception exception)
            {
                throw new Exception("Exception - ConfigPaymentType - Update", exception);
            }
        }

        /// <summary>
        /// 删除支付类别记录
        /// </summary>
        /// <param name="Id">
        /// 要删除的类别Id
        /// </param>
        /// <returns>
        /// 删除的Id
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int Delete(int Id)
        {
            try
            {
                this.SqlServer.ExecuteNonQuery(
                    CommandType.StoredProcedure,
                    "[sp_Config_Payment_Type_DeleteRow]",
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
                throw new Exception("Exception - ConfigPaymentType - Delete", exception);
            }
        }

        #endregion
    }
}