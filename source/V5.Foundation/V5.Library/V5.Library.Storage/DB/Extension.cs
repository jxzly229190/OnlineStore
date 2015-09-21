// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extension.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.DB
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    using V5.Library.Reflecter;

    /// <summary>
    /// The extension.
    /// </summary>
    public static class Extension
    {
        #region Constants and Fields

        /// <summary>
        /// The binding flag.
        /// </summary>
        private const BindingFlags BindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        #endregion

        #region Public Methods and Operators

		/// <summary>
		/// 新的分页方法【推荐使用】
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sqlServer"></param>
		/// <param name="paging"></param>
		/// <param name="pageCount"></param>
		/// <param name="totalCount"></param>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public static List<T> PagingNew<T>(this SqlServer sqlServer, Paging paging, out int pageCount, out int totalCount, string commandText) where T : new()
		{
			List<SqlParameter> parameters;

			if (string.IsNullOrEmpty(commandText))
			{
				commandText = "sp_Paging";
				parameters = paging.GetSqlParameters();
			}
			else if (commandText == "sp_User_Paging")
			{
				parameters = paging.GetSqlParametersForUserPaging();
			}
			else
			{
				parameters = paging.GetSqlParameters();
			}

			if (parameters == null)
			{
				pageCount = 0;
				totalCount = 0;

				return null;
			}

			var dataSet = sqlServer.ExecuteDataAdapter(CommandType.StoredProcedure, commandText, parameters, null);
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				var list = dataSet.Tables[0].ToListNew<T>();
				pageCount = (int)parameters.Find(parameter => parameter.ParameterName == "pageCount").Value;
				totalCount = (int)parameters.Find(parameter => parameter.ParameterName == "totalCount").Value;
				return list;
			}

			totalCount = 0;
			pageCount = 1;

			return null;
		}

        /// <summary>
        /// 分页方法【不推荐使用】
        /// </summary>
        /// <param name="sqlServer">
        /// SqlServer 对象
        /// </param>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <param name="commandText">
        /// The command Text.
        /// </param>
        /// <typeparam name="T">
        /// 返回分页列表类型
        /// </typeparam>
        /// <returns>
        /// 分页列表
        /// </returns>
        public static List<T> Paging<T>(this SqlServer sqlServer, Paging paging, out int pageCount, out int totalCount, string commandText) where T : new()
        {
            return PagingNew<T>(sqlServer, paging, out pageCount, out totalCount, commandText);

            //List<SqlParameter> parameters;

            //if (string.IsNullOrEmpty(commandText))
            //{
            //    commandText = "sp_Paging";
            //    parameters = paging.GetSqlParameters();
            //}
            //else if (commandText == "sp_User_Paging")
            //{
            //    parameters = paging.GetSqlParametersForUserPaging();
            //}
            //else
            //{
            //    parameters = paging.GetSqlParameters();
            //}

            //if (parameters == null)
            //{
            //    pageCount = 0;
            //    totalCount = 0;

            //    return null;
            //}

            //var dataReader = sqlServer.ExecuteDataReader(CommandType.StoredProcedure, commandText, parameters, null);
            //if (!dataReader.HasRows)
            //{
            //    totalCount = 0;
            //    pageCount = 1;

            //    return null;
            //}

            //var list = dataReader.ToList<T>();
            //pageCount = (int)parameters.Find(parameter => parameter.ParameterName == "pageCount").Value;
            //totalCount = (int)parameters.Find(parameter => parameter.ParameterName == "totalCount").Value;
            //return list;
        }
        
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="sqlServer">
        /// SqlServer 对象
        /// </param>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <param name="commandText">
        /// The command Text.
        /// </param>
        /// <typeparam name="T">
        /// 返回分页列表类型
        /// </typeparam>
        /// <returns>
        /// 分页列表
        /// </returns>
        public static List<int> Paging(this SqlServer sqlServer, Paging paging, out int pageCount, out int totalCount, string commandText)
        {
            List<SqlParameter> parameters;

            if (string.IsNullOrEmpty(commandText))
            {
                commandText = "sp_Paging";
                parameters = paging.GetSqlParameters();
            }
            else if (commandText == "sp_User_Paging")
            {
                parameters = paging.GetSqlParametersForUserPaging();
            }
            else
            {
                parameters = paging.GetSqlParameters();
            }

            if (parameters == null)
            {
                pageCount = 0;
                totalCount = 0;

                return null;
            }

            var dataReader = sqlServer.ExecuteDataReader(CommandType.StoredProcedure, commandText, parameters, null);
            if (!dataReader.HasRows)
            {
                totalCount = 0;
                pageCount = 1;

                return null;
            }

            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            var list = new List<int>();
            while (dataReader.Read())
            {
                list.Add(Convert.ToInt32(dataReader[0]));
            }

            dataReader.Close();
            dataReader.Dispose();

            pageCount = (int)parameters.Find(parameter => parameter.ParameterName == "pageCount").Value;
            totalCount = (int)parameters.Find(parameter => parameter.ParameterName == "totalCount").Value;
            return list;
        }

        /// <summary>
        /// 返回指定类型的列表
        /// </summary>
        /// <param name="dataReader">
        /// The data reader.
        /// </param>
        /// <typeparam name="T">
        /// 指定类型
        /// </typeparam>
        /// <returns>
        /// 指定类型的列表
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        public static List<T> ToList<T>(this SqlDataReader dataReader) where T : class,new()
        {
            DataTable table = new DataTable();
            table.Load(dataReader);
            dataReader.Close();
            dataReader.Dispose();
            return table.ToListNew<T>();

            //if (dataReader == null)
            //{
            //    throw new ArgumentNullException("dataReader");
            //}

            //var result = new List<T>();

            //var type = typeof(T);
            //var properties = type.GetProperties(BindingFlag);

            //while (dataReader.Read())
            //{
            //    var t = new T();

            //    foreach (var propertyInfo in properties)
            //    {
            //        if (!string.IsNullOrEmpty(propertyInfo.PropertyType.FullName))
            //        {
            //            if (propertyInfo.PropertyType.FullName.StartsWith("System.Collections"))
            //            {
            //                continue;
            //            }
            //        }

            //        object item;

            //        try
            //        {
            //            item = dataReader[propertyInfo.Name];
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }

            //        propertyInfo.SetValue(t, item);
            //    }

            //    result.Add(t);
            //}

            //dataReader.Close();
            //dataReader.Dispose();

            //return result;
        }

		public static List<TResult> ToListNew<TResult>(this DataTable dt) where TResult : new()
		{ 
			var list = new List<TResult>();
			if (dt == null) return list;
			if (dt.Rows.Count > 0)
			{
				DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
				foreach (DataRow info in dt.Rows)
				{
					list.Add(eblist.Build(info));
				}
			}
			dt.Dispose(); dt = null;
			return list;
		}

        /// <summary>
        /// 将 DataReader 转换为字符串类型的列表
        /// </summary>
        /// <param name="dataReader">
        /// DataReader 对象
        /// </param>
        /// <returns>
        /// 字符串类型的列表
        /// </returns>
        public static List<string> ToList(this SqlDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            var result = new List<string>();

            while (dataReader.Read())
            {
                result.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            dataReader.Dispose();

            return result;
        }

        /// <summary>
        /// 返回指定类型的列表
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        /// <typeparam name="T">
        /// 指定类型
        /// </typeparam>
        /// <returns>
        /// 指定类型的列表
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }

            var result = new List<T>();

            var type = typeof(T);
            var properties = type.GetProperties(BindingFlag);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var t = new T();

                foreach (var propertyInfo in properties)
                {
                    if (!string.IsNullOrEmpty(propertyInfo.PropertyType.FullName))
                    {
                        if (propertyInfo.PropertyType.FullName.StartsWith("System.Collections"))
                        {
                            continue;
                        }
                    }

                    object item;

                    try
                    {
                        item = dataRow[propertyInfo.Name];
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    propertyInfo.SetValue(t, item);
                }

                result.Add(t);
            }

            return result;
        }

        /// <summary>
        /// 返回 json 格式字符串
        /// </summary>
        /// <param name="dataReader">
        /// The data reader.
        /// </param>
        /// <returns>
        /// json 格式字符串
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 转换异常
        /// </exception>
        public static string ToJson(this SqlDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            try
            {
                while (dataReader.Read())
                {
                    stringBuilder.Append("{");

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        var type = dataReader.GetFieldType(i);

                        var key = dataReader.GetName(i);
                        var value = dataReader[i].ToString();

                        stringBuilder.Append("\"" + key + "\":");
                        value = JsonDataFormat(value, type);

                        if (i < dataReader.FieldCount - 1)
                        {
                            stringBuilder.Append(value + ",");
                        }
                        else
                        {
                            stringBuilder.Append(value);
                        }
                    }

                    stringBuilder.Append("},");
                }

                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("]");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                dataReader.Close();
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 返回 json 格式字符串
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        /// <returns>
        /// json 格式字符串
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 转换异常
        /// </exception>
        public static string ToJson(this DataTable dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }

            if (dataTable.Rows == null || dataTable.Rows.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            var dataRowCollection = dataTable.Rows;

            try
            {
                for (var i = 0; i < dataRowCollection.Count; i++)
                {
                    stringBuilder.Append("{");

                    for (var j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var type = dataTable.Columns[j].DataType;

                        var key = dataTable.Columns[j].ColumnName;
                        var value = dataRowCollection[i][j].ToString();

                        stringBuilder.Append("\"" + key + "\":");
                        value = JsonDataFormat(value, type);

                        if (j < dataTable.Columns.Count - 1)
                        {
                            stringBuilder.Append(value + ",");
                        }
                        else
                        {
                            stringBuilder.Append(value);
                        }
                    }

                    stringBuilder.Append("},");
                }

                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("]");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 返回 json 格式字符串
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        /// <param name="jsonName">
        /// The json name.
        /// </param>
        /// <returns>
        /// json 格式字符串
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        /// <exception cref="Exception">
        /// 转换异常
        /// </exception>
        public static string ToJson(this DataTable dataTable, string jsonName)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }

            if (string.IsNullOrEmpty(jsonName))
            {
                throw new ArgumentNullException("jsonName");
            }

            if (dataTable.Rows == null || dataTable.Rows.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("{\"" + jsonName + "\":[");

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    for (var i = 0; i < dataTable.Rows.Count; i++)
                    {
                        stringBuilder.Append("{");

                        for (var j = 0; j < dataTable.Columns.Count; j++)
                        {
                            var type = dataTable.Rows[i][j].GetType();

                            stringBuilder.Append("\"" + dataTable.Columns[j].ColumnName + "\":" + JsonDataFormat(dataTable.Rows[i][j].ToString(), type));

                            if (j < dataTable.Columns.Count - 1)
                            {
                                stringBuilder.Append(",");
                            }
                        }

                        stringBuilder.Append("}");

                        if (i < dataTable.Rows.Count - 1)
                        {
                            stringBuilder.Append(",");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            stringBuilder.Append("]}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The create sql parameter.
        /// </summary>
        /// <param name="sqlServer">
        /// The sql server.
        /// </param>
        /// <param name="parameterName">
        /// The parameter name.
        /// </param>
        /// <param name="sqlDbType">
        /// The sql db type.
        /// </param>
        /// <param name="parameterValue">
        /// The parameter value.
        /// </param>
        /// <param name="parameterDirection">
        /// The parameter direction.
        /// </param>
        /// <returns>
        /// The <see cref="SqlParameter"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        public static SqlParameter CreateSqlParameter(this SqlServer sqlServer, string parameterName, SqlDbType sqlDbType, object parameterValue, ParameterDirection parameterDirection)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }

            if (parameterValue == null)
            {
                throw new ArgumentNullException("parameterValue");
            }

            return new SqlParameter(parameterName, sqlDbType)
            {
                Value = parameterValue,
                Direction = parameterDirection
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// The json data format.
        /// </summary>
        /// <param name="jsonData">
        /// The json data.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        private static string JsonDataFormat(string jsonData, Type type)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentNullException("jsonData");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type == typeof(string))
            {
                jsonData = JsonDataFiltrate(jsonData);
                jsonData = "\"" + jsonData + "\"";
            }
            else if (type == typeof(bool))
            {
                jsonData = jsonData.ToLower();
            }
            else
            {
                jsonData = "\"" + jsonData + "\"";
            }

            return jsonData;
        }

        /// <summary>
        /// The json data filtrate.
        /// </summary>
        /// <param name="jsonData">
        /// The json data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string JsonDataFiltrate(string jsonData)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < jsonData.Length; i++)
            {
                var temporary = jsonData.ToCharArray()[i];
                switch (temporary)
                {
                    case '\"':
                        stringBuilder.Append("\\\"");
                        break;
                    case '\\':
                        stringBuilder.Append("\\\\");
                        break;
                    case '/':
                        stringBuilder.Append("\\/");
                        break;
                    case '\b':
                        stringBuilder.Append("\\b");
                        break;
                    case '\f':
                        stringBuilder.Append("\\f");
                        break;
                    case '\n':
                        stringBuilder.Append("\\n");
                        break;
                    case '\r':
                        stringBuilder.Append("\\r");
                        break;
                    case '\t':
                        stringBuilder.Append("\\t");
                        break;
                    default:
                        stringBuilder.Append(temporary);
                        break;
                }
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
