// --------------------------------------------------------------------------------------------------------------------
// <copyright file="City.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   城市类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    /// 城市类
    /// </summary>
    [Serializable]
    public class City
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置城市所在省会编号．
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// 获取或设置城市名称．
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}