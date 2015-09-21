// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemHomeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统主页数据库访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统主页数据库访问类
    /// </summary>
    public class SystemHomeDA : ISystemHomeDA
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

        public List<Province> SelectProvinces()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Province_SelectAll", null, null);
                if (!dataReader.HasRows)
                {
                    return null;
                }

                var list = dataReader.ToList<Province>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
               throw new Exception("Exception - SystemHomeDA - SelectProvinces", exception);
            }

            return null;
        }

        public List<City> SelectCities()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_City_SelectAll", null, null);
                if (!dataReader.HasRows)
                {
                    return null;
                }
                
                var list = dataReader.ToList<City>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemHomeDA - SelectCities", exception);
            }

            return null;
        }

        public List<County> SelectCounties()
        {
            

            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_County_SelectAll", null, null);
                if (!dataReader.HasRows)
                {
                    return null;
                }

                var list = dataReader.ToList<County>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemHomeDA - SelectCounties", exception);
            }

            return null;
        }

        #endregion 
    }
}