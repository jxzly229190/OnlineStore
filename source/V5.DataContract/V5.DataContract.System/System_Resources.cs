// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Resources.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The System_Resources class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    /// The System_Resources class.
    /// </summary>
    public class System_Resources
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置资源编码．
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置父级资源编码．
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 获取或设置标识符．
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置资源描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置位置．
        /// </summary>
        public int Position { get; set; }

        #endregion
    }
}