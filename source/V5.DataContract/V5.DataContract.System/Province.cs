// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Province.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   省会类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    /// 省会类
    /// </summary>
    [Serializable]
    public class Province
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置省会名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        #endregion
    }
}