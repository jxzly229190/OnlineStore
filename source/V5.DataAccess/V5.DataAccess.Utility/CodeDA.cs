// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChannelGroupBuyDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ChannelGroupBuyDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace V5.DataAccess.Utility
{
    using System;
    using V5.DataContract.Utility;
    using V5.Library.Storage.DB;
    using V5.DataContract.Transact.Order;

    public class CodeDA : ICodeDA
    {
        #region  Constants and Fields
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
        #region Public Methods and Operator
        /// <summary>
        /// 编码插入数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Insert(Code code)
        {
            if (code == null)
            {
                throw new ArgumentNullException("code");
            }
            var parameters = new List<SqlParameter>
                                 {
                             
                                   this.SqlServer.CreateSqlParameter(
                                   "UserCode",
                                    SqlDbType.NVarChar,
                                     code.UserCode,
                                    ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "Business",
                                   SqlDbType.NVarChar,
                                   code.Business,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "PrefixName",
                                   SqlDbType.NVarChar,
                                   code.PrefixName,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "DateFormat",
                                   SqlDbType.NVarChar,
                                   code.DateFormat,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "TransactLength",
                                   SqlDbType.Int,
                                   code.TransactLength,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "Transaction",
                                   SqlDbType.NVarChar,
                                   code.Transaction,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "CodeFormat",
                                   SqlDbType.NVarChar,
                                   code.CodeFormat,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "IsIterator",
                                   SqlDbType.Bit,
                                   code.IsIterator,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "Iterator",
                                   SqlDbType.Int,
                                   code.Iterator,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "StartTime",
                                   SqlDbType.Int,
                                   code.StartTime,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "UserIterator",
                                   SqlDbType.Int,
                                   code.UserIterator,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "ExpireDate",
                                   SqlDbType.Int,
                                   code.ExpireDate,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
					                     "ReferenceID",
					                     SqlDbType.Int,
					                     null,
					                     ParameterDirection.Output)
                                  };
            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Code_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;

        }

        public List<Code> FindById(int id)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Code_SelectRow", parameters, null);
            var list = dataReader.ToList<Code>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }

        public int UpdateIterator(int iterator, int id)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Iterator",
                    SqlDbType.Int,
                    iterator,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Code_Iterator_Update", parameters, null);
        }

        public int UpdateStartTime(DateTime time, int id, int iterator)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "StartTime",
                    SqlDbType.DateTime,
                    time,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "iterator",
                    SqlDbType.Int,
                    iterator,
                    ParameterDirection.Input
                    )

            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Code_StartTime_Update", parameters, null);
        }

        public int UpdateUserIterator(int useriterator, int id)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "UserIterator",
                    SqlDbType.Int,
                    useriterator,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Code_UserIterator_Update", parameters, null);
        }
        /// <summary>
        /// 查找用户编码
        /// </summary>
        /// <returns></returns>
        public List<Code> FindByUserCode(string userCode)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "UserCode",
                    SqlDbType.VarChar,
                    userCode,
                    ParameterDirection.Input
                    )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Code_UserCode_SelectRow", parameters, null);
            var list = dataReader.ToList<Code>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }
        #endregion


        public int Update(Code code)
        {
            var parameters = new List<SqlParameter>
                                 {
                                    this.SqlServer.CreateSqlParameter(
                                   "ID",
                                   SqlDbType.NVarChar,
                                   code.ID,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "Business",
                                   SqlDbType.NVarChar,
                                   code.Business,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "PrefixName",
                                   SqlDbType.NVarChar,
                                   code.PrefixName,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "DateFormat",
                                   SqlDbType.NVarChar,
                                   code.DateFormat,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "TransactLength",
                                   SqlDbType.Int,
                                   code.TransactLength,
                                   ParameterDirection.Input
                                   ),
                                   this.SqlServer.CreateSqlParameter(
                                   "Transaction",
                                   SqlDbType.NVarChar,
                                   code.Transaction,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "CodeFormat",
                                   SqlDbType.NVarChar,
                                   code.CodeFormat,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "IsIterator",
                                   SqlDbType.Bit,
                                   code.IsIterator,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "Iterator",
                                   SqlDbType.Int,
                                   code.Iterator,
                                   ParameterDirection.Input
                                   ),
                                    this.SqlServer.CreateSqlParameter(
                                   "ExpireDate",
                                   SqlDbType.Int,
                                   code.ExpireDate,
                                   ParameterDirection.Input
                                   )
                                  };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Code_Update", parameters, null);
            //return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
        }


        public Order_Code GetOrderCount()
        {
            int orderCount = 0;
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "[sp_GetOrderCount]", null, null);
            if (dataReader.Read())
            {
                var code = new Order_Code
                {
                    OrderCode = Convert.ToInt32(dataReader["OrderCount"]),
                    CreateTime = Convert.ToDateTime(dataReader["CreateTime"])
                };

                return code;
            }

            return null;
        }
    }

}
