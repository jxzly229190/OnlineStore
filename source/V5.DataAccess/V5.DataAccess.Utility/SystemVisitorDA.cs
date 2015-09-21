using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V5.DataAccess.Utility;
using V5.DataContract.Utility;
using V5.Library.Storage.DB;

namespace V5.DataAccess.Utility
{
    public class SystemVisitorDA : ISystemVisitorDA
    {
        #region 数据访问对象
        private SqlServer _sqlServer;

        public SqlServer SqlServer
        {
            get { return this._sqlServer ?? (this._sqlServer = new SqlServer()); }
        }
        #endregion
        /// <summary>
        /// 获取系 统访问人数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns>
        public List<DataContract.Utility.System_Visitor> Query(DateTime startTime, DateTime endTime, string condition)
        {
            var parameter = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter("@starttime", SqlDbType.DateTime, startTime, ParameterDirection.Input),
                this.SqlServer.CreateSqlParameter("@endtime", SqlDbType.DateTime, endTime, ParameterDirection.Input),
                this.SqlServer.CreateSqlParameter("@condition", SqlDbType.NVarChar, condition, ParameterDirection.Input)
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "[sp_System_Visitor_Count]",
                parameter, null);
            var list = dataReader.ToList<System_Visitor>();
            if (list.Count > 0)
            {
                return list;

            }
            return list;
        }
        /// <summary>
        /// 查询系统访问人数
        /// </summary>
        /// <returns></returns>
        public int QueryPV()
        {
            object result = this.SqlServer.ExecuteScalar(CommandType.Text, "select count(id) from System_Visitor", null, null);
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 添加系统访问记录
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public int Insert(System_Visitor visitor)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "SessionID",
                    SqlDbType.NVarChar,
                    visitor.SessionID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "UserName",
                    SqlDbType.NVarChar,
                    visitor.UserName,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "IP4Address",
                    SqlDbType.NVarChar,
                    visitor.IP4Address,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "EndTime",
                    SqlDbType.DateTime,
                    visitor.EndTime,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ReferenceID",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_visitor_InsertUser", parameters, null);
        }

        /// <summary>
        /// 修改系统访问记录
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public int Update(System_Visitor visitor)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "SessionID",
                    SqlDbType.NVarChar,
                    visitor.SessionID,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Visitor_Update", parameters, null);
        }
    }
}
