// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   缓存接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Storage.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        #region Public Properties

        /// <summary>
        /// 当前缓存总数量
        /// </summary>
        int Count { get; }

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
        object this[string key] { get; }

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
        bool Exists(string key);

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
        void Set(string key, object value, int duration);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        /// <returns>
        /// 缓存值
        /// </returns>
        object Get(string key);

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存键
        /// </param>
        void Remove(string key);

        /// <summary>
        /// 清空缓存项
        /// </summary>
        void Clear();

        #endregion
    }
}
