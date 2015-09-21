// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Delivery_Corporation.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   配送公司类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// 配送公司类
    /// </summary>
    public class Config_Delivery_Corporation
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货公司名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置送货公司物流信息查询网址．
        /// </summary>
        public string URL { get; set; }

		/// <summary>
		/// 获取或设置送货公司代号
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// 获取或设置送货公司电话
		/// </summary>
		public string Tel { get; set; }

        /// <summary>
        /// 获取或设置送货公司描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion

	}
}