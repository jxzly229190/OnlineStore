using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using V5.DataContract.Product;
using V5.Library.Storage.DB;

namespace V5.DataAccess.Product
{
    public class BrandInformationDA : IBrandInformationDA
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
        #region Public Method
        /// <summary>
        /// 执行添加操作
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int Insert(Brand_Information brand)
        {
            var parameters = new List<SqlParameter>
            {

                this.SqlServer.CreateSqlParameter(
                    "BrandID",
                    SqlDbType.Int,
                    brand.BrandID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Title",
                    SqlDbType.Text,
                    brand.Title,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Introduce",
                    SqlDbType.Text,
                    brand.Introduce,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Logo",
                    SqlDbType.VarChar,
                    brand.Logo,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ProductID",
                    SqlDbType.VarChar,
                    brand.ProductID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ReferenceID",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output
                    )
            };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BrandInformation_Insert", parameters, null);
            return (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;

        }
        /// <summary>
        /// 执行修改操作 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int Update(Brand_Information brand)
        {
            var parameters = new List<SqlParameter>
            {

                this.SqlServer.CreateSqlParameter(
                    "BrandID",
                    SqlDbType.Int,
                    brand.BrandID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Title",
                    SqlDbType.Text,
                    brand.Title,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Introduce",
                    SqlDbType.Text,
                    brand.Introduce,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Logo",
                    SqlDbType.VarChar,
                    brand.Logo,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ProductID",
                    SqlDbType.VarChar,
                    brand.ProductID,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ID",
                    SqlDbType.Int,
                    brand.ID,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BrandInformation_Update", parameters,
                null);
        }
        /// <summary>
        /// 执行批量添加操作 
        /// </summary>
        /// <param name="list">添加的列表</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public int InsertBatch(List<Brand_Information> list, SqlTransaction transaction)
        {
            var dt = this.BuildBrandDescriptionTable(list);
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("BrandType", SqlDbType.Structured)
                {
                    TypeName = "BrandInformation",
                    Value = dt,
                    Direction = ParameterDirection.Input
                },
                this.SqlServer.CreateSqlParameter(
                    "RowCount",
                    SqlDbType.Int,
                    null,
                    ParameterDirection.Output
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_BrandInformation_BatchInsert]", parameters, transaction);
        }
        #endregion
        #region private Method
        /// <summary>
        /// 表值参数，批量添加数据的类型
        /// </summary>
        /// <returns></returns>
        private DataTable GetBrandDescriptionSchema()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[]
            {
                new DataColumn("BrandID",typeof(int)),
                new DataColumn("Title",typeof(string)),
                new DataColumn("Introduce",typeof(string)),
                new DataColumn("Logo",typeof(string)), 
                new DataColumn("ProductID",typeof(string)),    
            });
            return dt;
        }
        /// <summary>
        /// 创建与概要表的对应数据的参数
        /// </summary>
        /// <param name="listEnumerable"></param>
        /// <returns></returns>
        private DataTable BuildBrandDescriptionTable(IEnumerable<Brand_Information> listEnumerable)
        {
            DataTable dt = this.GetBrandDescriptionSchema();
            foreach (var description in listEnumerable)
            {
                DataRow dr = dt.NewRow();
                dr[0] = description.BrandID;
                dr[1] = description.Title;
                dr[2] = description.Introduce;
                dr[3] = description.Logo;
                dr[4] = description.ProductID;
            }
            return dt;
        }
        #endregion

        /// <summary>
        /// 根据ID查数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Brand_Information QueryByID(int id)
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
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_BrandInformation_SelectByID", parameters, null);
            var list = dataReader.ToList<Brand_Information>();
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public Brand_Information QueryByBrandID(int brandId)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "BrandID",
                    SqlDbType.Int,
                    brandId,
                    ParameterDirection.Input
                    )
            };
            var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_BrandInformation_SelectByBrandID", parameters,
                  null);
            var list = dataReader.ToList<Brand_Information>();
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// 根据品牌ID执行修改操作 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int UpdateByBrandID(Brand_Information brand)
        {
            var parameters = new List<SqlParameter>
            {
            this.SqlServer.CreateSqlParameter(
            "BrandID",
            SqlDbType.Int,
            brand.BrandID,
            ParameterDirection.Input
            ),
            this.SqlServer.CreateSqlParameter(
            "Title",
            SqlDbType.Text,
            brand.Title,
            ParameterDirection.Input
            ),
            this.SqlServer.CreateSqlParameter(
            "Introduce",
            SqlDbType.Text,
            brand.Introduce,
            ParameterDirection.Input
            ),
            this.SqlServer.CreateSqlParameter(
            "ProductID",
            SqlDbType.VarChar,
            brand.ProductID,
            ParameterDirection.Input
            ),
            this.SqlServer.CreateSqlParameter(
            "logo",
            SqlDbType.VarChar,
            brand.Logo,
            ParameterDirection.Input
            )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BrandInformation_UpdateByBrandID",
                 parameters, null);
        }

        /// <summary>
        /// 修 改商品Logo操作
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public int UpdateLogo(int brandId, string logo)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "brandId",
                    SqlDbType.Int,
                    brandId,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "Logo",
                    SqlDbType.VarChar,
                    logo,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BrandInfomation_UpdateLogo",
                parameters, null);
        }
        /// <summary>
        /// 修改产品ID，为字符串类型 
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="productString"></param>
        /// <returns></returns>
        public int UpdteProductId(int brandId, string productString)
        {
            var parameters = new List<SqlParameter>
            {
                this.SqlServer.CreateSqlParameter(
                    "brandId",
                    SqlDbType.Int,
                    brandId,
                    ParameterDirection.Input
                    ),
                this.SqlServer.CreateSqlParameter(
                    "ProductID",
                    SqlDbType.VarChar,
                    productString,
                    ParameterDirection.Input
                    )
            };
            return this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BrandInformation_UpdateProductID",
                parameters, null);
        }
    }
}
