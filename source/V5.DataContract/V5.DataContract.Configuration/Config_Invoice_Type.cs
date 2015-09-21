// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Invoice_Type.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票类型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// 发票类型类
    /// </summary>
    public class Config_Invoice_Type
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置发票类型名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置发票类型描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}