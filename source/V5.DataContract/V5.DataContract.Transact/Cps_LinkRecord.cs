// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cps_LinkRecord.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps 链接记录类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    ///     Cps 链接记录类
    /// </summary>
    public class Cps_LinkRecord
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置Cps 合作平台编号．
        /// </summary>
        public int CpsID { get; set; }

        /// <summary>
        ///     获取或设置外部链入地址．
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        ///     获取或设置目标地址．
        /// </summary>
        public string TargetURL { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public int IsDelete { get; set; }

		/// <summary>
		/// 扩展字段
		/// </summary>
		public string ExtField { get; set; }


        #endregion
    }
}