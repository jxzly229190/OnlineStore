// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Payment_Organization.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//  付款机构实体访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// The Config_Payment_Organization class.
    /// </summary>
    public class Config_Payment_Organization
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int PaymentTypeID { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}