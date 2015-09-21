// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Delivery_Method.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货方式类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// 送货方式类
    /// </summary>
    public class Config_Delivery_Method
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货方式名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置送货方式描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}