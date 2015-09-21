// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryCache.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   全局缓存
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.Cache
{
    using System;
    using System.Runtime.Caching;

    /// <summary>
    /// 全局缓存
    /// </summary>
    public sealed class MemoryCache : ICache
    {
        #region Constants and Fields

        /// <summary>
        /// MemoryCache 对象
        /// </summary>
        private static MemoryCache memoryCache;

        /// <summary>
        /// 锁定依赖对象
        /// </summary>
        private static readonly object Locker = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MemoryCache"/> class from being created.
        /// </summary>
        private MemoryCache()
        {
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// 获取 MemoryCache 的实例
        /// </summary>
        public static MemoryCache Instance
        {
            get
            {
                // 多线程下安全，线程并不是每次都加锁，判断对象实例没有被创建时，才会加锁
                if (memoryCache == null)
                {
                    lock (Locker)
                    {
                        // 加锁后还得再进行对象是否已被创建的判断
                        // 它解决了线程并发问题，同时避免在每个 Instance 属性方法的调用中都出现独占锁定
                        if (memoryCache == null)
                        {
                            memoryCache = new MemoryCache();
                        }
                    }
                }

                return memoryCache;
            }
        }

        /// <summary>
        /// 当前缓存总数量
        /// </summary>
        public int Count
        {
            get
            {
                return Convert.ToInt32(System.Runtime.Caching.MemoryCache.Default.GetCount());
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
                return System.Runtime.Caching.MemoryCache.Default[key];
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
            return System.Runtime.Caching.MemoryCache.Default.Get(key) != null;
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
                var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(duration) };

                System.Runtime.Caching.MemoryCache.Default.Set(key, value, cacheItemPolicy);
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
                return System.Runtime.Caching.MemoryCache.Default.Get(key);
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
                System.Runtime.Caching.MemoryCache.Default.Remove(key);
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

            foreach (var item in System.Runtime.Caching.MemoryCache.Default) 
            {
                this.Remove(item.Key);
            }
        }

        #endregion
    }
}
