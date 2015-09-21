// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Picture_Category.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   图片类别类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     图片类别类
    /// </summary>
    public class Picture_Category
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置父图片类别编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置图片类别名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}