using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using V5.DataContract.Advertise;
using V5.Library.Storage.DB;
using System;

namespace V5.DataAccess.Advertise
{
    public class AdvertiseConfigDA : IAdvertiseConfigDA
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

        #region Public Methods and Operators

        /// <summary>
        /// 根据Pid查询子节点
        /// </summary>
        /// <param name="pid">子节点标识</param>
        /// <returns></returns>
        public List<Advertise_Config> QueryPid(int pid)
        {
            var parameter = new List<SqlParameter>()
            {
                this.SqlServer.CreateSqlParameter("@pId", SqlDbType.Int, pid, ParameterDirection.Input)
            };
            var dataReader = this._sqlServer.ExecuteDataReader(CommandType.Text,
                "select *from [Advertise_Config] IsDelete=0 AND where [PID]=@pId", parameter, null);
            var list = dataReader.ToList<Advertise_Config>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advertise_Config QueryID(int id)
        {
            var parameters = new List<SqlParameter>{
            this.SqlServer.CreateSqlParameter(
            "@ID",
            SqlDbType.Int,
            id,ParameterDirection.Input
            )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.Text, "select *from Advertise_Config where IsDelete=0 AND ID=@ID", parameters, null);
            var list = dataReader.ToList<Advertise_Config>();
            if (list.Count > 0)
            {
                return list[0];
            }
            return list[0];
        }

        /// <summary>
        /// 查找所有节点
        /// </summary>
        /// <returns></returns>
        public List<Advertise_Config> QueryAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.Text, "select *from Advertise_Config Where IsDelete=0", null, null);
            var list = dataReader.ToList<Advertise_Config>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }

        /// <summary>
        /// 获取LP所有节点
        /// </summary>
        /// <returns></returns>
        public List<LandingPageTree> QueryLPTree()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure,
                "sp_TreelandingPage_SelectAll", null, null);
            var list = dataReader.ToList<LandingPageTree>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="count"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<Advertise_Config> Query(int count, string search)
        {
            var parameters = new List<SqlParameter>{
                this.SqlServer.CreateSqlParameter(
                    "count",
                    SqlDbType.Int,
                    count,
                    ParameterDirection.Input
                ),
                this.SqlServer.CreateSqlParameter(
                    "search",
                    SqlDbType.VarChar,
                    search,
                    ParameterDirection.Input
                )
            };

            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Advertise_Config_Select", parameters, null);
            var list = dataReader.ToList<Advertise_Config>();
            if (list.Count > 0)
            {
                return list;
            }
            return list;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Advertise_Config> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Advertise_Config>(paging, out pageCount, out totalCount, null);
        }
        
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int Insert(Advertise_Config config)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@PID",
                    SqlDbType.Int,
                    config.PID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Name",
                    SqlDbType.VarChar,
                    config.Name,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Source",
                    SqlDbType.NVarChar,
                    config.Source,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@URL",
                    SqlDbType.VarChar,
                    config.URL,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@ImagePath",
                    SqlDbType.NVarChar,
                    config.ImagePath,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Description",
                    SqlDbType.VarChar,
                    config.Description,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@CreateTime",
                    SqlDbType.DateTime,
                    config.CreateTime,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@IndexID",
                    SqlDbType.Int,
                    config.IndexID,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@Width",
                    SqlDbType.Int,
                    config.Width,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@Height",
                    SqlDbType.Int,
                    config.Height,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@ThumbnailImagePath",
                    SqlDbType.VarChar,
                    config.ThumbnailImagePath,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@ImageID",
                    SqlDbType.Int,
                    config.ImageID,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@BackgroundColor",
                    SqlDbType.VarChar,
                    config.BackgroundColor,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@isParent",
                    SqlDbType.Bit,
                    config.isParent,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@filter",
                    SqlDbType.Int,
                    config.filter,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@ReferenceID",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output)
            };
            var SQLStr = new StringBuilder();
            SQLStr.Append(
                "Insert Into Advertise_Config([PID],[Name] ,[Source],[URL],[ImagePath],[Description],[CreateTime],[IndexID],[Width],[Height],[ThumbnailImagePath],[ImageID],[BackgroundColor],[isParent],[filter] ) Values ");
            SQLStr.Append(
                "(@PID,@Name,@Source,@URL,@ImagePath,@Description,@CreateTime,@IndexID,@Width,@Height,@ThumbnailImagePath,@ImageID,@BackgroundColor,@isParent,@filter) ");
            SQLStr.Append("Select @ReferenceID = @@IDENTITY ");
            SQLStr.Append(" update Advertise_Config set IsOrder=@ReferenceID,Code=@ReferenceID where ID=@ReferenceID");
            this.SqlServer.ExecuteNonQuery(CommandType.Text, SQLStr.ToString(), parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "@ReferenceID").Value;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Advertise_Config model)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@ID",
                    SqlDbType.Int,
                    model.ID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Name",
                    SqlDbType.VarChar,
                    model.Name,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Source",
                    SqlDbType.NVarChar,
                    model.Source,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@URL",
                    SqlDbType.VarChar,
                    model.URL,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@ImagePath",
                    SqlDbType.NVarChar,
                    model.ImagePath,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@Description",
                    SqlDbType.VarChar,
                    model.Description,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@IndexID",
                    SqlDbType.Int,
                    model.IndexID,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@Width",
                    SqlDbType.Int,
                    model.Width,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@Height",
                    SqlDbType.Int,
                    model.Height,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@ThumbnailImagePath",
                    SqlDbType.VarChar,
                    model.ThumbnailImagePath,
                    ParameterDirection.Input
                    ),
               this.SqlServer.CreateSqlParameter(
                    "@ImageID",
                    SqlDbType.Int,
                    model.ImageID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@BackgroundColor",
                    SqlDbType.VarChar,
                    model.BackgroundColor,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@isParent",
                    SqlDbType.Bit,
                    model.isParent,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@filter",
                    SqlDbType.Int,
                    model.filter,
                    ParameterDirection.Input
                    )
            };
            string sqlString =
                "Update Advertise_Config Set [Name]=@Name,[Source]=@Source,[URL]=@URL,[ImagePath]=@ImagePath,[Description]=@Description,[IndexID] = @IndexID,[Width] = @Width,[Height] = @Height,[ThumbnailImagePath] = @ThumbnailImagePath,[ImageID] = @ImageID ,[BackgroundColor] = @BackgroundColor,[isParent]=@isParent,[filter]=@filter Where ID=@ID";
            return this.SqlServer.ExecuteNonQuery(CommandType.Text, sqlString, parameters, null);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int BatchInsert(List<Advertise_Config> list, SqlTransaction transaction)
        {
            var dt = this.BuildAdvertiseTable(list);
            var parameters = new List<SqlParameter>{
            new SqlParameter("TVP",SqlDbType.Structured)
            {
                TypeName="AdvertiseType",
                Value=dt,
                Direction=ParameterDirection.Input
            },
            this.SqlServer.CreateSqlParameter(
            "RowCount",
            SqlDbType.Int,
            null,
            ParameterDirection.Output
            )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_AdvertiseConfig_BatchInsert]", parameters, transaction);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int BatchInsert(List<Advertise_Config> list)
        {
            var builderStringSql = new StringBuilder();
            var parameters = new List<SqlParameter>();
            int i = 0;
            foreach (var item in list)
            {

                string sqlString =
                    "Insert Into Advertise_Config([PID],[Name] ,[Source],[URL],[ImagePath],[Description],[CreateTime],[IndexID],[Width],[Height],[ThumbnailImagePath],[ImageID],[BackgroundColor],[isParent] ) Values (@PID" +
                    i + ",@Name" + i + ",@Source" + i + ",@URL" + i + ",@ImagePath" + i + ",@Description" + i +
                    ",@CreateTime" + i + ",@IndexID" + i + ",@Width" + i + ",@Height" + i + ",@ThumbnailImagePath" + i +
                    ",@ImageID" + i + ",@BackgroundColor" + i + ",@isParent" + i + ") ";
                parameters.AddRange(new List<SqlParameter>
                {
                    this.SqlServer.CreateSqlParameter(
                        "@PID" + i + "",
                        SqlDbType.Int,
                        item.PID,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@Name" + i + "",
                        SqlDbType.VarChar,
                        item.Name,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@Source" + i + "",
                        SqlDbType.NVarChar,
                        item.Source,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@URL" + i + "",
                        SqlDbType.VarChar,
                        item.URL,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@ImagePath" + i + "",
                        SqlDbType.NVarChar,
                        item.ImagePath,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@Description" + i + "",
                        SqlDbType.VarChar,
                        item.Description,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@CreateTime" + i + "",
                        SqlDbType.DateTime,
                        item.CreateTime,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@IndexID" + i + "",
                        SqlDbType.Int,
                        item.IndexID,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@Width" + i + "",
                        SqlDbType.Int,
                        item.Width,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@Height" + i + "",
                        SqlDbType.Int,
                        item.Height,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@ThumbnailImagePath" + i + "",
                        SqlDbType.VarChar,
                        item.ThumbnailImagePath,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@ImageID" + i + "",
                        SqlDbType.Int,
                        item.ImageID,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@BackgroundColor" + i + "",
                        SqlDbType.VarChar,
                        item.BackgroundColor,
                        ParameterDirection.Input
                        ),
                    this.SqlServer.CreateSqlParameter(
                        "@isParent" + i + "",
                        SqlDbType.Bit,
                        item.isParent,
                        ParameterDirection.Input
                        )
                }
                    );
                i++;
                builderStringSql.Append(sqlString);
            }
            return this.SqlServer.ExecuteNonQuery(CommandType.Text, builderStringSql.ToString(), parameters, null);

        }
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_Advertise_Config_DeleteRow]", parameters, null);
        }

        /// <summary>
        /// 广告配置表，批量添加数据的的类型，表值参数
        /// </summary>
        /// <returns></returns>
        private DataTable GetAdvertiseConfigTableSchema()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[]{
            new DataColumn("PID",typeof(int)),
            new DataColumn("Name",typeof(string)),
            new DataColumn("Source",typeof(string)),
            new DataColumn("URL",typeof(string))
            }
                );
            return dt;
        }
        
        /// <summary>
        /// 创建与概要表的对应数据的参数
        /// </summary>
        /// <param name="enumeconfig"></param>
        /// <returns></returns>
        private DataTable BuildAdvertiseTable(IEnumerable<Advertise_Config> enumeconfig)
        {
            DataTable dt = this.GetAdvertiseConfigTableSchema();
            foreach (var config in enumeconfig)
            {
                DataRow r = dt.NewRow();
                r[0] = config.ID;
                r[1] = config.Name;
                r[2] = config.Source;
                r[3] = config.URL;
                dt.Rows.Add(r);
            }
            return dt;
        }

        /// <summary>
        /// 修改排字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public int UpdateIsOrder(int id, int pid)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@PID",
                    SqlDbType.Int,
                    pid,
                    ParameterDirection.Input
                    )
            };

            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Advertise_Config_Up", parameters, null);            
            //string strSql = "Update Advertise_Config set IsOrder=@IsOrder where ID=@ID";
            //return this.SqlServer.ExecuteNonQuery(CommandType.Text, strSql, parameters,
            //     null);
        }

        /// <summary>
        /// 更新删除标识
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        public void UpdateFilter(int id, int filter)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@ID",
                    SqlDbType.Int,
                    id,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@filter",
                    SqlDbType.Int,
                    filter,
                    ParameterDirection.Input
                    )
            };
            const string SqlStr = "Update Advertise_Config set filter=@filter where ID=@ID Update Advertise_Config set filter=@filter where PID=@ID";
            this.SqlServer.ExecuteNonQuery(CommandType.Text, SqlStr, parameters, null);
        }

        #endregion
    }
}
