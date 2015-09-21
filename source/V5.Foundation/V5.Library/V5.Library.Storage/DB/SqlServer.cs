// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServer.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The sql server.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.DB
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// The sql server.
    /// </summary>
    public sealed class SqlServer : IDisposable
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString;

        /// <summary>
        /// 命令超时时间
        /// </summary>
        private int commandTimeout = 30;

        /// <summary>
        /// 事务标记
        /// </summary>
        private bool transactionFlag;

        /// <summary>
        /// 事务对象
        /// </summary>
        private SqlTransaction transaction;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServer"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        public SqlServer()
        {
            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                throw new ArgumentNullException("请检查 App.config 或 Web.config 文件，确认是否存在名称为：ConnectionString 的数据库连接字符串配置项。", (Exception)null);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlServer"/> class. 
        /// </summary>
        ~SqlServer()
        {
            this.Dispose();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(this.connectionString))
                {
                    this.connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }

                return this.connectionString;
            }
        }

        /// <summary>
        /// 获取命令超时时间
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                var temporary = ConfigurationManager.AppSettings["CommandTimeout"];
                if (string.IsNullOrEmpty(temporary))
                {
                    return this.commandTimeout;
                }

                int.TryParse(temporary, out this.commandTimeout);

                return this.commandTimeout;
            }
        }

        /// <summary>
        /// 获取事务标记
        /// </summary>
        public bool TransactionFlag
        {
            get
            {
                return this.transactionFlag;
            }
        }

        /// <summary>
        /// 获取事务对象
        /// </summary>
        public SqlTransaction Transaction
        {
            get
            {
                return this.transaction;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">
        /// 隔离级别
        /// </param>
        /// <exception cref="Exception">
        /// 开启事务异常
        /// </exception>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            var connection = this.GetConnection();
            if (connection != null)
            {
                try
                {
                    connection.Open();
                    this.transaction = connection.BeginTransaction(isolationLevel);
                }
                catch (Exception exception)
                {
                    this.CloseConnection(connection);

                    throw new Exception(exception.Message, exception);
                }
                finally
                {
                    this.transactionFlag = false;
                }
            }

            this.transactionFlag = true;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionStringParameter">
        /// 数据库连接字符串
        /// </param>
        public void BeginTransaction(string connectionStringParameter)
        {
            this.BeginTransaction(connectionStringParameter, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionStringParameter">
        /// 数据库连接字符串
        /// </param>
        /// <param name="isolationLevel">
        /// 隔离级别
        /// </param>
        /// <exception cref="Exception">
        /// 开启事务异常
        /// </exception>
        public void BeginTransaction(string connectionStringParameter, IsolationLevel isolationLevel)
        {
            var connection = this.GetConnection(connectionStringParameter);
            if (connection != null)
            {
                try
                {
                    connection.Open();
                    this.transaction = connection.BeginTransaction(isolationLevel);
                }
                catch (Exception exception)
                {
                    this.CloseConnection(connection);

                    throw new Exception(exception.Message, exception);
                }
                finally
                {
                    this.transactionFlag = false;
                }
            }

            this.transactionFlag = true;
        }

        /// <summary>
        /// 创建数据库参数
        /// </summary>
        /// <param name="parameterName">
        /// 参数名称
        /// </param>
        /// <param name="sqlDbType">
        /// 参数数据类型
        /// </param>
        /// <param name="parameterValue">
        /// 参数值
        /// </param>
        /// <param name="parameterDirection">
        /// 参数类型
        /// </param>
        /// <returns>
        /// 参数对象
        /// </returns>
        public SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, object parameterValue, ParameterDirection parameterDirection)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                return null;
            }

            return new SqlParameter(parameterName, sqlDbType)
            {
                Value = parameterValue == null ? DBNull.Value : parameterValue,
                Direction = parameterDirection
            };
        }

        /// <summary>
        /// 执行非查询操作
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <param name="parameters">
        /// 参数列表
        /// </param>
        /// <param name="transactionParameter">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 受影响的行数
        /// </returns>
        /// <exception cref="Exception">
        /// 执行非查询操作异常
        /// </exception>
        public int ExecuteNonQuery(CommandType commandType, string commandText, List<SqlParameter> parameters, SqlTransaction transactionParameter)
        {
            int result;

            try
            {
                var connection = transactionParameter == null ? this.GetConnection() : transactionParameter.Connection;
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var command = this.GetCommand(commandType, commandText, parameters, transactionParameter);
                command.Connection = connection;

                result = command.ExecuteNonQuery();

                if (transactionParameter == null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(this.GetExecptionMessage(commandType, commandText), exception);
            }

            return result;
        }

        /// <summary>
        /// 执行查询单个结果的操作
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <param name="parameters">
        /// 参数数组
        /// </param>
        /// <param name="transactionParameter">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 结果对象
        /// </returns>
        /// <exception cref="Exception">
        /// 执行查询单个结果的操作异常
        /// </exception>
        public object ExecuteScalar(CommandType commandType, string commandText, List<SqlParameter> parameters, SqlTransaction transactionParameter)
        {
            object result;

            try
            {
                using (var connection = this.GetConnection())
                {
                    connection.Open();

                    var command = this.GetCommand(commandType, commandText, parameters, null);
                    command.Connection = connection;

                    result = command.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(this.GetExecptionMessage(commandType, commandText), exception);
            }

            return result;
        }

        /// <summary>
        /// 执行数据读取器操作
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <param name="parameters">
        /// 参数数组
        /// </param>
        /// <param name="transactionParameter">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 数据读取器
        /// </returns>
        /// <exception cref="Exception">
        /// 执行查询单个结果的操作异常
        /// </exception>
        public SqlDataReader ExecuteDataReader(CommandType commandType, string commandText, List<SqlParameter> parameters, SqlTransaction transactionParameter)
        {
            SqlDataReader result;
            SqlConnection connection = null;

            try
            {
                connection = this.GetConnection();
                connection.Open();

                var command = this.GetCommand(commandType, commandText, parameters, null);
                command.Connection = connection;

                result = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                if (connection == null)
                {
                    throw new Exception(this.GetExecptionMessage(commandType, commandText), exception);
                }
                connection.Close();
                connection.Dispose();

                throw new Exception(this.GetExecptionMessage(commandType, commandText), exception);
            }

            return result;
        }

        /// <summary>
        /// 执行数据适配器操作
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <param name="parameters">
        /// 参数数组
        /// </param>
        /// <param name="transactionParameter">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 数据集
        /// </returns>
        /// <exception cref="Exception">
        /// 执行数据适配器操作异常
        /// </exception>
        public DataSet ExecuteDataAdapter(CommandType commandType, string commandText, List<SqlParameter> parameters, SqlTransaction transactionParameter)
        {
            var result = new DataSet();

            try
            {
                using (var connection = this.GetConnection())
                {
                    connection.Open();

                    var command = this.GetCommand(commandType, commandText, parameters, null);
                    command.Connection = connection;

                    var dataAdapter = this.GetDataAdapter(command);
                    dataAdapter.Fill(result);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(this.GetExecptionMessage(commandType, commandText), exception);
            }

            return result;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <exception cref="Exception">
        /// 回滚事务异常
        /// </exception>
        public void RollbackTransaction()
        {
            if (!this.transactionFlag)
            {
                return;
            }

            if (this.Transaction == null || this.Transaction.Connection == null)
            {
                return;
            }

            try
            {
                this.Transaction.Rollback();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                this.transactionFlag = false;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <exception cref="Exception">
        /// 提交事务异常
        /// </exception>
        public void CommitTransaction()
        {
            if (!this.transactionFlag)
            {
                return;
            }

            if (this.Transaction == null || this.Transaction.Connection == null)
            {
                return;
            }

            try
            {
                this.Transaction.Commit();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                this.transactionFlag = false;
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns>
        /// 数据库连接对象
        /// </returns>
        private SqlConnection GetConnection()
        {
            return !string.IsNullOrEmpty(this.ConnectionString) ? new SqlConnection(this.ConnectionString) : null;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="connectionStringParameter">
        /// 连接字符串
        /// </param>
        /// <returns>
        /// 数据库连接对象
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 获取数据库连接对象异常
        /// </exception>
        private SqlConnection GetConnection(string connectionStringParameter)
        {
            if (string.IsNullOrEmpty(connectionStringParameter))
            {
                throw new ArgumentNullException("connectionStringParameter");
            }

            return new SqlConnection(connectionStringParameter);
        }

        /// <summary>
        /// 附加数据库命令参数
        /// </summary>
        /// <param name="command">
        /// 数据库命令对象
        /// </param>
        /// <param name="parameters">
        /// 参数数组
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 附加数据库命令参数异常
        /// </exception>
        private void AttachCommandParameters(SqlCommand command, IEnumerable<SqlParameter> parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            foreach (var sqlParameter in parameters)
            {
                ////if (sqlParameter.Direction != ParameterDirection.Input)
                ////{
                ////    continue;
                ////}

                ////if (sqlParameter.Value == null)
                ////{
                ////    throw new Exception("ParameterDirection.Input 类型的参数，值不能为空");
                ////}

                command.Parameters.Add(sqlParameter);
            }
        }

        /// <summary>
        /// 获取数据库命令对象
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <param name="parameters">
        /// 参数数组
        /// </param>
        /// <param name="transactionParameter">
        /// 事务对象
        /// </param>
        /// <returns>
        /// 数据库命令对象
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 获取数据库命令对象异常
        /// </exception>
        private SqlCommand GetCommand(CommandType commandType, string commandText, List<SqlParameter> parameters, SqlTransaction transactionParameter)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }

            SqlCommand command = null;

            if (transactionParameter == null)
            {
                command = new SqlCommand
                              {
                                  CommandType = commandType,
                                  CommandText = commandText,
                                  CommandTimeout = this.CommandTimeout
                              };
            }

            if (transactionParameter != null)
            {
                command = new SqlCommand
                {
                    CommandType = commandType,
                    CommandText = commandText,
                    CommandTimeout = this.CommandTimeout,
                    Transaction = transactionParameter
                };
            }

            if (parameters != null && parameters.Count > 0)
            {
                this.AttachCommandParameters(command, parameters);
            }

            return command;
        }

        /// <summary>
        /// 获取数据适配器对象
        /// </summary>
        /// <param name="command">
        /// 数据库命令对象
        /// </param>
        /// <returns>
        /// 数据适配器对象
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 获取数据适配器对象异常
        /// </exception>
        private SqlDataAdapter GetDataAdapter(SqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return new SqlDataAdapter(command);
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">
        /// 数据库连接对象
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 关闭数据库连接异常
        /// </exception>
        private void CloseConnection(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 获取数据库异常消息
        /// </summary>
        /// <param name="commandType">
        /// 命令类型
        /// </param>
        /// <param name="commandText">
        /// 命令文本
        /// </param>
        /// <returns>
        /// 异常消息
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 获取数据库异常消息异常
        /// </exception>
        private string GetExecptionMessage(CommandType commandType, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("执行");

            if (commandType == CommandType.Text)
            {
                stringBuilder.Append(" Text：");
                stringBuilder.Append(commandText);
            }

            if (commandType == CommandType.StoredProcedure)
            {
                stringBuilder.Append(" StoredProcedure：");
                stringBuilder.Append(commandText);
            }

            if (commandType == CommandType.TableDirect)
            {
                stringBuilder.Append(" TableDirect：");
                stringBuilder.Append(commandText);
            }

            stringBuilder.Append(" 发生异常。");
            return stringBuilder.ToString();
        }

        #endregion
    }
}