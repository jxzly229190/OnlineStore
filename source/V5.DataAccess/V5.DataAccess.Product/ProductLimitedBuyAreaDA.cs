using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V5.DataContract.Product;
using V5.Library.Storage.DB;

namespace V5.DataAccess.Product
{
    public class ProductLimitedBuyAreaDA : IProductLimitedBuyAreaDA
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Product_LimitedBuy_Area model)
        {
            var parameters = new List<SqlParameter>()
            {
                this.SqlServer.CreateSqlParameter(
                    "@ProductID",
                    SqlDbType.Int,
                    model.ProductID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@AreaID",
                    SqlDbType.NVarChar,
                    model.AreaID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@AreaType",
                    SqlDbType.Int,
                    model.AreaType,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@CreateTime",
                    SqlDbType.DateTime,
                    DateTime.Now,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@IsDelete",
                    SqlDbType.Int,
                    0,
                    ParameterDirection.Input
                    ),
                    this.SqlServer.CreateSqlParameter(
                    "@ReferenceID",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output
                    )
            };
            var SqlStr = new StringBuilder();
            SqlStr.Append(
                "insert into Product_LimitedBuy_Area (ProductID,AreaID,AreaType,CreateTime,IsDelete) values(@ProductID,@AreaID,@AreaType,@CreateTime,@IsDelete)");
            SqlStr.Append("select @ReferenceID=@@IDENTITY");
            this.SqlServer.ExecuteNonQuery(CommandType.Text, SqlStr.ToString(), parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "@ReferenceID").Value;
        }

        /// <summary>
        /// 根据productId查旬记录
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Product_LimitedBuy_Area> SelectByProductId(int productId)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@ProductId",
                    SqlDbType.Int,
                    productId,
                    ParameterDirection.Input
                    )
            };
            string sqlStr = "select [ID],[ProductID],[AreaID],[AreaType],[CreateTime],[IsDelete] from [Product_LimitedBuy_Area] where [ProductID]=@ProductId";
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.Text, sqlStr, parameters, null);
            var list = dataReader.ToList<Product_LimitedBuy_Area>();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int UpdateByProductId(string area, int productId)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "@ProductId",
                    SqlDbType.Int,
                    productId,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "@AreaID",
                    SqlDbType.NVarChar,
                    area,
                    ParameterDirection.Input
                    )
            };
            string sqlStr = "Update [Product_LimitedBuy_Area] set AreaID=@AreaID where [ProductID]=@ProductId";
            return this.SqlServer.ExecuteNonQuery(CommandType.Text, sqlStr, parameters, null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int BatchInsert(int[] id, string content)
        {
            return 0;
        }
    }
}