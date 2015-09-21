// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   数据库访问父类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using global::System;
    using global::System.Configuration;
    using global::System.Reflection;

    using V5.Library.Logger;
    using V5.Library.Storage.Cache;

    /// <summary>
    /// 数据库访问父类
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccess"/> class.
        /// </summary>
        public DataAccess()
        {
            this.AssemblyPath = ConfigurationManager.AppSettings["DataAccessAssemblyPath"];
        }

        /// <summary>
        /// The assembly path.
        /// </summary>
        protected string AssemblyPath { get; set; }

        /// <summary>
        /// 创建指定类型的数据库访问对象
        /// </summary>
        /// <param name="className">
        /// 类名称
        /// </param>
        /// <typeparam name="T">
        /// 指定类型
        /// </typeparam>
        /// <returns>
        /// 指定类型的对象
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        public T Create<T>(string className) where T : new()
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }

            string nameSpace = this.AssemblyPath + "." + className;
            object dataAccessObject = this.Create(this.AssemblyPath, nameSpace);
            return (T)dataAccessObject;
        }

        /// <summary>
        /// 创建数据库访问对象
        /// </summary>
        /// <param name="assemblyPath">
        /// 程序集路径
        /// </param>
        /// <param name="nameSpace">
        /// 类的名称空间
        /// </param>
        /// <returns>
        /// 数据库访问对象
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 参数为空异常
        /// </exception>
        protected object Create(string assemblyPath, string nameSpace)
        {
            if (string.IsNullOrEmpty(assemblyPath))
            {
                throw new ArgumentNullException("assemblyPath");
            }

            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }

            var dataAccessObject = MemoryCache.Instance.Get(nameSpace);
            if (dataAccessObject != null)
            {
                return dataAccessObject;
            }

            try
            {
                dataAccessObject = Assembly.Load(assemblyPath).CreateInstance(nameSpace);
                MemoryCache.Instance.Set(nameSpace, dataAccessObject, 86400);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log(exception.Message, Category.Error, exception);
            }

            return dataAccessObject;
        }
    }
}