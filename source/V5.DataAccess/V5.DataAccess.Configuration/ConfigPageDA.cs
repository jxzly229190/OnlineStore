using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V5.DataContract.Configuration;
using V5.Library.Storage.DB;

namespace V5.DataAccess.Configuration
{
    public class ConfigPageDA : IConfigPageDA
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
        #region Method
        public List<Config_Page> Query(int type)
        {
            var parameters = new List<SqlParameter>
            {

                this.SqlServer.CreateSqlParameter(
                    "type",
                    SqlDbType.Int,
                    type,
                    ParameterDirection.Input
                    )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Config_Page_SelectAll",
                parameters, null);
            var list = dataReader.ToList<Config_Page>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }
        #endregion


        public Config_Page QueryByID(int id)
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
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "[sp_Config_Page_SelectByID]",
                parameters, null);
            var list = dataReader.ToList<Config_Page>();
            if (list.Count > 0)
            {
                return list[0];
            }
            return list[0];
        }

        /// <summary>
        /// 修改Content
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <param name="content">内容</param>
        /// <param name="name">标题</param>
        /// <returns></returns>
        public int UpdateContent(int id, string content, string name)
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
                "Content",
                SqlDbType.Text,
                content,
                ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "Name",
                    SqlDbType.VarChar,
                    name,
                    ParameterDirection.Input
                    )
            };
            try
            {
                return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_Config_Page_Update]", parameters,
                    null);
            }
            catch (Exception exception)
            {

                throw new Exception("Exception - ConfigPageDA - Update", exception);
            }


        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model">数据对象模型</param>
        /// <returns></returns>
        public int Insert(Config_Page model)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "PID",
                    SqlDbType.Int,
                    model.PID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Type",
                    SqlDbType.Int,
                    model.Type,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Name",
                    SqlDbType.VarChar,
                    model.Name,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Content",
                    SqlDbType.Text,
                    model.Content,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "Source",
                    SqlDbType.VarChar,
                    model.Source,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ReferenceID",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output)
            };
            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_Config_Page_Insert]", parameters, null);
                return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {

                throw new Exception("Exception - ConfigPageDA - Insert", exception);
            }

        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <param name="pageCount">页数</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public List<Config_Page> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Config_Page>(paging, out pageCount, out totalCount, null);
        }


        public int DeleteRow(int id)
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
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Config_Page_DeleteRow", parameters, null);
        }

        /// <summary>
        /// 修改链接操作
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int UpdateLink(Config_Page config)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "id",
                    SqlDbType.Int,
                    config.ID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "source",
                    SqlDbType.VarChar,
                    config.Source,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "name",
                    SqlDbType.NVarChar,
                    config.Name,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_ConfigPage_UpdateLink", parameters, null);
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
