// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRuntimeCache.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Asp.net 运行时缓存类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.Cache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    /// <summary>
    /// Asp.net 运行时缓存类
    /// </summary>
    public sealed class HttpRuntimeCache : ICache
    {
        #region Constants and Fields

        /// <summary>
        /// HttpRuntimeCache 对象
        /// </summary>
        private static HttpRuntimeCache httpRuntimeCache;

        /// <summary>
        /// 锁定依赖对象
        /// </summary>
        private static readonly object Locker = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="HttpRuntimeCache"/> class from being created.
        /// </summary>
        private HttpRuntimeCache()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取 HttpRuntimeCache 的实例
        /// </summary>
        public static HttpRuntimeCache Instance
        {
            get
            {
                // 多线程下安全，线程并不是每次都加锁，判断对象实例没有被创建时，才会加锁
                if (httpRuntimeCache == null)
                {
                    lock (Locker)
                    {
                        // 加锁后还得再进行对象是否已被创建的判断
                        // 它解决了线程并发问题，同时避免在每个 Instance 属性方法的调用中都出现独占锁定
                        if (httpRuntimeCache == null)
                        {
                            httpRuntimeCache = new HttpRuntimeCache();
                        }
                    }
                }

                return httpRuntimeCache;
            }
        }

        /// <summary>
        /// 当前缓存总数量
        /// </summary>
        public int Count
        {
            get
            {
                return HttpRuntime.Cache.Count;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// 缓存索引器
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <returns>
        /// 缓存值
        /// </returns>
        public object this[string key]
        {
            get
            {
                return HttpRuntime.Cache[key];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 缓存项是否存在
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <returns>
        /// 是否存在
        /// </returns>
        public bool Exists(string key)
        {
            return HttpRuntime.Cache.Get(key) != null;
        }

        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <param name="value">
        /// 缓存值
        /// </param>
        /// <param name="duration">
        /// 持续时间（单位：秒）
        /// </param>
        public void Set(string key, object value, int duration)
        {
            if (!this.Exists(key) && value != null) 
            {
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(duration), Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <param name="value">
        /// 缓存值
        /// </param>
        /// <param name="duration">
        /// 持续时间（单位：秒）
        /// </param>
        /// <param name="cacheItemPriority">
        /// 缓存项优先级
        /// </param>
        public void Set(string key, object value, int duration, CacheItemPriority cacheItemPriority)
        {
            if (!this.Exists(key) && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(duration), Cache.NoSlidingExpiration, cacheItemPriority, null);
            }
        }

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <returns>
        /// 缓存值
        /// </returns>
        public object Get(string key)
        {
            if (this.Exists(key)) 
            {
                return HttpRuntime.Cache.Get(key);
            }

            return null;
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        public void Remove(string key)
        {
            if (this.Exists(key))
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 清空缓存项
        /// </summary>
        public void Clear()
        {
            if (this.Count <= 0) 
            {
                return;
            }

            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            var keys = new List<string>();

            while (enumerator.MoveNext()) 
            {
                keys.Add(enumerator.Key.ToString());
            }

            if (keys.Count > 0) 
            {
                foreach (string key in keys)
                {
                    this.Remove(key);
                }
            }
        }

        #endregion
    }
}
