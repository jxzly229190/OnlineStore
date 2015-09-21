// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerRequestCache.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   一次请求缓存类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.Cache
{
    using System.Web;

    /// <summary>
    /// 一次请求缓存类
    /// </summary>
    public sealed class PerRequestCache : ICache
    {
        #region Fields

        /// <summary>
        /// Http 请求上下文基类对象
        /// </summary>
        private readonly HttpContextBase httpContextBase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextBase">
        /// Http 请求上下文基类对象
        /// </param>
        public PerRequestCache(HttpContextBase httpContextBase)
        {
            this.httpContextBase = httpContextBase;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 当前缓存总数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.httpContextBase.Items.Count;
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
                if (this.httpContextBase != null)
                {
                    return this.httpContextBase.Items[key];
                }

                return null;
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
            if (this.httpContextBase != null)
            {
                return this.httpContextBase.Items.Contains(key);
            }

            return false;
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
        /// 持续时间（单位：秒）[注意：此参数在此无效]
        /// </param>
        public void Set(string key, object value, int duration)
        {
            if (!this.Exists(key) && value != null)
            {
                this.httpContextBase.Items.Add(key, value);
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
                return this.httpContextBase.Items[key];
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
                this.httpContextBase.Items.Remove(key);
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

            if (this.httpContextBase != null)
            {
                this.httpContextBase.Items.Clear();
            }
        }

        #endregion
    }
}
