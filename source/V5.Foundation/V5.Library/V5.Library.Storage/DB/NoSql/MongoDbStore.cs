// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MongoDbStore.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The mongo db helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.DB.NoSql
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB;
    using MongoDB.Configuration;
    using MongoDB.Linq;

    /// <summary>
    /// The mongo db helper.
    /// </summary>
    /// <typeparam name="T">
    /// 指定类型
    /// </typeparam>
    public class MongoDbStore<T> where T : class
    {
        #region Constants and Fields

        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string connectionString = string.Empty;

		/// <summary>
		/// The back connection string.
		/// </summary>
		private readonly string connectionString_bak = string.Empty;

        /// <summary>
        /// The database name.
        /// </summary>
        private readonly string databaseName = string.Empty;

        /// <summary>
        /// The collection name.
        /// </summary>
        private readonly string collectionName = string.Empty;

        #endregion 

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbStore{T}"/> class.
        /// </summary>
        public MongoDbStore()
        {
            this.connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
			this.connectionString_bak = ConfigurationManager.AppSettings["MongoDbConnectionString_bak"];
            this.databaseName = ConfigurationManager.AppSettings["MongoDbName"];
            this.collectionName = ConfigurationManager.AppSettings["MongoDbCollectionName"];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbStore{T}"/> class.
        /// </summary>
        /// <param name="collectionName">
        /// The collection name.
        /// </param>
        public MongoDbStore(string collectionName)
        {
            this.connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
            this.databaseName = ConfigurationManager.AppSettings["MongoDbName"];
            this.collectionName = collectionName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public MongoConfiguration Configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();
                config.Mapping(mapping =>
                {
                    mapping.DefaultProfile(profile => profile.SubClassesAre(t => t.IsSubclassOf(typeof(T))));
                    mapping.Map<T>();
                    mapping.Map<T>();
                });

                config.ConnectionString(this.connectionString);
                return config.BuildConfiguration();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        public void Insert(T t)
        {
            using (var mongo = new Mongo(this.Configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    collection.Insert(t, true);

                    mongo.Disconnect();
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="t"></param>
		public void BatchInsert(List<T> items)
		{
			using (var mongo = new Mongo(this.Configuration))
			{
				try
				{
					mongo.Connect();

					var db = mongo.GetDatabase(this.databaseName);
					var collection = db.GetCollection<T>(this.collectionName);
					foreach (var item in items)
					{
						collection.Insert(item, true);
					}

					mongo.Disconnect();
				}
				catch (Exception exception)
				{
					mongo.Disconnect();
					throw new Exception(exception.Message);
				}
			}
		}

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="func">
        /// The func.
        /// </param>
        public void Update(T t, Expression<Func<T, bool>> func)
        {
            using (var mongo = new Mongo(this.Configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    collection.Update<T>(t, func, true);

                    mongo.Disconnect();
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="totalCount">
        /// The total Count.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<T> List(int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int totalCount)
        {
            totalCount = 0;
            using (var mongo = new Mongo(this.Configuration))
            {
                try
                {
                    mongo.Connect();
                    
                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    var list = collection.Linq().Where(func);
                    totalCount = Convert.ToInt32(collection.Linq().Where(func).Count());
                    var result = list.Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select(i => i).ToList();
                    mongo.Disconnect();
                    
                    return result;
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<T> List(Expression<Func<T, bool>> func)
        {
            using (var mongo = new Mongo(this.Configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    var list = collection.Linq().Where(func).ToList();
                    mongo.Disconnect();

                    return list;
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

        /// <summary>
        /// The single.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Single(Expression<Func<T, bool>> func)
        {
            using (var mongo = new Mongo(this.Configuration))
            {
                T item = null;
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    var list = collection.Linq();
                    if (list != null)
                    {
                        item = list.FirstOrDefault(func);
                    }

                    mongo.Disconnect();

                    return item ?? default(T);
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        public void Delete(Expression<Func<T, bool>> func)
        {
            using (var mongo = new Mongo(this.Configuration))
            {
                try
                {
                    mongo.Connect();
                    
                    var db = mongo.GetDatabase(this.databaseName);
                    var collection = db.GetCollection<T>(this.collectionName);
                    collection.Remove<T>(func);
                    
                    mongo.Disconnect();
                }
                catch (Exception exception)
                {
                    mongo.Disconnect();
                    throw new Exception(exception.Message);
                }
            }
        }

        #endregion
    }
}